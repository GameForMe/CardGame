using System;

/// <summary>
/// Message AN.
/// 通讯 通用错误回复;
/// </summary>
public enum MessageANS
{
	/// 请求处理成功;
	ANS_SUCCESS = 0x00000001,

	/// 原因不明 参数错数;
	ANS_FAILED = 0x00000002,

	/// 由于尚未登录，请求非法，发生此错误说明此请求仅可由已登录用户发出;
	ANS_NOT_LOGON = 0x00000003,

	/// 请求发送过于频繁，时间间隔太短;
	ANS_HIGH_FREQ = 0x00000004,

	/// 请求不符合游戏逻辑，非法;
	ANS_REQ_FORBID = 0x00000005,

	/// 该请求需要资源不足;
	ANS_RES_LACK = 0x00000006,

	/// 数量限制，;
	ANS_COUNT_LIMIT = 0x00000007,

	/// 等级限制，;
	ANS_LEVEL_LIMIT = 0x00000008,

	///目标玩家不在线;
	ANS_TARGET_OFF_LINE = 0x00000009,

	/// 此请求中需提供对应物品的接收位置或接收方式，接收的位置或方式不合法;
	ANS_ITEM_CONTAINER_ERROR = 0x0000000A,

	/// 时间限制，如不到CD时间等;
	ANS_TIME_LIMIT = 0x0000000B,

	/// 权限限制;
	ANS_COMPETENCE_LIMIT = 0x0000000C,


}
/// <summary>
/// Game message.
/// 通讯消息标示;
/// </summary>
public enum GameMessage
{
	///获取公告内容;
	RAV_Req_NoticeInfo 			= 0x00000200,

	///设置游戏参数;
	RAV_Req_SetGameParam		= 0x00000210,

	///获取游戏参数;
	RAV_Req_GetGameParam		= 0x00000220,

	///验证是否有这个帐号;
	RAV_Req_Logon_CHECKNEWUSER 	= 0x00000300,

	///登录请求;
	RAV_Req_Logon_EXISTUSER 	= 0x00000310,

	///进行账号创建的消息;
	RAV_Req_Logon_NEWUSER		= 0x00000320,

	//客户端验证版本号;
	RAV_Req_VERIFY_VERSION		= 0x00000330,

	///客户端请求试玩账号;
	RAV_Req_TEASTACCOUNT_REQUEST= 0x00000340,

	///客户端请求绑定试玩账号;
	RAV_Req_BANDTESTACCOUNT_REQUEST= 0x00000350,

	//玩家被离开游戏;
	RAV_Nof_LeaveGame			= 0x08000390,

	//登陆微信账号进入游戏;
	RAV_Req_Logon_WX 			= 0x00000400,




	/// 获取角色信息;
	RAV_Req_GetPlayerInfo			= 0x00001000,

	//设置玩家信息;
	RAV_Req_SetPlayerInfo			= 0x00001020,

	/// 获取排行榜信息;
	RAV_Req_GetRankPlayerList		= 0x00001100,



	/// 获取用户所有角色的配装方案;
	RAV_Req_GetRoleEquipList	= 0x00001200,

	/// 更换 配装方案;
	RAV_Req_EquipItem	= 0x00001210,

	/// 获取所有角色 信息 ;
	RAV_Req_GetAllRoles	= 0x00001220,

	/// 激活角色 ;
	RAV_Req_ActiveRole = 0x00001230,

	/// 获得出战角色 ;
	RAV_Req_GetBattleRole = 0x00001240,

	/// 设置出战角色 ;
	RAV_Req_SetBattleRole = 0x00001250,


	/// 道具出售 ;
	RAV_Req_SellItem	= 0x00001300,


	/// 获取商品列表 ;
	RAV_Req_GetShopItems	= 0x00001400,

	/// 获取背包信息 ;
	RAV_Req_GetBagItems	= 0x00001500,



	/// 房间信息 ---  创建房间 ;
	RAV_Req_CreateRoom	= 0x00003000,

	/// 房间信息 ---  加入房间 ;
	RAV_Req_JoinRoom	= 0x00003010,

	/// 房间信息 ---  随机加入房间 ;
	RAV_Req_RandomJoinRoom	= 0x00003020,

	/// 房间信息 ---  退出房间 ;
	RAV_Req_ExitRoom	= 0x00003030,

	/// 房间信息 ---  设置房间状态 ;
	RAV_Req_SetRoomState	= 0x00003040,

	/// 房间信息 ---  设置自身准备状态 ;
	RAV_Req_SetSelfState	= 0x00003050,

	/// 房间信息 ---  玩家状态改变通知 ;
	RAV_Nof_ChangePlayerMsg	= 0x08003100,

	/// 房间信息 ---  房间状态改变通知 ;
	RAV_Nof_ChangeRoomMsg	= 0x08003110,

	/// 房间信息 ---  玩家加入房间 ;
	RAV_Nof_PlAddRoom		= 0x08003120,

	/// 房间信息 ---  玩家l离开房间 ;
	RAV_Nof_PlLeaveRoom		= 0x08003130,

	/// 房间信息 ---  开始读秒 ;
	RAV_Nof_RoomStartCount	= 0x08003140,

	/// 房间信息 ---  开始去战场 ;
	RAV_Nof_RoomGotoWar		= 0x08003150,


	///  获取玩家引导信息;
	RAV_Req_GetPlayerGuideInfo	= 0x08003160,

	///  设置玩家引导信息;
	RAV_Req_SetPlayerGuideInfo	= 0x08003170,

}

