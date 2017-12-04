using System;
using UnityEngine;
using System.Collections.Generic;


public class MsgSet
{
	public MsgSet ()
	{
	}

	public static void loadDataFile ()
	{
		InitData_MsgIDInfo ();
		InitData_WarTipInfo ();
		InitData_LoadingTipInfo ();
	}
	#region 消息等待的 msgid;
	protected static List<int> msgIDArr = new List<int>();


	static void InitData_MsgIDInfo ()
	{
		msgIDArr.Clear ();

		string filePath = Application.persistentDataPath + "/Data/MsgTipInfoTab.txt";

		List<MsgSocketTipData> temArr = ReflectionAssignment.LoadAndResolveData<MsgSocketTipData> (filePath);
		for (int i = 0; i < temArr.Count; i++) {
			MsgSocketTipData oneTem = temArr [i];
			if (oneTem != null) {
				msgIDArr.Add( oneTem.MsgID);
			}
		}
		if (temArr.Count == 0) {
			Debuger.LogError ("MsgTipInfoTab.txt no content ");
		}
	}
	/// <summary>
	/// Ises the need tip by message I.
	/// 这个消息是否需要提示;
	/// </summary>
	/// <returns><c>true</c>, if need tip by message I was ised, <c>false</c> otherwise.</returns>
	/// <param name="msgID">Message I.</param>
	public static bool IsNeedTipByMsgID(int msgID)
	{
		if(msgIDArr.Contains(msgID))
		{
			return true;
		}
		return false;
	}

	#endregion


	#region 战场信息提示;
	protected static List<WarTipData> warTipArr = new List<WarTipData>();

	static void InitData_WarTipInfo()
	{
		warTipArr.Clear ();

		string filePath = Application.persistentDataPath + "/Data/TipTab.txt";

		List<WarTipData> temArr = ReflectionAssignment.LoadAndResolveData<WarTipData> (filePath);
		for (int i = 0; i < temArr.Count; i++) {
			WarTipData oneTem = temArr [i];
			if (oneTem != null) {
				warTipArr.Add( oneTem);
			}
		}
		if (temArr.Count == 0) {
			Debuger.LogError ("TipTab.txt no content ");
		}
	}

	/// <summary>
	/// Gets the type of the tip arr by.
	/// 根据提示类型获得相应的提示列表l
	/// 某一种提示可能在几个位置都有;
	/// </summary>
	/// <returns>The tip arr by type.</returns>
	/// <param name="tipType">Tip type.</param>
	public static List<WarTipData> GetTipArrByType(int tipActiveType)
	{
		List<WarTipData> temArr = new List<WarTipData> ();
		Dictionary<int, List<WarTipData>> randomArr = new   Dictionary<int, List<WarTipData>> ();
		foreach(WarTipData tem in warTipArr)
		{
			if(tem.ActiveType == tipActiveType)
			{
				if (tem.IsRandomRule == 1) {
					if(!randomArr.ContainsKey(tem.TipLV))
					{
						randomArr.Add (tem.TipLV, new List<WarTipData> ());
					}
					List<WarTipData> curLvArr = randomArr[tem.TipLV];
					curLvArr.Add (tem);
				} else {
					temArr.Add (tem);
				}
			}
		}
		foreach(KeyValuePair<int,List<WarTipData>> temObj in randomArr)
		{
			List<WarTipData> arr = temObj.Value;
			int index = UnityEngine.Random.Range (0, arr.Count);
			temArr.Add (arr[index] );
		}
		return temArr;
	}

	#endregion

	#region loading信息提示;
	public static List<LoadingTipData> loadingTipArr = new List<LoadingTipData>();
//	static List<int> showedArr = 
	static void InitData_LoadingTipInfo()
	{
		loadingTipArr.Clear ();

		string filePath = Application.persistentDataPath + "/Data/TipLoading.txt";

		List<LoadingTipData> temArr = ReflectionAssignment.LoadAndResolveData<LoadingTipData> (filePath);
		for (int i = 0; i < temArr.Count; i++) {
			LoadingTipData oneTem = temArr [i];
			if (oneTem != null) {
				loadingTipArr.Add( oneTem);
			}
		}
		if (temArr.Count == 0) {
			Debuger.LogError ("TipLoading.txt no content ");
		}
	}

	public static int GetRandomLoadingTipIndex(int lastShow)
	{
		int index = UnityEngine.Random.Range( 0, loadingTipArr.Count-1);
		while(index == lastShow)
		{
			index = UnityEngine.Random.Range( 0, loadingTipArr.Count-1);
		}
		return index;
	}

	public static LoadingTipData GetRandomLoadingTip(int index)
	{
		if(loadingTipArr.Count > 0 && loadingTipArr[index] != null)
		{
			return loadingTipArr[index];
		}
		return new LoadingTipData();
	}



	#endregion
}
/// <summary>
/// War tip type.
/// 消息提示类型，决定在哪部分显示;
/// </summary>
enum WarTipType{
	///屏幕顶部;
	UITop		 = 1,
	///角色身上;
	PlayModel		 = 2,
	///屏幕中间;
	UICenter		 = 3,
	///屏幕右下角;
	UIBottom		 = 4
}
enum WarTipActiveType{
	///;
	GainScore		 = 1,
	///;
	OpenRunDoor	,	
	///;
	OpenHideDoor	,	
	///;
	AllFZDie	,	
	///;
	BFeeledByKiller	,	
	///;
	AddBuff	,	
	///;
	RemoveBuff	,	
	///;
	San0	,	
	///;
	San50	,	
	///;
	HP0	=10,	
	///;
	RunnerDie	,	
	///;
	RunnerRunAway	,	
	///;
	FriendOffline	,	
	///;
	ActiveTrap	,	
	///;
	KillTrap	,	
	///使用了全视之眼  释放某种类型的技能   释放技能  看ActiveCon;
	CastSkill	,
	///暴露位置;
	ShowSelf	,	
	///使用道具;
	UserItem	,
	///释放了陷阱	;
	CastTrap	,	
	///获得物品;
	GainItem	= 20,
	///死 了 一个法阵;
	OneFZDie	,	

}