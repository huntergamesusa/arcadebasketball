using UnityEngine;
using System.Collections;
using Fabric.Answers;
public class AnsersBuddyToss : MonoBehaviour {

	public void customEvent(string eventname){
		Answers.LogCustom (
			eventname
);

		if (eventname == "FGStart"||eventname =="PongStart"||eventname =="TNTStart"||eventname =="WindStart") {
			UnitySocial.SocialOverlay.notificationActorLocation = UnitySocial.SocialOverlay.NotificationLocation.Hidden; 
			UnitySocial.SocialOverlay.entryPointUpdatesEnabled = false; 
		}
	}

	public static void sendCustomEvent(string myEvent){
		Answers.LogCustom (
			myEvent
			);




	}




}
