using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine.Events;
using UnityEngine.EventSystems;
public class HeyPlayManager : MonoBehaviour {
	int m_storedNotificationCount;
	public GameObject m_notificationBadge;
	public GameObject m_notificationBadgeGO;
	public Text m_notificationText;
	public Text m_notificationTextGO;
	string newText;
	public GameObject ResetGameOverWind;
	public GameObject ResetGameOverFG;
	public GameObject ResetGameOverTNT;
	public GameObject ResetGameOverPong;

	public GameObject WindLevel;
	public GameObject FGLevel;
	public GameObject UnlockTNT;
	public GameObject UnlockPong;
	public GameObject TNTLevel;
	public GameObject PongLevel;

	public GameObject CoinShower;

	// Use this for initialization
	void Awake () {

	

//		Dictionary<string, object> colorTheme = new Dictionary<string, string>();
//		colorTheme.Add("main", "#147A00");
//		colorTheme.Add("accent", "#FFBE00");
//		UnitySocial.SetColorTheme(colorTheme);

		UnitySocial.SocialCore.onInitialized.AddListener(HandleInitialized);
		UnitySocial.SocialCore.onGameShouldPause.AddListener(HandleGameShouldPause);
		UnitySocial.SocialCore.onGameShouldResume.AddListener(HandleGameShouldResume);
		UnitySocial.SocialCore.onRewardClaimed.AddListener(HandleRewardClaimed);
		UnitySocial.Challenges.onChallengeStarted.AddListener(HandleChallengeStarted);
		UnitySocial.SocialCore.Initialize();

	}
	
	// Update is called once per frame
	public void ShowSocialUI () {
		UnitySocial.SocialOverlay.EntryPointClicked();
	}

	void HandleInitialized(bool success)
	{
		Debug.Log("Unity Social Initialized, success: "+success);
	}
	void HandleGameShouldPause()
	{
		Debug.Log("Game will pause");
	}
	void HandleGameShouldResume()
	{

		Debug.Log("Game will resume");
	}

	public void ResetGameOverMethod(string level){
		switch (level) {
		case "WindMode":
			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(WindLevel), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			UnitySocial.PlaySession.Begin ();
			break;
		case "FGMode":
			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(FGLevel), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			UnitySocial.PlaySession.Begin ();
			break;
		case "TNTMode":
			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(TNTLevel), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			UnitySocial.PlaySession.Begin ();
			break;
		case "BuddyPong":
			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(PongLevel), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			UnitySocial.PlaySession.Begin ();
			break;

		}

	}

	void HandleChallengeStarted(UnitySocial.Entities.ChallengeStatus status)
	{
		unHideHP ();

		int challengeType = -1;

		string challengeStr = "";
		Debug.Log("Challenge starts");
	
		foreach(KeyValuePair<string,object> kvp in status.metadata)
		{
			//This metadata is provided by you when creating challenges
			Debug.Log("Challenge metadata: "+kvp.Key+ ", "+kvp.Value);
			//As an example, let's assume that you have given the following as a metadata for achallenge:
			//challengeType: 1, challengeStr: "my first challenge"
			if (kvp.Key == "ChallengeType") {
				challengeStr = kvp.Value.ToString ();

				if (challengeStr  == "FGMode") {

					ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverFG), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			
				}
				if (challengeStr  ==  "WindMode") {
					ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverWind), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);


				}

