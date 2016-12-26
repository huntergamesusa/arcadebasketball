using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
//using UnionAssets.FLE;

#if UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
using UnityEngine;
#else
using UnityEngine.iOS;
#endif
public class MissionsController : MonoBehaviour {
	public DateTime unbiasedTimerEndTimestampMissions;
	public GameObject MissionsMain;
	public AudioClip bullseyePointSound;
	public RectTransform[] missionSlider1;
	public RectTransform[] missionSlider2;
	public RectTransform[] missionSlider3;

	public Text[] missionDivision1;
	public Text[] missionDivision2;
	public Text[] missionDivision3;

	public Text [] mission1TXT;
	public Text [] mission2TXT;
	public Text [] mission3TXT;

	public string[] missions20;
	public string[] missions50;
	public string [] missions100;
	public int []currentMissions;
	public GameObject CreditsCamera;
	int notificationID; 
	Text credits;
	bool pendingBonus;
	public Text missionText;
	int note;
	public Image missionsImage;
	public Sprite missionsUp;
	public Sprite missionsDn;
	void OnApplicationPause (bool paused) {
		if (paused) {
	

		} else {
			missionsCheck();
		}
		}

	public void notificationState(){
		if (PlayerPrefs.GetInt ("missionsNotification") == 0) {
			missionText.text = "OFF";
			missionsImage.sprite = missionsDn;
		} else {
			UM_NotificationController.Instance.CancelLocalNotification(notificationID);
			missionText.text = "ON";
			missionsImage.sprite = missionsUp;

		}
		if (PlayerPrefs.GetInt ("missionsNotification") == 1) {
			PlayerPrefs.SetInt ("missionsNotification",0);

		} else {
			PlayerPrefs.SetInt ("missionsNotification",1);

		}

		if(PlayerPrefs.GetInt ("missionsNotification")<1){
			UM_NotificationController.Instance.CancelLocalNotification(notificationID);
			UM_NotificationController.OnPushIdLoadResult += OnPushIdLoaded;
			UM_NotificationController.Instance.RetrieveDevicePushId();
			ISN_LocalNotification notification =  new ISN_LocalNotification(DateTime.Now.AddDays(1),"New Daily Missions Available - Play now and earn some new characters!", true);
			notification.SetData("some_test_data");
			notificationID = notification.Id;
			notification.Schedule();
		}

	}

	public void missionsCheck(){
		if (PlayerPrefs.GetInt ("missionsNotification") < 1) {
			missionText.text = "ON";
			missionsImage.sprite = missionsUp;

		} else {
			missionText.text = "OFF";
			missionsImage.sprite = missionsDn;

		}


		unbiasedTimerEndTimestampMissions = this.ReadTimestamp("missionsTimer", UnbiasedTime.Instance.Now());
		
		TimeSpan unbiasedRemaining = unbiasedTimerEndTimestampMissions - UnbiasedTime.Instance.Now();

		if (unbiasedRemaining.TotalSeconds <= 0) {
			unbiasedTimerEndTimestampMissions = UnbiasedTime.Instance.Now ().Date.AddDays (1);
//			unbiasedTimerEndTimestampMissions = UnbiasedTime.Instance.Now ().AddMinutes (1);

			this.WriteTimestamp ("missionsTimer", unbiasedTimerEndTimestampMissions);
			setMissions ();
			tempReset ();
			
		}
		//need to check completeness of non sliders here
		if (PlayerPrefs.GetInt ("CurrentMission1") == 5 || PlayerPrefs.GetInt ("CurrentMission1") == 7 || PlayerPrefs.GetInt ("CurrentMission1") == 8) {
			updateSliderSmall (PlayerPrefs.GetInt ("CurrentMission1"));
		} else if (PlayerPrefs.GetInt ("Missions1") > 0) {
			updateSliderSmall (-2);
		} else {
			updateSliderSmall(PlayerPrefs.GetInt ("CurrentMission1"));
		}
		if (PlayerPrefs.GetInt ("CurrentMission2") >= 5 && PlayerPrefs.GetInt ("CurrentMission2") <= 7) {
			updateSliderMedium (PlayerPrefs.GetInt ("CurrentMission2"));
		} else if (PlayerPrefs.GetInt ("Missions2") > 0) {
			updateSliderMedium (-2);

		} else {
			updateSliderMedium(PlayerPrefs.GetInt ("CurrentMission2"));
		}
	

		if(PlayerPrefs.GetInt ("CurrentMission3")>=2&&PlayerPrefs.GetInt ("CurrentMission3")<=4){
			updateSliderLarge (PlayerPrefs.GetInt ("CurrentMission3"));
		}
		else if (PlayerPrefs.GetInt("Missions3")>0){
			updateSliderLarge(-2);
		}
		else {
			updateSliderLarge(PlayerPrefs.GetInt ("CurrentMission3"));
		}
		mission1TXT[0].text = missions20 [PlayerPrefs.GetInt ("CurrentMission1")];
		mission2TXT[0].text = missions50 [PlayerPrefs.GetInt ("CurrentMission2")];
		mission3TXT[0].text = missions100 [PlayerPrefs.GetInt ("CurrentMission3")];
		mission1TXT[1].text = missions20 [PlayerPrefs.GetInt ("CurrentMission1")];
		mission2TXT[1].text = missions50 [PlayerPrefs.GetInt ("CurrentMission2")];
		mission3TXT[1].text = missions100 [PlayerPrefs.GetInt ("CurrentMission3")];
	}
	
