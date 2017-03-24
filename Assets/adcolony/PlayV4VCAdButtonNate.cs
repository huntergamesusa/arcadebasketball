using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;
/// <summary>
/// This is a class to wrap the gui texture button class. It serves as a way to integrate button press logic.
/// <summary>
/// <value name="currencyText">This is the GUIText GameObject to be used for displaying the currency being tracked by this script</value>
/// <value name="videoZoneStateTexture">This is the GUITexture GameObject that is used to communicate when the video is ready to display.</value>
/// <value name="readyTexture">This is the Texture that is used to communicate that the video is ready to play.</value>
/// <value name="notReadyTexture">This is the Texture that is used to communicate that the video is NOT ready to play.</value>
/// <value name="currencyAmount">This is the property used to track the amount of currency available to the play.</value>
/// <value name="appVersion">This is an arbitrary string to be used by the developer to indicate what version their app is on.</value>
/// <value name="appId">This is the app id provided by AdColony to help link the app created within AdColony to the application using the app id.</value>
/// <value name="zoneId">This is the zone id provided by AdColony. This is used to indicate what ads to play with the application.</value>
public class PlayV4VCAdButtonNate : MonoBehaviour {
	//---------------------------------------------------------------------------
	//private
	//---------------------------------------------------------------------------
	//---------------------------------------------------------------------------
	//public
	//---------------------------------------------------------------------------

	//	public Text AdIntegerVariable2; // assign it from inspector
	public string url2;
	public string myText2; 
	bool isVungleAdAvailable = false;

	public GameObject earnVidButton;
	public GameObject winPrizeButton;
	public GameObject winGiftButton;

	public Text earnVidTxt;
	public Text winPrizeTxt;
	public Text winGiftTxt;

	public GameObject mainSlider;

	public int currencyAmount = 0;

	public string appVersion = "1.0";
	public string appId = "";
	public string zoneId = "";
	public static Text credits;
	public GameObject CreditsCamera;
	private int multiplier;

	public bool isAdcolony;
	public Button videoButton;
	bool isVunglePlayable;

	//---------------------------------------------------------------------------

	// Use this for initialization
	void Start() {
		//    base.Start();
		credits = GameObject.Find ("Credits UI").GetComponent<Text>();
		credits.text  = PlayerPrefs.GetInt("myCredits").ToString("f0");
		ConfigureZoneString();
		AdColony.Configure(appVersion, appId, zoneId);
		AdColony.OnAdAvailabilityChange = OnAdAvailabilityChange;
		AdColony.OnV4VCResult = UpdateCurrencyText;

		Vungle.init("com.HunterGames.BuddyToss", "980241548", "980241548" );
	

		//		Vungle.init ("","980241548","");
		videoButton.enabled=true;
		#if UNITY_IOS
		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize ("73029", false);
		}
		#endif
		#if UNITY_ANDROID
		Advertisement.Initialize ("122862", false);
		#endif
		//IF YOU HAVE GALACTIC MAN MULTIPLY ALL COINS
	
			multiplier=1;


