using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;

namespace ETHotfix
{

    [ObjectSystem]
    public class GameControllerComponentAwakeSystem : AwakeSystem<GameControllerComponent, RoomConfig>
    {
        public override void Awake(GameControllerComponent self, RoomConfig config)
        {
            self.Awake(config);
        }
    }

    public static class GameControllerComponentSystem
    {
        public static void Awake(this GameControllerComponent self, RoomConfig config)
        {
            self.Config = config;
            self.BasePointPerMatch = config.GameScore;
            self.Multiples = 1;
            self.MinThreshold = 100;
        }

        /// <summary>
        /// 准备开始游戏
        /// </summary>
        /// <param name="self"></param>
        public static async void ReadyStartGame(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            Gamer[] gamers = room.GetAll();

            if (room.Count==self.Config.PlayerCount&&gamers.Count(model => model.IsReady)==self.Config.PlayerCount)
            {
                //所有玩家准备！！
                //发送游戏开始
                room.Broadcast(new Actor_GamerStart_Ntt());

                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);

                self.DealCards();

                foreach (var _gamer in gamers)
                {
                    ActorMessageSender actorProxy = _gamer.GetComponent<UnitGateComponent>().GetActorMessageSender();
                    actorProxy.Send(new Actor_SendCard_Ntt()
                    {
                        Cards = _gamer.GetComponent<HandCardsComponent>().GetAll()
                    });
                    Log.Info($"{_gamer.UserID}手牌:{_gamer.GetComponent<HandCardsComponent>().ShowAllCard()}");
                }

                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);

                Log.Info("可以开始出牌了");
            }
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="chairID"></param>
        public static void DealTo(this GameControllerComponent self, ushort chairID)
        {
            Room room = self.GetParent<Room>();
            byte[] cards = room.GetComponent<DeckComponent>().Deals(5);
            Gamer gamer = room.Get(chairID);
            
            gamer.GetComponent<HandCardsComponent>().AddCard(cards);
        }

        /// <summary>
        /// 更新身份
        /// </summary>
        /// <param name="self"></param>
        /// <param name="gamer"></param>
        /// <param name="IsBanker"></param>
        public static void UpdateInIdentity(this GameControllerComponent self, Gamer gamer,bool IsBanker)
        {
            gamer.GetComponent<HandCardsComponent>().IsBanker = IsBanker;
        }


        /// <summary>
        /// 场上所有牌回收到牌库中
        /// </summary>
        /// <param name="self"></param>
        public static void BackToDeck(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            DeckComponent deckComponent = room.GetComponent<DeckComponent>();

            foreach (var gamer in room.GetAll())
            {
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                while (handCards.CardsCount>0)
                {
                    byte card = handCards.library[handCards.CardsCount - 1];
                    handCards.PopCard(card);
                    deckComponent.AddCard(card);
                }
            }
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        /// <param name="self"></param>
        public static void DealCards(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();

            //牌库洗牌
            room.GetComponent<DeckComponent>().Shuffle();

            Gamer[] gamers = room.GetAll();

            int index = 0;

            for (int i = 0; i < room.roomConfig.PlayerCount; i++)
            {
                self.DealTo(gamers[i].uChairID);
            }
        }
    }
}
