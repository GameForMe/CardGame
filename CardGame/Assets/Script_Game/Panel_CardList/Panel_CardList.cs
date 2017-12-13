using System.Collections;
using System.Collections.Generic;
using ExcelParser;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CardList : BaseWindow
{
	private ReferenceCollector rc;
	private GameObject cardTem;
	private void Awake()
	{
		rc = gameObject.GetComponent<ReferenceCollector>();
		if (rc != null)
		{
			
			GameObject Btn_Close = rc.Get<GameObject>("Btn_Close");
			Btn_Close.GetComponent<Button>().onClick.AddListener(BtnClick_CloseUI);
			
			cardTem = rc.Get<GameObject>("cardTem");
			cardTem.gameObject.SetActive(false);
			ShowAllCardToList();
		}
		else
		{
			Debuger.LogError("mainUI 没有引用关系");
		}
	
	}

	void ShowAllCardToList()
	{
		Dictionary<object, IDataBean> allCards = CardSetMgr.instance.idDataDic;
		foreach (var keyPair in allCards)
		{
			if (keyPair.Value != null)
			{
				CardData dagta = (CardData) keyPair.Value;
				OneCardUI oneUI = InstantiateObjFun.AddOneObjToParent<OneCardUI>(cardTem, cardTem.transform.parent);
				oneUI.CurData = dagta;
//				break;
			}
		}
	
	}

	void BtnClick_CloseUI()
	{
		CloseUI();
	}
}
