using System;
using UnityEngine;

public class GTUIManager : MonoBehaviour
{
	public GTUIManager ()
	{
	}

	protected static GameObject m_gameObject;

	private static GTUIManager _instance;

	public static GTUIManager Instance ()
	{
		if (_instance == null) {
			m_gameObject = new GameObject ("GTUIManager");
			_instance = m_gameObject.AddComponent<GTUIManager> ();
			DontDestroyOnLoad (m_gameObject);
		}
		return _instance;
	}

	BaseUI RootParentUI;
	MainRootUI rootTrans;

	public MainRootUI RootTrans {
		get {
			return rootTrans;
		}
		set {
			rootTrans = value;
		}
	}
	/// <summary>
	/// Gets the manager base root.
	/// 获取ui管理器的 跟节点l
	/// </summary>
	/// <returns>The manager base root.</returns>
	public Transform GetManagerBaseRoot()
	{
		return m_gameObject.transform;
	}



	/// <summary>
	/// Inits the user interface base.
	/// 讲根目录显示在舞台上;
	/// </summary>
	public void InitUIBase ()
	{
		if (rootTrans == null) {
			GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/GUIUI");
//			GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/GUIUI_Lower");

			if (OriginalObj != null) {
				GameObject temObj = GameObject.Instantiate (OriginalObj) as GameObject;
				temObj.name = "RootUI";
				DontDestroyOnLoad (temObj);

				if (temObj.GetComponent<MainRootUI> () != null) {
					RootParentUI = temObj.GetComponent<BaseUI> ();
				} else {
					RootParentUI = temObj.AddComponent<BaseUI> ();
				}
//				Transform find = RootParentUI.transform.Find ("Camera");
//				rootTrans = find.gameObject.AddComponent<MainRootUI> ();

				rootTrans = RootParentUI.gameObject.AddComponent<MainRootUI> ();
			}
		}

		//这里可以给不同的管理器挂载不同的跟节点;
		GTSenceManage.Instance ().RootTrans = RootTrans;
		GTWindowManage.Instance ().RootTrans = RootTrans;
		GTMessageTipManage.Instance().RootTrans = RootTrans;
//		GuideModel.Instance().RootTrans = RootTrans;
	}

}


