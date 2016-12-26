#pragma strict
import UnityEngine.UI;
var playBttn:RectTransform;
var closeBttn:RectTransform;
var congratsTXT:RectTransform;
var isItNewGameObject:GameObject;

var pointsGift:GameObject;

var  characterName:GameObject;
var FreeItemCamUI:GameObject;
var FreeItem_UI:GameObject;
var NameTxt:Text;
var MainUI:GameObject;
var creditsTXT:Text;
var FreeCharacterMaterial:Material;
var characterTextures:Texture[];
var randomItem :int;
var bestChar:int[];
var avgChar:int[];
private var randomCharac :int;
private var randomHat :int;
private var randomHand :int;
private var randomShoe :int;
var freecharacterName :String[];
var freehatName :String[];
var freefeetName :String[];
var freehandName :String[];


private var randPerc:int;
var handMesh:Mesh[];
var shoeMesh:Mesh[];
var hatMesh:Mesh[];

var propsMaterial:Material[];
var handmetalMaterial:Material[];
var saberMaterial:Material[];
var saberBlueMaterial:Material[];
var crownMaterial:Material[];
var hawkMaterial:Material[];
var darkhelmetMaterial:Material[];
var knightsMaterial:Material[];
var gasMaterial:Material[];
var patriotShieldMaterial:Material[];


var capeMaterial:Material;
var numberOfCycles:int;
var cycleSpeed:float;
var inMenuGiveAwayBttn:GameObject;
var inMenuShareBttn:GameObject;
var cycleAudio:AudioClip;

var popUpController:GameObject;
//var ScaleAnim:GameObject;
//var ScaleScript : ScaleAnimCharac;

function Start () {
// var csScript: ScaleAnimCharac = GetComponent(ScaleAnimCharac);
//characterTextures = ScaleAnimCharac.characterTexturePassthrough;
}
//receiving textures from ScaleAnimCharac
function receiveTextures ( me:Texture[]){
characterTextures =me;
popUpController.SendMessage("receiveTextures", me);
}

//receiving strings from ScaleAnimCharac
function receiveCharacNameStrings(charcName:String[]){
freecharacterName =charcName;
popUpController.SendMessage("receiveCharacNameStrings", charcName);


}
//receiving strings from ScaleAnimCharac
function receiveFeetNameStrings(feetName:String[]){
freefeetName =feetName;
popUpController.SendMessage("receiveFeetNameStrings",feetName);

}
//receiving strings from ScaleAnimCharac
function receiveHatNameStrings(hatName:String[]){
freehatName  =hatName;
popUpController.SendMessage("receiveHatNameStrings", hatName);

}
//receiving strings from ScaleAnimCharac
function receiveHandNameStrings(handName:String[]){
freehandName = handName;
popUpController.SendMessage("receiveHandNameStrings",handName);

}

function Update () {
//if(Input.GetKeyUp(KeyCode.F)){
//randomFreeItemFinal();
//}
//if(Input.GetKeyUp(KeyCode.M)){
//PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")+100);
//creditsTXT.text = PlayerPrefs.GetInt("myCredits").ToString("f0");
//}

}

