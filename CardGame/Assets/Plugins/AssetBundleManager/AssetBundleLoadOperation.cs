using UnityEngine;
using System.Collections;

namespace AssetBundles
{
	public abstract class AssetBundleLoadOperation : IEnumerator
	{
		public object Current
		{
			get
			{
				return null;
			}
		}
		public bool MoveNext()
		{
			bool isDEdn = IsDone ();
			Debug.LogError ("--- zys ---- checkisdone "+  isDEdn);
			return !isDEdn;
		}
		
		public void Reset()
		{
		}
		
		abstract public bool Update ();
		
		abstract public bool IsDone ();
	}
	
	#if UNITY_EDITOR
	public class AssetBundleLoadLevelSimulationOperation : AssetBundleLoadOperation
	{	
		AsyncOperation m_Operation = null;
	
	
		public AssetBundleLoadLevelSimulationOperation (string assetBundleName, string levelName, bool isAdditive)
		{
			string[] levelPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundleName, levelName);
			if (levelPaths.Length == 0)
			{
				///@TODO: The error needs to differentiate that an asset bundle name doesn't exist
				//        from that there right scene does not exist in the asset bundle...
				
				Debug.LogError("There is no scene with name \"" + levelName + "\" in " + assetBundleName);
				return;
			}
			
			if (isAdditive)
				m_Operation = UnityEditor.EditorApplication.LoadLevelAdditiveAsyncInPlayMode(levelPaths[0]);
			else
				m_Operation = UnityEditor.EditorApplication.LoadLevelAsyncInPlayMode(levelPaths[0]);
		}
		
		public override bool Update ()
		{
			return false;
		}
		
		public override bool IsDone ()
		{		
			return m_Operation == null || m_Operation.isDone;
		}
	}
	
	#endif
	public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
	{
		protected string 				m_AssetBundleName;
		protected string 				m_LevelName;
		protected bool 					m_IsAdditive;
		protected string 				m_DownloadingError;
		protected AsyncOperation		m_Request;
	
		public AssetBundleLoadLevelOperation (string assetbundleName, string levelName, bool isAdditive)
		{
			m_AssetBundleName = assetbundleName;
			m_LevelName = levelName;
			m_IsAdditive = isAdditive;
		}
	
		public override bool Update ()
		{
			if (m_Request != null)
				return false;
			
			LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle (m_AssetBundleName, out m_DownloadingError);
			if (bundle != null)
			{
				if (m_IsAdditive)
					m_Request = Application.LoadLevelAdditiveAsync (m_LevelName);
				else
					m_Request = Application.LoadLevelAsync (m_LevelName);
				return false;
			}
			else
				return true;
		}
		
		public override bool IsDone ()
		{
			// Return if meeting downloading error.
			// m_DownloadingError might come from the dependency downloading.
			if (m_Request == null && m_DownloadingError != null)
			{
				Debug.LogError(m_DownloadingError);
				return true;
			}
			
			return m_Request != null && m_Request.isDone;
		}
	}
	
	public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
	{
		public abstract T GetAsset<T>() where T : UnityEngine.Object;
	}
	
	public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
	{
		Object							m_SimulatedObject;
		
		public AssetBundleLoadAssetOperationSimulation (Object simulatedObject)
		{
			m_SimulatedObject = simulatedObject;
		}
		
		public override T GetAsset<T>()
		{
			return m_SimulatedObject as T;
		}
		
		public override bool Update ()
		{
			return false;
		}
		
		public override bool IsDone ()
		{
			return true;
		}
	}
	
	public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
	{
		protected string 				m_AssetBundleName;
		protected string 				m_AssetName;
		protected string 				m_DownloadingError;
		protected System.Type 			m_Type;
		protected AssetBundleRequest	m_Request = null;
	
		public AssetBundleLoadAssetOperationFull (string bundleName, string assetName, System.Type type)
		{
			m_AssetBundleName = bundleName;
			m_AssetName = assetName;
			m_Type = type;
		}
		
		public override T GetAsset<T>()
		{
			if (m_Request != null && m_Request.isDone)
				return m_Request.asset as T;
			else
				return null;
		}
		
		// Returns true if more Update calls are required.
		public override bool Update ()
		{
			if (m_Request != null)
				return false;
	
			LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle (m_AssetBundleName, out m_DownloadingError);
			if (bundle != null)
			{
				///@TODO: When asset bundle download fails this throws an exception...
				m_Request = bundle.m_AssetBundle.LoadAssetAsync (m_AssetName, m_Type);
				Debug.LogError ("zys=====  "+m_AssetBundleName+" ---- "+ m_AssetName +"  " + m_Request.isDone );
				return false;
			}
			else
			{
				return true;
			}
		}
		
		public override bool IsDone ()
		{
			// Return if meeting downloading error.
			// m_DownloadingError might come from the dependency downloading.
			if (m_Request == null && m_DownloadingError != null)
			{
				Debug.LogError(m_DownloadingError);
				return true;
			}
			if (m_Request != null) {
				Debug.LogError ("zys===== check " + m_AssetBundleName + " ---- " + m_AssetName + "  " + m_Request.isDone);
			}
			return m_Request != null && m_Request.isDone;
		}
	}
	
	public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
	{
		public AssetBundleLoadManifestOperation (string bundleName, string assetName, System.Type type)
			: base(bundleName, assetName, type)
		{
			Debug.LogError ("zys!++++!!  one " + bundleName);
		}
	
		public override bool Update ()
		{
			base.Update();
			if (m_Request != null) {
				Debug.LogError ("--- zys mainfest Update ----   " + m_AssetBundleName + "  " + m_Request.isDone);
			}
			if (m_Request != null && m_Request.isDone)
			{
				Debug.LogError ("--- zys ---- load aset end  "+ m_AssetBundleName );
				AssetBundleManager.AssetBundleManifestObject = GetAsset<AssetBundleManifest>();
				return false;
			}
			else
				return true;
		}
	}

    /// <summary>
    /// 自己添加的。;
    /// </summary>
	public class AssetBundleLoadOperationFull : AssetBundleLoadOperation
    {
        protected string m_AssetBundleName;
        protected string m_DownloadingError;
        protected AssetBundleRequest m_Request = null;
        protected bool isGetBundle = false;

        public AssetBundleLoadOperationFull(string bundleName)
        {
            m_AssetBundleName = bundleName;
        }


        // Returns true if more Update calls are required.
        public override bool Update()
        {
            if (isGetBundle)
                return false;

            LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle(m_AssetBundleName, out m_DownloadingError);
            Debuger.Log(" load res " + m_AssetBundleName);
            if (bundle != null)
            {
                isGetBundle = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool IsDone()
        {
            // Return if meeting downloading error.
            // m_DownloadingError might come from the dependency downloading.


            return isGetBundle;
        }
    }
    /// <summary>
    /// 自己加的一个空的加载bundle 的东西。 如果本地模式化直接给过。让后面去读本地文件;
    /// </summary>
    public class AssetBundleLoadOperationSimulation : AssetBundleLoadOperationFull
    {
        Object m_SimulatedObject;

        public AssetBundleLoadOperationSimulation(string bundleName) : base(bundleName)
        {
           
        }



        public override bool Update()
        {
            return false;
        }

        public override bool IsDone()
        {
            return true;
        }
    }

}
