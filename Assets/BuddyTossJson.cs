using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using LitJson;

public class BuddyTossJson : MonoBehaviour {

	private string jsonString;
	private JsonData itemData;
	public GameObject VideoAds;
	public GameObject InterstitialAds;
//	public GameObject Snow;

	public GameObject mainMessage;
	public Text TextHeader;
	public Text TextBody;
	private int isMessageEnabled;

	// Use this for initialization
	public  IEnumerator Start () {

		Caching.CleanCache ();

//		WWW zzz = new WWW ("http://www.buddytoss.com/JSONBool.php");
//		yield return zzz;
//
//		if (zzz.error == null) {
//
//			if(zzz.text == "off"){
//				PlayerPrefs.SetString("PrimaryAd", "vungle");
//				PlayerPrefs.SetInt("myAdFrequency", 4);
//				PlayerPrefs.SetInt("buyGuyMin", 5);
//				PlayerPrefs.SetInt("buyGuyMax", 7);
////				PlayerPrefs.SetInt("isSnowing", 0);
//				PlayerPrefs.SetInt("NetworkScoreMult", 1);
//				
//				VideoAds.SendMessage("EnableAdCompany");
////				Snow.SendMessage("enableSnow");
//				mainMessage.SetActive(false);
//		
//			print ("json is off");
//				yield  break;
//			}
//
//
//		} else {
//			PlayerPrefs.SetString("PrimaryAd", "vungle");
//			PlayerPrefs.SetInt("myAdFrequency", 4);
//			PlayerPrefs.SetInt("buyGuyMin", 5);
//			PlayerPrefs.SetInt("buyGuyMax", 7);
////			PlayerPrefs.SetInt("isSnowing", 0);
//			PlayerPrefs.SetInt("NetworkScoreMult", 1);
//			
//			VideoAds.SendMessage("EnableAdCompany");
////			Snow.SendMessage("enableSnow");
//			mainMessage.SetActive(false);
//			
//			Debug.Log("ERROR: " + zzz.error);
//
//		}

#if UNITY_ANDROID
		jsonString = "http://www.buddytoss.com/BuddyTossSettingsAndroid.json";
#endif
#if UNITY_IOS
		jsonString = "http://www.buddytoss.com/BuddyTossSettingsAug.json";

#endif

		WWW www = new WWW (jsonString);
		yield return www;
//		Debug.Log ( www.data);

		if (www.error == null) {

			itemData = JsonMapper.ToObject(www.text);
//			print (itemData["BuddyToss"][0]["adnetwork"]);
			print ("json is on");

//			;
//			print (tempAdString);

				PlayerPrefs.SetString("PrimaryAd", itemData["BuddyToss"][0]["adnetwork"].ToString());
		

			int myFrequencyInt;

			int.TryParse (itemData["BuddyToss"][0]["adfrequency"].ToString(),out myFrequencyInt);

			PlayerPrefs.SetInt("myAdFrequency", myFrequencyInt);

			int myFrequencyBGMin;
			
			int.TryParse (itemData["BuddyToss"][0]["buyGuyFrequencyMin"].ToString(),out myFrequencyBGMin);
			
			PlayerPrefs.SetInt("buyGuyMin", myFrequencyBGMin);

			int myFrequencyBGMax;
			
			int.TryParse (itemData["BuddyToss"][0]["buyGuyFrequencyMax"].ToString(),out myFrequencyBGMax);
			
			PlayerPrefs.SetInt("buyGuyMax", myFrequencyBGMax);

			int scoreMultiplier;

			int.TryParse (itemData["BuddyToss"][0]["scoreMult"].ToString(),out scoreMultiplier);


			PlayerPrefs.SetInt("NetworkScoreMult", scoreMultiplier);

			int outTopChar;

			int.TryParse (itemData["BuddyToss"][0]["topChar"].ToString(),out outTopChar);
			PlayerPrefs.SetInt("topChar", outTopChar);

			int outRanChar;
			int.TryParse (itemData["BuddyToss"][0]["ranChar"].ToString(),out outRanChar);
			PlayerPrefs.SetInt("ranChar", outRanChar);

			int outCharNum;
			int.TryParse (itemData["BuddyToss"][0]["characters"].ToString(),out outCharNum);
			PlayerPrefs.SetInt("characterbuyguy", outCharNum);

			int chartPop;
			int.TryParse (itemData["BuddyToss"][0]["crossPop"].ToString(),out chartPop);
			PlayerPrefs.SetInt("chartPop", chartPop);
			
			Ads_Nate.ShowCross();

		
			if(PlayerPrefs.GetInt("StartMsg") >=1){

			int isMsg;
			int.TryParse (itemData["BuddyToss"][0]["showMsg"].ToString(),out isMsg);



			if(isMsg>0){
				if(PlayerPrefs.GetString("NetworkMessage") != itemData["BuddyToss"][0]["msgBody"].ToString()){

					PlayerPrefs.SetString("NetworkMessage",itemData["BuddyToss"][0]["msgBody"].ToString());
					TextBody.text = PlayerPrefs.GetString("NetworkMessage");
					TextHeader.text = itemData["BuddyToss"][0]["msgHeader"].ToString();
					mainMessage.SetActive(true);

					                         }
			}
			}

	
			
			VideoAds.SendMessage("EnableAdCompany");
//			Snow.SendMessage("enableSnow");


		} else {

			PlayerPrefs.SetString("PrimaryAd", "vungle");
			PlayerPrefs.SetInt("myAdFrequency", 4);
			PlayerPrefs.SetInt("buyGuyMin", 5);
			PlayerPrefs.SetInt("buyGuyMax", 7);
//			PlayerPrefs.SetInt("isSnowing", 0);
			PlayerPrefs.SetInt("chartPop", 3);
			PlayerPrefs.SetInt("NetworkScoreMult", 1);
			PlayerPrefs.SetInt("topChar", 70);
			PlayerPrefs.SetInt("ranChar", 10);
			VideoAds.SendMessage("EnableAdCompany");
			PlayerPrefs.SetInt("characterbuyguy", 50);
//			Snow.SendMessage("enableSnow");
			mainMessage.SetActive(false);

			Debug.Log("ERROR: " + www.error);

		}



//		if (!string.IsNullOrEmpty (www.error)) {
//			Debug.Log (www.error);
//			Debug.Log ("Error with server");
//		}


//		jsonString = File.ReadAllText ("http://www.buddytoss.com/BuddyTossSettings.json");
//		print (jsonString);
	}


	
	// Update is called once per frame



}
