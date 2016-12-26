using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	#if UNITY_IOS

	public void openImageSheet(){

		FDViewController.PhotoCamPicker();
	}

	#endif
}
