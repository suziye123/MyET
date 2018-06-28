using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_Login_ReqHandler : AMRpcHandler<C2R_Login, R2C_Login>
    {
        protected override async void Run(Session session, C2R_Login message, Action<R2C_Login> reply)
        {
            R2C_Login response = new R2C_Login();

            try
            {
                DBProxyComponent db = Game.Scene.GetComponent<DBProxyComponent>();

                Log.Info($"玩家登录请求:{message.Account},{message.Password}");

                List<ComponentWithId> result = await db.Query<AccountInfo>(model => model.Account==message.Account&&model.Password==message.Password);
                if (result.Count==0)
                {
                    response.Error = ErrorCode.ERR_LoginError;
                    reply(response);
                    return;
                }

                AccountInfo account = result[0] as AccountInfo;
                Log.Info($"账号登录成功{MongoHelper.ToJson(account)}");

                //将已在线玩家踢下线
                await RealmHelper.KickOutPlayer(account.Id);

                //随机分配网关服务器
                StartConfig gateConfig = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(gateConfig.GetComponent<InnerConfig>().IPEndPoint);

                //请求登录Gate服务器密匙
                G2R_GetLoginKey_Ack getLoginKey_Ack = await gateSession.Call(new R2G_GetLoginKey_Req() { UserID = account.Id }) as G2R_GetLoginKey_Ack;

                response.Key = getLoginKey_Ack.Key;
                response.Address = gateConfig.GetComponent<OuterConfig>().IPEndPoint.ToString();
                reply(response);

            }
            catch (Exception e)
            {
                ReplyError(response,e,reply);
            }
        }
    }
}
