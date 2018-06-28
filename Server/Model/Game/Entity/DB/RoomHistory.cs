using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [BsonIgnoreExtraElements]
    public class RoomHistory : Entity
    {
        /// <summary>
        /// 创建房间次数
        /// </summary>
        public long CreateCount { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
