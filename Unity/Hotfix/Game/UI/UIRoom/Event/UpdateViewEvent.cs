using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.UpdateView)]
    public class UpdateViewEvent :AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Get(UIType.UIRoom).GetComponent<UIRoom_Component>().UpdateView();
        }
    }
}
