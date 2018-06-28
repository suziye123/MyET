using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_GamerStart : AMHandler<Actor_GamerStart_Ntt>
    {
        protected override void Run(ETModel.Session session, Actor_GamerStart_Ntt message)
        {
            Log.Error("游戏开始啦！！！");
        }
    }
}
