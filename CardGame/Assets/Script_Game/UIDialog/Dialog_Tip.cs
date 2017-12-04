using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Tip type.
/// 提示框的显示类型;
/// </summary>
public enum TipType{
	stopShow = 0,
	justShow,
	callShow
}
public class Dialog_Tip : BaseWindow {
	
	public delegate void SureContinueCall(bool isOK);
	public SureContinueCall sureContinueCall;


	public TipType curTipType;
	protected string tipDes;

	public string TipDes {
		get {
			return tipDes;
		}
		set {
			tipDes = value;
			ShowData();
		}
	}



	void Awake()
	{
		
	}


	// Use this for initialization
	void Start () {
		ShowData ();
	}

	void ShowData()
	{
		if( TipDes != null){
		
			if(curTipType == TipType.justShow){
				
			}else if(curTipType == TipType.callShow){
				
			}
			else{
			
			}
		}
	}
	
	void BtnClick_Continue(GameObject obj)
	{
//		AudioManager.Instance().playSound(AudioType.Btn_Big,AudioSortType.UISound);
		if(sureContinueCall != null)
		{
			sureContinueCall(true);
		}
		CloseDia ();
	}

	
	void BtnClick_Cancle(GameObject obj)
	{
		if(sureContinueCall != null)
		{
			sureContinueCall(false);
		}
//		AudioManager.Instance().playSound(AudioType.Btn_Big,AudioSortType.UISound);
		CloseDia ();
	}
	void BtnClick_Return(GameObject obj)
	{
//		AudioManager.Instance().playSound(AudioType.Btn_Small,AudioSortType.UISound);
		CloseDia ();
	}
	void CloseDia()
	{
		Destroy(gameObject);
	}
	
}
