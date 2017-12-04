using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Model_Type
{
	Model_Logon = 0, 	//登陆;
	Model_Player,		//玩家;
	Model_Battle,		//战斗;
	Model_GameInit,		//游戏中心;
	Model_Hall,			//大厅;
	Model_LogonGift,	//登陆奖励;
	Model_Task,			//任务;

	Count,			//数据模块总数;   

};
public class ModelManager  {

	private Dictionary<Model_Type,BaseModel> allModel;
	private GameObject gameObject;
	private Transform transform;

	private ModelManager ()
	{
		allModel = new Dictionary<Model_Type, BaseModel>();
		gameObject = new GameObject("ModelManager");
		transform = gameObject.transform;
		MonoBehaviour.DontDestroyOnLoad (gameObject);
	}
	private static ModelManager m_Instance;

	static public ModelManager Instance ()
	{
		if (m_Instance == null) {
			m_Instance = new ModelManager ();
		}
		return m_Instance;
	}
	/// <summary>
	/// Inits all model.
	/// 初始化所有的model控制;
	/// </summary>
	public void InitAllModel()
	{
//		PhotonNetController.Instance ();
//		PlayerModel.Instance ().transform.parent = transform;
//		GameEnterModel.Instance ().transform.parent = transform;
//		WarModel.Instance ().transform.parent = transform;
		AddModel<GameEnterModel>(Model_Type.Model_Logon);
//		AddModel<PlayerModel>(Model_Type.Model_Player);
//		AddModel<BattleModel>(Model_Type.Model_Battle);
//		AddModel<GameInitModel>(Model_Type.Model_GameInit);
//		AddModel<HallRoomModel>(Model_Type.Model_Hall);
//		AddModel<LogonGiftModel>(Model_Type.Model_LogonGift);
//		AddModel<TaskModel>(Model_Type.Model_Task);

	}

	/// <summary>
	/// View 对应的数据是否存在
	/// </summary>
	/// <returns>
	/// The model by name.
	/// </returns>
	/// <param name='modelName'>
	/// If set to <c>true</c> model name.
	/// </param>
	public bool CheckModelByName(Model_Type modelID)
	{
		return allModel.ContainsKey(modelID);
	}

	/// <summary>
	/// 添加数据模型
	/// </summary>
	/// <returns>
	/// The model.
	/// </returns>
	/// <param name='modelName'>
	/// If set to <c>true</c> model name.
	/// </param>
	/// <param name='baseModel'>
	/// If set to <c>true</c> base model.
	/// </param>
	public bool AddModel(BaseModel baseModel)
	{
		if(baseModel==null)
		{
			return false;
		}
		bool bExist = allModel.ContainsKey(baseModel.ModelID);
		if(!bExist)
		{
			allModel.Add(baseModel.ModelID,baseModel);
			return true;
		}

		return false;
	}


	public BaseModel AddModel<T>(Model_Type type) where T : BaseModel
	{
		if(!allModel.ContainsKey(type))
		{
			GameObject obj = new GameObject(typeof(T).Name);
			BaseModel com = obj.AddComponent<T>();
			com.ModelID = type;
			obj.transform.parent = gameObject.transform;

			allModel.Add(type, com);

			return com;
		}
		else
		{
			return allModel[type];
		}
	}
	/// <summary>
	/// 通过名字获取数据模型
	/// </summary>
	/// <returns>
	/// The model by name.
	/// </returns>
	/// <param name='modelName'>
	/// Model_Type name.
	/// </param>
	public BaseModel GetModelByName(Model_Type modelID)
	{
		bool bExist = allModel.ContainsKey(modelID);
		if(bExist)
		{
			BaseModel tmpmodel=null;
			bool b=allModel.TryGetValue(modelID,out tmpmodel);
			if(b==false)
			{
				return null;
			}
			return tmpmodel;
		}

		return null;
	}

	public bool  removeModelByName(Model_Type modelID)
	{
		bool bExist = allModel.ContainsKey(modelID);
		if(bExist)
		{
			GameObject.DestroyImmediate(allModel[modelID].gameObject);
			allModel.Remove(modelID);
			return true;
		}

		return false;
	}


	public void ClearModels()
	{
		allModel.Clear();
		GameObject.DestroyImmediate(gameObject);
		gameObject = new GameObject("ModelManager");
	}

	public void OnPlayerLogin()
	{
		foreach(KeyValuePair<Model_Type, BaseModel> i in allModel)
		{
			i.Value.playerHadLogin = true;
			i.Value.OnPlayerLogin();
		}
	}

	public void OnPlayerLogout()
	{
		foreach(KeyValuePair<Model_Type, BaseModel> i in allModel)
		{
			i.Value.playerHadLogin = false;
			i.Value.OnPlayerLogout();
		}
	}

}
