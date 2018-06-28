using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    /// <summary>
    /// 房间管理扩展类
    /// </summary>
    public static class RoomComponentEx
    {
        public static void CheckAndDestoryRoom(this RoomComponent roomComponent,long Roomid,long Userid)
        {
            Room room = roomComponent.Get(Roomid);
            if (room==null)
            {
                return;
            }
            if (room.Count<=1)
            {
                roomComponent.Remove(Roomid);
                room.Dispose();
            }
            else
            {
                room.Delete(Userid);
            }
        }
    }
}