	public void Awake(){
		UM_NotificationController.OnPushIdLoadResult += OnPushIdLoaded;

		missionsCheck ();

		if (PlayerPrefs.HasKey ("missionsNotification") == false) {
			PlayerPrefs.SetInt ("missionsNotification",0);
			if (PlayerPrefs.GetInt ("missionsNotification") < 1) {
				UM_NotificationController.Instance.CancelLocalNotification (notificationID);
				ISN_LocalNotification notification = new ISN_LocalNotification (DateTime.Now.AddDays (1), "New Daily Missions Available - Play now and earn some new characters!", true);
				notification.SetData ("some_test_data");
				notificationID = notification.Id;
				notification.Schedule ();
			}
		}
	}

	public void tempReset(){
		if (PlayerPrefs.HasKey ("showmissionsfirst")) {
			MissionsMain.SetActive(true);
		}
			else{
				PlayerPrefs.SetInt ("showmissionsfirst", 1);

			}
		PlayerPrefs.SetInt("Missions1",0);
		PlayerPrefs.SetInt("Missions2",0);
		PlayerPrefs.SetInt("Missions3",0);
		PlayerPrefs.SetInt("numFGs",0);
		PlayerPrefs.SetInt("numBullseyes",0);
		PlayerPrefs.SetInt("localpoints",0);
		PlayerPrefs.SetInt("missionNew",0);
		pendingBonus = false;

	}

	public void setMissions(){
		PlayerPrefs.SetInt ("CurrentMission1",UnityEngine.Random.Range (0, missions20.Length));
		PlayerPrefs.SetInt ("CurrentMission2",UnityEngine.Random.Range (0, missions50.Length));
		PlayerPrefs.SetInt ("CurrentMission3",UnityEngine.Random.Range (0, missions100.Length));

//		PlayerPrefs.SetInt ("CurrentMission1", currentMissions [0]);
//		PlayerPrefs.SetInt ("CurrentMission2", PlayerPrefs.GetInt ("CurrentMission2"));
//		PlayerPrefs.SetInt ("CurrentMission3", PlayerPrefs.GetInt ("CurrentMission3"));

		


	}

	private DateTime ReadTimestamp (string key, DateTime defaultValue) {
		long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
		if ( tmp == 0 ) {
			return defaultValue;
		}
		return DateTime.FromBinary(tmp);
	}
	
	private void WriteTimestamp (string key, DateTime time) {
		PlayerPrefs.SetString(key, time.ToBinary().ToString());
	}

	public void sendMissionDataSmall(int mission){
		if(mission ==PlayerPrefs.GetInt ("CurrentMission1")){
			if(PlayerPrefs.GetInt("Missions1")==0){
				PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits") + 20);
				//not for unlock item
				if(mission!=4){
				pendingBonus=true;
				}
			}
			PlayerPrefs.SetInt("Missions1",1);
			if(PlayerPrefs.GetInt ("CurrentMission1")!=5||PlayerPrefs.GetInt ("CurrentMission1")!=7||PlayerPrefs.GetInt ("CurrentMission1")!=8){
		
				updateSliderSmall (-2);
			}
		

		}
	
	}

