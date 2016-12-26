using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class TimeRewardArch : MonoBehaviour {
	public DateTime currentDate;
	public DateTime oldDate;
	TimeSpan difference;

	public GameObject earnVidButton;
	public GameObject winPrizeButton;
	public GameObject winGiftButton;

	public Text earnVidTxt;
	public Text winPrizeTxt;
	public Text winGiftTxt;

	// Use this for initialization
	void Start () {

//		if (PlayerPrefs.GetInt ("myCredits") < 100) {
//		}
//
//
//		//Store the current time when it starts
//		currentDate = System.DateTime.Now;
//		
//		//Grab the old time from the player prefs as a long
//		long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
//		
//		//Convert the old time from binary to a DataTime variable
//		DateTime oldDate = DateTime.FromBinary(temp);
//		print("oldDate: " + oldDate);
//		
//		//Use the Subtract method and store the result as a timespan variable
//		TimeSpan difference = currentDate.Subtract(oldDate);
//		print("Difference: " + difference);
	}

	void gameEndChangeButtons(){
		print ("changingButtons");
		if(PlayerPrefs.GetInt("myCredits")>=100){
			earnVidTxt.enabled =false;
			earnVidButton.GetComponent<Image>().enabled =false;
			earnVidButton.GetComponent<Button>().enabled =false;
			winGiftTxt.enabled =false;
			winGiftButton.GetComponent<Image>().enabled =false;
			winGiftButton.GetComponent<Button>().enabled =false;
			winPrizeTxt.enabled =true;
			winPrizeButton.GetComponent<Image>().enabled =true;
			winPrizeButton.GetComponent<Button>().enabled =true;
		}
		else{
			if(PlayerPrefs.GetInt("GiftReady")>0){
				earnVidTxt.enabled =false;
				earnVidButton.GetComponent<Image>().enabled =false;
				earnVidButton.GetComponent<Button>().enabled =false;
				winGiftTxt.enabled =true;
				winGiftButton.GetComponent<Image>().enabled =true;
				winGiftButton.GetComponent<Button>().enabled =true;
				winPrizeTxt.enabled =false;
				winPrizeButton.GetComponent<Image>().enabled =false;
				winPrizeButton.GetComponent<Button>().enabled =false;
			}
			else{
			earnVidTxt.enabled =true;
			earnVidButton.GetComponent<Image>().enabled =true;
			earnVidButton.GetComponent<Button>().enabled =true;
			winGiftTxt.enabled =false;
				winGiftButton.GetComponent<Image>().enabled =false;
				winGiftButton.GetComponent<Button>().enabled =false;
			winPrizeTxt.enabled =false;
			winPrizeButton.GetComponent<Image>().enabled =false;
			winPrizeButton.GetComponent<Button>().enabled =false;
			}
		}

	}
	void Update(){
		if (Input.GetKeyUp (KeyCode.T)) {
			endGameTimeDifference();
			print("Difference: " + difference.TotalMinutes);

		}
	}


	void endGameTimeDifference(){
		if (PlayerPrefs.GetInt ("Gifts") > 0) {
			//Grab the old time from the player prefs as a long
			long temp = Convert.ToInt64 (PlayerPrefs.GetString ("sysString"));
		
			//Convert the old time from binary to a DataTime variable
			DateTime oldDate = DateTime.FromBinary (temp);
			print ("oldDate: " + oldDate);
			currentDate = System.DateTime.Now;
			//Use the Subtract method and store the result as a timespan variable
			difference = currentDate.Subtract (oldDate);
//			print ("Difference: " + difference);
		}

	}

	void checkGiftReady(){
		if(PlayerPrefs.GetInt("Gifts")==0){
			//0 min
			PlayerPrefs.SetInt("GiftReady",1);
		}

		if (PlayerPrefs.GetInt ("Gifts") == 1) {
			//3 min
			if (difference.Minutes >= 3) {
				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}

		if(PlayerPrefs.GetInt("Gifts")==2){
			//6 min
			if (difference.Minutes >= 6) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==3){
			//30 min
			if (difference.Minutes >= 30) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==4){
			//1hr
			if (difference.Minutes >= 60) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==5){
			//2hr
			if (difference.Minutes >= 120) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==6){
			//3hr
			if (difference.Minutes >= 180) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==7){
			//4hr
			if (difference.Minutes >= 240) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}
		if(PlayerPrefs.GetInt("Gifts")==8){
			//5hr
			if (difference.Minutes >= 300) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
			
		}
		if(PlayerPrefs.GetInt("Gifts")>=9){
			//6hr
			if (difference.Minutes >= 360) {

				PlayerPrefs.SetInt("GiftReady",1);
			}
			
		}

	}

	public void giftExpensedResetTime(){

		if(PlayerPrefs.GetInt("Gifts")==0){
			//0 min
			PlayerPrefs.SetInt("Gifts",1);
		}
			if (PlayerPrefs.GetInt ("Gifts") == 1) {
				//3 min

				PlayerPrefs.SetInt ("Gifts", 2);
	

			}



		if(PlayerPrefs.GetInt("Gifts")==2){
			//6 min
			PlayerPrefs.SetInt("Gifts",3);
	
		}
		if(PlayerPrefs.GetInt("Gifts")==3){
			//30 min
			PlayerPrefs.SetInt("Gifts",4);

		}
		if(PlayerPrefs.GetInt("Gifts")==4){
			//1hr
			PlayerPrefs.SetInt("Gifts",5);

		}
		if(PlayerPrefs.GetInt("Gifts")==5){
			//2hr
			PlayerPrefs.SetInt("Gifts",6);

		}
		if(PlayerPrefs.GetInt("Gifts")==6){
			//3hr
			PlayerPrefs.SetInt("Gifts",7);
	
		}
		if(PlayerPrefs.GetInt("Gifts")==7){
			//4hr
			PlayerPrefs.SetInt("Gifts",8);
	
		}
		if(PlayerPrefs.GetInt("Gifts")==8){
			//5hr
			PlayerPrefs.SetInt("Gifts",9);
			
			
		}
		if(PlayerPrefs.GetInt("Gifts")>=9){
			//6hr
			PlayerPrefs.SetInt("Gifts",10);
	
		}
		PlayerPrefs.SetInt ("GiftReady", 0);

//		currentDate = System.DateTime.Now;
		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());

	}


	//OnApplicationPause 
//	void OnApplicationQuit()
//	{
//		//Savee the current system time as a string in the player prefs class
//		PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
//		
//		print("Saving this date to prefs: " + System.DateTime.Now);
//	}
}
