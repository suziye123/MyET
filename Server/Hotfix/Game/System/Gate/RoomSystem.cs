using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class RoomAwakeSystem : AwakeSystem<Room, byte>
    {
        public override void Awake(Room self, byte a)
        {
            self.Awake(a);
        }
    }

    public static class RoomEx
    {
        public static void Awake(this Room room,byte PlayerCount)
        {
           
            room.gamers = new Gamer[PlayerCount];
            room.RoomId = RandomHelper.RandomNumber(100000, 999999);
        }

        /// <summary>
        /// 获取该玩家
        /// </summary>
        /// <param name="room"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Gamer Get(this Room room, long id)
        {
            if (!room.seats.ContainsKey(id))
            {
                return null;
            }

            return room.gamers[room.seats[id]];
        }
        /// <summary>
        /// 根据椅子号获取玩家
        /// </summary>
        /// <param name="room"></param>
        /// <param name="ChairId"></param>
        /// <returns></returns>
        public static Gamer Get(this Room room, ushort ChairId)
        {
            return room.gamers[ChairId];
        }

        /// <summary>
        /// 删除玩家
        /// </summary>
        /// <param name="room"></param>
        /// <param name="id"></param>
        public static void Delete(this Room room, long id)
        {
            if (!room.seats.ContainsKey(id))
            {             
                return;
            }
            room.gamers[room.seats[id]] = null;

            room.seats.Remove(id);

        }

        /// <summary>
        /// 添加玩家
        /// </summary>
        /// <param name="room"></param>
        /// <param name="id"></param>
        public static void Add(this Room room, Gamer gamer)
        {
            if (room.seats.ContainsKey(gamer.UserID))
            {
                return;
            }
            room.gamers[room.Count] = gamer;
            room.seats.Add(gamer.UserID, (ushort)room.Count);
        }

        /// <summary>
        /// 获取椅子号
        /// </summary>
        /// <param name="room"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ushort GetChairId(this Room room,long id)
        {
            if (!room.seats.ContainsKey(id))
            {
                return 255;
            }
            room.seats.TryGetValue(id, out var chairId);
            return chairId;
        }

        /// <summary>
        /// 获取所有玩家
        /// </summary>
        /// <returns></returns>
        public static Gamer[] GetAll(this Room self)
        {
            return self.gamers;
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        public static void Broadcast(this Room self, IActorMessage message)
        {
            foreach (Gamer gamer in self.gamers)
            {
                if (gamer == null || gamer.isOffline)
                {
                    continue;
                }
                ActorMessageSender actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorMessageSender();
                actorProxy.Send(message);
            }
        }
        /// <summary>
        /// 是否还可以加入玩家
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsCanAddPlayer(this Room self)
        {
            return self.seats.Count == self.roomConfig.PlayerCount;
        }
    }
}