	public void sendMissionDataMedium(int mission){
		if(mission ==PlayerPrefs.GetInt ("CurrentMission2")){
			if(PlayerPrefs.GetInt("Missions2")==0){
				PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits") + 50);
				pendingBonus =true;
			}
			PlayerPrefs.SetInt("Missions2",1);
			if(PlayerPrefs.GetInt ("CurrentMission2")>=5&&PlayerPrefs.GetInt ("CurrentMission2")<=7){
			}
			else{
		
			updateSliderMedium (-2);
			}
		

		}
		
	}
	public void sendMissionDataLarge(int mission){
		if(mission ==PlayerPrefs.GetInt ("CurrentMission3")){
			if(PlayerPrefs.GetInt("Missions3")==0){
				PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits") + 100);
				pendingBonus =true;
			}
			PlayerPrefs.SetInt("Missions3",1);
			if(PlayerPrefs.GetInt ("CurrentMission3")>=2&&PlayerPrefs.GetInt ("CurrentMission3")<=4){
			}
			else{
	
			updateSliderLarge (-2);
			}

		}
		
	}

	public void receivePostHit(){
		sendMissionDataSmall(3);
	}

	public void receiveBullseye(int bullseyes){
		if (PlayerPrefs.GetInt ("selectedLevel") == 0) {

			if (bullseyes == 1) {
				sendMissionDataSmall(0);
			}
			if (bullseyes == 2) {
				sendMissionDataMedium(0);
			}

			PlayerPrefs.SetInt("numBullseyes",PlayerPrefs.GetInt("numBullseyes")+1);
		
			if(PlayerPrefs.GetInt ("CurrentMission1") == 8){
				updateSliderSmall (PlayerPrefs.GetInt ("CurrentMission1"));
			}
			if(PlayerPrefs.GetInt ("CurrentMission2") == 7){
				updateSliderMedium (PlayerPrefs.GetInt ("CurrentMission2"));
			}
			if(PlayerPrefs.GetInt ("CurrentMission3") == 4){
				updateSliderLarge (PlayerPrefs.GetInt ("CurrentMission3"));
			}

			if(PlayerPrefs.GetInt("numBullseyes") >=5){
				sendMissionDataSmall(8);
			}
			if(PlayerPrefs.GetInt("numBullseyes") >=10){
				sendMissionDataMedium(7);
			}
			if(PlayerPrefs.GetInt("numBullseyes") >=25){
				sendMissionDataLarge(4);
			}
		}
		if (PlayerPrefs.GetInt ("selectedLevel") == -1) {
			if (bullseyes == 1) {
				sendMissionDataSmall(6);
			}
			if (bullseyes == 3) {
				sendMissionDataMedium(1);
			}
			PlayerPrefs.SetInt("numFGs",PlayerPrefs.GetInt("numFGs")+1);

			if(PlayerPrefs.GetInt ("CurrentMission1") == 7){
				updateSliderSmall (PlayerPrefs.GetInt ("CurrentMission1"));
			}
			if(PlayerPrefs.GetInt ("CurrentMission2") == 6){
				updateSliderMedium (PlayerPrefs.GetInt ("CurrentMission2"));
			}
			if(PlayerPrefs.GetInt ("CurrentMission3") == 3){
				updateSliderLarge (PlayerPrefs.GetInt ("CurrentMission3"));
			}

			if(PlayerPrefs.GetInt("numFGs") >=5){
				sendMissionDataSmall(7);
			}
			if(PlayerPrefs.GetInt("numFGs") >=10){
				sendMissionDataMedium(6);

			}
			if(PlayerPrefs.GetInt("numFGs") >=25){
				sendMissionDataLarge(3);
			}

		}

	}


	public void HitBird(){
		sendMissionDataMedium(4);
	}

	public void unlockedFreeGift(){
		sendMissionDataSmall(4);
		unlockShowCoins ();
	}

