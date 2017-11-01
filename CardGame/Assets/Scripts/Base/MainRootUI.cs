using UnityEngine;
using System.Collections;

public class MainRootUI : MonoBehaviour {

	bool isOpenListener = true;

	public bool IsOpenListener {
		get {
			return isOpenListener;
		}
		set {
			isOpenListener = value;
			ShowData ();
		}
	}
	public AudioListener listener;
	void Awake()
	{
//		listener = transform.GetComponent<AudioListener>();
		Transform find = transform.Find("Camera");
		if (find != null) {
			listener = find.GetComponent<AudioListener>();
		}
		ShowData ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	void ShowData () {
		if(listener != null)
		{
			listener.enabled = IsOpenListener;
		}
	}
}
