using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public static partial class ErrorCode
    {
        //掉线
        public const int ERR_Disconnect = 100;
        //账号已经注册
        public const int ERR_AccountAlreadyRegister = 101;
        //加入房间失败
        public const int ERR_JoinRoomError = 103;
        //玩家金币不足
        public const int ERR_UserMoneyLessError = 104;
        //出牌错误
        public const int ERR_PlayCardError = 105;
        //登录错误
        public const int ERR_LoginError = 106;
        //房间满人
        public const int ERR_RoomIsFull = 107;
        //找不到此房间
        public const int ERR_NotFoundRoom = 108;

        //签名错误
        public const int ERR_SignError = 30001;
    }
}