	public void PongMode(int min){
		if (min <= 2) {
			sendMissionDataLarge(6);
			pendingBonus = true;
			playAnimationCredits();
		}
		if (min <= 3) {
			sendMissionDataMedium(9);
			pendingBonus = true;
			playAnimationCredits();

		}
		if (min <= 4) {
			sendMissionDataSmall(9);
			playAnimationCredits();

		}
	}
	public void receiveTNTTwelve(){
		sendMissionDataMedium(10);
	}
	public void receiveTNTSix(){
		sendMissionDataSmall(10);
	}

	public void hitTNT(){
		sendMissionDataSmall(12);
	}

	public void receiveEndScore(int mypoints){
		if (PlayerPrefs.GetInt ("selectedLevel") != 2) {
			PlayerPrefs.SetInt("localpoints",PlayerPrefs.GetInt("localpoints")+mypoints);

			if(PlayerPrefs.GetInt ("CurrentMission1") == 5){
				updateSliderSmall (PlayerPrefs.GetInt ("CurrentMission1"));
			}
			if(PlayerPrefs.GetInt ("CurrentMission2") == 5){
				updateSliderMedium (PlayerPrefs.GetInt ("CurrentMission2"));
			}
			if(PlayerPrefs.GetInt ("CurrentMission3") == 2){
				updateSliderLarge (PlayerPrefs.GetInt ("CurrentMission3"));
			}
		}

		if (PlayerPrefs.GetInt("localpoints")>=100) {
			sendMissionDataSmall(5);
		}
		if (PlayerPrefs.GetInt("localpoints") >= 250) {
			sendMissionDataMedium(5);
		}
		if (PlayerPrefs.GetInt("localpoints") >= 500) {
			sendMissionDataLarge(2);
		}

		if (mypoints >= 25) {
			if (PlayerPrefs.GetInt ("selectedLevel") == -1) {
				sendMissionDataSmall(2);
			}
			if (PlayerPrefs.GetInt ("selectedLevel") == 0) {
				sendMissionDataSmall(1);
				
			}
			if (PlayerPrefs.GetInt ("selectedLevel") == 1) {
				sendMissionDataSmall(11);
			}
		}
		if (mypoints >= 50) {
	
				if (PlayerPrefs.GetInt ("selectedLevel") == -1) {
					sendMissionDataMedium(3);
				}
				if (PlayerPrefs.GetInt ("selectedLevel") == 0) {
					sendMissionDataMedium(2);
					
				}
				if (PlayerPrefs.GetInt ("selectedLevel") == 1) {
					sendMissionDataMedium(8);
				}	
			}
			if (mypoints >= 100) {
				if (PlayerPrefs.GetInt ("selectedLevel") == -1) {
					sendMissionDataLarge(1);
				}
				if (PlayerPrefs.GetInt ("selectedLevel") == 0) {
					sendMissionDataLarge(0);
					
				}
				if (PlayerPrefs.GetInt ("selectedLevel") == 1) {
					sendMissionDataLarge(5);
				}	

			}

	}
	public void updateSliderSmall(int missions){
		switch (missions) {
		case(-2):
			standardComplete(0);
			break;
		case(-1):
			standardDivision(0);
			break;
		case(0):
			standardDivision(0);
			break;	
		case(1):
			standardDivision(0);
			break;
		case(2):
			standardDivision(0);
			break;
		case(3):
			standardDivision(0);
			break;
		case(4):
			standardDivision(0);
			break;
		case(5):
			if( PlayerPrefs.GetInt("localpoints")<=100){
				missionDivision1 [0].text = PlayerPrefs.GetInt("localpoints") + "/100";
				missionDivision1 [1].text = PlayerPrefs.GetInt("localpoints") + "/100";
				missionSlider1[0].localScale = new Vector3((float)PlayerPrefs.GetInt("localpoints")/100,1,1);
				missionSlider1[1].localScale = new Vector3((float)PlayerPrefs.GetInt("localpoints")/100,1,1);
			}
			else{
				missionDivision1 [0].text =  "100/100";
				missionDivision1 [1].text =  "100/100";
				missionSlider1[0].localScale = new Vector3(1,1,1);
				missionSlider1[1].localScale = new Vector3(1,1,1);
			}
			break;
		case(6):
			standardDivision(0);
			break;
		case(7):
			if( PlayerPrefs.GetInt("numFGs")<=5){
				missionDivision1 [0].text = PlayerPrefs.GetInt("numFGs") + "/5";
				missionDivision1 [1].text = PlayerPrefs.GetInt("numFGs") + "/5";
				missionSlider1[0].localScale = new Vector3((float)PlayerPrefs.GetInt("numFGs")/5,1,1);
				missionSlider1[1].localScale = new Vector3((float)PlayerPrefs.GetInt("numFGs")/5,1,1);
			}
			else{
				missionDivision1 [0].text =  "5/5";
				missionDivision1 [1].text =  "5/5";
				missionSlider1[0].localScale = new Vector3(1,1,1);
				missionSlider1[1].localScale = new Vector3(1,1,1);
			}
			break;
		case(8):
			if( PlayerPrefs.GetInt("numBullseyes")<=5){
				missionDivision1 [0].text = PlayerPrefs.GetInt("numBullseyes") + "/5";
				missionDivision1 [1].text = PlayerPrefs.GetInt("numBullseyes") + "/5";
				missionSlider1[0].localScale = new Vector3((float)PlayerPrefs.GetInt("numBullseyes")/5,1,1);
				missionSlider1[1].localScale = new Vector3((float)PlayerPrefs.GetInt("numBullseyes")/5,1,1);
			}
			else{
				missionDivision1 [0].text =  "5/5";
				missionDivision1 [1].text =  "5/5";
				missionSlider1[0].localScale = new Vector3(1,1,1);
				missionSlider1[1].localScale = new Vector3(1,1,1);
			}
			break;
		case(9):
			standardDivision(0);
			break;
		case(10):
			standardDivision(0);
			break;
		case(11):
			standardDivision(0);
			break;
		case(12):

			standardDivision(0);
			break;
		}
	}

