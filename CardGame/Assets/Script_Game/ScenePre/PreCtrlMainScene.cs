﻿using System;
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
		StartCoroutine(LoadLoginUI());
		
		if (EndLoadCall != null) {
			EndLoadCall ();
		} else {

		}
	}
	
	protected IEnumerator LoadLoginUI()
	{
		AssetBundleLoadOperationFull request_uil = AssetBundleManager.LoadAssetBundleAsync("uimain.unity3d");
		if (request_uil == null)
			yield break;
		yield return StartCoroutine(request_uil);
	}

}
