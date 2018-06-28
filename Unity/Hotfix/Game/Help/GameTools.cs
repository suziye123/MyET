using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public static class GameTools
    {
        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public static long GetUserID()
        {
            return GetUser().UserID;
        }

        public static Gamer GetUser()
        {
            return Game.Scene.GetComponent<GameDataComponent>().MySelf;
        }

        public static byte GetPlayerCount()
        {
            return Game.Scene.GetComponent<RoomComponent>().room.GamePlayer;
        }

        public static RoomConfig GetRoomConfig()
        {
            return Game.Scene.GetComponent<RoomComponent>().room;
        }

        /// <summary>
        /// 座位转视图
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public static ushort ToView(this ushort us)
        {
            byte wViewChairID = (byte)((us - GetUser().ChairId+ GetRoomConfig().GamePlayer));

            return (byte)(wViewChairID % GetRoomConfig().GamePlayer);
        }
        /// <summary>
        /// 视图转座位
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public static ushort ToChair(this ushort us)
        {
            byte wChairID = (byte)((us + GetUser().ChairId) % GetRoomConfig().GamePlayer);
            return wChairID;
        }
    }
}
