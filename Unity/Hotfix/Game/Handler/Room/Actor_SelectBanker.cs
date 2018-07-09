using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_SelectBanker :AMHandler<Actor_SelectBanker_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_SelectBanker_Ntt message)
        {
            Log.Debug($"庄家为:{message.ChairId.ToView()}");
            if (GameTools.IsSelf(message.ChairId))
            {
                GameTools.GetUser().GetComponent<GamerUIComponent>().ShowBanker();
            }
            else
            {
                GameTools.GetOtherUser(message.ChairId).GetComponent<GamerUIComponent>().ShowBanker();
            }

            GameTools.GetRoomComponent().playerOperateComponent.ShowAddRob();

        }
    }
}
