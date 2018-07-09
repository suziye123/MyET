using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_ShowHandCard_NttHandler :AMActorHandler<Gamer,Actor_ShowHandCard_Ntt>
    {
        protected override async Task Run(Gamer entity, Actor_ShowHandCard_Ntt message)
        {
            Room room = RoomHelp.GetRoom(entity.RoomID);

            DeckComponent deck = room.GetComponent<DeckComponent>();

            byte[] HandCards = entity.GetComponent<HandCardsComponent>().GetAll();

            deck.AddShowCardGamer(entity.uChairID,HandCards);

            room.Broadcast(new Actor_ShowHandResult_Ntt(){ChairId = entity.uChairID,Cards = HandCards});

            if (deck.IsShowCardAll())
            {
                //如果所有玩家都摊牌了
                //显示结果
            }
            await Task.CompletedTask;
        }
    }
}
