using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 关联User对象组件.
    /// Session断线的时候会触发下线,执行Destory
    /// </summary>
    public class SessionUserComponent :Component
    {
        // User对象
        public User User { get; set; }
    }
}
