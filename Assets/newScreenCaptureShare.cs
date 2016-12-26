using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class newScreenCaptureShare : MonoBehaviour {
//	public Texture2D shot;
	public RawImage guiPoloroid;
//	public RenderTexture shotSqr;
	public Texture2D shotSqr;

	public Camera mainCam;
	public Text freeObjName;
	public int numScreensTaken;
	public Transform target;
//	public RenderTexture rt;
	// Use this for initialization
	void Start () {
//		shotSqr = new Texture2D (Screen.width, Screen.width, TextureFormat.RGB24, false);
		shotSqr = new Texture2D (Screen.width, Screen.width, TextureFormat.RGB24, false);

	}

	
	// Update is called once per frame
//	void Update () {
//	if(Input.GetKeyUp(KeyCode.S)){
//			newScreen();
//
//		}
//	}

	public void resetScreenshotCount(){
		numScreensTaken = 0;

	}



	public IEnumerator newScreen(){


		if (numScreensTaken <= 1) {

			numScreensTaken++;
			if(shotSqr!=null){

			System.GC.Collect();
			yield return new WaitForEndOfFrame ();
			GetComponent<Camera> ().enabled = true;
			target = GameObject.Find("Body_jnt").transform;
			transform.LookAt(target);

			RenderTexture rt = new RenderTexture (Screen.width, Screen.width, 24, RenderTextureFormat.Default);
			rt.useMipMap = false;
			rt.antiAliasing = 1;
			RenderTexture.active = rt;
			GetComponent<Camera> ().targetTexture = rt;	
		
		
			GetComponent<Camera> ().Render ();
			shotSqr.ReadPixels (new Rect (0, 0, Screen.width, Screen.width), 0, 0, false);
		
		
		
		
			GetComponent<Camera> ().targetTexture = null;
			rt = null;
			GetComponent<Camera> ().enabled = false;
	}
		}

	}

	IEnumerator shareScreen(){
		yield return new WaitForEndOfFrame ();
		shotSqr.Apply ();

		guiPoloroid.texture = shotSqr;
//		RenderTexture.active = shotSqr;
//		Texture2D tempTex = new Texture2D (shotSqr.width, shotSqr.height);
//		tempTex.ReadPixels (new Rect (0, 0, shotSqr.width, shotSqr.height), 0, 0);
//		tempTex.Apply ();
		yield return new WaitForSeconds (0.5f);


		
			
				
		if (PlayerPrefs.GetInt ("selectedLevel") == -1) {
			
			UM_ShareUtility.ShareMedia ("Buddy Toss","Buddy Toss - "+SmartLocalization_Nate.niceTossTrans+ " " +  SmartLocalization_Nate.bestScoreWindTrans + " "  + PlayerPrefs.GetInt ("fieldgoalhs") + " #buddytoss", shotSqr);
		}		

		if (PlayerPrefs.GetInt ("selectedLevel") == 0) {

			UM_ShareUtility.ShareMedia ("Buddy Toss","Buddy Toss - "+SmartLocalization_Nate.niceTossTrans+ " " +  SmartLocalization_Nate.bestScoreWindTrans + " "  + PlayerPrefs.GetInt ("highscore") + " #buddytoss", shotSqr);
		}
		if (PlayerPrefs.GetInt ("selectedLevel") == 1) {
			
			UM_ShareUtility.ShareMedia ("Buddy Toss","Buddy Toss - "+SmartLocalization_Nate.niceTossTrans+ " " + SmartLocalization_Nate.bestScoreTNTTrans + " "   + PlayerPrefs.GetInt ("highscoreTNT") + " #buddytoss", shotSqr);
		}
		if (PlayerPrefs.GetInt ("selectedLevel") == 2) {
			string highScoreTimeString = string.Format("{00:00}:{1:00}:{2:00}",PlayerPrefs.GetFloat("hiMin"),PlayerPrefs.GetFloat("hiSec"),PlayerPrefs.GetFloat("hiFraction"));

			UM_ShareUtility.ShareMedia ("Buddy Toss","Buddy Toss - "+SmartLocalization_Nate.niceTossTrans+ " " +   SmartLocalization_Nate.bestTimePongTrans + " " + highScoreTimeString + " #buddytoss", shotSqr);
		}
	}
	IEnumerator shareScreenCharacterFreeGift(){
		yield return new WaitForEndOfFrame();
		Texture2D shotFree = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
		
		shotFree.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0, false);
		
		
		
		UM_ShareUtility.ShareMedia("Buddy Toss",SmartLocalization_Nate.justUnlockedTrans+" "+freeObjName.text  +" #buddytoss", shotFree);
	}





}
