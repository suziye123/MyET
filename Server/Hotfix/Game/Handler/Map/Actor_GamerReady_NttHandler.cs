using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_GamerReady_NttHandler : AMActorHandler<Gamer, Actor_GamerReady_Ntt>
    {
        protected override async Task Run(Gamer gamer, Actor_GamerReady_Ntt message)
        {
            gamer.IsReady = true;

            Room room = RoomHelp.GetRoom(gamer.RoomID);

            Actor_GamerReady_Ntt transpond = new Actor_GamerReady_Ntt();
            transpond.ChairId = gamer.uChairID;
            room.Broadcast(transpond);
            Log.Info($"玩家{gamer.uChairID}准备");

            //检查是否所有玩家准备
            room.GetComponent<GameControllerComponent>().ReadyStartGame();
            await Task.CompletedTask;

        }
    }
}
