using UnityEngine;
using System.Collections;
using Fabric.Answers;
public class AnsersBuddyToss : MonoBehaviour {

	public void customEvent(string eventname){
		Answers.LogCustom (
			eventname
);

		if (eventname == "FGStart"||eventname =="PongStart"||eventname =="TNTStart"||eventname =="WindStart") {
			UnitySocial.notificationLocation = UnitySocial.NotificationLocation.Hidden; 
			UnitySocial.entryPointUpdatesEnabled = false; 
		}
	}

	public static void sendCustomEvent(string myEvent){
		Answers.LogCustom (
			myEvent
			);




	}




}
