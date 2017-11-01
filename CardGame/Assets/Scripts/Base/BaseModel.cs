using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel : MonoBehaviour
{
	protected Dictionary<string, List<GameObject>> m_AllViewObjs;
	private Model_Type modelID;
	protected bool mPlayerHadLogin;

	public bool playerHadLogin {
		get{ return mPlayerHadLogin; } 
		set{ mPlayerHadLogin = value; }
	}

	public Model_Type ModelID
	{
		get
		{
			return modelID;
		}

		set
		{
			modelID = value;
		}
	}


	public BaseModel ()
	{
		m_AllViewObjs = new Dictionary<string, List<GameObject>> ();
		mPlayerHadLogin = false;
	}



	private void OnDestroy ()
	{
		if (m_AllViewObjs != null)
			m_AllViewObjs.Clear ();

	}

	~BaseModel ()
	{
		if (m_AllViewObjs != null) {
			m_AllViewObjs.Clear ();
		}
	}

	public bool RegisterViewObject (string funnam, GameObject viewObject)
	{
		if (m_AllViewObjs.ContainsKey (funnam) == true) {
			List<GameObject> receiveObjs = null;
			bool b = m_AllViewObjs.TryGetValue (funnam, out receiveObjs);
			if (b == false || receiveObjs == null) {
				return false;
			}

			if (receiveObjs.Contains (viewObject) == true) {
				return true;
			}


			receiveObjs.Add (viewObject);
			return true;
		} else {
			List<GameObject> receiveObjs = new List<GameObject> ();
			receiveObjs.Add (viewObject);
			m_AllViewObjs.Add (funnam, receiveObjs);
			return true;
		}
	}

	public bool UngisterViewObject (string funname, GameObject viewObject)
	{
		if (m_AllViewObjs.ContainsKey (funname) == false) {
			return false;
		}

		List<GameObject> receiveObj = null;
		if (m_AllViewObjs.TryGetValue (funname, out receiveObj) == false || receiveObj == null) {
			return false;
		}

		return receiveObj.Remove (viewObject);
	}

	public bool UngisterViewObject (GameObject viewObject)
	{
		foreach (KeyValuePair<string, List<GameObject>> receivePair in m_AllViewObjs) {
			receivePair.Value.Remove (viewObject);
		}

		return true;
	}

//	public void notifyfunction (string funname, System.Object functiojnParamenter)
//	{
////		if (gameObject != null) {
////			gameObject.SendMessage (funname, functiojnParamenter);
////		}
//	}

	public void updateView (string functionName, System.Object functionParameter)
	{
		if (m_AllViewObjs == null || m_AllViewObjs.ContainsKey (functionName) == false) {
			return;
		}

		List<GameObject> receiveObj = null;
		if (m_AllViewObjs.TryGetValue (functionName, out receiveObj) == false || receiveObj == null) {
			return;
		}

		foreach (GameObject obj in receiveObj) {
			if (obj != null) {
				obj.SendMessage (functionName, functionParameter, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void updateData (string functionName, System.Object functionParameter)
	{
	}

	public virtual void initNetwork ()
	{
	}

	public virtual void OnPlayerLogin ()
	{
	}

	public virtual void OnPlayerLogout ()
	{
	}

	/// <summary>
	/// Checks the net pack common tip.
	/// 消息包回复结果通用检测并提示;
	/// </summary>
	/// <returns><c>true</c>, if net pack common tip was checked, <c>false</c> otherwise.</returns>
	/// <param name="packet">Packet.</param>
	public bool CheckNetPackCommonTip ()
	{
		
		return false;
	}
}