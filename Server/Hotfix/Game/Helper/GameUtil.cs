using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    public static class GameUtil
    {
        /// <summary>
        /// 获取User信息
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static User GetUser(this Session session)
        {
            return session.GetComponent<SessionUserComponent>().User;
        }
    }
}
