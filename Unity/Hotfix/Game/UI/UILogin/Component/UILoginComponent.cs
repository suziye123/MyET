using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLoginComponentSystem : AwakeSystem<UILoginComponent>
	{
		public override void Awake(UILoginComponent self)
		{
			self.Awake();
		}
	}
	
	public class UILoginComponent: UIBaseComponent
	{
	    private InputField Filed_Account;
	    private InputField Filed_PassWord;
	    private Button btn_Register;
	    private Button btn_Login;
	    private Text txt_LoginState;
	    private bool isRegistering = false;


        public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
		    Filed_Account = rc.GetComponent<InputField>("InputField_Account");
		    Filed_PassWord = rc.GetComponent<InputField>("InputField_PassWord");
		    btn_Register = rc.GetComponent<Button>("btn_Register");
		    btn_Login = rc.GetComponent<Button>("btn_Login");
		    txt_LoginState = rc.GetComponent<Text>("txt_LoginState");

		    btn_Login.onClick.AddListener(OnLogin);
		    btn_Register.onClick.AddListener(this.OnRegister);

            txt_LoginState.text = string.Empty;
        }

		public async void OnLogin()
		{
			try
			{
			    if (string.IsNullOrEmpty(this.Filed_Account.text)||string.IsNullOrEmpty(this.Filed_PassWord.text))
			    {
			        txt_LoginState.text = "请输入正确的账号和密码";
                    return;
			    }
				IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalConfigComponent.Instance.GlobalProto.Address);
				// 创建一个ETModel层的Session
				ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
				// 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
				Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
				R2C_Login r2CLogin = (R2C_Login) await realmSession.Call(new C2R_Login() { Account = Filed_Account.text, Password = Filed_PassWord.text});
				realmSession.Dispose();

				connetEndPoint = NetworkHelper.ToIPEndPoint(r2CLogin.Address);
				// 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
				ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
				ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;
				
				// 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
				Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);
				
				G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });

				Log.Info("登陆gate成功!");



			    Gamer gamer = GamerFactory.Create(g2CLoginGate.UserID, false);
                
                Game.Scene.AddComponent<GameDataComponent, Gamer>(gamer);

                Game.Scene.GetComponent<UIComponent>().CreateOrShow(UIType.UILobby);
			    Close();
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


	    /// <summary>
	    /// 注册按钮事件
	    /// </summary>
	    public async void OnRegister()
	    {
	        if (isRegistering || this.IsDisposed)
	        {
	            return;
	        }

	        //设置登录中状态
	        isRegistering = true;
	        Session sessionWrap = null;
	        txt_LoginState.text = "";
	        try
	        {
	            //创建登录服务器连接
	            IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint(GlobalConfigComponent.Instance.GlobalProto.Address);
	            ETModel.Session session = Game.Scene.ModelScene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
	            sessionWrap = ComponentFactory.Create<Session, ETModel.Session>(session);

                //发送注册请求
	            txt_LoginState.text = "正在注册中....";
	            R2C_Register r2C_Register_Ack = await sessionWrap.Call(new C2R_Register() { Account = Filed_Account.text, Password = Filed_PassWord.text }) as R2C_Register;//等待服务器返回注册消息.
	            txt_LoginState.text = "";

	            if (this.IsDisposed)
	            {
	                return;
	            }

	            if (r2C_Register_Ack.Error == ErrorCode.ERR_AccountAlreadyRegister)
	            {
	                txt_LoginState.text = "注册失败，账号已被注册";
	                Filed_Account.text = "";
	                Filed_PassWord.text = "";
	                return;
	            }

	            //注册成功自动登录
	            OnLogin();
	        }
	        catch (Exception e)
	        {
	            txt_LoginState.text = "注册异常";
	            Log.Error(e.ToStr());
	        }
	        finally
	        {
	            //断开验证服务器的连接
	            sessionWrap?.Dispose();
	            //设置注册处理完成状态
	            isRegistering = false;
	        }
	    }
    }
}
