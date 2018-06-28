using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    public static class GamerFactory
    {
        public static Gamer Create(long userId,bool IsReady)
        {
            Gamer gamer = ComponentFactory.Create<Gamer>();
            gamer.UserID = userId;
            gamer.IsReady = IsReady;
            gamer.AddComponent<GamerUIComponent>();
            gamer.AddComponent<HandCardComponent>();
            return gamer;
        }

        public static Gamer CreateOther(ushort ChairId, bool IsReady)
        {
            Gamer gamer = ComponentFactory.Create<Gamer>();
            gamer.ChairId = ChairId;
            gamer.IsReady = IsReady;
            gamer.AddComponent<GamerUIComponent>();
            gamer.AddComponent<HandCardComponent>();
            return gamer;
        }
    }
}
