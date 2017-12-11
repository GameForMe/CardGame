using System.Collections;
using System.Collections.Generic;
using ExcelParser;
/// <summary>
/// 自动生成类，请不要修改;
/// </summary>
public partial class ChaptersSetMgr : DataMgrBase<ChaptersSetMgr> {


	protected override string GetXlsxPath ()
	{
		return "Chapters.txt";
	}


	protected override System.Type GetBeanType ()
	{
		return typeof(ChaptersData);
	}


	public ChaptersData GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (ChaptersData)dataBean;
		}else{
			return null;
		}
	}



}
