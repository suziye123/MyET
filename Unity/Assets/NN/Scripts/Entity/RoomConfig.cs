using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    public struct RoomConfig
    {
        public byte GameCount { get; set; }

        public byte GamePlayer { get; set; }

        public long RoomId { get; set; }

        public int GameScore { get; set; }
    }

}