function randomFreeItem(){
if(PlayerPrefs.GetInt("myCredits")<100)

return;


randomItem = 30;
randPerc = Random.Range(0,100);
for(var i : int = 0; i < randomItem  ; i++)
{
randPerc = Random.Range(0,100);

//CHARACTER
if(randPerc < 70){
transform.GetChild(0).gameObject.SetActive(true);
transform.GetChild(1).gameObject.SetActive(false);
transform.GetChild(2).gameObject.SetActive(false);
transform.GetChild(3).gameObject.SetActive(false);
randomCharac = Random.Range(0,characterTextures.Length);
FreeCharacterMaterial.mainTexture = characterTextures[randomCharac];

}
//HANDS
if(randPerc >= 71 && randPerc <= 80){
transform.GetChild(0).gameObject.SetActive(false);
transform.GetChild(1).gameObject.SetActive(true);
transform.GetChild(2).gameObject.SetActive(false);
transform.GetChild(3).gameObject.SetActive(false);

randomHand = Random.Range(0,handMesh.Length);
transform.GetChild(1).GetComponent.<MeshFilter>().mesh = handMesh[randomHand];


}
//SHOES
if(randPerc >= 81 && randPerc <= 90){
transform.GetChild(0).gameObject.SetActive(false);
transform.GetChild(1).gameObject.SetActive(false);
transform.GetChild(2).gameObject.SetActive(true);
transform.GetChild(3).gameObject.SetActive(false);

randomShoe = Random.Range(0,shoeMesh.Length);
transform.GetChild(2).GetComponent.<MeshFilter>().mesh = shoeMesh[randomShoe];

}
//HATS
if(randPerc >= 91 && randPerc <= 100){
transform.GetChild(0).gameObject.SetActive(false);
transform.GetChild(1).gameObject.SetActive(false);
transform.GetChild(2).gameObject.SetActive(false);
transform.GetChild(3).gameObject.SetActive(true);

//start at 1 because the santa hat is free
randomHat = Random.Range(1,hatMesh.Length);
transform.GetChild(3).GetComponent.<MeshFilter>().mesh = hatMesh[randomHat];
}

 yield WaitForSeconds(.2);

}

PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")-100);
creditsTXT.text = PlayerPrefs.GetInt("myCredits").ToString("f0");

}