	public void updateSliderMedium(int missions){
		switch (missions) {
		case(-2):
			standardComplete(1);

			break;
		case(-1):
			standardDivision(1);

			break;
		case(0):
			standardDivision(1);
			break;
		case(1):
			standardDivision(1);
			break;
		case(2):
			standardDivision(1);
			break;
		case(3):
			standardDivision(1);
			break;
		case(4):
			standardDivision(1);
			break;
		case(5):
			if( PlayerPrefs.GetInt("localpoints")<=250){
				missionDivision2 [0].text = PlayerPrefs.GetInt("localpoints") + "/250";
				missionDivision2 [1].text = PlayerPrefs.GetInt("localpoints") + "/250";
				missionSlider2[0].localScale = new Vector3((float)PlayerPrefs.GetInt("localpoints")/250,1,1);
				missionSlider2[1].localScale = new Vector3((float)PlayerPrefs.GetInt("localpoints")/250,1,1);
			}
			else{
				missionDivision2 [0].text = "250/250";
				missionDivision2 [1].text = "250/250";
				missionSlider2[0].localScale = new Vector3(1,1,1);
				missionSlider2[1].localScale = new Vector3(1,1,1);
			}
			break;
		case(6):
			if( PlayerPrefs.GetInt("numFGs")<=10){
				missionDivision2 [0].text = PlayerPrefs.GetInt("numFGs") + "/10";
				missionDivision2 [1].text = PlayerPrefs.GetInt("numFGs") + "/10";
				missionSlider2[0].localScale = new Vector3((float)PlayerPrefs.GetInt("numFGs")/10,1,1);
				missionSlider2[1].localScale = new Vector3((float)PlayerPrefs.GetInt("numFGs")/10,1,1);
			}
			else{
				missionDivision2 [0].text =  "10/10";
				missionDivision2 [1].text ="10/10";
				missionSlider2[0].localScale = new Vector3(1,1,1);
				missionSlider2[1].localScale =  new Vector3(1,1,1);
			}
			break;
		case(7):
			if( PlayerPrefs.GetInt("numBullseyes")<=10){
				missionDivision2 [0].text = PlayerPrefs.GetInt("numBullseyes") + "/10";
				missionDivision2 [1].text = PlayerPrefs.GetInt("numBullseyes") + "/10";
				missionSlider2[0].localScale = new Vector3((float)PlayerPrefs.GetInt("numBullseyes")/10,1,1);
				missionSlider2[1].localScale = new Vector3((float)PlayerPrefs.GetInt("numBullseyes")/10,1,1);
			}
			else{
				missionDivision2 [0].text =  "10/10";
				missionDivision2 [1].text ="10/10";
				missionSlider2[0].localScale = new Vector3(1,1,1);
				missionSlider2[1].localScale =  new Vector3(1,1,1);
			}
			break;
		case(8):
			standardDivision(1);
			break;
		case(9):
			standardDivision(1);
			break;
		case(10):
			standardDivision(1);
			break;
		case(11):
			standardDivision(1);
			break;
		case(12):
			standardDivision(1);
			break;
		}
	}

