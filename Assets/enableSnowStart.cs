using UnityEngine;
using System.Collections;

public class enableSnowStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("isSnowing") >=1) {
			
			gameObject.GetComponent<ParticleSystem>().Play ();
			
		}
	
		
		if(PlayerPrefs.HasKey("Shadows")==false ){

			if( PlayerPrefs.GetInt("isSnowing")>=1){
				gameObject.GetComponent<ParticleSystem>().Play ();

//				gameObject.SetActive(true);
			}
	
		}
		
		if(PlayerPrefs.GetInt("Shadows")==1){
			gameObject.GetComponent<ParticleSystem>().Play ();

//			gameObject.SetActive(true);

	
		}
		else{
			gameObject.GetComponent<ParticleSystem>().Stop ();

//			gameObject.SetActive(false);

		}

		if (PlayerPrefs.GetInt ("isSnowing") < 1) {
			gameObject.GetComponent<ParticleSystem>().Stop ();

//			gameObject.SetActive(false);

		}
	}

	public void enableSnow(){
		if (PlayerPrefs.GetInt ("isSnowing") >=1) {
			
			gameObject.GetComponent<ParticleSystem>().Play ();
			
		}
		
		
		if(PlayerPrefs.HasKey("Shadows")==false ){
			
			if( PlayerPrefs.GetInt("isSnowing")>=1){
				gameObject.GetComponent<ParticleSystem>().Play ();
				
				//				gameObject.SetActive(true);
			}
			
		}
		
		if(PlayerPrefs.GetInt("Shadows")==1){
			gameObject.GetComponent<ParticleSystem>().Play ();
			
			//			gameObject.SetActive(true);
			
			
		}
		else{
			gameObject.GetComponent<ParticleSystem>().Stop ();
			
			//			gameObject.SetActive(false);
			
		}
		
		if (PlayerPrefs.GetInt ("isSnowing") < 1) {
			gameObject.GetComponent<ParticleSystem>().Stop ();
			
			//			gameObject.SetActive(false);
			
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