public function randomFreeItemFinal(){

//gives ability in free giveaway menu to run through this again.
if(PlayerPrefs.GetInt("myCredits")<100){
inMenuGiveAwayBttn.SetActive(false);
}
else{
inMenuGiveAwayBttn.SetActive(true);

}

var totalObjSize:int;
totalObjSize = (characterTextures.Length-1) + handMesh.Length + shoeMesh.Length + hatMesh.Length;

GetComponent.<Camera>().enabled=true;
FreeItemCamUI.SetActive(true);
FreeItem_UI.SetActive(true);
characterName.SetActive (true);
pointsGift.SetActive (false);
inMenuShareBttn.SetActive(true);
//inMenuGiveAwayBttn.SetActive(true);

playBttn.gameObject.transform.GetChild(0).GetComponent.<Button>().enabled =false;
playBttn.gameObject.transform.GetChild(0).GetComponent.<Image>().enabled =false;
closeBttn.gameObject.transform.GetChild(0).GetComponent.<Button>().enabled =false;
closeBttn.gameObject.transform.GetChild(0).GetComponent.<Image>().enabled =false;



playBttn.gameObject.GetComponent.<Button>().enabled =true;
playBttn.gameObject.GetComponent.<Image>().enabled =true;
closeBttn.gameObject.GetComponent.<Button>().enabled =true;
closeBttn.gameObject.GetComponent.<Image>().enabled =true;


//MainUI.SetActive(false);
randPerc = Random.Range(0,100);
//NEEDS TO BE 2 TO SKIP FREE CHARACTERS...CHARACTER TEXTURES BEING RECEIVED IN FROM SCALEANIMCHARAC
for(var i : int = 2 ; i < numberOfCycles  ; i++)
{
randPerc = Random.Range(0,100);


//characters
if(randPerc < 70){




transform.GetChild(0).gameObject.SetActive(true);
transform.GetChild(1).gameObject.SetActive(false);
transform.GetChild(2).gameObject.SetActive(false);
transform.GetChild(3).gameObject.SetActive(false);
var randomBestChar = Random.Range(0,100);
if(randomBestChar<=PlayerPrefs.GetInt("ranChar")){
var randomBest = Random.Range(0,bestChar.Length);
randomCharac = bestChar[randomBest];
}
else{
var randomAvg = Random.Range(0,avgChar.Length);
randomCharac = avgChar[randomAvg];
}
NameTxt.text = freecharacterName[randomCharac];
checkHat(randomCharac);
checkRequiredObjects(randomCharac);
FreeCharacterMaterial.mainTexture = characterTextures[randomCharac];


}

//HANDS
if(randPerc >= 71 && randPerc <= 80){
randomHand = Random.Range(0,handMesh.Length);

NameTxt.text = freehandName[randomHand];

transform.GetChild(0).gameObject.SetActive(false);
transform.GetChild(1).gameObject.SetActive(true);
transform.GetChild(2).gameObject.SetActive(false);
transform.GetChild(3).gameObject.SetActive(false);


transform.GetChild(1).GetComponent.<MeshFilter>().mesh = handMesh[randomHand];
if(randomHand == 6 || randomHand== 5||randomHand== 9 ||randomHand== 12 ||randomHand== 13||randomHand== 14||randomHand== 15||randomHand== 16||randomHand== 17||randomHand== 18||randomHand== 19||randomHand== 20){
//properly positions stuff
transform.GetChild(1).localEulerAngles.x = 270f;


if(randomHand == 6 || randomHand== 5||randomHand== 9||randomHand== 17){
transform.GetChild(1).localPosition.y = -1.1f;
transform.GetChild(1).localEulerAngles.y = 80.7f;
}
else{
transform.GetChild(1).localPosition.y = -.38f;
transform.GetChild(1).localEulerAngles.y =215.56f;


//gun
if(randomHand== 15){
transform.GetChild(1).localPosition.y = -.7f;
transform.GetChild(1).localEulerAngles.y =173.16f;
}
//yeti hand
if(randomHand== 16||randomHand == 20 ||randomHand ==18||randomHand ==19){
transform.GetChild(1).localPosition.y = -.2f;
transform.GetChild(1).localEulerAngles.y =248.56f;
}
//wand
if(randomHand== 14){
transform.GetChild(1).localEulerAngles.x = 0f;

transform.GetChild(1).localPosition.y =  -.7f;
transform.GetChild(1).localEulerAngles.y =90f;
}
//butcher
if(randomHand== 13){
transform.GetChild(1).localEulerAngles.x = 0f;

transform.GetChild(1).localPosition.y = -1.1f;
transform.GetChild(1).localEulerAngles.y =242.3f;
}



}

transform.GetChild(1).GetComponent.<MeshRenderer>().materials= handmetalMaterial;

}

else{
transform.GetChild(1).localPosition.y = -1.1f;
transform.GetChild(1).localEulerAngles.y = 80.7f;
transform.GetChild(1).localEulerAngles.x = 270f;

transform.GetChild(1).GetComponent.<MeshRenderer>().materials= propsMaterial;
if(randomHand == 8){
transform.GetChild(1).localPosition.y = -1.1f;
transform.GetChild(1).localEulerAngles.y = 80.7f;
transform.GetChild(1).GetComponent.<MeshRenderer>().materials= saberMaterial;

}
if(randomHand==10){
transform.GetChild(1).GetComponent.<MeshRenderer>().materials= patriotShieldMaterial;
transform.GetChild(1).localPosition.y = -.35f;
transform.GetChild(1).localEulerAngles.y = 296.08f;
}
if(randomHand == 11){
transform.GetChild(1).localPosition.y = -1.1f;
transform.GetChild(1).localEulerAngles.y = 80.7f;
transform.GetChild(1).GetComponent.<MeshRenderer>().materials= saberBlueMaterial;

}



}


}

//SHOES
if(randPerc >= 81 && randPerc <= 90){
randomShoe= Random.Range(0,shoeMesh.Length);

NameTxt.text = freefeetName[randomShoe];

transform.GetChild(0).gameObject.SetActive(false);
transform.GetChild(1).gameObject.SetActive(false);
transform.GetChild(2).gameObject.SetActive(true);
transform.GetChild(3).gameObject.SetActive(false);

transform.GetChild(2).GetComponent.<MeshFilter>().mesh = shoeMesh[randomShoe];

}


//HATS
if(randPerc >= 91 && randPerc <= 100){
//start at 1 because the santa hat is free

randomHat= Random.Range(1,hatMesh.Length);

NameTxt.text = freehatName[randomHat];

transform.GetChild(0).gameObject.SetActive(false);
transform.GetChild(1).gameObject.SetActive(false);
transform.GetChild(2).gameObject.SetActive(false);
transform.GetChild(3).gameObject.SetActive(true);

transform.GetChild(3).GetComponent.<MeshFilter>().mesh = hatMesh[randomHat];
//changes material during randomizing
if(randomHat == 4){
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= crownMaterial;

}

else{
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= propsMaterial;

if(randomHat == 7){
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= gasMaterial;

}

if(randomHat == 8){
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= knightsMaterial;

}


if(randomHat == 11){
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= hawkMaterial;

}
if(randomHat== 12){
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= darkhelmetMaterial;

}
if(randomHat== 13){
transform.GetChild(3).GetComponent.<MeshRenderer>().materials= patriotShieldMaterial;

}
}

}
GetComponent.<AudioSource>().PlayOneShot(cycleAudio,0.5);

 yield WaitForSeconds(cycleSpeed);

}
//GIVE PURCHASE AWAY 

//characters
if(randPerc < 70){
if(PlayerPrefs.HasKey(randomCharac.ToString("f0"))==true){
isItNewGameObject.SetActive(false);

}
else{
isItNewGameObject.SetActive(true);
}

PlayerPrefs.SetInt(randomCharac.ToString("f0"),1);


}
//HANDS
if(randPerc >= 71 && randPerc <= 80){

if(PlayerPrefs.HasKey("hand"+ randomHand)==true){
isItNewGameObject.SetActive(false);

}
else{
isItNewGameObject.SetActive(true);
}

PlayerPrefs.SetInt("hand"+ randomHand,1);

}
//SHOES
if(randPerc >= 81 && randPerc <= 90){

if(PlayerPrefs.HasKey("shoe"+ randomShoe)==true){
isItNewGameObject.SetActive(false);

}
else{
isItNewGameObject.SetActive(true);
}

PlayerPrefs.SetInt("shoe"+ randomShoe,1);

}
//HATS
if(randPerc >= 91 && randPerc <= 100){

if(PlayerPrefs.HasKey("hat"+ randomHat)==true){
isItNewGameObject.SetActive(false);

}
else{
isItNewGameObject.SetActive(true);
}

PlayerPrefs.SetInt("hat"+ randomHat,1);

}
//ANIMATE BUTTONS
LeanTween.move(congratsTXT, Vector3(0,-123.9,0), .2).setEase(LeanTweenType.easeOutSine);
LeanTween.move(closeBttn, Vector3(14.5,-12.2,0), .2).setEase(LeanTweenType.easeOutSine).setDelay(.1);
LeanTween.move(playBttn, Vector3(0,80.7,0), .2).setEase(LeanTweenType.easeOutSine).setDelay(.1);


//subtract 100 credits
PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")-100);
creditsTXT.text = PlayerPrefs.GetInt("myCredits").ToString("f0");

if(PlayerPrefs.GetInt("myCredits")<100){
inMenuGiveAwayBttn.SetActive(false);
}
else{
inMenuGiveAwayBttn.SetActive(true);

}

}