	public void standardDivision(int missionSize){
		if (missionSize == 0) {
			missionDivision1 [0].text = "0/1";
			missionDivision1 [1].text = "0/1";	
			missionSlider1 [0].localScale = new Vector3 (0, 1, 1);
			missionSlider1 [1].localScale = new Vector3 (0, 1, 1);
		}
		if (missionSize == 1) {
			missionDivision2 [0].text = "0/1";
			missionDivision2 [1].text = "0/1";	
			missionSlider2 [0].localScale = new Vector3 (0, 1, 1);
			missionSlider2 [1].localScale = new Vector3 (0, 1, 1);
		}
		if (missionSize == 2) {
			missionDivision3 [0].text = "0/1";
			missionDivision3 [1].text = "0/1";	
			missionSlider3 [0].localScale = new Vector3 (0, 1, 1);
			missionSlider3 [1].localScale = new Vector3 (0, 1, 1);
		}
	}
	public void standardComplete(int missionSize){
		if (missionSize == 0) {

			missionDivision1 [0].text = "1/1";
			missionDivision1 [1].text = "1/1";		
			missionSlider1 [0].localScale = new Vector3 (1, 1, 1);
			missionSlider1 [1].localScale = new Vector3 (1, 1, 1);
		}
		if (missionSize == 1) {
			missionDivision2 [0].text = "1/1";
			missionDivision2 [1].text = "1/1";		
			missionSlider2 [0].localScale = new Vector3 (1, 1, 1);
			missionSlider2 [1].localScale = new Vector3 (1, 1, 1);
		}
		if (missionSize == 2) {
			missionDivision3 [0].text = "1/1";
			missionDivision3 [1].text = "1/1";		
			missionSlider3 [0].localScale = new Vector3 (1, 1, 1);
			missionSlider3 [1].localScale = new Vector3 (1, 1, 1);
		}
	}

	public void updateSliderLarge(int missions){

		switch (missions) {


		case(-1):
			standardDivision(2);
			print ("setting slider to zero1");
			break;
			
		case(-2):
			standardComplete(2);
			print ("setting slider to zero");
			break;
		case(0):
			standardDivision(2);
			break;
		case(1):
			standardDivision(2);
			break;
		case(2):
			if( PlayerPrefs.GetInt("localpoints")<=500){
				missionDivision3 [0].text = PlayerPrefs.GetInt("localpoints") + "/500";
				missionDivision3 [1].text = PlayerPrefs.GetInt("localpoints") + "/500";
				missionSlider3[0].transform.localScale = new Vector3((float)PlayerPrefs.GetInt("localpoints")/500,1,1);
				missionSlider3[1].transform.localScale = new Vector3((float)PlayerPrefs.GetInt("localpoints")/500,1,1);
			}
			else{
				missionDivision3 [0].text = "500/500";
				missionDivision3 [1].text = "500/500";
				missionSlider3[0].transform.localScale = new Vector3(1,1,1);
				missionSlider3[1].transform.localScale = new Vector3(1,1,1);
			}
			break;
		case(3):
			if( PlayerPrefs.GetInt("numFGs")<=25){
				missionDivision3 [0].text = PlayerPrefs.GetInt("numFGs") + "/25";
				missionDivision3 [1].text = PlayerPrefs.GetInt("numFGs") + "/25";
				missionSlider3[0].transform.localScale = new Vector3((float)PlayerPrefs.GetInt("numFGs")/25,1,1);
				missionSlider3[1].transform.localScale = new Vector3((float)PlayerPrefs.GetInt("numFGs")/25,1,1);
			}
			else{
				missionDivision3 [0].text = "25/25";
				missionDivision3 [1].text =  "25/25";
				missionSlider3[0].transform.localScale = new Vector3(1,1,1);
				missionSlider3[1].transform.localScale = new Vector3(1,1,1);
			}
			break;
		case(4):
			if( PlayerPrefs.GetInt("numBullseyes")<=25){
				missionDivision3 [0].text = PlayerPrefs.GetInt("numBullseyes") + "/25";
				missionDivision3 [1].text =  PlayerPrefs.GetInt("numBullseyes") + "/25";
				missionSlider3[0].transform.localScale = new Vector3((float) PlayerPrefs.GetInt("numBullseyes")/25,1,1);
				missionSlider3[1].transform.localScale = new Vector3( (float)PlayerPrefs.GetInt("numBullseyes")/25,1,1);
			}
			else{
				missionDivision3 [0].text = "25/25";
				missionDivision3 [1].text =  "25/25";
				missionSlider3[0].transform.localScale = new Vector3(1,1,1);
				missionSlider3[1].transform.localScale = new Vector3(1,1,1);
			}
			break;
		case(5):
			standardDivision(2);
			break;
		case(6):
			standardDivision(2);
			break;
		case(7):
			standardDivision(2);
			break;
		case(8):
			standardDivision(2);
			break;
		case(9):
			standardDivision(2);
			break;
		case(10):
			standardDivision(2);
			break;
		case(11):
			standardDivision(2);
			break;
		case(12):
			standardDivision(2);
			break;
		}

	}

