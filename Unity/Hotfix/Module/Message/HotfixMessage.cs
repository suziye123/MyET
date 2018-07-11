using ProtoBuf;
using ETModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
namespace ETHotfix
{
//玩家准备
	[Message(HotfixOpcode.Actor_GamerReady_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerReady_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

	}

//抢庄
	[Message(HotfixOpcode.Actor_RobBanker_Ntt)]
	[ProtoContract]
	public partial class Actor_RobBanker_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

		[ProtoMember(2, IsRequired = true)]
		public byte BankerNumber;

	}

//下注
	[Message(HotfixOpcode.Actor_GamerBet_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerBet_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

		[ProtoMember(2, IsRequired = true)]
		public byte BetNumber;

	}

//摊牌
	[Message(HotfixOpcode.Actor_ShowHandCard_Ntt)]
	[ProtoContract]
	public partial class Actor_ShowHandCard_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

	}

//游戏开始
	[Message(HotfixOpcode.Actor_GamerStart_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerStart_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

	}

//开始抢庄
	[Message(HotfixOpcode.Actor_StartRobBanker_Ntt)]
	[ProtoContract]
	public partial class Actor_StartRobBanker_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

	}

//开始发牌
	[Message(HotfixOpcode.Actor_SendCard_Ntt)]
	[ProtoContract]
	public partial class Actor_SendCard_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public byte[] Cards;

	}

//抢庄结果
	[Message(HotfixOpcode.Actor_RobBankerResult_Ntt)]
	[ProtoContract]
	public partial class Actor_RobBankerResult_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

		[ProtoMember(2, IsRequired = true)]
		public byte BankerNumber;

	}

//指定庄家
	[Message(HotfixOpcode.Actor_SelectBanker_Ntt)]
	[ProtoContract]
	public partial class Actor_SelectBanker_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

	}

//下注结果
	[Message(HotfixOpcode.Actor_BetResult_Ntt)]
	[ProtoContract]
	public partial class Actor_BetResult_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

		[ProtoMember(2, IsRequired = true)]
		public byte BetNumber;

	}

//开始摊牌
	[Message(HotfixOpcode.Actor_StartShowHand_Ntt)]
	[ProtoContract]
	public partial class Actor_StartShowHand_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

	}

//摊牌结果
	[Message(HotfixOpcode.Actor_ShowHandResult_Ntt)]
	[ProtoContract]
	public partial class Actor_ShowHandResult_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

		[ProtoMember(2, IsRequired = true)]
		public byte[] Cards;

		[ProtoMember(3, IsRequired = true)]
		public byte CardType;

	}

	[Message(HotfixOpcode.Actor_XJGameResult_Ntt)]
	[ProtoContract]
	public partial class Actor_XJGameResult_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, TypeName = "ETHotfix.XJResultInfo")]
		public List<XJResultInfo> XJResult = new List<XJResultInfo>();

	}

	[Message(HotfixOpcode.XJResultInfo)]
	[ProtoContract]
	public partial class XJResultInfo
	{
		[ProtoMember(1, IsRequired = true)]
		public ushort ChairId;

		[ProtoMember(2, IsRequired = true)]
		public int XJScore;

		[ProtoMember(3, IsRequired = true)]
		public int AllScore;

	}

//退出房间
	[Message(HotfixOpcode.Actor_GamerExitRoom_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerExitRoom_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public ushort ChairId;

	}

//玩家进入房间
	[Message(HotfixOpcode.Actor_GamerEnterRoom_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerEnterRoom_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, TypeName = "ETHotfix.GamerInfo")]
		public List<GamerInfo> users = new List<GamerInfo>();

	}

	[Message(HotfixOpcode.GamerInfo)]
	[ProtoContract]
	public partial class GamerInfo
	{
		[ProtoMember(1, IsRequired = true)]
		public string NickName;

		[ProtoMember(2, IsRequired = true)]
		public int Wins;

		[ProtoMember(3, IsRequired = true)]
		public int Loses;

		[ProtoMember(4, IsRequired = true)]
		public long Money;

		[ProtoMember(5, IsRequired = true)]
		public ushort ChairID;

	}

	[Message(HotfixOpcode.C2G_HeartBeat)]
	[ProtoContract]
	public partial class C2G_HeartBeat: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

	}

	[Message(HotfixOpcode.G2C_HeartBeat)]
	[ProtoContract]
	public partial class G2C_HeartBeat: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

	}

//获取用户信息
	[Message(HotfixOpcode.C2G_GetUserInfo)]
	[ProtoContract]
	public partial class C2G_GetUserInfo: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

//获取用户信息
	[Message(HotfixOpcode.G2C_GetUserInfo)]
	[ProtoContract]
	public partial class G2C_GetUserInfo: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string NickName;

		[ProtoMember(2, IsRequired = true)]
		public int Wins;

		[ProtoMember(3, IsRequired = true)]
		public int Loses;

		[ProtoMember(4, IsRequired = true)]
		public long Money;

	}

	[Message(HotfixOpcode.RoomInfo)]
	[ProtoContract]
	public partial class RoomInfo
	{
		[ProtoMember(1, IsRequired = true)]
		public byte PlayerCount;

		[ProtoMember(2, IsRequired = true)]
		public byte GameCount;

		[ProtoMember(3, IsRequired = true)]
		public int GameScore;

	}

//创建房间
	[Message(HotfixOpcode.C2G_CreateRoom)]
	[ProtoContract]
	public partial class C2G_CreateRoom: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(2, IsRequired = true)]
		public RoomInfo Room;

	}

//创建房间
	[Message(HotfixOpcode.G2C_CreateRoom)]
	[ProtoContract]
	public partial class G2C_CreateRoom: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long RoomId;

		[ProtoMember(2, IsRequired = true)]
		public RoomInfo Room;

		[ProtoMember(3, IsRequired = true)]
		public ushort ChairID;

	}

//加入房间
	[Message(HotfixOpcode.C2G_JoinRoom)]
	[ProtoContract]
	public partial class C2G_JoinRoom: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long RoomId;

	}

//加入房间
	[Message(HotfixOpcode.G2C_JoinRoom)]
	[ProtoContract]
	public partial class G2C_JoinRoom: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long RoomId;

		[ProtoMember(2, IsRequired = true)]
		public RoomInfo Room;

		[ProtoMember(3, IsRequired = true)]
		public ushort ChairID;

	}

//返回大厅
	[Message(HotfixOpcode.C2G_ReturnLobby_Ntt)]
	[ProtoContract]
	public partial class C2G_ReturnLobby_Ntt: IMessage
	{
	}

	[Message(HotfixOpcode.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Account;

		[ProtoMember(2, IsRequired = true)]
		public string Password;

	}

	[Message(HotfixOpcode.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Address;

		[ProtoMember(2, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.C2R_Register)]
	[ProtoContract]
	public partial class C2R_Register: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Account;

		[ProtoMember(2, IsRequired = true)]
		public string Password;

	}

	[Message(HotfixOpcode.R2C_Register)]
	[ProtoContract]
	public partial class R2C_Register: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(HotfixOpcode.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long PlayerID;

		[ProtoMember(2, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	[ProtoContract]
	public partial class G2C_TestHotfixMessage: IMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	[ProtoContract]
	public partial class C2M_TestActorRequest: IActorRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	[ProtoContract]
	public partial class M2C_TestActorResponse: IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

}
