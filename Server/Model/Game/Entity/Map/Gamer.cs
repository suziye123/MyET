using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class GamerAwakeSystem : AwakeSystem<Gamer, long>
    {
        public override void Awake(Gamer self, long id)
        {
            self.Awake(id);
        }
    }

    /// <summary>
    /// 房间玩家对象
    /// </summary>
    public sealed class Gamer : Entity
    {
        /// <summary>
        /// 用户ID（唯一）
        /// </summary>
        public long UserID { get; private set; }

        /// <summary>
        /// 玩家GateActorID
        /// </summary>
        public long PlayerID { get; set; }

        /// <summary>
        /// 玩家所在房间ID
        /// </summary>
        public long RoomID { get; set; }

        /// <summary>
        /// 是否准备
        /// </summary>
        public bool IsReady { get; set; }

        /// <summary>
        /// 是否离线
        /// </summary>
        public bool isOffline { get; set; }

        /// <summary>
        /// 玩家的椅子号
        /// </summary>
        public ushort uChairID { get; set; }

        public void Awake(long id)
        {
            this.UserID = id;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.UserID = 0;
            this.PlayerID = 0;
            this.RoomID = 0;
            this.uChairID = 0;

            this.IsReady = false;
            this.isOffline = false;
        }
    }
}