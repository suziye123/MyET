using System;
using System.Collections.Generic;
using System.Linq;
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

            HandCardsComponent handCardsComponent =entity.GetComponent<HandCardsComponent>();

            //排序
            byte CardType = handCardsComponent.Sort();
            //得到排序后的手牌
            byte[] HandCards = handCardsComponent.GetAll();
            //记录
            deck.AddShowCardGamer(entity.uChairID,HandCards);

            room.Broadcast(new Actor_ShowHandResult_Ntt(){ChairId = entity.uChairID,Cards = HandCards,CardType = CardType});

            if (deck.IsShowCardAll())
            {
                //如果所有玩家都摊牌了
                Log.Info("所有玩家都摊牌了");
                //判断牌最大的玩家
                Gamer[] gamers = room.GetAll();
                //找到庄
                Gamer BankerGamer = gamers.Single(model => model.IsRobBanker);
                byte BankerCardType = BankerGamer.GetComponent<HandCardsComponent>().CardType;

                //找到比庄大的玩家
                var MoreTranBanker = gamers.Where(model => model.GetComponent<HandCardsComponent>().CardType > BankerCardType);

                //找到比庄小的玩家
                var LessTranBanker = gamers.Where(model => model.GetComponent<HandCardsComponent>().CardType < BankerCardType);
                int AllMultiple = 0;
                //赢的玩家处理
                foreach (Gamer gamer in MoreTranBanker)
                {
                    int Multiple = 0;
                    Multiple = gamer.GetComponent<HandCardsComponent>().CardTypeMultiple *
                               deck.GetOrderPlayerBet_Bet(gamer.uChairID) *
                               deck.GetOrderPlayerBet_RobBanker(gamer.uChairID);
                    gamer.SingleMultiple = Multiple;
                    gamer.AllMultiple += Multiple;
                    AllMultiple -= Multiple;
                }
                //输的玩家处理
                foreach (Gamer gamer in LessTranBanker)
                {
                    int Multiple = 0;
                    Multiple = BankerGamer.GetComponent<HandCardsComponent>().CardTypeMultiple *
                               deck.GetOrderPlayerBet_Bet(gamer.uChairID) *
                               deck.GetOrderPlayerBet_RobBanker(gamer.uChairID);
                    gamer.SingleMultiple = -Multiple;
                    gamer.AllMultiple += -Multiple;
                    AllMultiple += Multiple;
                }
                //庄的得分
                BankerGamer.SingleMultiple = AllMultiple;
                BankerGamer.AllMultiple += AllMultiple;

                List<XJResultInfo> ResultList = new List<XJResultInfo>();
                foreach (Gamer gamer in gamers)
                {
                    XJResultInfo result = new XJResultInfo();
                    result.ChairId = gamer.uChairID;
                    result.AllScore = gamer.AllMultiple;
                    result.XJScore = gamer.SingleMultiple;
                    ResultList.Add(result);
                }

                room.Broadcast(new Actor_XJGameResult_Ntt(){XJResult = ResultList});
                //foreach (Gamer gamer in MoreTranBanker)
                //{
                //    gamer.GetComponent<HandCardsComponent>().CardTypeMultiplp
                //}              
            }
            await Task.CompletedTask;
        }
    }
}
