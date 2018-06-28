using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class GameGateSessionKeyComponent :Component
    {
        /// <summary>
        /// Session的网关Key的存储
        /// </summary>
        private readonly Dictionary<long, long> sessionKey = new Dictionary<long, long>();

        public void Add(long key, long userId)
        {
            this.sessionKey.Add(key, userId);
            this.TimeoutRemoveKey(key);
        }

        public long Get(long key)
        {
            this.sessionKey.TryGetValue(key, out var userId);
            return userId;
        }

        public void Remove(long key)
        {
            this.sessionKey.Remove(key);
        }

        private async void TimeoutRemoveKey(long key)
        {
            await Game.Scene.GetComponent<TimerComponent>().WaitAsync(20000);
            this.sessionKey.Remove(key);
        }
    }
}
