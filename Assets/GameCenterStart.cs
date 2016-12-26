using UnityEngine;
//using UnionAssets.FLE;
using System.Collections;
using System.Collections.Generic;

public class GameCenterStart : MonoBehaviour {
	private static bool IsInitialized = false;
	private string leaderBoardId =  "GCPoints";
	private string leaderBoardId2 =  "TNTMode";
	private string leaderBoardId3 =  "cuppong";
	private string leaderBoardId4 =  "fieldgoalhs";

	private string TEST_ACHIEVEMENT_1_ID = "explosive";
	private string TEST_ACHIEVEMENT_2_ID = "bullseye";
	private string TEST_ACHIEVEMENT_3_ID = "25points";
	private string TEST_ACHIEVEMENT_4_ID = "50points";
	private string TEST_ACHIEVEMENT_5_ID = "level12";
	private string TEST_ACHIEVEMENT_6_ID = "10cups";
	private string TEST_ACHIEVEMENT_7_ID = "birdhit";
	private string TEST_ACHIEVEMENT_8_ID = "under2minutes";
	private string TEST_ACHIEVEMENT_9_ID = "uprights";
	private string TEST_ACHIEVEMENT_10_ID = "threefieldgoals";


	private int currentPlayersHighScore;
	// Use this for initialization
	void Awake () {
		if(!IsInitialized) {

//			UM_GameServiceManager.RegisterAchievement (TEST_ACHIEVEMENT_1_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_2_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_3_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_4_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_5_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_6_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_7_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_8_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_9_ID);
//			GameCenterManager.RegisterAchievement (TEST_ACHIEVEMENT_10_ID);


//			UM_GameServiceManager.ActionAchievementsInfoLoaded += OnAchievementsLoaded;

			UM_GameServiceManager.Instance.Connect();
			IsInitialized = true;
		}
	}

//	private void OnLeaderboardScoreLoaded(CEvent e) {
//		ISN_PlayerScoreLoadedResult result = e.data as ISN_PlayerScoreLoadedResult;
//		
//		if(result.IsSucceeded) {
//			GCScore score = result.loadedScore;
//			IOSNativePopUpManager.showMessage("Leaderboard " + score.leaderboardId, "Score: " + score.score + "\n" + "Rank:" + score.rank);
//		}
//		
//	}

	public void loadLeaderBoards(){
		UM_GameServiceManager.Instance.ShowLeaderBoardsUI ();
	}

	public void updateLeaderBoard(int score){

		UM_GameServiceManager.ActionScoreSubmitted += OnScoreSubmitted;


		UM_GameServiceManager.Instance.SubmitScore(leaderBoardId,(long)score);
	}

	public void updateLeaderBoardFG(int score){

		UM_GameServiceManager.ActionScoreSubmitted += OnScoreSubmitted;
		UM_GameServiceManager.Instance.SubmitScore( leaderBoardId4,(long)score);
	}

	public void updateLeaderBoardTNT(int score){

		UM_GameServiceManager.ActionScoreSubmitted += OnScoreSubmitted;

		UM_GameServiceManager.Instance.SubmitScore( leaderBoardId2,(long)score);
		//		}
	}

	public void updateLeaderBoardPong(float thisTime){

		UM_GameServiceManager.Instance.UnlockAchievement( TEST_ACHIEVEMENT_6_ID);


		UM_GameServiceManager.ActionScoreSubmitted += OnScoreSubmitted;

		UM_GameServiceManager.Instance.SubmitScore(leaderBoardId3,(long)thisTime);

	}

	public void tenCupsUnderTwoMinutes(){
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_8_ID);

	}


	private void OnScoreSubmitted (UM_LeaderboardResult result) {
		UM_GameServiceManager.ActionScoreSubmitted -= OnScoreSubmitted;
		
		if(result.IsSucceeded)  {
			Debug.Log("Score Submitted");
		} else {
			Debug.Log("Score Submit Failed");
		}
	}

	private void OnAchievementsLoaded(UM_LeaderboardResult result) {
		
		Debug.Log("OnAchievementsLoaded");
		Debug.Log(result.IsSucceeded);
		
		if(result.IsSucceeded) {
			Debug.Log ("Achievements were loaded from iOS Game Center");
			
			foreach(GK_AchievementTemplate tpl in GameCenterManager.Achievements) {
				Debug.Log (tpl.Id + ":  " + tpl.Progress);
			}
		}
		
	}

	public void tntAchievement(){
	
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_1_ID);
	}

	public void bullseyeAchievement(){
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_2_ID);
	}

	public void twentyfivepointAchievement(){
		UM_GameServiceManager.Instance.UnlockAchievement( TEST_ACHIEVEMENT_3_ID);
	}
	public void fiftyfivepointAchievement(){
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_4_ID);
	}


	public void birdacheivement(){

		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_7_ID);

	}
	public void uprightacheivement(){
		
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_9_ID);
		
	}
	public void triplefgachievement(){
		
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_10_ID);
		
	}
	public void Level12(){
		UM_GameServiceManager.Instance.UnlockAchievement(TEST_ACHIEVEMENT_5_ID);

	}





}
