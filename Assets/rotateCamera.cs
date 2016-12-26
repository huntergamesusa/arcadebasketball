using UnityEngine;
using System.Collections;

public class rotateCamera : MonoBehaviour {
	public GameObject thisCamera;
	public Transform startCamPos;
	public Transform characterCamPos;
	public Transform characterSpawnPos;
	 Vector3 initialCamPos;
	 Vector3 initialCamRot;
	bool stopRotate;
//	public basketballThrow_spring script;
	// Use this for initialization

	public void stopRotateandPosCam(){
		stopRotate=true;

		iTween.MoveTo(thisCamera,iTween.Hash("position",startCamPos.position,"easetype",iTween.EaseType.easeInOutSine,"time",1f,"onComplete","enableControls","OnCompleteTarget",gameObject));
		iTween.RotateTo(thisCamera,iTween.Hash("rotation",startCamPos.eulerAngles,"easetype",iTween.EaseType.easeInOutSine,"time",1f));
	}

	public void stopRotateandPosCam_CharacterSelect(){
		stopRotate=true;

		iTween.MoveTo(thisCamera,iTween.Hash("position",characterCamPos.position,"easetype",iTween.EaseType.easeInOutSine,"time",1f,"OnCompleteTarget",gameObject,"oncomplete", "animateSideCharacters"));
		iTween.RotateTo(thisCamera,iTween.Hash("rotation",characterCamPos.eulerAngles,"easetype",iTween.EaseType.easeInOutSine,"time",1f));
	}

	public void completeCharacterSelect(){
		stopRotate=false;
		iTween.MoveTo(thisCamera,iTween.Hash("position",initialCamPos,"easetype",iTween.EaseType.easeInOutSine,"time",1f,"OnCompleteTarget",gameObject));
		iTween.RotateTo(thisCamera,iTween.Hash("rotation",initialCamRot,"easetype",iTween.EaseType.easeInOutSine,"time",1f));
	}

	void Start () {
		initialCamPos = thisCamera.transform.position;
		initialCamRot = thisCamera.transform.eulerAngles;
//		iTween.MoveTo(gameObject,iTween.Hash("position",startCamPos.position,"easetype",iTween.EaseType.easeInOutSine,"time",1f));
//		iTween.RotateTo(gameObject,iTween.Hash("rotation",startCamPos.eulerAngles,"easetype",iTween.EaseType.easeInOutSine,"time",1f));
	}

	 void enableControls(){
//		((MonoBehaviour)points.GetComponent("points2")).enabled=true;
		print ("enabledControls");
		((MonoBehaviour)thisCamera.GetComponent("basketballThrow_TouchY")).enabled=true;
		//enables correct type of power
		thisCamera.SendMessage ("adjustthePower");
		//animates leveltnt text on start

//		var pointsColl = GameObject.Find("PointsCollect");
//		pointsColl.SendMessage("animateTNTLevelTxt");
		

	}
	
	// Update is called once per frame
	void Update () {
		if(!stopRotate){
			thisCamera.transform.RotateAround(characterSpawnPos.position, Vector3.up, 20 * Time.deltaTime);
		}
	}

	public void mainMenuCameraReset(){

		thisCamera.transform.position = initialCamPos;
		thisCamera.transform.eulerAngles = initialCamRot;
		stopRotate=false;
	}

	public void animateSideCharacters(){
		print ("animatingpreviewguys");
		GameObject mySideCharacterScript = GameObject.Find("updatePreviewBuddy");
		mySideCharacterScript.SendMessage("menuStatus", true);

	}
}
