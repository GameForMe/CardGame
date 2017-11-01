using UnityEngine;
using System.Collections;

public class audio3Dctrl : MonoBehaviour {

	string audioName; //声音 名;

	public string AudioName {
		get {
			return audioName;
		}
		set {
			audioName = value;
		}
	}




	float originalVolume; //原生 音量 大小;

	public float OriginalVolume {
		get {
			return originalVolume;
		}
		set {
			originalVolume = value;
		}
	}



	int soundType; //类型;

	public int SoundType {
		get {
			return soundType;
		}
		set {
			soundType = value;
		}
	}
}
