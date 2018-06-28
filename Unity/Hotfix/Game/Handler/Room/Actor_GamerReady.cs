using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerReady : AMHandler<Actor_GamerReady_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerReady_Ntt message)
        {
            try
            {
                if (message.ChairId==GameTools.GetUser().ChairId)
                {
                    Log.Error("自己准备了");
                }
                else
                {
                    Log.Error($"玩家{message.ChairId.ToView()}准备了");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
