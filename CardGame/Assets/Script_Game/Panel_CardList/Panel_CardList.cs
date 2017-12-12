using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CardList : BaseWindow
{
	private ReferenceCollector rc;
	
	private void Awake()
	{
		rc = gameObject.GetComponent<ReferenceCollector>();
		if (rc != null)
		{
			
			GameObject Btn_Close = rc.Get<GameObject>("Btn_Close");
			Btn_Close.GetComponent<Button>().onClick.AddListener(BtnClick_CloseUI);
		}
		else
		{
			Debuger.LogError("mainUI 没有引用关系");
		}
	
	}

	void BtnClick_CloseUI()
	{
		CloseUI();
	}
}
