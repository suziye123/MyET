using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_StartShowHand : AMHandler<Actor_StartShowHand_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_StartShowHand_Ntt message)
        {
            Game.Scene.GetComponent<GameDataComponent>().HideAllImg_Banker();

            GameTools.GetRoomComponent().playerOperateComponent.ShowTanpai();
        }
    }
}
