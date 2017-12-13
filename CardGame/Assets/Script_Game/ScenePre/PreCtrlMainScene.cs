using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using  AssetBundles;

/// <summary>
/// Pre ctrl main scene.
/// 进入主场景前的准备工作;
/// </summary>
public class PreCtrlMainScene :PreCtrlBase
{
	public PreCtrlMainScene ()
	{
	}

	public override void StarLoadData(params object[] args)
	{
		StartCoroutine(LoadMainUI());

	}
	
	protected IEnumerator LoadMainUI()
	{
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("uimain.unity3d", "uimain", typeof(GameObject));
		if (request == null)
			yield break;
		yield return StartCoroutine(request);
//		AssetBundleLoadOperationFull request_uil = AssetBundleManager.LoadAssetBundleAsync("uimain.unity3d");
//		if (request_uil == null)
//			yield break;
//		yield return StartCoroutine(request_uil);
				
		if (EndLoadCall != null) {
			EndLoadCall ();
		} else {

		}
	}

}

