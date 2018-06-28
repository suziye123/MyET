using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_PlayerEnter : AMHandler<Actor_GamerEnterRoom_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerEnterRoom_Ntt message)
        {
            
            GameDataComponent GameData = Game.Scene.GetComponent<GameDataComponent>();
            for (int i = 0; i < message.users.Count; i++)
            {
                GamerInfo user = message.users[i];
                //如果User为空  如果已经添加了此玩家的信息               如果是自己的信息
                if (user==null|| GameData.IsExist(user.ChairID)|| user.ChairID == GameTools.GetUser().ChairId)
                {
                    continue;
                }
                Gamer gamer = GamerFactory.CreateOther(user.ChairID,false);
                gamer.Loses = user.Loses;
                gamer.Money = user.Money;
                gamer.NickName = user.NickName;
                gamer.Wins = user.Wins;
                GameData.Add(user.ChairID, gamer);
                Log.Debug($"存储新来的玩家的信息:{gamer.NickName}");
            }           

            Game.EventSystem.Run(EventIdType.UpdateView);
        }
    }
}
