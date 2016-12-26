using UnityEngine;
using System.Collections;

public class PopUpManager : MonoBehaviour {

	// Use this for initialization
	public static void NewAlert(string message){
		MobileNativeMessage msg = 	new MobileNativeMessage("Alert",message);
		msg.OnComplete += OnMessageClose;

	}

	public static void OnMessageClose() {

		//		new MobileNativeMessage("Result", "Message Closed");
	}
}
