
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_SendCard :AMHandler<Actor_SendCard_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_SendCard_Ntt message)
        {
            //显示手牌
            Log.Debug($"收到手牌：{message.Cards.BytesToString()}");

            HandCardComponent handCardComponent = GameTools.GetUser().GetComponent<HandCardComponent>();
            handCardComponent.ShowPuke(message.Cards);


            foreach (KeyValuePair<ushort, Gamer> gamer in Game.Scene.GetComponent<GameDataComponent>().UserInfos)
            {
                //如果是自己就跳过
                if (gamer.Key == GameTools.GetUser().ChairId)
                {
                    continue;
                }
                //显示其他玩家的背牌
                gamer.Value.GetComponent<HandCardComponent>().HideAllPuke();
            }

            GameTools.GetRoomComponent().playerOperateComponent.ShowRob();
        }
    }
}
