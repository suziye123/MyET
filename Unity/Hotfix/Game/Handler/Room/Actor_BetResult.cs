using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_BetResult :AMHandler<Actor_BetResult_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_BetResult_Ntt message)
        {
            if (GameTools.IsSelf(message.ChairId))
            {
                GameTools.GetUser().GetComponent<GamerUIComponent>().ShowGoldBg(message.BetNumber);
            }
            else
            {
                GameTools.GetOtherUser(message.ChairId).GetComponent<GamerUIComponent>().ShowGoldBg(message.BetNumber);
            }
        }
    }
}
