using UnityEngine;
using System.Collections;
//using UnityStandardAssets.ImageEffects;

public class animateVignette : MonoBehaviour {

	public float inten;
	public float maxVig;
	public float speed;
	bool anim;

	// Use this for initialization
//	void Awake () {
//		anim =false;
//		inten = GetComponent<VignetteAndChromaticAberration>().intensity;  
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if(GetComponent<VignetteAndChromaticAberration>().intensity<maxVig && anim){
//			GetComponent<VignetteAndChromaticAberration>().intensity = Mathf.Lerp (GetComponent<VignetteAndChromaticAberration>().intensity,maxVig,Time.deltaTime * speed);
//		}
//		if(GetComponent<VignetteAndChromaticAberration>().intensity>0 && !anim){
//			GetComponent<VignetteAndChromaticAberration>().intensity = Mathf.Lerp (GetComponent<VignetteAndChromaticAberration>().intensity,0,Time.deltaTime * speed);
//		}
//	}

	public void animateVinIncrease(){
		anim = true;

	}
	public void animateVinDecrease(){
		anim = false;
		
	}
}
