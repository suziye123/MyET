using ProtoBuf;
using ETModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
namespace ETHotfix
{
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

	[Message(HotfixOpcode.C2G_GetUserInfo)]
	[ProtoContract]
	public partial class C2G_GetUserInfo: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

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

	[Message(HotfixOpcode.C2G_CreateRoom)]
	[ProtoContract]
	public partial class C2G_CreateRoom: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(2, IsRequired = true)]
		public RoomInfo Room;

	}

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

	[Message(HotfixOpcode.C2G_JoinRoom)]
	[ProtoContract]
	public partial class C2G_JoinRoom: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long RoomId;

	}

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
