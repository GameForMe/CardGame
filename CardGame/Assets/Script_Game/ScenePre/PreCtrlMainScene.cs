using System;

/// <summary>
/// Pre ctrl main scene.
/// 进入主场景前的准备工作;
/// </summary>
public class PreCtrlMainScene :PreCtrlBase
{
	public PreCtrlMainScene ()
	{
	}

	public override void StarLoadData(params object[] args){
		if (EndLoadCall != null) {
			EndLoadCall ();
		} else {

		}
	}

}

