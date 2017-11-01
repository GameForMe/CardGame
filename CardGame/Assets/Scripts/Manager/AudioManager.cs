using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class AudioManager : MonoBehaviour
{
	class ClipHolder
	{
		public AudioClip clip = null;
	}

	class ClipChache
	{
		public ClipChache (AudioClip c)
		{
			clip = c;
		}

		public AudioClip clip = null;
		public int num = 0;
	}
	protected static GameObject m_gameObject;

	private static AudioManager _instance;

	public static AudioManager Instance ()
	{
		if (_instance == null) {
			m_gameObject = new GameObject ("AudioManager");
			_instance = m_gameObject.AddComponent<AudioManager> ();
			DontDestroyOnLoad (m_gameObject);
		}
		return _instance;
	}
	#region 设置 与读取   配置
	/// <summary>
	/// Gets the sound set.
	/// 读取声音相关配置;
	/// </summary>
	public void GetSoundSet ()
	{
		if (PlayerPrefs.HasKey (CatchSet.SoundSwitchStrKey)) {
			int isOnSound = PlayerPrefs.GetInt (CatchSet.SoundSwitchStrKey);
			GameSet.bSoundOn = isOnSound == 1 ? true : false;
		} else {
			GameSet.bSoundOn = true;
		}


		if (PlayerPrefs.HasKey (CatchSet.MusicSwitchStrKey)) {
			int isOnMusic = PlayerPrefs.GetInt (CatchSet.MusicSwitchStrKey);
			GameSet.bMusicOn = isOnMusic == 1 ? true : false;
		} else {
			GameSet.bMusicOn = true;
		}
	}
	/// <summary>
	/// Buttons the sound change.
	/// 音效配置变化;
	/// </summary>
	/// <param name="isOn">If set to <c>true</c> is on.</param>
	public void BtnSound_Change(bool isOn){
		if (GameSet.bSoundOn != isOn) {
			GameSet.bSoundOn = isOn;
			PlayerPrefs.SetInt (CatchSet.SoundSwitchStrKey, GameSet.bSoundOn ? 1 : 0);
			if (GameSet.bSoundOn) {
				//				AudioManager.Instance ().stopAllSoundEff ();
			} else {
				AudioManager.Instance ().stopAllSoundEff (true);
			}
		}
	}
	/// <summary>
	/// Buttons the music change. 
	/// 音乐配置变化;
	/// </summary>
	/// <param name="isOn">If set to <c>true</c> is on.</param>
	public void BtnMusic_Change(bool isOn){
		if (GameSet.bMusicOn != isOn) {
			GameSet.bMusicOn = isOn;
			PlayerPrefs.SetInt (CatchSet.MusicSwitchStrKey, GameSet.bMusicOn ? 1 : 0);
			if (GameSet.bMusicOn) {
				AudioManager.Instance ().playAllSoundEff (false);
			} else {
				AudioManager.Instance ().stopAllSoundEff (false);
			}
		}
	}

	public void BtnVolume_Change(){

	}

	#endregion
	bool m_EffPlayFlag = true;


	/// <summary>
	/// Plaies the eff.
	/// 一定要自己关声音l;
	/// </summary>
	/// <returns>The eff.</returns>
	/// <param name="key">Key.</param>
	/// <param name="sortName">Sort name.</param>
	public void playSound (string key, string sortName, bool isSound = true)
	{
		StartCoroutine (playSoundEff (key, sortName, 1.0f, 0, false, isSound));
	}

//	public void playSound (string key, string sortName, bool bLoop, bool isSound = true)
//	{
//		StartCoroutine (playSoundEff (key, sortName, 1.0f, 0, bLoop, isSound));
//	}

	public void playSound (string key, string sortName, float vol, bool isSound = true)
	{
		StartCoroutine (playSoundEff (key, sortName, vol, 0, false, isSound));
	}


	Dictionary<string,Dictionary<string,GTAudioSource >  > catchAudio = new Dictionary<string, Dictionary<string,GTAudioSource > > ();

	 IEnumerator playSoundEff (string key, string sortName, float vol, ulong delay, bool bLoop, bool isSound)
	{
		if (m_EffPlayFlag == false) {
			yield break;
		}
		ClipHolder holder = new ClipHolder ();
		yield return StartCoroutine (loadAudioFromCache (key, sortName, holder));
		if (holder.clip == null) {
			yield break;
		}
		string nameStr = sortName + "_" + key;
		if (!catchAudio.ContainsKey (sortName)) {
			catchAudio [sortName] = new Dictionary<string, GTAudioSource> ();
		} 
		Dictionary<string,GTAudioSource > oneSortArr = catchAudio [sortName];
		if (oneSortArr != null) {
			if (oneSortArr.ContainsKey (key)) {
				GTAudioSource sourceOld = oneSortArr [key];
				if (sourceOld != null) {
                    if (vol < 0.0f)
                    {
                        vol = 0.1f;
                    }
                        if (isSound) {
						sourceOld.source.volume = GameSet.bSoundvolume * vol;
					} else {
						sourceOld.source.volume = GameSet.bMusicOnvolume * vol;
					}


                    //if (vol < 0.0f)
                    //{
                    //    sourceOld.source.volume = 0.1f;
                    //}
                    //else
                    //{
                    //    sourceOld.source.volume = vol;
                    //}
                    if (delay != 0) {
						sourceOld.source.Play (delay);
					} else {
						sourceOld.source.Play ();
					}
					sourceOld.source.loop = !isSound;
					if (!GameSet.bSoundOn) {
						sourceOld.source.Stop ();

					}
				}
			} else {
				GameObject obj = new GameObject (key);
				obj.transform.parent = gameObject.transform;
				AudioSource source = obj.AddComponent<AudioSource> ();
				GTAudioSource newSource = new GTAudioSource ();
		
				newSource.sortName = sortName;
				newSource.sortKey = key;
				newSource.isSound = isSound;
				if (source != null && holder != null && holder.clip != null) {
					source.clip = holder.clip;
					newSource.source = source;
					oneSortArr [key] = newSource;
					source.loop = bLoop;

                    if (vol < 1f)
                    {
                        vol = 1f;
                    }
                    if (isSound) {
						source.volume = GameSet.bSoundvolume * vol;
					} else {
						source.volume = GameSet.bMusicOnvolume * vol;
					}
                    //if (vol < 1.0f)
                    //{
                    //    source.volume = 1.0f;
                    //}
                    //else
                    //{
                    //    source.volume = vol;
                    //}
                    if (delay != 0) {
						source.Play (delay);
					} else {
						source.Play ();
					}
					source.loop = !isSound;
					if (!GameSet.bSoundOn) {
						source.Stop ();
					}
				} else {
					catchAudio.Remove (key);
					GameObject.Destroy (obj);
				}
			}
		}
	

	}

	IEnumerator loadAudioFromCache (string audioPath, string sortName, ClipHolder holder)
	{
		holder.clip = GTResouseManage.Instance ().GetOneAudio (audioPath, sortName);
		yield return	holder;
	}

	public void stopEff (string key, string sortName)
	{
		string nameStr = sortName + "_" + key;
		if (catchAudio.ContainsKey (sortName)) {
			Dictionary<string,GTAudioSource > oneSortArr = catchAudio [sortName];
			if (oneSortArr != null && oneSortArr.ContainsKey (key)) {
				GTAudioSource sourceOld = oneSortArr [key];
				if (sourceOld != null && sourceOld.source != null) {
					sourceOld.source.Stop ();
					oneSortArr.Remove (key);
					GameObject.Destroy (sourceOld.source.gameObject);
				}
			}
		} 

//		yield return null;
	}

	public void stopOneSortEff (string sortName)
	{
		if (catchAudio.ContainsKey (sortName)) {
			Dictionary<string,GTAudioSource > oneSortArr = catchAudio [sortName];
			if (oneSortArr != null) {
				foreach (KeyValuePair<string,GTAudioSource> oneVa in oneSortArr) {

					GTAudioSource sourceOld = oneVa.Value;
					if (sourceOld != null && sourceOld.source != null) {
						sourceOld.source.Stop ();
						GameObject.Destroy (sourceOld.source.gameObject);
					}
				}
				catchAudio [sortName] = new Dictionary<string, GTAudioSource> ();
			}
		} 

		//		yield return null;
	}

	public void stopAllSoundEff (bool isSound)
	{
		foreach (KeyValuePair<string,Dictionary<string,GTAudioSource > > oneVaArr in catchAudio) {
			Dictionary<string,GTAudioSource > oneSortArr = oneVaArr.Value;
			foreach (KeyValuePair<string,GTAudioSource> oneVa in oneSortArr) {

				GTAudioSource sourceOld = oneVa.Value;
				if (sourceOld != null && sourceOld.source != null && sourceOld.isSound == isSound) {
					sourceOld.source.Stop ();
				}
			}
		}
	}

	public void playAllSoundEff (bool isSound)
	{
		foreach (KeyValuePair<string,Dictionary<string,GTAudioSource > > oneVaArr in catchAudio) {
			Dictionary<string,GTAudioSource > oneSortArr = oneVaArr.Value;
			foreach (KeyValuePair<string,GTAudioSource> oneVa in oneSortArr) {

				GTAudioSource sourceOld = oneVa.Value;
				if (sourceOld != null && sourceOld.source != null && sourceOld.isSound == isSound) {
					sourceOld.source.Play ();
				}
			}
		}
	}





	//3D 音效;


//	AudioSource PlayAudioClip(AudioClip clip, Vector3 position, float volume) {
//		if (clip== null)
//			return;
//		
//		GameObject go = new GameObject("One shot audio");
//		go.transform.position = position;
//		AudioSource source = go.AddComponent<AudioSource>();
//		source.clip = clip;
//		source.volume = volume;
//		source.Play();
//		Destroy(go, clip.length);
//		return source;
//	}



	string BattleAudio = "BattleAudio/";

	float battleVolmRate = 1f; //当前 音量 大小 倍率;

	public List<AudioSource> BatAudioSrcList = new List<AudioSource> ();

	/// <summary>
	/// Sets the bat audio vol.
	/// //重新 设置 战场音效 大小;
	/// </summary>
	/// <param name="volumeRate">Volume rate.</param>
	public void SetBatAudioVol(float volumeRate){
		battleVolmRate = volumeRate; // 

		//遍历 所有 当前声音源;
		for (int i = 0; i < BatAudioSrcList.Count; i++) {
			if (BatAudioSrcList [i] != null) {
				audio3Dctrl tmpCtrl = (BatAudioSrcList [i].transform.gameObject).GetComponent<audio3Dctrl>();
				BatAudioSrcList [i].volume = tmpCtrl.OriginalVolume * battleVolmRate;
			}
		}
	}

	/// <summary>
	/// Play3s the D audio.
	/// </summary>
	/// <param name="audioname">Audioname.</param>
	/// <param name="transfPosi">Transf posi.</param>
	/// <param name="position">Position.</param>
	/// <param name="volume">Volume.</param>  自己的 原生 声音大小;
	/// <param name="isLoop">If set to <c>true</c> is loop.</param>
	/// <param name="soundTp">Sound tp.</param>
	/// <param name="soundData">Sound data.</param>
	public void Play3DAudio(string audioname,Transform transfPosi, Vector3 position, float volume,bool isLoop,int soundTp,Sound3DData soundData)
	{
		if (audioname.Equals (""))
			return;
		
		AudioClip clip =(AudioClip)Resources.Load(BattleAudio + audioname, typeof(AudioClip));//调用Resources方法加载AudioClip资源
		Play3DAudioClip(audioname,clip,transfPosi,position,volume,isLoop,soundTp,soundData);
	}

	void  Play3DAudioClip(string audioname,AudioClip clip,Transform transfPosi, Vector3 position, float volume,bool isLoop,int soundTp,Sound3DData soundData) {
		if (clip== null)
			return;


		//已经有了的话;
		if (transfPosi.Find (audioname) != null) {
			if (isLoop) {
				//循环中的话, 不做处理;
				return;
			} else {
				// 不循环的话, 接着新增,往下走, 即时性 触发;

			}
		}


		GameObject go = new GameObject(audioname);
		go.transform.position = position;
		go.transform.parent = transfPosi;

		audio3Dctrl tmpCtrl = go.AddComponent<audio3Dctrl>();
		tmpCtrl.AudioName = audioname; // 
		tmpCtrl.OriginalVolume = volume; // //原生 音量 大小;
		tmpCtrl.SoundType = soundTp; //

		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = tmpCtrl.OriginalVolume * battleVolmRate;  //最后的音量为:    原生 音量 大小 * curVolumeRate; 
		source.spatialBlend = 1.0f; //3d效果的 设定;

//		source.minDistance = 1f;
//		source.maxDistance = 20f;  //目前,此处暂定为 该定值, 后期有要求,可动态 根据数据 传递;

		//确定下 3D 音效 衰减属性;
		DecideSpreadProp(source,soundData);



		source.spread = 180f;
		source.rolloffMode = AudioRolloffMode.Linear;

		source.Play();



		//
		BatAudioSrcList.Add (source);

		//// 不循环的话, 接着新增,往下走, 即时性 触发;
		if (isLoop) {
			source.loop = true;
		} else {
			Destroy(go, clip.length);
		}
	}



	//确定下 3D 音效 衰减属性;
	void DecideSpreadProp(AudioSource source,Sound3DData soundData){
		if (soundData.SoundLv == 0) { //默认的 衰减属性  级别;
			source.minDistance = 1f;
			source.maxDistance = 18f; 
		}
		if (soundData.SoundLv == 1) { // 级别 1 ;
			source.minDistance = 1f;
			source.maxDistance = 21f; 
		}
		if (soundData.SoundLv == 2) { // 级别 2 ;
			source.minDistance = 1f;
			source.maxDistance = 22f; 
		}
		if (soundData.SoundLv == 3) { // 级别 3 ;
			source.minDistance = 1f;
			source.maxDistance = 23f; 
		}


		//最后 再数据 匹配一次; (如有 设定的 有效数据的 话;)
		if(soundData.SoundMinDist > 0){
			source.minDistance = soundData.SoundMinDist;
		}
		if(soundData.SoundMaxDist > 0){
			source.maxDistance = soundData.SoundMaxDist;
		}

	}



	/// <summary>  
	/// 停止特定的音效  
	/// </summary>  
	/// <param name="audioname"></param>  
	public void Delete3DAudio(string audioname,Transform transfPosi)   
	{  
		//修复下, 初始 法阵 出现不见的  bug;
		if(audioname.Equals(""))
			return;


		if (transfPosi.Find (audioname) == null) {
			return;
		}
			

		GameObject obj=  transfPosi.Find(audioname).gameObject;  
		if (obj!=null)  
		{  
			Destroy(obj);  
		}  
	}  


//	public void Play(string str)
//	{
//		AudioClip clip =(AudioClip)Resources.Load(str, typeof(AudioClip));//调用Resources方法加载AudioClip资源
//		PlayAudioClip(clip);
//	}
//
//	public void PlayAudioClip(AudioClip clip)
//	{
//　　if (clip== null)
//			return;
//		
//		AudioSource source = (AudioSource)gameObject.GetComponent("AudioSource");
//		if (source == null)
//			source =(AudioSource)gameObject.AddComponent("AudioSource");
//		source.clip = clip;
//　　source.minDistance= 1.0f;
//　　source.maxDistance= 50;
//　　source.rolloffMode= AudioRolloffMode.Linear;
//		source.transform.position =transform.position;
//		source.Play();
//	}





}


