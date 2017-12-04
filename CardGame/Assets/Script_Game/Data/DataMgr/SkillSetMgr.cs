using System.Collections;
using System.Collections.Generic;
using ExcelParser;
/// <summary>
/// 自动生成类，请不要修改;
/// </summary>
public partial class SkillSetMgr : DataMgrBase<SkillSetMgr> {


	protected override string GetXlsxPath ()
	{
		return "Skill.txt";
	}


	protected override System.Type GetBeanType ()
	{
		return typeof(SkillBean);
	}


	public SkillBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (SkillBean)dataBean;
		}else{
			return null;
		}
	}



}
