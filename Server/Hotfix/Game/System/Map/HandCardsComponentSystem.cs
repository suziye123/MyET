using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public static class HandCardsComponentSystem
    {
        /// <summary>
        /// 获取所有手牌
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static byte[] GetAll(this HandCardsComponent self)
        {
            return self.library.ToArray();
        }

        /// <summary>
        /// 打印所有的手牌
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ShowAllCard(this HandCardsComponent self)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < self.CardsCount; i++)
            {
                sb.Append($"{self.library[i]} |");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取所有手牌
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<byte> GetCardList(this HandCardsComponent self)
        {
            return self.library;
        }
        /// <summary>
        /// 向牌库添加牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="cards"></param>
        public static void AddCard(this HandCardsComponent self,byte[] cards)
        {
            self.library.AddRange(cards);
        }

        /// <summary>
        /// 清除手牌
        /// </summary>
        /// <param name="self"></param>
        public static void ClearCard(this HandCardsComponent self)
        {
            self.library.Clear();
        }

        /// <summary>
        /// 出牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="card"></param>
        public static void PopCard(this HandCardsComponent self, byte card)
        {
            self.library.Remove(card);
        }
        /// <summary>
        /// 排序,得到牛型
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static byte Sort(this HandCardsComponent self)
        {
            //手牌排序 . 牛数算法  TODO
            Arithmetic(self);

            return self.CardType;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="self"></param>
        public static void Reset(this HandCardsComponent self)
        {
            self.library.Clear();
            self.CardType = 0;
        }

        private static void Arithmetic(this HandCardsComponent self)
        {
            byte[] Cards = self.GetAll();
            List<byte> NewList = new List<byte>();
            int RemainNumber = 0;
            int NiuNumber = -1;
            bool IsHaveNiu = false;
            for (int i = 0; i <= 2; i++)
            {
                if (IsHaveNiu)
                {
                    break;
                }
                for (int j = i + 1; j <= 3; j++)
                {
                    if (IsHaveNiu)
                    {
                        break;
                    }
                    for (int k = j + 1; k <= 4; k++)
                    {
                        if (IsHaveNiu)
                        {
                            break;
                        }
                        byte NumberA = GetCardValue(Cards[i]);
                        byte NumberB = GetCardValue(Cards[j]);
                        byte NumberC = GetCardValue(Cards[k]);
                        int Numbers = NumberA + NumberB + NumberC;
                        if (Numbers % 10 == 0)
                        {
                            IsHaveNiu = true;
                            self.library.Clear();
                            NewList.Clear();
                            NewList.Add(Cards[i]);
                            NewList.Add(Cards[j]);
                            NewList.Add(Cards[k]);

                            for (int l = 0; l <= 4; l++)
                            {
                                if (l != i && l != j && l != k)
                                {
                                    NewList.Add(Cards[l]);
                                    RemainNumber += GetCardValue(Cards[l]);
                                }
                            }
                            //判断牛型
                            self.CardType = (byte)(RemainNumber % 10);
                            //if (NiuNumber<temp||temp==0)
                            //{
                            //    NiuNumber = temp;
                            //}                        
                            self.library.AddRange(NewList);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 得到卡片的值
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static byte GetCardValue(byte Value)
        {
            byte MASK_COLOR = 0xF0;
            byte MASK_VALUE = 0x0F;
            byte bColor = (byte)((Value & MASK_COLOR) >> 4);
            byte bValue = (byte)(Value & MASK_VALUE);
            if (bValue>10)
            {
                bValue = 10;
            }
            return bValue;
        }
    }
}
