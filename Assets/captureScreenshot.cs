using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.IO;
using System;
using System.Collections.Generic;

using System.Text;

public class captureScreenshot : MonoBehaviour {
	public RawImage funPic;
	public bool grab;
	public Text NamePurchasable;
	public string content;
	int shotCount;
	bool turnOff;
//	public float widthPos;
//	public float heightPos;
//	public int widthSize;
//	public int heightSize;
//	public int widthSizePixel;
//	public int heightSizePixel;
//	private byte[] bytes;

//	string path;

//	string filename;

//	private Texture2D _screenshot = null;
	// Use this for initialization
	private Texture2D tex;
//
//		get{
//			if (_screenshot == null) {
//		filename = "shot.png";
//		path = "/Volumes/Macintosh HD/Users/Nate/Documents/Buddy Toss Post GD/"+filename;
//
//		bytes = File.ReadAllBytes(path);
//		_screenshot = new Texture2D( 0, 0, TextureFormat.RGB24, false );
//		_screenshot.LoadImage( bytes );
//
//	}
//	return _screenshot;
//}
//
//	}

//	public void snapPic(){
//
//		grab = true;
//	}


	void Update(){
//
		if(Input.GetKeyUp(KeyCode.Space)){
			turnOff =false;
			StartCoroutine(		capturetheScreen());
		}

		if(Input.GetKeyUp(KeyCode.L)){
			turnOff =true;

			StopCoroutine(		capturetheScreen());
		}
	}

//	void OnApplicationPause (bool paused) {
//		if (paused) {
//
//		} else {
//
//			capturetheScreen ();
//
//		}
//	}

	IEnumerator capturetheScreen(){
		for(int i=0; i<1; i++)
		{
			if(!turnOff){
			yield return new WaitForEndOfFrame ();
			shotCount++;
				Application.CaptureScreenshot (NamePurchasable.text + ".png", 1);
			}

		}

	}

//	 Update is called once per frame
//	public void OnPostRender () {
////		if (grab) {
////			Application.CaptureScreenshot
//		int width = Screen.width;
//		int height = Screen.height;
//		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
//		// Read screen contents into the texture
//		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
////		tex = new Texture2D(Screen.width*widthSize, Screen.width*heightSize,TextureFormat.RGB24,false);
////		tex.ReadPixels(new Rect(Screen.width*widthPos,Screen.width*heightPos,Screen.width*widthSizePixel,Screen.width*heightSizePixel),0,0);
//			tex.Apply();
//			funPic.texture = tex;
//
////				grab = false;
////		}
////		}
//	public void sharePicture(){
//	IOSSocialManager.instance.ShareMedia("Buddy just got wrecked!", tex);
//	}



//	}
}
