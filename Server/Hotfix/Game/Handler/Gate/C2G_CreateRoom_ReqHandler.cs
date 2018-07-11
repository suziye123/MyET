using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using ETModel;
using MongoDB.Driver;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_CreateRoom_ReqHandler : AMRpcHandler<C2G_CreateRoom,G2C_CreateRoom>
    {
        protected override async void Run(Session session, C2G_CreateRoom message, Action<G2C_CreateRoom> reply)
        {
            G2C_CreateRoom response = new G2C_CreateRoom();
            try
            {
                //验证合法性
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply(response);
                    return;
                }
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                User user = session.GetUser();
                UserInfo userInfo = await dbProxy.Query<UserInfo>(user.UserID, false);
                if (userInfo.Money<message.Room.GameScore)
                {
                    response.Error = ErrorCode.ERR_UserMoneyLessError;
                    reply(response);
                    return;
                }

                //创建房间
                Room room = await RoomFactory.Create(message.Room.PlayerCount, message.Room.GameCount,
                    message.Room.GameScore,session);
               
                //创建房间玩家对象
                Gamer gamer = GamerFactory.Create(user.Id, user.UserID,room.RoomId,(ushort)room.Count);
                await gamer.AddComponent<MailBoxComponent>().AddLocation();
                gamer.AddComponent<UnitGateComponent, long>(session.Id);
                
                //房间添加玩家
                room.Add(gamer);

                //网关服务器和玩家对应
                user.ActorID = gamer.Id;

                //返回房间信息给玩家
                response.RoomId = room.RoomId;
                response.Room = message.Room;
                response.ChairID = (ushort)(room.Count - 1);
                Log.Info($"创建房间成功:--- {userInfo.NickName} ,房号:{room.RoomId}");
                //返回给客户端
                reply(response);

                //存储该用户房间的创建次数
                RoomHistory roomInfo = await dbProxy.Query<RoomHistory>(user.UserID, false);
                if (roomInfo==null)
                {
                    RoomHistory roomHistory = ComponentFactory.CreateWithId<RoomHistory>(userInfo.Id);
                    roomHistory.CreateCount = 1;
                    roomHistory.NickName = userInfo.NickName;
                    await dbProxy.Save(roomHistory,false);
                }
                else
                {
                    roomInfo.CreateCount += 1;
                    await dbProxy.Save(roomInfo, false);
                }



                //连接游戏服务器
                //StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                //IPEndPoint GameIPEndPoint= config.GameConfig.GetComponent<InnerConfig>().IPEndPoint;
                //Session GameSession = Game.Scene.GetComponent<NetInnerComponent>().Get(GameIPEndPoint);
                //GS2G_EnterRoom gs2g_EnterRoom =await GameSession.Call(new G2GS_EnterRoom()
                //{
                //    PlayerId = user.Id,
                //    UserID = user.UserID,
                //    GateSessionId = session.Id
                //}) as GS2G_EnterRoom;
            }
            catch (Exception e)
            {
                ReplyError(response,e,reply);
            }
        }
    }
}
