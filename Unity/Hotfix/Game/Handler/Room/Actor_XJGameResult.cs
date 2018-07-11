using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_XJGameResult : AMHandler<Actor_XJGameResult_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_XJGameResult_Ntt message)
        {
            List<XJResultInfo> Results = message.XJResult;
            for (int i = 0; i < Results.Count; i++)
            {
                XJResultInfo info = Results[i];
                Log.Debug($"玩家{info.ChairId.ToView()},当局得分:{info.XJScore}");
                if (GameTools.IsSelf(info.ChairId))
                {
                    GameTools.GetUser().GetComponent<GamerUIComponent>().UpdateScore(info.AllScore);
                }
                else
                {
                    GameTools.GetOtherUser(info.ChairId).GetComponent<GamerUIComponent>().UpdateScore(info.AllScore);
                }
            }

            GameTools.GetRoomComponent().playerOperateComponent.ShowReady();
        }
    }
}
