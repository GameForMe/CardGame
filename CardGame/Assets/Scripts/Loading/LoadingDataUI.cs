using UnityEngine;
using System.Collections;

public class LoadingDataUI : BaseBorderUI {
	protected int passedTime = 0;
//	public GameSet.EndLoading EndForceCall;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("CheckIsOutTime", 0f, 1f);
	}
	public void ResetCheck()
	{
		passedTime = 0;
	} 
		
	void CheckIsOutTime()
	{
		passedTime++;
		if (passedTime > 6) {
			CancelInvoke ("CheckIsOutTime");
//			if(EndForceCall != null)
//			{
//				EndForceCall ();
//			}
			CloseUI ();
		}
	}

}
