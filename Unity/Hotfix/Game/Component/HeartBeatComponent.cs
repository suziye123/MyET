using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class HeartBeatUpdateSystem: UpdateSystem<HeartBeatComponent>
    {
        public override void Update(HeartBeatComponent self)
        {
            self.Update();
        }
    }

    /// <summary>
    /// Session心跳组件(需要挂载到Session上)
    /// </summary>
    public class HeartBeatComponent : Component
    {
        /// <summary>
        /// 心跳包间隔
        /// </summary>
        public float SendInterval = 5f;
        /// <summary>
        /// 记录时间
        /// </summary>
        private float RecordDeltaTime = 0f;
        public async void Update()
        {
            // 如果还没有建立Session直接返回、或者没有到达发包时间
            if (!((Time.time - this.RecordDeltaTime) > this.SendInterval)) return;
            // 记录当前时间
            this.RecordDeltaTime = Time.time;
            // 开始发包
            try
            {
                //G2C_HeartBeat result = (G2C_HeartBeat)await this.GetParent<Session>().Call(new C2G_HeartBeat());
            }
            catch
            {
                // 执行断线后的操作 
                Debug.Log("断线了");
            }
        }
    }
}
