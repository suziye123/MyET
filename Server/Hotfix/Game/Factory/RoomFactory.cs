using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public static class RoomFactory
    {
        public static async Task<Room> Create(byte PlayerCount,byte GameCount,int GameScore,Session client)
        {
            Room room = ComponentFactory.Create<Room,byte>(PlayerCount);
            RoomConfig config = new RoomConfig
            {
                PlayerCount = PlayerCount,
                GameCount = GameCount,
                GameScore = GameScore
            };
            room.roomConfig = config;
            await room.AddComponent<MailBoxComponent>().AddLocation();
            room.AddComponent<GameControllerComponent, RoomConfig>(config);
            Game.Scene.GetComponent<RoomComponent>().Add(room);
            client.GetUser().RoomID = room.RoomId;
            Log.Info($"创建房间{room.Id}");
            return room;
        }
    }
}
