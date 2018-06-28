﻿using System;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_Register_ReqHandler : AMRpcHandler<C2R_Register, R2C_Register>
    {
        protected override async void Run(Session session, C2R_Register message, Action<R2C_Register> reply)
        {
            R2C_Register response = new R2C_Register();
            try
            {
                //数据库操作对象
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                //查询账号是否存在
                List<ComponentWithId> result = await dbProxy.Query<AccountInfo>(model=>model.Account==message.Account);
                if (result.Count > 0)
                {
                    response.Error = ErrorCode.ERR_AccountAlreadyRegister;
                    reply(response);
                    return;
                }

                //新建账号
                AccountInfo newAccount = ComponentFactory.CreateWithId<AccountInfo>(IdGenerater.GenerateId());
                newAccount.Account = message.Account;
                newAccount.Password = message.Password;

                Log.Info($"注册新账号：{MongoHelper.ToJson(newAccount)}");

                //新建用户信息
                UserInfo newUser = ComponentFactory.CreateWithId<UserInfo>(newAccount.Id);
                newUser.NickName = $"用户{message.Account}";
                newUser.Money = 10000;

                //保存到数据库
                await dbProxy.Save(newAccount);
                await dbProxy.Save(newUser, false);

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
