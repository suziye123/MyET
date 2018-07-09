using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 牌库组件
    /// </summary>
    public class DeckComponent : Component
    {
        //牌库中的牌
        public readonly List<byte> library = new List<byte>();
        //下注记录
        public readonly Dictionary<ushort,byte> RobBankerDic = new Dictionary<ushort, byte>(); 
        //抢庄记录
        public readonly Dictionary<ushort,byte> BetDic = new Dictionary<ushort, byte>();
        //摊牌记录
        public readonly Dictionary<ushort,byte[]> ShowCardDic = new Dictionary<ushort, byte[]>();
        //牌库中的总牌数
        public int CardsCount { get { return this.library.Count; } }



        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            library.Clear();
            RobBankerDic.Clear();
            BetDic.Clear();
        }
    }
}
