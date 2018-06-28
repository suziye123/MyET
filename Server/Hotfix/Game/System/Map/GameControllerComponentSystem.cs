using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;

namespace ETHotfix
{

    [ObjectSystem]
    public class GameControllerComponentAwakeSystem : AwakeSystem<GameControllerComponent, RoomConfig>
    {
        public override void Awake(GameControllerComponent self, RoomConfig config)
        {
            self.Awake(config);
        }
    }

    public static class GameControllerComponentSystem
    {
        public static void Awake(this GameControllerComponent self, RoomConfig config)
        {
            self.Config = config;
            self.BasePointPerMatch = config.GameScore;
            self.Multiples = 1;
            self.MinThreshold = 100;
        }

        /// <summary>
        /// 准备开始游戏
        /// </summary>
        /// <param name="self"></param>
        public static void ReadyStartGame(this GameControllerComponent self)
        {
            Room room = self.GetParent<Room>();
            Gamer[] gamers = room.GetAll();

            if (room.Count==self.Config.PlayerCount&&gamers.Count(model => model.IsReady)==self.Config.PlayerCount)
            {
                //所有玩家准备！！
                //发送游戏开始
                room.Broadcast(new Actor_GamerStart_Ntt());
            }
        }
    }
}
