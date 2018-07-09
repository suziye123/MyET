using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_ShowHandResult :AMHandler<Actor_ShowHandResult_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_ShowHandResult_Ntt message)
        {
            Log.Debug($"玩家{message.ChairId},摊牌数据:{message.Cards.BytesToString()}");
        }
    }
}
