using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_JoinRoom_ReqHandler :AMRpcHandler<C2G_JoinRoom,G2C_JoinRoom>
    {
        protected override async void Run(Session session, C2G_JoinRoom message, Action<G2C_JoinRoom> reply)
        {
            G2C_JoinRoom response = new G2C_JoinRoom();
            try
            {
                //验证合法性
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply(response);
                    return;
                }
                //获取房间信息
                Room room = Game.Scene.GetComponent<RoomComponent>().Get(message.RoomId);
                if (room == null)
                {
                    Log.Info($"房间不存在：{message.RoomId}");
                    response.Error = ErrorCode.ERR_NotFoundRoom;
                    reply(response);
                    return;
                }
                if (room.IsCanAddPlayer())
                {
                    Log.Info($"房间已经满人：{message.RoomId}");
                    response.Error = ErrorCode.ERR_RoomIsFull;
                    reply(response);
                    return;               
                }

                //设置用户的房间ID
                User user = session.GetUser();
                user.RoomID = room.RoomId;
                //返回参数设置
                response.Room = new RoomInfo
                {
                    PlayerCount = room.roomConfig.PlayerCount,
                    GameCount = room.roomConfig.GameCount,
                    GameScore = room.roomConfig.GameScore
                };
                response.RoomId = room.RoomId;
                response.ChairID = (ushort)(room.Count);
                reply(response);
                Log.Info("成功申请加入房间！！！");


                //向游戏服务器发送玩家进入请求
                ActorMessageSender actorProxy = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(room.Id);
                actorProxy.Send(new Actor_PlayerEnterRoom_Ntt()
                {
                    PlayerID = user.Id,
                    UserID = user.UserID,
                    SessionID = session.Id,
                });

            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
