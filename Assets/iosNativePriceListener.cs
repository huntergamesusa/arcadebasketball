using UnityEngine;
using System.Collections;

public class iosNativePriceListener : MonoBehaviour {


	public GameObject scriptController;
	// Use this for initialization
	public string mypricing;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateLocalizedPricing(string characterTexture){
		if(IOSInAppPurchaseManager.Instance.IsStoreLoaded){
			if (characterTexture== "SuperGoldBuddy") {
				mypricing =  IOSInAppPurchaseManager.Instance.GetProductById("supergalacticbuddy").LocalizedPrice;
				
			}
			else{
				mypricing =  IOSInAppPurchaseManager.Instance.GetProductById("thunderbuddy").LocalizedPrice;
				
			}

		}
		
		else{
			mypricing = "$0.99";
	
			if (characterTexture == "SuperGoldBuddy") {
				mypricing = "$3.99";
				
			}
	
			
		}
		scriptController.SendMessage ("receivePrice", mypricing);
		
	}

}
