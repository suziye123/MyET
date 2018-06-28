using ETModel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIRoomComponentAwake : AwakeSystem<UIRoom_Component>
    {
        public override void Awake(UIRoom_Component self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class UIRoomComponentStart : StartSystem<UIRoom_Component>
    {
        public override void Start(UIRoom_Component self)
        {
            self.Start();
        }
    }

    public class UIRoom_Component : UIBaseComponent
    {

        private Text txt_GamePlayerSetting;
        private Text txt_GameCountSetting;
        private Text txt_RoomIdSetting;
        private Button btn_ReturnLobby;
        public List<GameObject> PlayerObj = new List<GameObject>();
        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            txt_GamePlayerSetting = rc.GetComponent<Text>("txt_GamePlayerSetting");
            txt_GameCountSetting = rc.GetComponent<Text>("txt_GameCountSetting");
            txt_RoomIdSetting = rc.GetComponent<Text>("txt_RoomIdSetting");
            btn_ReturnLobby = rc.GetComponent<Button>("btn_Exit");
            for (int i = 0; i < GameTools.GetPlayerCount(); i++)
            {
                PlayerObj.Add(rc.Get<GameObject>($"Player{i}"));
            }
            this.btn_ReturnLobby.onClick.AddListener(OnReturnLobby);
            InitRoomInfo();
        }

        public void Start()
        {
            UpdateView();
        }

        /// <summary>
        /// 更新视图
        /// </summary>
        public void UpdateView()
        {
            GameDataComponent GameData = Game.Scene.GetComponent<GameDataComponent>();
            foreach (KeyValuePair<ushort, Gamer> info in GameData.UserInfos)
            {
                //如果是自己就跳过
                if (info.Key == GameTools.GetUser().ChairId)
                {
                    continue;
                }
                ushort ViewId = info.Key.ToView();
                this.PlayerObj[ViewId].SetActive(true);
                info.Value.GetComponent<GamerUIComponent>().SetPanel(this.PlayerObj[ViewId]);
            }
            PlayerObj[0].SetActive(true);
            GameData.MySelf.GetComponent<GamerUIComponent>().SetPanel(PlayerObj[0]);
        }

        private void OnReturnLobby()
        {
            try
            {
                SessionComponent.Instance.Session.Send(new C2G_ReturnLobby_Ntt());
                GameTools.GetUser().ChairId = 255;
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIRoom);
                Game.Scene.GetComponent<UIComponent>().CreateOrShow(UIType.UILobby);
            }
            catch (Exception e)
            {
                Log.Debug(e.Message);
            }
        }



        public void InitRoomInfo()
        {
            this.txt_GameCountSetting.text = $"1/{Game.Scene.GetComponent<RoomComponent>().room.GameCount}";
            this.txt_GamePlayerSetting.text = Game.Scene.GetComponent<RoomComponent>().room.GamePlayer.ToString();
            this.txt_RoomIdSetting.text = Game.Scene.GetComponent<RoomComponent>().room.RoomId.ToString();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();
            PlayerObj.Clear();
            btn_ReturnLobby.onClick.RemoveAllListeners();
            //Game.Scene.GetComponent<GameDataComponent>().RemoveAll();
        }
    }
}
