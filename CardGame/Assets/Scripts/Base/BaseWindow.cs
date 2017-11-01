using UnityEngine;
using System.Collections;

public class BaseWindow : BaseUI {

	public override void CloseUI()
	{
		GTWindowManage.Instance ().RemoveOneUI (this);
		gameObject.SetActive (false);
		transform.parent = null;
		Destroy (gameObject);
	}
}