public function animateGUI(){
LeanTween.move(congratsTXT, Vector3(0,-123.9,0), .2).setEase(LeanTweenType.easeOutSine);
LeanTween.move(closeBttn, Vector3(14.5,-12.2,0), .2).setEase(LeanTweenType.easeOutSine).setDelay(.1);
LeanTween.move(playBttn, Vector3(0,80.7,0), .2).setEase(LeanTweenType.easeOutSine).setDelay(.1);
}

public function resetUIPos(){
congratsTXT.localPosition.x = -800;
//itween issue really not working local position this is an offset
closeBttn.position.x = -75;
playBttn.position.y = -260;
}


public function activateCharacter(){
if(randPerc < 70){
PlayerPrefs.SetInt(randomCharac.ToString("f0"),1);
PlayerPrefs.SetInt("Character",randomCharac);
}

//HANDS
if(randPerc >= 71 && randPerc <= 80){
PlayerPrefs.SetInt("hand"+ randomHand,1);
PlayerPrefs.SetInt("Hand",randomHand);

}
//SHOES
if(randPerc >= 81 && randPerc <= 90){
PlayerPrefs.SetInt("shoe"+ randomShoe,1);
PlayerPrefs.SetInt ("Shoe",randomShoe);
}
//HATS
if(randPerc >= 91 && randPerc <= 100){

PlayerPrefs.SetInt("hat"+ randomHat,1);
PlayerPrefs.SetInt("Hat",randomHat);


}
}


