using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatchPoolManage {
	
	private static CatchPoolManage _instance;
	public static CatchPoolManage Instance()
	{
		if (null == _instance)
			_instance = new CatchPoolManage();
		return _instance;
	} 

	Dictionary<string, GameObject> curCatchManage = new  Dictionary<string, GameObject>();

	/// <summary>
	/// Gets the one prefabs object.
	/// 获取一个原始预的实例化对象;
	/// </summary>
	/// <returns>The one prefabs object.</returns>
	/// <param name="path">Path.</param>
	public GameObject GetOnePrefabsObj(string path)
	{
		if(!curCatchManage.ContainsKey(path))
		{
			curCatchManage[path] = Resources.Load (path) as GameObject;
		}
		GameObject OriginalObj = curCatchManage [path];
		if(OriginalObj != null)
		{
			return  OriginalObj;
		}
		else{
			return new GameObject();
		}
	}

	/// <summary>
	/// Clears all catch.
	/// 清除所有的缓存;
	/// </summary>
	public void ClearAllCatch()
	{
		curCatchManage.Clear ();
	}
}
