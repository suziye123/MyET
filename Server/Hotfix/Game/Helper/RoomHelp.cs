using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public static class RoomHelp
    {
        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="RoomID">房间号</param>
        /// <returns></returns>
        public static Room GetRoom(long RoomID)
        {
            return Game.Scene.GetComponent<RoomComponent>().Get(RoomID);
        }
    }
}
