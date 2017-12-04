using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 登陆界面。暂时只有开始游戏  没有用户注册等;
/// </summary>
public class LoginUI : MonoBehaviour {
	
	 void Awake()
	 {
		 Transform find = transform.Find("Button");
		 if (find != null)
		 {
			 Button btn = find.GetComponent<Button>();
			 btn.onClick.AddListener (BtnClick_StartGame);
		 }
	 }

	// Use this for initialization
	void Start () {
		
	}

	void BtnClick_StartGame()
	{
		//;
		GameEnterModel model = (GameEnterModel)ModelManager.Instance().GetModelByName(Model_Type.Model_Logon);
		model.LogonGame();
	}
}
