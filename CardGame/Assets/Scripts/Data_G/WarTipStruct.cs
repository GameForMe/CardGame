using System;

public class WarTipStruct
{
	public WarTipStruct ()
	{
		
	}

	public WarTipStruct (WarTipData onetip )
	{
		data = onetip;
	}

	WarTipData data;

	public WarTipData Data {
		get {
			return data;
		}
	}


	public int AliveTime { get; set;}  //  ;

	public string Content { get; set;}  //  ;


}


