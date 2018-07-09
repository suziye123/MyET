using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_GamerBet_NttHandler :AMActorHandler<Gamer,Actor_GamerBet_Ntt>
    {
        protected override async Task Run(Gamer entity, Actor_GamerBet_Ntt message)
        {
            Room room = RoomHelp.GetRoom(entity.RoomID);
            DeckComponent deck = room.GetComponent<DeckComponent>();
            deck.AddBetGamer(message.ChairId,message.BetNumber);
            room.Broadcast(new Actor_BetResult_Ntt(){ChairId = message.ChairId,BetNumber = message.BetNumber});
            if (deck.IsBetAll())
            {
                //开始摊牌
                room.Broadcast(new Actor_StartShowHand_Ntt());
            }

            await Task.CompletedTask;
        }
    }
}
