using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class GameDataComponentExAwakeSystem : AwakeSystem<GameDataComponent, Gamer>
    {
        public override void Awake(GameDataComponent self, Gamer user)
        {
            self.Awake(user);
        }
    }

    public static class GameDataComponentEx
    {
        public static void Awake(this GameDataComponent GameData, Gamer user)
        {
            if (user == null)
            {
                Log.Debug("user is NULL！！！！！！");
                return;
            }
            GameData.MySelf = user;
        }

        /// <summary>
        /// 是否存在此玩家信息
        /// </summary>
        /// <param name="GameData"></param>
        /// <param name="wChairId"></param>
        /// <returns></returns>
        public static bool IsExist(this GameDataComponent GameData, ushort wChairId)
        {
            return GameData.UserInfos.ContainsKey(wChairId);
        }
        

        /// <summary>
        /// 添加玩家信息
        /// </summary>
        /// <param name="GameData"></param>
        /// <param name="wChairId"></param>
        /// <param name="user"></param>
        public static void Add(this GameDataComponent GameData, ushort wChairId, Gamer user)
        {
            if (GameData.UserInfos.ContainsKey(wChairId))
            {
                Log.Error($"Is Exit This Key !!!!  ------- {wChairId} -----------");
                return;
            }

            GameData.UserInfos.Add(wChairId, user);
        }

        /// <summary>
        /// 删除玩家信息
        /// </summary>
        /// <param name="GameData"></param>
        /// <param name="wChairId"></param>
        public static void Remove(this GameDataComponent GameData, ushort wChairId)
        {
            if (!GameData.UserInfos.ContainsKey(wChairId))
            {
                Log.Error($"Not Find This Key !!!!  ------- {wChairId} -----------");
                return;
            }
            //释放掉
            GameData.UserInfos[wChairId].Dispose();
            //移除
            GameData.UserInfos.Remove(wChairId);
        }

        /// <summary>
        /// 清除所有的玩家数据
        /// </summary>
        /// <param name="GameData"></param>
        public static void RemoveAll(this GameDataComponent GameData)
        {
            GameData.MySelf.GetComponent<GamerUIComponent>().Dispose();
            foreach (KeyValuePair<ushort, Gamer> gameDataUserInfo in GameData.UserInfos)
            {
                gameDataUserInfo.Value.Dispose();
            }
            GameData.UserInfos.Clear();
        }
    }
}
