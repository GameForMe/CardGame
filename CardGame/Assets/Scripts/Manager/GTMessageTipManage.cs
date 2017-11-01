using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// GameTool message tip manage.
/// 提示信息管理;
/// </summary>
public class GTMessageTipManage {

//	private GameObject gameObject;
	private GTMessageTipManage ()
	{
//		gameObject = new GameObject("GTMessageTipManage");
	}

	private static GTMessageTipManage _instance;
	public static GTMessageTipManage Instance()
	{
		if (null == _instance)
			_instance = new GTMessageTipManage();
		return _instance;
	} 

	//原始对象;
	GameObject OriginalTipObj;
	//正在显示的u对象;
//	Tip_MsgUI tipShowObj;


	MainRootUI rootTrans;

	public MainRootUI RootTrans {
		get {
			return rootTrans;
		}
		set {
			rootTrans = value;
		}
	}
	Transform RootTransform {
		get {
			return rootTrans!=null?rootTrans.transform:null;
		}
	}


	public void showOneMsg(string txt)
	{
		showOneMsg (txt,1);
	}
	public void showOneMsg(string txt,int count)
	{
		if( RootTrans == null)
		{
			return;
		}
		MsgTipStruct msg = new MsgTipStruct ();
		msg.content = txt;
		msg.showCount = count;

		CheckLoadOriginalObj ();

//		if(tipShowObj == null )
//		{
//			tipShowObj = InstantiateObjFun.AddOneObjToParent<Tip_MsgUI>(OriginalTipObj,RootTransform);
//
//			tipShowObj.ShowMsgEndCall = EndShowOneMsg;
//			tipShowObj.name = "tipUI";
//		}
//		
////		tipShowObj.gameObject.SetActive (true);
//		tipShowObj.AddOneMsg( msg);
		
	}

	/// <summary>
	/// Checks the load original object.
	/// 原始对象检查;
	/// </summary>
	void CheckLoadOriginalObj ()
	{
		if(OriginalTipObj == null)
		{
			OriginalTipObj = CatchPoolManage.Instance().GetOnePrefabsObj("UI/Tip/Prefabs/Tip_Msg");
//			GTWindowManage.Instance().SetCamera(OriginalTipObj.gameObject);
		}
	}

	/// <summary>
	/// Ends the show one message.
	/// 显示完一条信息后;
	/// </summary>
	void EndShowOneMsg(GameObject obj)
	{
//		tipShowObj.gameObject.SetActive (false);
		//关闭显示;
//		GameObject.Destroy(tipShowObj.gameObject);
	}
}
