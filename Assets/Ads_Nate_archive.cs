using UnityEngine;
using System.Collections;
using ChartboostSDK;
//using UnionAssets.FLE;


//THIS SCRIPT IS CALLED BY POPBUTTONS SCRIPT WHICH IS TRIGGERED BY GAMEOVER

//using GoogleMobileAds.Api;

public class Ads_Nate_archive : MonoBehaviour {
	int numberofPlays;
	bool displayiAd;
//	private InterstitialAd interstitial;

//	public GUITexture videoZoneStateTexture = null;
	
//	public Texture readyTexture = null;
//	public Texture notReadyTexture = null;
	
//	public int currencyAmount = 0;
	


	// Use this for initialization
//	void Start () {
//		print (GetComponent<Canvas>().enabled);

//		AdColony.Configure( "1.0", "appda8bfc328d6146d884", "vz918aa255e1d941458c" );

//	}

	//
	void OnEnable(){
		Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
	}

	public void adMobPlay(){
//		InterstitialAd interstitial = new InterstitialAd("ca-app-pub-8309021549509898/2927346763");
//		AdRequest request = new AdRequest.Builder().Build();
//		interstitial.LoadAd(request);
//
//		if (interstitial.IsLoaded()) {
//			interstitial.Show();
//		}

	}


	public void iadLoad(){
//		iAdBannerController.instance.StartInterstitialAd ();
//		iAdBannerController.instance.LoadInterstitialAd ();
//		iAdBannerController.instance.ShowInterstitialAd ();
	}
	
	// Update is called once per frame

	//CHARTBOOST
	public void cacheAd(){
		//dont always cache ad
		if(Chartboost.hasInterstitial(CBLocation.Default) == false){
		Chartboost.cacheInterstitial(CBLocation.Default);
		}

	}

	void didFailToLoadInterstitial(CBLocation location, CBImpressionError error){
		if(numberofPlays > 3){
			displayiAd = true;


		}
	}

	public void AdmobRequest(){
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
	

	}

	public void AdmobShow(){
//		if (interstitial.IsLoaded()) {
//			interstitial.Show();
//		}

	}

	//CHARTBOOST
	public void loadInterstitial(){
		numberofPlays++;
		if(numberofPlays > 4){
			//if Chartboost fails to load play iad
//			if(Chartboost.didCacheInterstitial == false){
				//			}
//			else{

//			if(!displayiAd){
				Chartboost.showInterstitial(CBLocation.Default);
				numberofPlays = 0;
//			}

//			if(displayiAd){
//				displayiAd =false;
//				InterstitialAdDidLoadAction();
//			iAdBannerController.instance.LoadInterstitialAd ();
//			}
		
//			}



		}
		
	}

	private void InterstitialAdDidLoadAction () {
		//unsubscribing from event
		InterstitialAdDidLoadAction ();
		
		//the ad content is loaded, now we can use ShowInterstitialAd, to show it imidiatly any time we need.
//		iAdBannerController.instance.ShowInterstitialAd ();
		numberofPlays = 0;

	}



	





}
