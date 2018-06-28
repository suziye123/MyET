using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class RoomComponent :Component
    {
        private readonly Dictionary<long,Room> rooms = new Dictionary<long, Room>();
        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="room"></param>
        public void Add(Room room)
        {
            this.rooms.Add(room.RoomId, room);
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room Get(long Roomid)
        {
            Room room;
            this.rooms.TryGetValue(Roomid, out room);
            return room;
        }

        /// <summary>
        /// 移除房间并返回
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room Remove(long Roomid)
        {
            Room room = Get(Roomid);
            this.rooms.Remove(Roomid);
            return room;
        }

        /// <summary>
        /// 清除所有房间
        /// </summary>
        public void ClearAll()
        {
            foreach (KeyValuePair<long, Room> room in rooms)
            {
                room.Value.Dispose();
            }
            rooms.Clear();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (var room in this.rooms.Values)
            {
                room.Dispose();
            }
        }
    }
}
