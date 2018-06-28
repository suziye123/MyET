using ETModel;

namespace ETHotfix
{
    /// <summary>
    /// 玩家对象
    /// </summary>
    public sealed class Gamer : Entity
    {
        //玩家唯一ID
        public long UserID { get; set; }

        //是否准备
        public bool IsReady { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 胜利场次
        /// </summary>
        public int Wins { get; set; }
        /// <summary>
        /// 失败次数
        /// </summary>
        public int Loses { get; set; }
        /// <summary>
        /// 游戏币
        /// </summary>
        public long Money { get; set; }
        /// <summary>
        /// 椅子号
        /// </summary>
        public ushort ChairId { get; set; }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.UserID = 0;
            this.IsReady = false;
            this.NickName = string.Empty;
            this.Wins = 0;
            this.Loses = 0;
            this.Money = 0;
            this.ChairId = 255;
        }
    }
}
