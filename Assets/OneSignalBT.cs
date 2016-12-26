using UnityEngine;
using System.Collections;
using System.Collections.Generic;  

public class OneSignalBT : MonoBehaviour {

	void Start () {
		// Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
		//OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);
		
		OneSignal.Init("e925e243-6475-4819-b2e5-9836dccdb70b", "703322744261", HandleNotification);
	}
	// Gets called when the player opens the notification.
	private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
	}
}
