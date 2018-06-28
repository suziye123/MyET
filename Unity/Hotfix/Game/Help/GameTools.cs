using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

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



        public const string Black = "pk_3_";

        public const string Heart = "pk_2_";

        public const string Piece = "pk_0_";

        public const string Plum = "pk_1_";

        public const string King = "joker_";

        public const string backPuke = "pk_back";

        /// <summary>
        /// 根据图片名字转成byte
        /// </summary>
        /// <param name="TexName"></param>
        /// <returns></returns>
        public static byte TexNameTobyteCard(string TexName)
        {
            int indexOf = TexName.IndexOf('_');
            string Front = TexName.Substring(0, indexOf + 1);
            string Dis = TexName.Substring(indexOf + 1);
            byte bColor = 0;
            byte bValue = 0;
            byte bCard = 0;
            if (Front.Equals(Piece))
            {
                bColor = 0;
            }
            else if (Front.Equals(Heart))
            {
                bColor = 2;
            }
            else if (Front.Equals(Plum))
            {
                bColor = 1;
            }
            else if (Front.Equals(Black))
            {
                bColor = 3;
            }
            else if (Front.Equals(King))
            {
                bColor = 4;
            }
            bValue = Dis.ToByte();
            bColor = (byte)((bColor << 4));
            bCard = (byte)(bColor | bValue);
            return bCard;
        }

        private static ReferenceCollector PukeRc;

        /// <summary>
        /// 得到图片的图集
        /// </summary>
        /// <param name="bCard"></param>
        /// <returns></returns>
        public static Sprite GetTexSprite(byte bCard)
        {
            if (PukeRc==null)
            {
                PukeRc = AltasHelp.GetPukeAltas().GetComponent<ReferenceCollector>();
                return PukeRc.Get<Sprite>($"{GetCardTex(bCard)}");
            }
            else
            {
               return PukeRc.Get<Sprite>($"{GetCardTex(bCard)}");
            } 
        }
        /// <summary>
        /// 获取背牌
        /// </summary>
        /// <returns></returns>
        public static Sprite GetBackPuke()
        {
            if (PukeRc == null)
            {
                PukeRc = AltasHelp.GetPukeAltas().GetComponent<ReferenceCollector>();
                return PukeRc.Get<Sprite>($"{backPuke}");
            }
            else
            {
                return PukeRc.Get<Sprite>($"{backPuke}");
            }
        }

        /// <summary>
        /// 得到卡片的名字
        /// </summary>
        /// <param name="bCard"></param>
        /// <returns></returns>
        public static string GetCardTex(byte bCard)
        {
            StringBuilder sb = new StringBuilder();
            string colorName = String.Empty;
            /*
            0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0C,0x0D,	//F A - K
            0x11,0x12,0x13,0x14,0x15,0x16,0x17,0x18,0x19,0x1A,0x1B,0x1C,0x1D,	//M A - K
            0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28,0x29,0x2A,0x2B,0x2C,0x2D,	//H A - K
            0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,0x3A,0x3B,0x3C,0x3D,	//B A - K
            0x4E,0x4F
            0x51                   花牌
            */
            byte MASK_COLOR = 0xF0;
            byte MASK_VALUE = 0x0F;
            byte bColor = (byte)((bCard & MASK_COLOR) >> 4);
            byte bValue = (byte)(bCard & MASK_VALUE);
            if (bColor == 0)
            {
                colorName = Piece;
            }
            else if (bColor == 1)
            {
                colorName = Plum;
            }
            else if (bColor == 2)
            {
                colorName = Heart;
            }
            else if (bColor == 3)
            {
                colorName = Black;
            }
            else if (bColor == 4)
            {
                colorName = King;
            }
            sb = sb.Append(String.Format("{0}{1}", colorName, bValue));          
            return sb.ToString();
        }
    }
}
