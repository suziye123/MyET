using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_RobBanker_NttHandler : AMActorHandler<Gamer,Actor_RobBanker_Ntt>
    {
        protected override async Task Run(Gamer entity, Actor_RobBanker_Ntt message)
        {
            Room room = RoomHelp.GetRoom(entity.RoomID);
            DeckComponent deck = room.GetComponent<DeckComponent>();
            deck.AddRobBankGamer(message.ChairId, message.BankerNumber);
            if (deck.IsRobBankerAll())
            {
                //如果所有人都抢庄结束
                ushort ChairId = deck.GetBanker();
                room.Broadcast(new Actor_RobBankerResult_Ntt() { ChairId = message.ChairId, BankerNumber = message.BankerNumber });
                room.Broadcast(new Actor_SelectBanker_Ntt() { ChairId = ChairId });
                //指定庄
                room.Get(ChairId).IsRobBanker = true;
            }
            else
            {
                //如果还没有抢庄完
                room.Broadcast(new Actor_RobBankerResult_Ntt(){ChairId = message.ChairId,BankerNumber = message.BankerNumber});                  
            }
            Log.Debug($"玩家:{message.ChairId},抢庄倍数:{message.BankerNumber},当前抢庄玩家人数:{deck.RobBankerDic.Count}");


            await Task.CompletedTask;
        }
    }
}
