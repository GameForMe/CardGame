using System.Collections;
using System.Collections.Generic;
using ExcelParser;
/// <summary>
/// 自动生成类，请不要修改;
/// </summary>
public partial class CardSetMgr : DataMgrBase<CardSetMgr> {


	protected override string GetXlsxPath ()
	{
		return "Card.txt";
	}


	protected override System.Type GetBeanType ()
	{
		return typeof(CardBean);
	}


	public CardBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (CardBean)dataBean;
		}else{
			return null;
		}
	}



}