		Vungle.adPlayableEvent += (adPlayable) => {
			if (adPlayable) {
				isVungleAdAvailable = true;
			}
			else {
				isVungleAdAvailable = false;
			}
		};



	}

	public void EnableAdCompany(){
		if (PlayerPrefs.GetString("PrimaryAd") == "adcolony") {
			isAdcolony = true;
		}
		else{
			isAdcolony = false;

		}
	}

	// Update is called once per frame






	/// <summary>
	/// This method uses platform dependent compilation to determine what type of app id and zone id to use for the buttons. There are other ways to do this, but platform dependent compliation makes it easier for the code to stay all in one place for configuration.
	/// Reference: http://docs.unity3d.com/Manual/PlatformDependentCompilation.html
	/// </summary>
	public void ConfigureZoneString() {
		#if UNITY_ANDROID
		// App ID
		appId= "appbc8a851cd9d349f487";
		// Video zones
		zoneId = "vz310861db915f446480";
		//If not android defaults to setting the zone strings for iOS
		#else
		// App ID
		appId = "appda8bfc328d6146d884";
		// Video zones
		zoneId = "vz918aa255e1d941458c";
		#endif
	}

	/// <summary>
	/// This checks every update if the zone specified is ready to be played. If it is, it sets the GUITexture being used to display the status to the correct image.
	/// </summary>
	public void OnAdAvailabilityChange(bool available, string zoneIdChanged) {
		if(available
			&& zoneId == zoneIdChanged) {
			//      videoZoneStateTexture.texture = readyTexture;
		}
		else {
			//      videoZoneStateTexture.texture = notReadyTexture;
		}
	}

	public IEnumerator playAnimationCredits(){
		credits = GameObject.Find ("Credits UI").GetComponent<Text>();
		CreditsCamera.SetActive(true);
		CreditsCamera.transform.GetChild(0).GetComponent<ParticleSystem>().Play ();
		//		GetComponent<AudioSource>().pitch =2.5f;
		for(int i = 0; i < 20*multiplier; i++)
		{	
			credits.transform.localScale = new Vector3(1.25f,1.25f,1.25f);

			GetComponent<AudioSource>().Play ();

			LeanTween.scale(credits.gameObject, new Vector3(1f,1f,1f), .1f).setEase(LeanTweenType.easeInOutElastic);

			PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")+1);
			credits.text  = PlayerPrefs.GetInt("myCredits").ToString("f0");

			//			GetComponent<AudioSource>().pitch = Random.Range(.9f,1.2f);
			yield return new WaitForSeconds(.05f);

		}
		//		GetComponent<AudioSource>().pitch =3.5f;
		GetComponent<AudioSource>().Play ();


		yield return new WaitForSeconds(1);
		CreditsCamera.SetActive(false);




	}


	public IEnumerator playAnimationCreditsHeyPlay(int coins){
		credits = GameObject.Find ("Credits UI").GetComponent<Text>();
		CreditsCamera.SetActive(true);
		CreditsCamera.transform.GetChild(0).GetComponent<ParticleSystem>().Play ();
		//		GetComponent<AudioSource>().pitch =2.5f;
		for(int i = 0; i < coins; i++)
		{	
			credits.transform.localScale = new Vector3(1.25f,1.25f,1.25f);

			GetComponent<AudioSource>().Play ();

			LeanTween.scale(credits.gameObject, new Vector3(1f,1f,1f), .1f).setEase(LeanTweenType.easeInOutElastic);

			PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")+1);
			credits.text  = PlayerPrefs.GetInt("myCredits").ToString("f0");

			//			GetComponent<AudioSource>().pitch = Random.Range(.9f,1.2f);
			yield return new WaitForSeconds(.05f);

		}
		//		GetComponent<AudioSource>().pitch =3.5f;
		GetComponent<AudioSource>().Play ();


		yield return new WaitForSeconds(1);
		CreditsCamera.SetActive(false);




	}

	public void playAnimationCreditsHeyPlayNoShower(){
		credits = GameObject.Find ("Credits UI").GetComponent<Text>();
		credits.text  = PlayerPrefs.GetInt("myCredits").ToString("f0");

	}



	public void afterVideoFunc(){
		if (PlayerPrefs.GetInt ("myCredits") >= 80) {
			earnVidTxt.enabled = false;
			earnVidButton.GetComponent<Image> ().enabled = false;
			earnVidButton.GetComponent<Button> ().enabled = false;
			winGiftTxt.enabled = false;
			winGiftButton.GetComponent<Image> ().enabled = false;
			winGiftButton.GetComponent<Button> ().enabled = false;
			winPrizeTxt.enabled = true;
			winPrizeButton.GetComponent<Image> ().enabled = true;
			winPrizeButton.GetComponent<Button> ().enabled = true;
		} 

		else {
			mainSlider.transform.localPosition = new Vector3(-900f,-84.5f,0f);

		}

	}



	/// <summary>
	/// This will update the currency text so that it will reflect an accurate count of what currency is available. It's method signature must be like this in order to be assigned as the AdColony.OnV4VCResult delegate. This allows the AdColony plug-in to fire events to this method when they occur.
	/// </summary>
	/// <param name="success">Indicates if the ad was successful in playing</param>
	/// <param name="currencyName">This is the name of currency being rewarded. I.E. coins, gems, balloons, etc.</param>
	/// <param name="currencyAmount">This the amount of currency awarded on the completion of the video.</param>
	public void UpdateCurrencyText(bool success, string currencyName, int currencyAwarded) {
		Debug.Log("OnV4VCResult WAS JUST TRIGGERED.");
		Debug.Log("Was Successful: " + success);
		Debug.Log("--------------------------------");

		StartCoroutine(playAnimationCredits());

		afterVideoFunc ();
		videoButton.enabled=true;

		//    currencyAmount += currencyAwarded;
		//    if(currencyText != null) {
		//      currencyText.GetComponent<GUIText>().text = currencyName + ": " + currencyAmount;
		//    }
	}

	/// <summary>
	/// This is the default logic to be performed on button pressed
	/// </summary>
	public  void PerformButtonPressLogic() {

		ShowOptions options = new ShowOptions ();
		options.resultCallback = HandleShowResult;

		if (PlayerPrefs.GetString ("PrimaryAd") == "adcolony") {


			Debug.Log("adcolony click: " + PlayerPrefs.GetString ("PrimaryAd"));

			if (AdColony.IsVideoAvailable (zoneId)) {

				Debug.Log (this.gameObject.name + " triggered playing a video ad.");
				AdColony.ShowV4VC (false, zoneId);
				videoButton.enabled = false;

			} else {
				Debug.Log (this.gameObject.name + " tried to trigger playing an ad, but the video is not available yet.");
				Vungle.playAd ();
				videoButton.enabled = false;


			}
		} else {

			if (PlayerPrefs.GetString ("PrimaryAd") == "vungle") {
				Debug.Log("vungle click: " + PlayerPrefs.GetString ("PrimaryAd"));

				if (isVungleAdAvailable) {
					Vungle.playAd ();
					videoButton.enabled = false;
				}
				else{
					if (Advertisement.IsReady()) {


						Advertisement.Show ("rewardedVideoZone",options);
						videoButton.enabled = false;
					} else {
						AdColony.ShowV4VC (false, zoneId);
						videoButton.enabled = false;

					}

				}


			}
			if (PlayerPrefs.GetString ("PrimaryAd") == "unity") {
				Debug.Log("unity click: " + PlayerPrefs.GetString ("PrimaryAd"));

				if (Advertisement.IsReady()) {

					Advertisement.Show ("rewardedVideoZone",options);
					videoButton.enabled = false;
				} else {

					if (isVungleAdAvailable) {
						Vungle.playAd ();
						videoButton.enabled = false;
					}
					else{
						AdColony.ShowV4VC (false, zoneId);
						videoButton.enabled = false;
					}

				}
			} else {

				if(PlayerPrefs.GetString ("PrimaryAd") != "vungle"){
					Debug.Log("adcolony or not entered properly click: " + PlayerPrefs.GetString ("PrimaryAd"));

					AdColony.ShowV4VC (false, zoneId);
					videoButton.enabled = false;
				}

			}




		}
	}



	private void HandleShowResult (ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");

			StartCoroutine(playAnimationCredits());

			afterVideoFunc ();
			videoButton.enabled=true;

			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			videoButton.enabled=true;

			break;
		case ShowResult.Failed:
			Debug.Log("I swear this has never happened to me before");
			videoButton.enabled=true;
			break;
		}
	}

	void OnEnable(){

		Vungle.onAdStartedEvent += onAdStartedEvent;
		Vungle.onAdFinishedEvent += onAdFinishedEvent;
		//		Vungle.adPlayableEvent += adPlayableEvent;

		//		Vungle.onAdViewedEvent += onAdViewedEvent;

	}

	void OnDisable()
	{
		if (PlayerPrefs.GetInt ("Ads") == 1)
			return;
		Vungle.onAdStartedEvent -= onAdStartedEvent;
		//		Vungle.adPlayableEvent -= adPlayableEvent;

		Vungle.onAdFinishedEvent -= onAdFinishedEvent;
		//		Vungle.onAdViewedEvent -= onAdViewedEvent;

	}
	//VUNGLE
	void onAdStartedEvent()
	{

		Debug.Log( "onAdStartedEvent" );
	}


	void onAdEndedEvent()
	{
		Debug.Log( "onAdEndedEvent" );
	}
	//	void adPlayableEvent()
	//	{
	//		Debug.Log( "onAdEndedEvent" );
	//	}


	void onAdFinishedEvent(AdFinishedEventArgs arg)
	{

		StartCoroutine(playAnimationCredits());
		afterVideoFunc ();

	}


	void onCachedAdAvailableEvent()
	{
		Debug.Log( "onCachedAdAvailableEvent" );
	}

	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus)
			Vungle.onPause();
		else
			Vungle.onResume();
	}








}
