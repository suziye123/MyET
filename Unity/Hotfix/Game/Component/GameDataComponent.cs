using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    /// <summary>
    /// GameDataComponent在ex中扩展
    /// </summary>
    public class GameDataComponent : Component
    {
        /// <summary>
        /// 自己的信息
        /// </summary>
        public Gamer MySelf { get; set; }

        /// <summary>
        /// 存储其他玩家的信息 key:椅子号 UserInfo:玩家信息
        /// </summary>
        public Dictionary<ushort, Gamer> UserInfos = new Dictionary<ushort, Gamer>();

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();
            MySelf.Dispose();
            foreach (KeyValuePair<ushort, Gamer> gamer in this.UserInfos)
            {
                gamer.Value.Dispose();
            }
            this.MySelf = null;
            this.UserInfos.Clear();
        }
    }

}


