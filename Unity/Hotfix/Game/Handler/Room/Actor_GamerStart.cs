using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerStart : AMHandler<Actor_GamerStart_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerStart_Ntt message)
        {
            Log.Error("游戏开始啦！！！");
            GameTools.GetUser().GetComponent<GamerUIComponent>().Reset();
            GameTools.GetUser().GetComponent<HandCardComponent>().Reset();

            foreach (KeyValuePair<ushort, Gamer> info in Game.Scene.GetComponent<GameDataComponent>().UserInfos)
            {
                info.Value.GetComponent<GamerUIComponent>().Reset();
                info.Value.GetComponent<HandCardComponent>().Reset();
            }
        }
    }
}
