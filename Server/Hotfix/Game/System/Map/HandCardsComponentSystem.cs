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

        public static void Sort(this HandCardsComponent self)
        {
            //手牌排序 . 牛数算法  TODO
        }
    }
}
