using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_GetUserInfo_ReqHandler : AMRpcHandler<C2G_GetUserInfo, G2C_GetUserInfo>
    {
        protected override async void Run(Session session, C2G_GetUserInfo message, Action<G2C_GetUserInfo> reply)
        {
            G2C_GetUserInfo response = new G2C_GetUserInfo();
            try
            {
                //验证session时候合法
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply(response);
                    return;
                }

                DBProxyComponent db = Game.Scene.GetComponent<DBProxyComponent>();
                UserInfo userInfo = await db.Query<UserInfo>(message.UserID,false);

                response.NickName = userInfo.NickName;
                response.Wins = userInfo.Wins;
                response.Loses = userInfo.Loses;
                response.Money = userInfo.Money;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response,e,reply);
            }
        }
    }
}
