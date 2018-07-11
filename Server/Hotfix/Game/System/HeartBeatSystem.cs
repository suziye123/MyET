using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class HeartBeatUpdateSystem : UpdateSystem<HeartBeatComponent>
    {
        public override void Update(HeartBeatComponent self)
        {
            self.Update();
        }
    }
}