				if (challengeStr == "TNTMode") {
					if (PlayerPrefs.GetInt ("TNTLevel") == 0) {
						PlayerPrefs.SetInt ("sessionHold", 1);
						ExecuteEvents.Execute<IPointerClickHandler> (ExecuteEvents.GetEventHandler<IPointerClickHandler> (UnlockTNT), new PointerEventData (EventSystem.current), ExecuteEvents.pointerClickHandler);
					
					} else {
						ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverTNT), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);

				
					}
				}
				if (challengeStr == "BuddyPong") {
					if (PlayerPrefs.GetInt ("PongLevel") == 0) {
						PlayerPrefs.SetInt ("sessionHold", 1);
						ExecuteEvents.Execute<IPointerClickHandler> (ExecuteEvents.GetEventHandler<IPointerClickHandler> (UnlockPong), new PointerEventData (EventSystem.current), ExecuteEvents.pointerClickHandler);

					} else {
						ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverPong), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);

					}
				}

			}
	
				//You should replace the StartGame with yourown function that starts up a game session.
			else{
			Debug.Log("Something went wrong, we didn't get all the data we needed!");
			}
	}
	}
	public void SessionHoldStart(){
		if (PlayerPrefs.GetInt ("sessionHold")==1) {
			PlayerPrefs.SetInt ("sessionHold", 0);
			UnitySocial.PlaySession.Begin ();

		}

	}

		void HandleRewardClaimed(Dictionary<string, object> metadata)
		{
		Debug.Log("Got reward(s)!");
		foreach(KeyValuePair<string,object> kvp in metadata)
		{
			Debug.Log("Reward: "+kvp.Key+" - "+kvp.Value+"!");
			//As an example, let's assume that you have given the following as an item name for areward: ‘Prizes’
			if(kvp.Key == "FirstFriend")
			{
//				PopUpManager.NewAlert ("you just got rewarded through unity social " +kvp.Key+" for "+kvp.Value+" coins!");
				Debug.Log("Player should be awarded " +
					System.Convert.ToInt32(kvp.Value) + "coins here!");
				CoinShower.SendMessage ("playAnimationCreditsHeyPlay",System.Convert.ToInt32 (kvp.Value));

				//AddToPrizes(System.Convert.ToInt32(kvp.Value));
			}

			if (kvp.Key == "RewardCoins") {
				CoinShower.SendMessage ("playAnimationCreditsHeyPlay", System.Convert.ToInt32 (kvp.Value));
			}
//			if (kvp.Key = "GiftCoins") {
//				playAnimationCreditsHeyPlayNoShower
//				PlayerPrefs.SetInt ("myCredits",PlayerPrefs.GetInt ("myCredits")-System.Convert.ToInt32 (kvp.Value))
//			}
	
		}
	}

	void OnEnable(){
		UnitySocial.SocialOverlay.onEntryPointStateUpdated.AddListener(HandleUnitySocialEntryPointStateUpdate);
		UnitySocial.SocialOverlay.notificationActorLocation = UnitySocial.SocialOverlay.NotificationLocation.RightTop;
		UnitySocial.SocialOverlay.notificationActorOffset = 0;
		UnitySocial.SocialOverlay.entryPointUpdatesEnabled = true;
		UnitySocial.SocialOverlay.entryPointImageSize = 128;
//		if(m_storedNotificationCount==0)
//		{
//			m_notificationBadge.SetActive(false);
//			m_notificationBadgeGO.SetActive(false);
//
//	}
	}

	public void unHideHP(){
		UnitySocial.SocialOverlay.notificationActorLocation = UnitySocial.SocialOverlay.NotificationLocation.RightTop;
		UnitySocial.SocialOverlay.entryPointUpdatesEnabled = true;
	}

	void OnDisable(){
		UnitySocial.SocialOverlay.onEntryPointStateUpdated.RemoveListener(HandleUnitySocialEntryPointStateUpdate);
		UnitySocial.SocialOverlay.notificationActorLocation = UnitySocial.SocialOverlay.NotificationLocation.Hidden; 
		UnitySocial.SocialOverlay.entryPointUpdatesEnabled = false;
//		m_notificationBadge.SetActive(false);
	}

	private void HandleUnitySocialEntryPointStateUpdate(UnitySocial.Entities.EntryPointState newState)
	{
		m_storedNotificationCount = newState.notificationCount;

		if(newState.notificationCount > 0)
		{
			m_notificationBadge.SetActive(true);
			m_notificationBadgeGO.SetActive(true);

			if (newState.notificationCount < 10 && newState.notificationCount > 0) {
				newText = newState.notificationCount.ToString ();
			} 
			if(newState.notificationCount >= 10){
				 newText = "9+";

			}
			m_notificationText.text = newText;
			m_notificationTextGO.text = newText;

		} else {
			m_notificationBadge.SetActive(false);
			m_notificationBadgeGO.SetActive(false);

		} }

	public void StartingLevel(){
		UnitySocial.PlaySession.Begin ();


	}
//	public void Update(){
//		if (Input.GetKeyUp (KeyCode.A)) {
//			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverFG), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
//		}
//		if (Input.GetKeyUp (KeyCode.B)) {
//			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverWind), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
//		}
//		if (Input.GetKeyUp (KeyCode.C)) {
//			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverTNT), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
//		}
//		if (Input.GetKeyUp (KeyCode.D)) {
//			ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(ResetGameOverPong), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
//		}
//		if (Input.GetKeyUp (KeyCode.E)) {
//			CoinShower.SendMessage ("playAnimationCreditsHeyPlay", System.Convert.ToInt32 (50));
//			print (System.Convert.ToInt32 (50));
//	
//		}
//	}
	 void EndingSessionWind(int endScore){
		int score = endScore;
//		Dictionary<string, object> sessionData = new Dictionary<string, object>();
//		sessionData.Add("WindMode", score);
		UnitySocial.PlaySession.SendEvent("WindMode", (float)score);

	}
	void EndingSessionFG(int endScore){
		int score = endScore;
//		Dictionary<string, object> sessionData = new Dictionary<string, object>();
//		sessionData.Add("FGMode", score);
		UnitySocial.PlaySession.SendEvent("FGMode", (float)score);

	}
	void EndingSessionTNT(int endScore){
		int score = endScore;
//		Dictionary<string, object> sessionData = new Dictionary<string, object>();
//		sessionData.Add("TNTMode", score);
		UnitySocial.PlaySession.SendEvent("TNTMode", (float)score);

	}
	 void EndingSessionPong(float endScore){
		float score = endScore*1000;

//		Dictionary<string, object> sessionData = new Dictionary<string, object>();
//		sessionData.Add("BuddyPong", (int)score);
		UnitySocial.PlaySession.SendEvent("BuddyPong", (float)score);

	}

}
