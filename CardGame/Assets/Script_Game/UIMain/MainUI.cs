using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		ReferenceCollector rc = gameObject.GetComponent<ReferenceCollector>();
		GameObject Btn_PVE = rc.Get<GameObject>("Btn_PVE");
		Btn_PVE.GetComponent<Button>().onClick.AddListener(BtnClick_OpenPVEPanel);
	}

	void BtnClick_OpenPVEPanel()
	{
		Debuger.LogError("btn click ");
		Debuger.LogError("skill tab len is "+  SkillSetMgr.instance.idDataDic.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
