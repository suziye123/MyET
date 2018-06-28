using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_ReturnLobby :AMHandler<Actor_GamerExitRoom_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerExitRoom_Ntt message)
        {
            //如果是自己退出房间
            if (message.ChairId==GameTools.GetUser().ChairId)
            {
                GameTools.GetUser().ChairId = 255;
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIRoom);
                Game.Scene.GetComponent<UIComponent>().CreateOrShow(UIType.UILobby);
                return;
            }
            //有其他玩家退出房间
            GameDataComponent GameData = Game.Scene.GetComponent<GameDataComponent>();
            GameData.Remove(message.ChairId);
            Game.EventSystem.Run(EventIdType.UpdateView);
        }
    }
}
