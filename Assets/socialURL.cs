using UnityEngine;
using System.Collections;

public class socialURL : MonoBehaviour {

//
//	public function FacebookOpen(){
//		Application.OpenURL ("http://facebook.com/buddytossapp");
//		
//	}
//	
//	public function TwitterOpen(){
//		Application.OpenURL ("https://twitter.com/buddytossapp");
//		
//	}


	// Use this for initialization
	void Start () {
		leftApp = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	IEnumerator OpenFacebook(){
//
//		Application.OpenURL(“fb://page/460071497487097”);
//
//	}


	 IEnumerator OpenFacebookPage(){
		Application.OpenURL("fb://profile/460071497487097");
			yield return new WaitForSeconds(1);
			if(leftApp){
				leftApp = false;
			}
			else{
			Application.OpenURL("http://facebook.com/buddytossapp");
			}
		}

	IEnumerator OpenTwitterPage(){
		Application.OpenURL("twitter:///user?screen_name=buddytossapp");
		yield return new WaitForSeconds(1);
		if(leftApp){
			leftApp = false;
		}
		else{
			Application.OpenURL("https://twitter.com/buddytossapp");
		}
	}
	
	bool leftApp = false;
		
		void OnApplicationPause(){
			leftApp = true;
		}
		}
		