using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class RoomComponentAwakeSystem : AwakeSystem<RoomComponent, RoomConfig>
    {
        public override void Awake(RoomComponent self, RoomConfig a)
        {
            self.Awake(a);
        }
    }
    /// <summary>
    /// 对房间的管理
    /// </summary>
    public class RoomComponent : Component
    {
        public RoomConfig room { get; set; }

        public void Awake(RoomConfig roomInfo)
        {
            room = roomInfo;
        }

        public void SetRoomConfig(RoomConfig config)
        {
            room = config;
        }
    }
}
