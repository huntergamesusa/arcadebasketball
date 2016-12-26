using UnityEngine;
using System.Collections;

public class updatePreviewBuddy : MonoBehaviour {
	public int leftNumber;
	public int rightNumber;
	public Material leftMaterial;
	public Material rightMaterial;
	public GameObject leftBuddy;
	public GameObject rightBuddy;
	Vector3 leftBuddyStartPos;
	Vector3  rightBuddyStartPos;

	Texture [] characterTextures;

	bool isMenuOpen;

	public Material capeLeft;
	public Material capeRight;


	// Use this for initialization
	void Start () {
		leftBuddyStartPos = leftBuddy.transform.position;
		rightBuddyStartPos = rightBuddy.transform.position;

	}

	public void menuStatus(bool menu){
		isMenuOpen = menu;
		if (menu) {
		receiveCharacternumber(PlayerPrefs.GetInt ("Character"));
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void receiveTextures (Texture[] me){
		characterTextures =me;
	}

	void receiveCharacternumber(int num){

		if (isMenuOpen) {

			leftNumber = num - 1;
			rightNumber = num + 1;

			if (leftNumber <= -1) {
//				leftBuddy.SetActive (false);
				leftNumber = characterTextures.Length-1;
				leftBuddy.SetActive (true);
				iTween.ScaleTo (leftBuddy, iTween.Hash ("scale", new Vector3 (.01f, .01f, .01f), "time", .125, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "changeLeftTexture", "OnCompleteTarget", gameObject));
				
				leftMaterial.mainTexture = characterTextures [leftNumber];
				
				
				StartCoroutine(changeLeftTexture());
				
				
				iTween.ScaleTo (leftBuddy, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f), "time", .5, "easeType", iTween.EaseType.easeOutElastic, "delay", .125f));

			} else {

				leftBuddy.SetActive (true);
				iTween.ScaleTo (leftBuddy, iTween.Hash ("scale", new Vector3 (.01f, .01f, .01f), "time", .125, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "changeLeftTexture", "OnCompleteTarget", gameObject));

				leftMaterial.mainTexture = characterTextures [leftNumber];


				StartCoroutine(changeLeftTexture());


				iTween.ScaleTo (leftBuddy, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f), "time", .5, "easeType", iTween.EaseType.easeOutElastic, "delay", .125f));

			}


		
			if (rightNumber >= characterTextures.Length) {
//				rightBuddy.SetActive (false);
				rightNumber =0;
				rightBuddy.SetActive (true);
				iTween.ScaleTo (rightBuddy, iTween.Hash ("scale", new Vector3 (.01f, .01f, .01f), "time", .125, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "changeRightTexture", "onCompleteTarget", gameObject));
				
				rightMaterial.mainTexture = characterTextures [rightNumber];
				StartCoroutine(changeRightTexture());
				
				iTween.ScaleTo (rightBuddy, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f), "time", .5, "easeType", iTween.EaseType.easeOutElastic, "delay", .125f));

			} else {
				rightBuddy.SetActive (true);
				iTween.ScaleTo (rightBuddy, iTween.Hash ("scale", new Vector3 (.01f, .01f, .01f), "time", .125, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "changeRightTexture", "onCompleteTarget", gameObject));

				rightMaterial.mainTexture = characterTextures [rightNumber];
				StartCoroutine(changeRightTexture());

				iTween.ScaleTo (rightBuddy, iTween.Hash ("scale", new Vector3 (1f, 1f, 1f), "time", .5, "easeType", iTween.EaseType.easeOutElastic, "delay", .125f));

			
			}
		}
	}

	public IEnumerator changeLeftTexture(){
		yield return new WaitForSeconds (.125f);
//		leftMaterial.mainTexture = characterTextures[leftNumber];
		checkRequiredObjectsLeft (leftNumber);
		checkHatLeft (leftNumber);

	}

	public IEnumerator changeRightTexture(){
		yield return new WaitForSeconds (.125f);

//		rightMaterial.mainTexture = characterTextures[rightNumber];
		checkRequiredObjectsRight (rightNumber);
		checkHatRight (rightNumber);

	}


	public void checkRequiredObjectsLeft(int characterint){
		
		
		GameObject requiredBody = GameObject.Find ("RequiredBody_L");
		GameObject theCape =GameObject.Find ("cape_L");
		if (requiredBody == null || theCape == null)
			return;
		for(int i = 0; i < requiredBody.transform.childCount; i++)
		{
			requiredBody.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
			requiredBody.transform.GetChild(i).gameObject.GetComponent<Cloth>().enabled = false;
			
		}
		if(characterTextures[characterint].name == "SIDEKICK_BUDDY"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.yellow);
		}
		if(characterTextures[characterint].name == "superhawkbuddy"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.black);
			
		}
		if(characterTextures[characterint].name == "EvilEmperor"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.black);
			
		}
		if(characterTextures[characterint].name == "SuperGasBuddy"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.red);
			
		}
		if(characterTextures[characterint].name == "ThunderBuddy"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.red);
			
		}

		if(characterTextures[characterint].name == "ThunderBuddy"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.red);
			
		}

		if(characterTextures[characterint].name == "SuperGoldBuddy"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",Color.yellow);
			
		}

		if(characterTextures[characterint].name == "Super_Magnet_Man"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeLeft.SetColor("_Diffusecolor",new Color(0.3f,0f,0.5f));
			
		}


		
		
		GameObject requiredHead = GameObject.Find ("RequiredHead_L");
		GameObject theMask;
		for(int j=  0; j <  requiredHead.transform.childCount; j++)
		{
			requiredHead.transform.GetChild(j).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		//makes sure that if a hat is selected doesnt add jason mask and robomask
		
		if(characterTextures[characterint].name == "johnson"){
			 theMask =GameObject.Find ("JohnsonMask_L");
			
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[characterint].name  == "robotBuddy"){
			 theMask =GameObject.Find ("robothelmet_L");
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}
		
		
		
		
		
		
	}
	public void checkHatLeft(int charact){
		//for(var i : int = 2 ; i < totalObjSize  ; i++)
		GameObject mainCharacter_Hat_Parent = GameObject.Find("Hat_jnt_L");
		GameObject mainCharacter_Hat ;

		if (mainCharacter_Hat_Parent == null)
			return;

		for(int i = 0; i < mainCharacter_Hat_Parent.transform.childCount; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild(i).gameObject.SetActive(true);
			if(mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>()){
				mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
			}
		}
		
		if(characterTextures[charact].name == "SimplePeople_RoadWorker_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Helmet_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(characterTextures[charact].name == "SimplePeople_Punk_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_L");
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
//		if(characterTextures[charact].name == "princess"){
//			
//			mainCharacter_Hat = GameObject.Find ("Hair_Princess_L");
//			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
//		}

		if(characterTextures[charact].name == "SimplePeople_Pimp_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Pimp_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Policeman_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Police_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_RiotCop_Brown"){
			
			mainCharacter_Hat = GameObject.Find ("RiotHelmet_L");
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_FireFighter_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_FireFighter_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_StreetMan_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_Backwards_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Redneck_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Hobo_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Robber_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Sheriff_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "tom"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(characterTextures[charact].name == "PlumberBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "CarpenterBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "BMXBillyBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "mrbaseball"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_L");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		
		
		
		
	}


	public void checkRequiredObjectsRight(int characterint){
		
		
		GameObject requiredBody = GameObject.Find ("RequiredBody_R");
		GameObject theCape =GameObject.Find ("cape_R");
		if (requiredBody == null || theCape == null)
			return;
		for(int i = 0; i < requiredBody.transform.childCount; i++)
		{
			requiredBody.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
			requiredBody.transform.GetChild(i).gameObject.GetComponent<Cloth>().enabled = false;
			
		}
		if(characterTextures[characterint].name == "SIDEKICK_BUDDY"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",Color.yellow);
		}
		if(characterTextures[characterint].name == "superhawkbuddy"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",Color.black);
			
		}
		if(characterTextures[characterint].name == "EvilEmperor"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",Color.black);
			
		}
		if(characterTextures[characterint].name == "SuperGasBuddy"){
			//			var theCape =GameObject.Find ("cape_L");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",Color.red);
			
		}
		if(characterTextures[characterint].name == "ThunderBuddy"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",Color.red);
			
		}
		if(characterTextures[characterint].name == "SuperGoldBuddy"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",Color.yellow);
			
		}
		if(characterTextures[characterint].name == "Super_Magnet_Man"){
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeRight.SetColor("_Diffusecolor",new Color(0.3f,0f,0.5f));
			
		}
		
		
		GameObject requiredHead = GameObject.Find ("RequiredHead_R");
		GameObject theMask;
		for(int j=  0; j <  requiredHead.transform.childCount; j++)
		{
			requiredHead.transform.GetChild(j).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		//makes sure that if a hat is selected doesnt add jason mask and robomask
		
		if(characterTextures[characterint].name == "johnson"){
			theMask =GameObject.Find ("JohnsonMask_R");
			
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[characterint].name  == "robotBuddy"){
			theMask =GameObject.Find ("robothelmet_R");
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}
		
		
		
		
		
		
	}
	public void checkHatRight(int charact){
		//for(var i : int = 2 ; i < totalObjSize  ; i++)
		GameObject mainCharacter_Hat_Parent = GameObject.Find("Hat_jnt_R");
		GameObject mainCharacter_Hat ;
		if (mainCharacter_Hat_Parent == null)
			return;

		for(int i = 0; i < mainCharacter_Hat_Parent.transform.childCount; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild(i).gameObject.SetActive(true);
			if(mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>()){
				mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
			}
		}
		
		if(characterTextures[charact].name == "SimplePeople_RoadWorker_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Helmet_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(characterTextures[charact].name == "SimplePeople_Punk_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_R");
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
//		if(characterTextures[charact].name == "princess"){
//			
//			mainCharacter_Hat = GameObject.Find ("Hair_Princess_R");
//			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
//		}
		if(characterTextures[charact].name == "SimplePeople_Pimp_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Pimp_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Policeman_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Police_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_RiotCop_Brown"){
			
			mainCharacter_Hat = GameObject.Find ("RiotHelmet_R");
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_FireFighter_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_FireFighter_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_StreetMan_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_Backwards_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Redneck_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Hobo_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Robber_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Sheriff_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "tom"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(characterTextures[charact].name == "PlumberBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "CarpenterBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "BMXBillyBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "mrbaseball"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_R");
			
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		
		
		
		
	}




}
