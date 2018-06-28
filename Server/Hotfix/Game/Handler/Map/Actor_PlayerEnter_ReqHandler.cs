using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_PlayerEnter_ReqHandler : AMActorHandler<Room, Actor_PlayerEnterRoom_Ntt>
    {
        protected override async Task Run(Room room, Actor_PlayerEnterRoom_Ntt message)
        {
            try
            {
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                Gamer gamer = room.Get(message.UserID);
                if (gamer == null)
                {
                    //创建房间玩家对象
                    gamer = GamerFactory.Create(message.PlayerID, message.UserID, room.RoomId, (ushort)room.Count);
                    await gamer.AddComponent<MailBoxComponent>().AddLocation();
                    gamer.AddComponent<UnitGateComponent, long>(message.SessionID);

                    //设置转接ID
                    Session session = Game.Scene.GetComponent<NetOuterComponent>().Get(message.SessionID);
                    session.GetUser().ActorID = gamer.Id;

                    room.Add(gamer);

                    Actor_GamerEnterRoom_Ntt broadcastMessage = new Actor_GamerEnterRoom_Ntt();
                    foreach (Gamer item in room.GetAll())
                    {
                        if (item == null)
                        {
                            //添加空位
                            broadcastMessage.users.Add(null);
                            continue;
                        }
                        UserInfo userInfo = await dbProxy.Query<UserInfo>(item.UserID, false);
                        GamerInfo info = new GamerInfo()
                            { ChairID = item.uChairID, NickName = userInfo.NickName, Loses = userInfo.Loses, Money = userInfo.Money, Wins = userInfo.Wins };
                        broadcastMessage.users.Add(info);
                    }
                    room.Broadcast(broadcastMessage);

                    Log.Info($"玩家{message.UserID}进入房间");
                }
                else
                {
                    Log.Info("断线重连?????");
                }
            }
            catch (Exception e)
            {
                Log.Info($"创建房间错误:{e.Message}");
            }
        }
    }
}