public function checkHat(charact:int){
//for(var i : int = 2 ; i < totalObjSize  ; i++)
var mainCharacter_Hat_Parent = GameObject.Find("Hat_jnt_F");
var mainCharacter_Hat :GameObject;
		for(var i:int = 0; i < mainCharacter_Hat_Parent.transform.childCount; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild(i).gameObject.SetActive(true);
			if(mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent.<MeshRenderer>()){
			mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent.<MeshRenderer>().enabled = false;
			}
		}
		
		if(characterTextures[charact].name == "SimplePeople_RoadWorker_White"){
		
			mainCharacter_Hat = GameObject.Find ("Hat_Helmet_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}

		if(characterTextures[charact].name == "SimplePeople_Punk_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_F");
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "princess"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Princess_F");
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
			if(characterTextures[charact].name == "Marley_Finn"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Harley_F");
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		
		
		if(characterTextures[charact].name == "SimplePeople_Pimp_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Pimp_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Policeman_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Police_F");
	
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_RiotCop_Brown"){
			
			mainCharacter_Hat = GameObject.Find ("RiotHelmet_F");
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_FireFighter_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_FireFighter_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_StreetMan_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_Backwards_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Redneck_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Hobo_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Robber_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_F");
	
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Sheriff_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "tom"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}

		if(characterTextures[charact].name == "PlumberBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "CarpenterBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "BMXBillyBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_F");
		
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "mrbaseball"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_F");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
	
		
		

	}
	
	
		public function checkRequiredObjects(characterint:int){


		var requiredBody = GameObject.Find ("RequiredBody_F");
		var theCape =GameObject.Find ("cape_F");

		for(var i:int = 0; i < requiredBody.transform.childCount; i++)
		{
			requiredBody.transform.GetChild(i).gameObject.GetComponent.<SkinnedMeshRenderer>().enabled = false;
			requiredBody.transform.GetChild(i).gameObject.GetComponent.<Cloth>().enabled = false;

		}
		if(characterTextures[characterint].name == "SIDEKICK_BUDDY"){
//			var theCape =GameObject.Find ("cape_F");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.yellow);
		}
		if(characterTextures[characterint].name == "superhawkbuddy"){
//			var theCape =GameObject.Find ("cape_F");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);

		}
		if(characterTextures[characterint].name == "EvilEmperor"){
//			var theCape =GameObject.Find ("cape_F");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);
			
		}
		if(characterTextures[characterint].name == "SuperGasBuddy"){
//			var theCape =GameObject.Find ("cape_F");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.red);

		}
		if(characterTextures[characterint].name == "ThunderBuddy"){
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.red);
			
		}
			if(characterTextures[characterint].name == "Super_Magnet_Man"){
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",new Color(0.3f,0f,0.5f));
			
		}
	

		var requiredHead = GameObject.Find ("RequiredHead_F");

		for(var j:int = 0; j <  requiredHead.transform.childCount; j++)
		{
			requiredHead.transform.GetChild(j).gameObject.GetComponent.<MeshRenderer>().enabled = false;
		}
		//makes sure that if a hat is selected doesnt add jason mask and robomask

		if(characterTextures[characterint].name == "johnson"){
				var theMask =GameObject.Find ("JohnsonMask_F");

			theMask.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[characterint].name  == "robotBuddy"){
			var theMask2 =GameObject.Find ("robothelmet_F");
			theMask2.GetComponent.<MeshRenderer>().enabled = true;
		}
		
		
		
		
		
		
	}
	
		

	
		
