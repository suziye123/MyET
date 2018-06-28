using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [BsonIgnoreExtraElements]
    public class UserInfo :Entity
    {
        //昵称
        public string NickName { get; set; }

        //胜场
        public int Wins { get; set; }

        //负场
        public int Loses { get; set; }

        //余额
        public long Money { get; set; }
    }
}
