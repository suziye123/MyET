using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 房间状态
    /// </summary>
    public enum RoomState : byte
    {
        Idle,
        Ready,
        Game
    }

    /// <summary>
    /// 房间对象
    /// </summary>
    public sealed class Room : Entity
    {
        //玩家ID  椅子号
        public readonly Dictionary<long, ushort> seats = new Dictionary<long, ushort>();
        public Gamer[] gamers;

        /// <summary>
        /// 房间状态
        /// </summary>
        public RoomState State { get; set; } = RoomState.Idle;

        /// <summary>
        /// 房间玩家数量
        /// </summary>
        public int Count { get { return seats.Values.Count; } }

        /// <summary>
        /// 房间号
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// 房间规则
        /// </summary>
        public RoomConfig roomConfig { get; set; }

        public override void Dispose()
        {
            Log.Info($"{this.RoomId}房间没人啦,释放了！！");
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            seats.Clear();

            for (int i = 0; i < gamers.Length; i++)
            {
                if (gamers[i] != null)
                {
                    gamers[i].Dispose();
                    gamers[i] = null;
                }
            }

            State = RoomState.Idle;
        }
    }
}