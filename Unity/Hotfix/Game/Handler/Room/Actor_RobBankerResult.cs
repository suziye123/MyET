using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_RobBankerResult:AMHandler<Actor_RobBankerResult_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_RobBankerResult_Ntt message)
        {
            Log.Debug($"玩家{message.ChairId.ToView()}抢庄多少:{message.BankerNumber}");
            if (GameTools.IsSelf(message.ChairId))
            {
                GameTools.GetUser().GetComponent<GamerUIComponent>().ShowRobBanker(message.BankerNumber);
            }
            else
            {
                GameTools.GetOtherUser(message.ChairId).GetComponent<GamerUIComponent>().ShowRobBanker(message.BankerNumber);
            }

        }
    }
}
