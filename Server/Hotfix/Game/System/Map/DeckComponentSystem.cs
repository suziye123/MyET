using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class DeckComponentAwakeSystem : AwakeSystem<DeckComponent>
    {
        public override void Awake(DeckComponent self)
        {
            self.Awake();
        }
    }

    public static class DeckComponentSystem
    {
        public static void Awake(this DeckComponent self)
        {
            self.CreateDeck();
        }

        /// <summary>
        /// 添加抢庄玩家
        /// </summary>
        /// <param name="self"></param>
        /// <param name="ChairId"></param>
        /// <param name="RobBankerNumber"></param>
        public static void AddRobBankGamer(this DeckComponent self,ushort ChairId,byte RobBankerNumber)
        {
            if (self.RobBankerDic.ContainsKey(ChairId))
            {
                return;
            }

            self.RobBankerDic.Add(ChairId,RobBankerNumber);
        }
        /// <summary>
        /// 判断是否所有玩家都抢庄完了
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsRobBankerAll(this DeckComponent self)
        {
            return self.RobBankerDic.Count == self.GetParent<Room>().roomConfig.PlayerCount;
        }

        /// <summary>
        /// 判断是否所有玩家都摊牌了
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsShowCardAll(this DeckComponent self)
        {
            return self.ShowCardDic.Count == self.GetParent<Room>().roomConfig.PlayerCount;
        }
        /// <summary>
        /// 添加摊牌的玩家
        /// </summary>
        /// <param name="self"></param>
        /// <param name="ChairId"></param>
        /// <param name="Cards"></param>
        public static void AddShowCardGamer(this DeckComponent self, ushort ChairId, byte[] Cards)
        {
            if (self.ShowCardDic.ContainsKey(ChairId))
            {
                return;
            }


            self.ShowCardDic.Add(ChairId,Cards);
        }

        /// <summary>
        /// 清除所有摊牌玩家的数据
        /// </summary>
        /// <param name="self"></param>
        public static void ClearAllShowCardGamer(this DeckComponent self)
        {
            self.ShowCardDic.Clear();
        }
        /// <summary>
        /// 获取装
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static ushort GetBanker(this DeckComponent self)
        {
            List<ushort> MaxChairId = new List<ushort>();
            int index = 0;
            foreach (KeyValuePair<ushort, byte> valuePair in self.RobBankerDic)
            {
                if (index<valuePair.Value)
                {
                  MaxChairId.Clear();
                  index = valuePair.Value;
                  MaxChairId.Add(valuePair.Key);
                }
                else if(index <= valuePair.Value)
                {
                  MaxChairId.Add(valuePair.Key);
                }
            }
            if (MaxChairId.Count==1)
            {
                //直接指定庄家
                return MaxChairId[0];
            }
            if (MaxChairId.Count>=2)
            {
                //随机指定庄
                Random random = new Random();
                int randomNumber = random.Next(0, MaxChairId.Count);
                return MaxChairId[randomNumber];
            }

            return 0;

        }
        /// <summary>
        /// 判断是否所有玩家都下注完了
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsBetAll(this DeckComponent self)
        {
            return self.BetDic.Count== self.GetParent<Room>().roomConfig.PlayerCount;
        }
        /// <summary>
        /// 添加下注玩家
        /// </summary>
        /// <param name="self"></param>
        /// <param name="ChairId"></param>
        /// <param name="BetNumber"></param>
        public static void AddBetGamer(this DeckComponent self, ushort ChairId, byte BetNumber)
        {
            if (self.BetDic.ContainsKey(ChairId))
            {
                return;
            }

            self.BetDic.Add(ChairId, BetNumber);
        }
        /// <summary>
        /// 清空抢庄玩家
        /// </summary>
        /// <param name="self"></param>
        public static void ClearRobBankGamer(this DeckComponent self)
        {
            self.RobBankerDic.Clear();
        }
        /// <summary>
        /// 清空下注玩家
        /// </summary>
        /// <param name="self"></param>
        public static void ClearBetGamer(this DeckComponent self)
        {
            self.BetDic.Clear();
        }

        /// <summary>
        /// 创建一副牌
        /// </summary>
        private static void CreateDeck(this DeckComponent self)
        {
            //创建普通扑克
            for (byte color = 0; color < 4; color++)
            {
                for (byte value = 1; value <= 13; value++)
                {

                    byte bCard = 0;
                    byte bColor = 0;
                    byte bValue = value;
                    bColor = (byte)((color << 4));
                    bCard = (byte)(bColor | bValue);
                    self.library.Add(bCard);
                }
            }

            //创建大小王扑克  无大小王
            //self.library.Add(new Card(Weight.SJoker, Suits.None));
            //self.library.Add(new Card(Weight.LJoker, Suits.None));
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static byte Deal(this DeckComponent self)
        {
            byte Card = self.library[self.CardsCount - 1];
            self.library.Remove(Card);
            return Card;
        }

        /// <summary>
        /// 发指定牌数
        /// </summary>
        /// <param name="self"></param>
        /// <param name="CardNumber"></param>
        /// <returns></returns>
        public static byte[] Deals(this DeckComponent self, int CardNumber)
        {
            byte[] Cards = new byte[CardNumber];
            for (int i = 0; i < CardNumber; i++)
            {
                byte Card = self.library[self.CardsCount - 1];
                self.library.Remove(Card);
                Cards[i] = Card;
            }
            return Cards;
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        /// <param name="self"></param>
        public static void Shuffle(this DeckComponent self)
        {
            if (self.CardsCount==52)
            {
                Random random = new Random();
                List<byte> NewCards = new List<byte>();
                foreach (byte temp in self.library)
                {
                    NewCards.Insert(random.Next(NewCards.Count+1),temp);
                }
                self.library.Clear();
                self.library.AddRange(NewCards);
            }
        }

        /// <summary>
        /// 添加牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="card"></param>
        public static void AddCard(this DeckComponent self, byte card)
        {
            self.library.Add(card);
        }
    }
}
