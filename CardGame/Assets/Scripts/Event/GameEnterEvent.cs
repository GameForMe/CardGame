using System;


public class GameEnterEvent
{
	public GameEnterEvent ()
	{
	}
	/// <summary>
	/// The user interface check user name vailde.
	/// 用户名是否合法;
	/// </summary>
	public static string UICheckUserNameVailde = "UICheckUserNameVailde";
	/// <summary>
	/// The data logon return.
	/// 登陆结果;
	/// </summary>
	public static string DataLogonReturn = "DataLogonReturn";


	public static string DataLogonNoPlayer = "DataLogonNoPlayer";

	/// <summary>
	/// The WX error goto common.
	/// 微信登录下不允许，跳转到正常登录流程;
	/// </summary>
	public static string WXErrorGotoCommon = "WXErrorGotoCommon";

}
