using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class HandCardsComponent :Component
    {
        /// <summary>
        /// 玩家手牌
        /// </summary>
        public readonly List<byte> library = new List<byte>();

        /// <summary>
        /// 是否是庄
        /// </summary>
        public bool IsBanker { get; set; }

        /// <summary>
        /// 是否托管
        /// </summary>
        public bool IsTrusteeship { get; set; }

        /// <summary>
        /// 牌型倍数
        /// </summary>
        public int CardTypeMultiple { get; set; }

        /// <summary>
        /// 手牌数
        /// </summary>
        public int CardsCount { get { return library.Count; } }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.library.Clear();
            IsBanker = false;
            IsTrusteeship = false;
            CardTypeMultiple = 0;
        }
    }
}
