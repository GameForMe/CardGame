using System;

/// <summary>
/// War tip data.
/// 战场提示的信息结构;
/// </summary>
public class WarTipData
{
	public WarTipData ()
	{
	}

	public int TipID { get; set;}  // id ;
	public string TipName { get; set;}  //  ;
	public int TipLV { get; set;}  //  ;
	public int IsRandomRule { get; set;}  //  ;
	public int TipType { get; set;}  //  ;
	public int ActiveType { get; set;}  //  ;
	public int ActivCon { get; set;}  //  ;
	public int IsHarmful { get; set;}  //  ;
	public string ActiveDes { get; set;}  //  ;
	public string TipStr { get; set;}  //  ;
	public int IsUseDynamic { get; set;}  //  ;
	public int ImgID { get; set;}  //  ;
	public int FlashImg { get; set;}  //  ;
	public int DurationTime { get; set;}  //  ;

}

public class LoadingTipData
{
	public int ID { get; set;}  // id ;
	public string Tips { get; set;}  //  ;
}