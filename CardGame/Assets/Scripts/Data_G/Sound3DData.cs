using System;

public class Sound3DData
{
	public Sound3DData ()
	{
	}


	int soundLv;
	/// <summary>
	/// Gets or sets the sound lv.
	/// 声音 扩散 级别;
	/// </summary>
	/// <value>The sound lv.</value>
	public int SoundLv {
		get {
			return soundLv;
		}
		set {
			soundLv = value;
		}
	}

	float soundMinDist;
	/// <summary>
	/// Gets or sets the sound minimum dist.
	/// 3d 音效 最短距离;
	/// </summary>
	/// <value>The sound minimum dist.</value>
	public float SoundMinDist {
		get {
			return soundMinDist;
		}
		set {
			soundMinDist = value;
		}
	}

	float soundMaxDist;
	/// <summary>
	/// Gets or sets the sound max dist.
	/// 3d 音效 最长距离;
	/// </summary>
	/// <value>The sound max dist.</value>
	public float SoundMaxDist {
		get {
			return soundMaxDist;
		}
		set {
			soundMaxDist = value;
		}
	}


	/*
	float originalVolume = 1.0f;
	/// <summary>
	/// Gets or sets the original volume.
	/// 原生音量 大小调节;
	/// </summary>
	/// <value>The original volume.</value>
	public float OriginalVolume {
		get {
			return originalVolume;
		}
		set {
			originalVolume = value;
		}
	}
	*/

}
