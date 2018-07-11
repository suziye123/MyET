using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLobbyComponentSystem : AwakeSystem<UILobbyComponent>
	{
		public override void Awake(UILobbyComponent self)
		{
			self.Awake();
		}
	}
	
	public class UILobbyComponent : UIBaseComponent
	{
	    private Text txt_MoneySetting;
	    private Text txt_NameSetting;
	    private Text txt_JoinRoomState;
	    private Button btn_JoinSure;
	    private Button btn_CreateRoom;
	    private Button btn_JoinRoom;
	    private Button btn_Return;


        private InputField InputField_RoomId;



        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
		    txt_MoneySetting = rc.GetComponent<Text>("txt_MoneySetting");
		    txt_NameSetting = rc.GetComponent<Text>("txt_NameSetting");
		    txt_JoinRoomState = rc.GetComponent<Text>("txt_JoinRoomState");


		    btn_JoinSure = rc.GetComponent<Button>("btn_JoinSure");
		    btn_CreateRoom = rc.GetComponent<Button>("btn_CreateRoom");
		    btn_JoinRoom = rc.GetComponent<Button>("btn_JoinRoom");
		    btn_Return = rc.GetComponent<Button>("btn_Return");

		    InputField_RoomId = rc.GetComponent<InputField>("InputField_RoomId");

            GetUserInfo();
            btn_CreateRoom.onClick.AddListener(this.OnCreateRoom);
		    btn_JoinSure.onClick.AddListener(this.OnJoinRoom);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
	    private async void GetUserInfo()
	    {
	        try
	        {
	            Gamer user = GameTools.GetUser();
	            G2C_GetUserInfo userinfo =
	                    await SessionComponent.Instance.Session.Call(new C2G_GetUserInfo() { UserID = GameTools.GetUserID() }) as G2C_GetUserInfo;
	            user.Loses = userinfo.Loses;
	            user.Money = userinfo.Money;
	            user.NickName = userinfo.NickName;
	            user.Wins = userinfo.Wins;
	            Log.Debug($"{user.NickName} | {user.Money}");

	            this.txt_NameSetting.text = userinfo.NickName;
	            this.txt_MoneySetting.text = userinfo.Money.ToString();
	        }
	        catch (Exception e)
	        {
	            Log.Error(e);
            }
        }
        /// <summary>
        /// 创建房间
        /// </summary>
	    private async void OnCreateRoom()
	    {
	        try
	        {
                RoomInfo ToServerinfo = new RoomInfo();
	            ToServerinfo.GameCount = 10;
	            ToServerinfo.GameScore = 20;
	            ToServerinfo.PlayerCount = 2;
	            G2C_CreateRoom RoomInfo = await SessionComponent.Instance.Session.Call(new C2G_CreateRoom(){Room = ToServerinfo}) as G2C_CreateRoom;

                Log.Debug($"RoomInfo={RoomInfo.Room.GameCount},{RoomInfo.Room.GameScore},{RoomInfo.Room.PlayerCount}");

                //添加房间配置
                RoomConfig config = new RoomConfig();
	            config.GameCount = RoomInfo.Room.GameCount;
	            config.GamePlayer = RoomInfo.Room.PlayerCount;
	            config.RoomId = RoomInfo.RoomId;
	            config.GameScore = RoomInfo.Room.GameScore;

	            GameTools.GetUser().ChairId = RoomInfo.ChairID;
	            RoomComponent roomComponent =Game.Scene.GetComponent<RoomComponent>();

                if (roomComponent == null)
	            {
	                Game.Scene.AddComponent<RoomComponent, RoomConfig>(config);
                }
	            else
	            {
	                roomComponent.SetRoomConfig(config);
                }


                Game.Scene.GetComponent<UIComponent>().CreateOrShow(UIType.UIRoom);
	            this.Close();
            }
	        catch (Exception e)
	        {
	            
            }
	    }

	    private async void OnJoinRoom()
	    {
	        try
	        {
	            if (string.IsNullOrEmpty(InputField_RoomId.text))
	            {
	                return;
	            }

	            G2C_JoinRoom g2c_JoinRoom = await SessionComponent.Instance.Session.Call(new C2G_JoinRoom()
                { RoomId = Convert.ToInt64(this.InputField_RoomId.text),
                })
                as G2C_JoinRoom;

                Log.Debug($"加入房间:{g2c_JoinRoom.Error},{g2c_JoinRoom.Message}");

	            //添加房间配置
                RoomConfig config = new RoomConfig();
	            config.GameCount = g2c_JoinRoom.Room.GameCount;
	            config.GamePlayer = g2c_JoinRoom.Room.PlayerCount;
	            config.RoomId = g2c_JoinRoom.RoomId;
	            config.GameScore = g2c_JoinRoom.Room.GameScore;

	            GameTools.GetUser().ChairId = g2c_JoinRoom.ChairID;
                Log.Debug($"自己的座位号:{g2c_JoinRoom.ChairID}");
	            RoomComponent roomComponent = Game.Scene.GetComponent<RoomComponent>();

	            if (roomComponent == null)
	            {
	                Game.Scene.AddComponent<RoomComponent, RoomConfig>(config);
	            }
	            else
	            {
	                roomComponent.SetRoomConfig(config);
	            }
                Game.Scene.GetComponent<UIComponent>().CreateOrShow(UIType.UIRoom);
	            this.Close();

            }
            catch (Exception e)
	        {
	            Log.Error(e);
            }
	    }

	    public override void Show()
	    {
	        base.Show();
	    }

	    public override void Close()
	    {
	        base.Close();
	    }
	}
}
