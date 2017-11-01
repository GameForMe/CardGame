using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;
/// <summary>
/// GameTool resouse manage.
/// </summary>
public class GTResouseManage  {
	private static GTResouseManage _instance;
	public static GTResouseManage Instance()
	{
		if (null == _instance)
			_instance = new GTResouseManage();
		return _instance;
	} 
	Dictionary<string,Sprite> spriteCatch = new Dictionary<string, Sprite> ();
	string firDir = "";
	/// <summary>
	/// Adds the one sprite res to load.
	/// 加载一个图片资源;
	/// </summary>
	/// <param name="resDir">Res dir.</param>
	public void AddOneSpriteResToLoad(string resDir)
	{
		if (!spriteCatch.ContainsKey (resDir)) {
			string pathStr = firDir + resDir ;
			Sprite curSp = Resources.Load ( pathStr, typeof(Sprite)) as Sprite;
			Debug.Log(" dir is "+ pathStr);
			if(curSp == null)
			{
				Debug.LogError("load "+ pathStr +" is null");
			}
			spriteCatch.Add (resDir, curSp);
		}
	}

	AssetBundle assetbundle ;
	public void AddOneBondleAboutSprite(AssetBundle _assetbundle )
	{
		assetbundle = _assetbundle;
	}
	/// <summary>
	/// Gets the one sprite.
	/// 获取一个缓冲的图片;
	/// </summary>
	/// <returns>The one sprite.</returns>
	/// <param name="resDir">Res dir.</param>
	public Sprite GetOneSprite(string resDir)
	{
		Sprite curSp = assetbundle.LoadAsset<Sprite> (resDir);
		return curSp;
	}
	protected Dictionary<string,GameObject> catchGameResArr = new Dictionary<string, GameObject> ();

	public void AddOneCatchRes(string atlasName)
	{
		GameObject obj = Resources.Load<GameObject> ("Atlas/"+atlasName);
		if ( !catchGameResArr.ContainsKey(atlasName) || catchGameResArr[atlasName] == null) {
			catchGameResArr[atlasName] = obj;
		}
	}
	public void AddOneCatchSoundRes(string dirName)
	{
		GameObject obj = Resources.Load<GameObject> (dirName);
		if ( !catchGameResArr.ContainsKey(dirName) || catchGameResArr[dirName] == null) {
			catchGameResArr[dirName] = obj;
		}
	}
	/// <summary>
	/// Inits the res load.
	/// 需要提前加载到内存的资源;
	/// </summary>
	public void InitResLoad(string atlasName)
	{
		AddOneCatchRes (atlasName);
	}
	/// <summary>
	/// Gets the one sprite from atlas.
	/// 从图集中获取图片;
	/// </summary>
	/// <returns>The one sprite from atlas.</returns>
	/// <param name="spiteName">Spite name.</param>
	/// <param name="atlasName">Atlas name.</param>
	public Sprite GetOneSpriteFromAtlas(string spiteName,string atlasName)
	{
		Sprite sp = null;

		string spriteDir = "Atlas/" + atlasName;

		//		GameObject obj = Resources.Load<GameObject> (spriteDir);
		GameObject obj = null;
		if(!catchGameResArr.ContainsKey(atlasName) || catchGameResArr[atlasName] == null)
		{
			AddOneCatchRes(atlasName);
		}

		obj = catchGameResArr[atlasName];
		if (obj != null) {
			Transform temSp = obj.transform.Find (spiteName);
			if (temSp != null) {
				sp = temSp.gameObject.GetComponent<SpriteRenderer> ().sprite;
			}
		} else {
			Debug.LogError ("get pic altas null ======= "+ spriteDir);
		}
		return sp;
	}
	IEnumerator LoadPrefabRes()
	{

		yield return null;
	}
	/// <summary>
	/// Gets all sprite from atlas.
	/// 得到图集中所有的;
	/// </summary>
	/// <returns>The all sprite from atlas.</returns>
	/// <param name="atlasName">Atlas name.</param>
	public List<Sprite> GetAllSpriteFromAtlas(string atlasName)
	{
		List<Sprite> allSpArr = new List<Sprite> ();

		//		string spriteDir = "Atlas/" + atlasName;
		GameObject obj = null;
		if(!catchGameResArr.ContainsKey(atlasName) || catchGameResArr[atlasName] == null)
		{
			AddOneCatchRes(atlasName);
		}

		obj = catchGameResArr[atlasName];
		if(obj != null)
		{
			for(int i=0;i<obj.transform.childCount;i++)
			{
				Sprite sp = null;
				Transform temSp =  obj.transform.GetChild(i);
				if(temSp != null)
				{
					sp = temSp.gameObject.GetComponent<SpriteRenderer>().sprite;
					allSpArr.Add( sp);
				}
			}
		}
		return allSpArr;
	}


	public AudioClip GetOneAudio(string audioName,string sortName)
	{
		AudioClip sp = null;

		string spriteDir = "sound/" + sortName;

		//		GameObject obj = Resources.Load<GameObject> (spriteDir);
		GameObject obj = null;
		if(!catchGameResArr.ContainsKey(spriteDir) || catchGameResArr[spriteDir] == null)
		{
			AddOneCatchSoundRes(spriteDir);
		}

		obj = catchGameResArr[spriteDir];
		if (obj != null) {
			Transform temSp = obj.transform.Find (audioName);
			if (temSp != null) {
				sp = temSp.gameObject.GetComponent<AudioSource> ().clip;
			}
		} else {
			Debug.LogError ("get audio  null ======= "+ spriteDir);
		}
		return sp;
	}

	#region assetBundle catch  每次场景前都要 添加这个场景用到的assetBundle

	Dictionary<string,AssetBundle> assetBundleCatch = new Dictionary<string, AssetBundle> ();
	/// <summary>
	/// Catchs the one asset.
	/// 缓存一个资源;
	/// </summary>
	/// <param name="assetName">Asset name.</param>
	/// <param name="catchOne">Catch one.</param>
	public void CatchOneAsset(string assetName , AssetBundle catchOne)
	{
		if (!assetBundleCatch.ContainsKey (assetName) || assetBundleCatch [assetName] == null) {//没有存或者被删了;
			assetBundleCatch.Add(assetName,catchOne);
		}
	}
	/// <summary>
	/// Uns the catch one asset.
	/// 卸载  assetBundle;
	/// </summary>
	/// <param name="assetName">Asset name.</param>
	public void UnCatchOneAsset(string assetName )
	{
		if(assetBundleCatch.ContainsKey (assetName)  && assetBundleCatch [ assetName] != null)
		{
			AssetBundle temOne = assetBundleCatch[assetName];
			AssetBundleManager.UnloadAssetBundle (assetName);
			assetBundleCatch.Remove(assetName);
		}
	}
	#endregion
}
