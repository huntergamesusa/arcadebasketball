using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ChartboostSDK;
//using UnionAssets.FLE;


//THIS SCRIPT IS CALLED BY POPBUTTONS SCRIPT WHICH IS TRIGGERED BY GAMEOVER

//using GoogleMobileAds.Api;

public class Ads_Nate : MonoBehaviour {
	int numberofPlays;
	bool displayAdmob;

	public void MoreGamesCall(){
		
		Chartboost.showMoreApps(CBLocation.Default);
	}
	



	//
	void OnEnable(){
		if (PlayerPrefs.GetInt ("Ads") == 1)
			return;
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;


	}

	public static void ShowCross(){
		PlayerPrefs.SetInt ("chartPopIncrease", PlayerPrefs.GetInt ("chartPopIncrease") + 1);
		if (PlayerPrefs.GetInt ("chartPopIncrease") >= PlayerPrefs.GetInt ("chartPop")) {
			PlayerPrefs.SetInt ("chartPopIncrease",0);
			Chartboost.showInterstitial(CBLocation.Default);
//			AnsersBuddyToss.sendCustomEvent("cross_promotion");
		}

	}



//	public void iadLoad(){
//		iAdBannerController.Instance.StartInterstitialAd ();
////		iAdBannerController.Instance.LoadInterstitialAd ();
////		iAdBannerController.Instance.ShowInterstitialAd ();
//	}
	
	// Update is called once per frame

	//CHARTBOOST
	public void cacheAd(){
		if (PlayerPrefs.GetInt ("Ads") == 1)
			return;

		//dont always cache ad
		if(Chartboost.hasInterstitial(CBLocation.Default) == false){
		Chartboost.cacheInterstitial(CBLocation.Default);
		}
		if(Chartboost.hasInterstitial(CBLocation.Default) == false){
			Chartboost.cacheInterstitial(CBLocation.Default);
		}

	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error){
		if (PlayerPrefs.GetInt ("Ads") == 1)
			return;

//		if(numberofPlays >= PlayerPrefs.GetInt("myAdFrequency")){
//			displayAdmob = true;
//			AdmobRequest();
//
//		}
	}

//	public void AdmobRequest(){
//		if (PlayerPrefs.GetInt ("Ads") == 1)
//			return;
//
//		 interstitial = new InterstitialAd("ca-app-pub-8309021549509898/2927346763");
//		// Create an empty ad request.
//	
//		AdRequest request = new AdRequest.Builder()
//			.AddTestDevice(AdRequest.TestDeviceSimulator)
//				.AddTestDevice("8a46da656de26b9be8f012e89038d41d")
//			.Build();
//
//		// Load the interstitial with the request.
//		interstitial.LoadAd(request);
//
//		interstitial.AdFailedToLoad += HandleAdFailedToLoad;
//
//	}

//	public void AdmobShow(){
//		if (PlayerPrefs.GetInt ("Ads") == 1)
//			return;
//
//		if (interstitial.IsLoaded()) {
//			interstitial.Show();
//		}
//
//
//	}

	void Awake(){
	

		Caching.CleanCache ();

		Chartboost.cacheMoreApps(CBLocation.Default);
		cacheAd ();
		
	}

	void resetAdPlays(){
		int highEndint = 2;
		if (PlayerPrefs.GetInt ("highscore") > 24) {
			if(highEndint>PlayerPrefs.GetInt("myAdFrequency")){
				
			}
			else{
				numberofPlays = Random.Range (0, 2);
			}
			//			loadInterstitial ();
		} else {
			numberofPlays=0;
		}

	}

	public void autoLoadAd(){

	
		numberofPlays = 5;
		loadInterstitial ();
		
	}
	//CHARTBOOST
	public void loadInterstitial(){
		if (PlayerPrefs.GetInt ("Ads") == 1)
			return;


		numberofPlays++;
		if(numberofPlays >= PlayerPrefs.GetInt("myAdFrequency")){
			//if Chartboost fails to load play iad
//			if(Chartboost.didCacheInterstitial == false){
				//			}
//			else{

//			if(!displayAdmob){
				Chartboost.showInterstitial(CBLocation.Default);
				resetAdPlays();
//			}

//			if(displayAdmob){
//				displayAdmob =false;
//				AdmobShow();
//				resetAdPlays();
////			iAdBannerController.Instance.InterstitialAdDidLoadAction += InterstitialAdDidLoadAction;
////			iAdBannerController.Instance.LoadInterstitialAd ();
//			}
		
//			}



		}
		
	}



//	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		if (PlayerPrefs.GetInt ("Ads") == 1)
//			return;
//		iAdBannerController.InterstitialAdDidLoadAction += HandleInterstitialAdDidLoadAction;
//		iAdBannerController.Instance.LoadInterstitialAd ();
//		print("Interstitial Failed to load: " + args.Message);
//		// Handle the ad failed to load event.
//	}

//	void HandleInterstitialAdDidLoadAction (){
//		if (PlayerPrefs.GetInt ("Ads") == 1)
//			return;
//		//unsubscribing from event
//		iAdBannerController.InterstitialAdDidLoadAction -= HandleInterstitialAdDidLoadAction;
//
//		//the ad content is loaded, now we can use ShowInterstitialAd, to show it imidiatly any time we need.
//		iAdBannerController.Instance.ShowInterstitialAd ();
//		resetAdPlays ();
//
//	}



	





}
