using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 初始化,给UserID赋值,在添加User组件的时候执行赋值操作
    /// </summary>
    [ObjectSystem]
    public class UserAwakeSystem : AwakeSystem<User, long>
    {
        public override void Awake(User self, long a)
        {
            self.Awake(a);
        }
    }

    public sealed class User:Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; private set; }

        /// <summary>
        /// 是否在匹配中
        /// </summary>
        public bool IsMatching { get; set; }

        /// <summary>
        /// Gate转发ActorID
        /// </summary>
        public long ActorID { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public long RoomID { get; set; }

        /// <summary>
        /// 初始化,必须给UserID赋值
        /// </summary>
        /// <param name="id"></param>
        public void Awake(long id)
        {
            this.UserID = id;
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();

            IsMatching = false;
            ActorID = 0;
        }
    }
}