	public IEnumerator playAnimationCredits(){
	
			yield return new WaitForSeconds (1.5f);
		if (pendingBonus) {
			pendingBonus=false;
			CreditsCamera.SetActive (true);
			CreditsCamera.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();

			GetComponent<AudioSource> ().Play ();
		
			credits = GameObject.Find ("Credits UI").GetComponent<Text> ();
			credits.text = PlayerPrefs.GetInt ("myCredits").ToString ("f0");
			FireWork ();
			yield return new WaitForSeconds (1);
			CreditsCamera.SetActive (false);
		
		}
		
		
	}

	public IEnumerator unlockShowCoins(){
	
	
			CreditsCamera.SetActive (true);
			CreditsCamera.transform.GetChild (0).GetComponent<ParticleSystem> ().Play ();
			
			GetComponent<AudioSource> ().Play ();
			
			credits = GameObject.Find ("Credits UI").GetComponent<Text> ();
			credits.text = PlayerPrefs.GetInt ("myCredits").ToString ("f0");
		FireWork ();
			yield return new WaitForSeconds (1);
			CreditsCamera.SetActive (false);

	}
	void FireWork(){
	GameObject toonWork = GameObject.Find("ToonFireWork");
		GameObject myCam = GameObject.Find("PointCamera");
	myCam.GetComponent<Camera>().enabled = true;
		Text pointEffectTXT = GameObject.Find("PointsEffectTXT").GetComponent<Text>();
	//yield WaitForSeconds(.1f);
	pointEffectTXT.gameObject.GetComponent<AudioSource>().PlayOneShot(bullseyePointSound);
	
	
	toonWork.GetComponent<ParticleSystem>().Play();
//	for(var i : int = 0; i <toonWork.transform.childCount ; i++)
//	{
//		toonWork.transform.GetChild(i).GetComponent.<ParticleSystem>().Play();
//	}
		for (int i = 0; i < toonWork.transform.childCount; i++) {
			toonWork.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
		}
		pointEffectTXT.text = "MISSION COMPLETE";


	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(1f,1f,1f), .7f).setEase(LeanTweenType.easeOutElastic);
	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(0f,0f,0f), .2f).setEase(LeanTweenType.easeOutExpo).setDelay(.7F).setOnComplete(offCamera);
	}

	void offCamera(){
		GameObject myCam = GameObject.Find("PointCamera");
		myCam.GetComponent<Camera>().enabled = false;

	}

	private void OnPushIdLoaded (UM_PushRegistrationResult res) {
		UM_NotificationController.OnPushIdLoadResult -= OnPushIdLoaded;
		
		if(res.IsSucceeded) {
//			Debug.Log("Succeeded", "Device Id: " + res.deviceId);
		} else {
//			Debug.Log("Failed", "No device id");
		}
		
		//		IOSMessage.Create("On Notification Schedule Result", msg);
	}

}
