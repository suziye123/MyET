using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_HeartBeatHandler :AMRpcHandler<C2G_HeartBeat,G2C_HeartBeat>
    {
        protected override void Run(Session session, C2G_HeartBeat message, Action<G2C_HeartBeat> reply)
        {
            Log.Info($"收到心跳");
            if (session.GetComponent<HeartBeatComponent>() != null)
            {
                session.GetComponent<HeartBeatComponent>().CurrentTime = TimeHelper.ClientNowSeconds();
            }

            reply(new G2C_HeartBeat());
        }
    }
}
