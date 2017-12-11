using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseSence
{
	private ReferenceCollector rc;
	// Use this for initialization
	void Start () {
		rc = gameObject.GetComponent<ReferenceCollector>();
		if (rc != null)
		{
			GameObject Btn_PVE = rc.Get<GameObject>("Btn_PVE");
			Btn_PVE.GetComponent<Button>().onClick.AddListener(BtnClick_OpenPVEPanel);
		
			GameObject Btn_MyCard = rc.Get<GameObject>("Btn_MyCard");
			Btn_MyCard.GetComponent<Button>().onClick.AddListener(BtnClick_OpenCardList);
		}
		else
		{
			Debuger.LogError("mainUI 没有引用关系");
		}
	
	}

	void BtnClick_OpenPVEPanel()
	{
		Debuger.LogError("btn click ");
		Debuger.LogError("skill tab len is "+  SkillSetMgr.instance.idDataDic.Count);
	}

	void BtnClick_OpenCardList()
	{
		
	}
	

}
