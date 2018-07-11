using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_GetLoginKey_ReqHandler : AMRpcHandler<R2G_GetLoginKey_Req, G2R_GetLoginKey_Ack>
    {
        protected override void Run(Session session, R2G_GetLoginKey_Req message, Action<G2R_GetLoginKey_Ack> reply)
        {
            G2R_GetLoginKey_Ack response = new G2R_GetLoginKey_Ack();
            try
            {
                long key = RandomHelper.RandInt64();
                Game.Scene.GetComponent<GameGateSessionKeyComponent>().Add(key, message.UserID);
                response.Key = key;

                //添加心跳组件

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
