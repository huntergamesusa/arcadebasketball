#pragma strict


import UnityEngine.UI;
 import System.Collections.Generic;

public var cameraCanvas:Camera;

private var rangeShowBuyGuyBar:int;
private var rangeShowBuyGuyBarInt:int;
private var isallowedtoshowBuyGuy:boolean=false;

public var arrayNotPurchasedCharacters : List.<int> = new List.<int>();
public var rareHolder : List.<int> = new List.<int>();
var isRare:boolean;
var isShowingMissions:boolean;
public var arrayNotPurchasedHats : List.<int> = new List.<int>();
public var arrayNotPurchasedHands : List.<int> = new List.<int>();
public var arrayNotPurchasedFeet : List.<int> = new List.<int>();

public var  buyGuyTxt : Text;
public var pricingReceiver:GameObject;

private var randomPropCharact :int;

private var randomPropCharactArray : List.<String> = new List.<String>();


var isGameOver:boolean;
var buyguy:GameObject;
var buyHatsB:GameObject;
var buyHandsB:GameObject;
var buyFeetB:GameObject;

var adsNate : GameObject;

var woosh : AudioClip;

var myGameObject1 : RectTransform;
var myGameObject2 : RectTransform;
var myGameObject3 : RectTransform;
var myGameObject4 : RectTransform;

var MissionTXTParent : RectTransform;



var myGameObjectMission : RectTransform;
var missionInt:int;

var myGameObjectReplay : RectTransform;
var myGameObjectMenu : RectTransform;
var myGameObjectSettings : RectTransform;

var myPicture : RectTransform;

var myTime : float;
var onoff : boolean;
var go1Delay : float;
var go2Delay : float;
var go3Delay : float;
var go4Delay : float;
var goButtonsDelay : float;
var goSettingsDelay : float;

var go1EndPosition : Vector3;
var go2EndPosition : Vector3;
var go3EndPosition : Vector2;
var goMissionEndPosition:Vector3;
var go4EndPosition : Vector3;

var goSettingdPosition : Vector3;
var goReplayPos : Vector3;
var goMenuPos : Vector3;
var goSettingsPos : Vector3;
var goPicturePos : Vector3;

private var go1StartPosition : Vector3;
private var go2StartPosition : Vector3;
private var go3StartPosition : Vector2;
private var go4StartPosition : Vector3;
private var goMissionStartPosition : Vector3;

var missionComplete:boolean;

private var StartReplayPos : Vector3;
private var startMenuPos : Vector3;
private var startSettingsPos : Vector3;

 var startPicturePos : Vector3;

var myEase : String;

var characterTextures:Texture[];

private var randomCharac :int;
private var randomHat :int;
private var randomHand :int;
private var randomShoe :int;
var freecharacterName :String[];
var freehatName :String[];
var freefeetName :String[];
var freehandName :String[];
var handMesh:Mesh[];
var shoeMesh:Mesh[];
var hatMesh:Mesh[];

var freecharacterNameUI :Text;


//var propsMaterial:Material[];
//var handmetalMaterial:Material[];
//var saberMaterial:Material[];
//var saberBlueMaterial:Material[];
//var crownMaterial:Material[];
//var hawkMaterial:Material[];
//var darkhelmetMaterial:Material[];
//var knightsMaterial:Material[];
//var gasMaterial:Material[];
//var patriotShieldMaterial:Material[];
var capeMaterial:Material;
var buyguyMaterial:Material;

//
//LeanTweenType.easeInSine
//
//
//

function receiveTextures ( me:Texture[]){
characterTextures =me;
}

//receiving strings from ScaleAnimCharac
function receiveCharacNameStrings(charcName:String[]){
freecharacterName =charcName;

}
//receiving strings from ScaleAnimCharac
function receiveFeetNameStrings(feetName:String[]){
freefeetName =feetName;
}
//receiving strings from ScaleAnimCharac
function receiveHatNameStrings(hatName:String[]){
freehatName  =hatName;
}
//receiving strings from ScaleAnimCharac
function receiveHandNameStrings(handName:String[]){
freehandName = handName;
}


function Awake () {
rangeShowBuyGuyBarInt = 0;
rangeShowBuyGuyBar = Random.Range (PlayerPrefs.GetInt("buyGuyMin"),PlayerPrefs.GetInt("buyGuyMax"));
go1StartPosition=myGameObject1.position;
go2StartPosition=myGameObject2.position;
go3StartPosition=Vector2(-900,25.55);
go4StartPosition=myGameObject4.position;
goMissionEndPosition =Vector2(-900,25.55);

StartReplayPos=myGameObjectReplay.position;
startMenuPos=myGameObjectMenu.position;
startSettingsPos = myGameObjectSettings.position;
startPicturePos = myPicture.localPosition;

		// Make the game run as fast as possible in the web player
//		Application.targetFrameRate = 60;
		
//		slideMenu1();			
}



public function resetMenu1(){
isGameOver=false;
LeanTween.cancel(MissionTXTParent.GetChild(0).gameObject);
LeanTween.cancel(MissionTXTParent.GetChild(1).gameObject);
LeanTween.cancel(MissionTXTParent.GetChild(2).gameObject);
isShowingMissions =false;
missionInt =0;
myGameObject1.position = go1StartPosition;
myGameObject2.position=go2StartPosition;
myGameObject3.localPosition=Vector3(-900f,25.55,0);
myGameObject4.position=go4StartPosition;
myGameObjectMission.localPosition = Vector3(-900f,25.55f,0);
MissionTXTParent.GetChild(0).localPosition  = new Vector3(0,0,0);
MissionTXTParent.GetChild(1).localPosition  = new Vector3(-800,0,0);
MissionTXTParent.GetChild(2).localPosition  = new Vector3(-800,0,0);


myGameObjectReplay.position=StartReplayPos;
myGameObjectMenu.position=startMenuPos;
myGameObjectSettings.position = startSettingsPos;
myPicture.position = startPicturePos;
}



public function scaleupPicture(){
		LeanTween.move(myPicture, goPicturePos, myTime).setEase(LeanTweenType.easeOutSine);
	LeanTween.scale(myPicture, new Vector3(1.25f,1.25f,1.25f), myTime).setEase(LeanTweenType.easeOutSine);

}
public function scaledownPicture(){
	LeanTween.move(myPicture, startPicturePos, myTime).setEase(LeanTweenType.easeOutSine);
	LeanTween.scale(myPicture, new Vector3(0,0,0), myTime).setEase(LeanTweenType.easeOutSine);

}



function slideMenu1 () {
//check to slide video if less than 100 credits if greater than show win a prize
isGameOver =true;
isallowedtoshowBuyGuy =false;
var timeReward = GameObject.Find("TimeReward");


rangeShowBuyGuyBarInt++;
//CHANGES EARN, GIFT, FREE ITEM
enablebuyguycam(true);

timeReward.SendMessage("checkGiftReady");

timeReward.SendMessage("gameEndChangeButtons");

timeReward.SendMessage("checkTimeLeft");

garbageCollect();

	LeanTween.move(myGameObject1, go1EndPosition, myTime).setEase(LeanTweenType.easeOutSine).setDelay(go1Delay);
		
	
	LeanTween.move(myGameObject2, go2EndPosition, myTime).setEase(LeanTweenType.easeOutSine).setDelay(go2Delay);
	


LeanTween.move(myGameObject4, go4EndPosition, myTime).setEase(LeanTweenType.easeOutSine).setDelay(go4Delay);

if(!missionComplete){
		///RANDOM TO SHOW BUY GUY DONT WANT TO SHOW ALL THE TIME
		if(rangeShowBuyGuyBarInt >= rangeShowBuyGuyBar){
	if(arrayNotPurchasedCharacters.Count >0 || arrayNotPurchasedHands.Count>0||arrayNotPurchasedHats.Count>0 || arrayNotPurchasedFeet.Count>0){
isShowingMissions =false;
	//buy guy is 3
	isallowedtoshowBuyGuy =true;
	LeanTween.move(myGameObject3, go3EndPosition, myTime).setEase(LeanTweenType.easeOutSine).setDelay(go3Delay);
}
}
else{
//show missions
isShowingMissions =true;
LeanTween.move(myGameObjectMission, go3EndPosition, myTime).setEase(LeanTweenType.easeOutSine).setDelay(go3Delay);

}
}
else{
isShowingMissions =true;
LeanTween.move(myGameObjectMission, go3EndPosition, myTime).setEase(LeanTweenType.easeOutSine).setDelay(go3Delay);

}

	LeanTween.move(myGameObjectReplay, goReplayPos, myTime).setEase(LeanTweenType.easeOutSine).setDelay(goButtonsDelay);
	LeanTween.move(myGameObjectMenu, goMenuPos, myTime).setEase(LeanTweenType.easeOutSine).setDelay(goButtonsDelay);
		LeanTween.move(myGameObjectSettings, goSettingsPos, myTime).setEase(LeanTweenType.easeOutSine).setDelay(goSettingsDelay);

yield WaitForSeconds(go1Delay);
GetComponent.<AudioSource>().pitch = 1;
GetComponent.<AudioSource>().PlayOneShot(woosh,.75);

yield WaitForSeconds(go2Delay-go1Delay);
GetComponent.<AudioSource>().pitch = .8;
GetComponent.<AudioSource>().PlayOneShot(woosh,.55);

yield WaitForSeconds(go3Delay-go2Delay);

if(isallowedtoshowBuyGuy){
GetComponent.<AudioSource>().pitch = 1.1;
GetComponent.<AudioSource>().PlayOneShot(woosh,.75);
}
yield WaitForSeconds(go4Delay-go3Delay);

GetComponent.<AudioSource>().pitch = .7;
GetComponent.<AudioSource>().PlayOneShot(woosh,.75);

yield WaitForSeconds(goButtonsDelay-go4Delay);
GetComponent.<AudioSource>().pitch = .9;
GetComponent.<AudioSource>().PlayOneShot(woosh,.75);

if(rangeShowBuyGuyBarInt >= rangeShowBuyGuyBar){
rangeShowBuyGuyBarInt=0;
rangeShowBuyGuyBar = Random.Range (PlayerPrefs.GetInt("buyGuyMin"),PlayerPrefs.GetInt("buyGuyMax"));
enablebuyguy();
}

yield WaitForSeconds(goSettingsDelay-goButtonsDelay);
GetComponent.<AudioSource>().pitch = .1;
GetComponent.<AudioSource>().PlayOneShot(woosh,.75);
if(isShowingMissions){
animateMissions();
}
//Call for ads
adsNate.SendMessage("cacheAd");
adsNate.SendMessage("loadInterstitial");

}

public function animateMissions(){

if(missionInt>=0){
LeanTween.move(MissionTXTParent.GetChild(missionInt).GetComponent.<RectTransform>(), new Vector3(800,0,0), myTime).setEase(LeanTweenType.easeOutSine).setDelay(goSettingsDelay+.9f).setOnComplete(increaseMission);
}
else{
increaseMission();
}
}


public function increaseMission(){
if(missionInt>=0){
MissionTXTParent.GetChild(missionInt).localPosition  = new Vector3(-800,0,0);

}


missionInt++;

LeanTween.move(MissionTXTParent.GetChild(missionInt).GetComponent.<RectTransform>(), new Vector3(0,0,0), myTime).setEase(LeanTweenType.easeOutSine);
animateMissions();
if(missionInt>=2){
MissionTXTParent.GetChild(2).localPosition  = new Vector3(-800,0,0);
missionInt =-1;
}


}

public function garbageCollect(){

System.GC.Collect();
}


public function toggleCamera(mybool:boolean){

buyguy.SetActive(mybool);
var freeCam = GameObject.Find("FreeItemCam");
freeCam.GetComponent.<Camera>().enabled = mybool;
cameraCanvas.enabled = mybool;
}



public function enablebuyguycam(mybool:boolean){
buyguy.SetActive(mybool);
var freeCam = GameObject.Find("FreeItemCam");
freeCam.GetComponent.<Camera>().enabled = mybool;
cameraCanvas.enabled = mybool;


randomPropCharactArray.Clear();
randomPropCharactArray.Add("character");
randomPropCharactArray.Add("hats");
randomPropCharactArray.Add("hands");
randomPropCharactArray.Add("feet");
//print("array item generated: "+ randomPropCharactArray[0]);

//print("remaining in array before: "+randomPropCharactArray.Count);

if(arrayNotPurchasedCharacters.Count ==0){
randomPropCharactArray.Remove("character");
}
if(arrayNotPurchasedHats.Count ==0){
randomPropCharactArray.Remove("hats");

}
if(arrayNotPurchasedHands.Count ==0){
randomPropCharactArray.Remove("hands");

}
if(arrayNotPurchasedFeet.Count ==0){
randomPropCharactArray.Remove("feet");

}

//print("remaining in array after: "+randomPropCharactArray.Count);

if(randomPropCharactArray.Count == 0)
return;


//PUTS CHARACTERS FIRST
if(arrayNotPurchasedCharacters.Count ==0){

var randomGiveaway = Random.Range(0,100);
if(randomGiveaway <=PlayerPrefs.GetInt("characterbuyguy")){
randomPropCharact = 0;
}
else{
randomPropCharact = Random.Range(1,randomPropCharactArray.Count);

}
}
else{
randomPropCharact = Random.Range(0,randomPropCharactArray.Count);

}


print("array item generated: "+ randomPropCharactArray[randomPropCharact]);

if(randomPropCharactArray[randomPropCharact] == "character"){
if(arrayNotPurchasedCharacters.Count>0){


if(rareHolder.Count ==0){
isRare=false;
randomCharac=Random.Range(0,arrayNotPurchasedCharacters.Count);
buyguyMaterial.mainTexture = characterTextures[arrayNotPurchasedCharacters[randomCharac]];
freecharacterNameUI.text = freecharacterName[arrayNotPurchasedCharacters[randomCharac]];

}
else{
var randomTop = Random.Range(0,100);
if(randomTop>=(100-PlayerPrefs.GetInt("topChar"))){

randomCharac = Random.Range(0,rareHolder.Count);
buyguyMaterial.mainTexture = characterTextures[rareHolder[randomCharac]];
freecharacterNameUI.text = freecharacterName[rareHolder[randomCharac]];
isRare=true;
}
else{
isRare=false;
randomCharac=Random.Range(0,arrayNotPurchasedCharacters.Count);
buyguyMaterial.mainTexture = characterTextures[arrayNotPurchasedCharacters[randomCharac]];
freecharacterNameUI.text = freecharacterName[arrayNotPurchasedCharacters[randomCharac]];

}
}





pricingReceiver.SendMessage("updateLocalizedPricing",characterTextures[arrayNotPurchasedCharacters[randomCharac]].name);

}
}



//HATS
if(randomPropCharactArray[randomPropCharact] == "hats"){
if(arrayNotPurchasedHats.Count > 0){

print("my hat count is" + arrayNotPurchasedHats.Count);

randomCharac=Random.Range(0,arrayNotPurchasedHats.Count);
buyHatsB.transform.GetChild(arrayNotPurchasedHats[randomCharac]).gameObject.SetActive(true);
freecharacterNameUI.text = freehatName[arrayNotPurchasedHats[randomCharac]];
pricingReceiver.SendMessage("updateLocalizedPricing",characterTextures[4].name);

}
}
//HANDS
if(randomPropCharactArray[randomPropCharact] == "hands"){
if(arrayNotPurchasedHands.Count > 0){

randomCharac=Random.Range(0,arrayNotPurchasedHands.Count);
buyHandsB.transform.GetChild(arrayNotPurchasedHands[randomCharac]).gameObject.SetActive(true);
freecharacterNameUI.text = freehandName[arrayNotPurchasedHands[randomCharac]];
pricingReceiver.SendMessage("updateLocalizedPricing",characterTextures[4].name);

}
}
//FEET
if(randomPropCharactArray[randomPropCharact] == "feet"){
if(arrayNotPurchasedFeet.Count >0){
randomCharac=Random.Range(0,arrayNotPurchasedFeet.Count);
buyFeetB.transform.GetChild(arrayNotPurchasedFeet[randomCharac]).gameObject.SetActive(true);
freecharacterNameUI.text = freefeetName[arrayNotPurchasedFeet[randomCharac]];
pricingReceiver.SendMessage("updateLocalizedPricing",characterTextures[4].name);

}
}

}

public function identifyCharactersNotPurchased(){
var myInt:int;
var myhatsInt:int;
var myhandsInt:int;
var myfeetInt:int;

arrayNotPurchasedCharacters.Clear();
arrayNotPurchasedHats.Clear();
arrayNotPurchasedHands.Clear();
arrayNotPurchasedFeet.Clear();
rareHolder.Clear();
for(var i:int = 0; i < characterTextures.Length; i++){
if(PlayerPrefs.GetInt(i.ToString("f0")) == 0 || PlayerPrefs.HasKey(i.ToString("f0")) == null){
arrayNotPurchasedCharacters.Add(i);

if(i == 50||i == 42||i == 35||i == 45||i == 47||i == 48||i == 37||i == 38||i == 5||i == 19||i == 41||i == 21||i == 36||i == 30||i == 61||i == 32||i == 65||i == 64||i == 66||i == 67||i == 68){
rareHolder.Add(i);
}

//arrayNotPurchasedCharacters[myInt] = i;
//print("I dont have this: " + i);
myInt++;
}
}

for(var j:int = 0; j < freehatName.Length; j++){
if(PlayerPrefs.GetInt("hat"+j.ToString("f0")) == 0 || PlayerPrefs.HasKey("hat"+j.ToString("f0")) == null){
arrayNotPurchasedHats.Add(j);

//arrayNotPurchasedHats[myhatsInt] = j;
//print("I dont have this hat: " + j);
myhatsInt++;
}
}

for(var k:int = 0; k < freehandName.Length; k++){
if(PlayerPrefs.GetInt("hand"+k.ToString("f0")) == 0 || PlayerPrefs.HasKey("hand"+k.ToString("f0")) == null){

arrayNotPurchasedHands.Add(k);

//arrayNotPurchasedHands[myhandsInt] = k;
//print("I dont have this hand: " + k);
myhandsInt++;
}
}

for(var l:int = 0; l < freefeetName.Length; l++){
if(PlayerPrefs.GetInt("shoe"+l.ToString("f0")) == 0 || PlayerPrefs.HasKey("shoe"+l.ToString("f0")) == null){
arrayNotPurchasedFeet.Add(l);


//arrayNotPurchasedFeet[myfeetInt] = l;
//print("I dont have this shoe: " + l);
myfeetInt++;
}
}


}

function Start(){
identifyCharactersNotPurchased();
}



public function actuallyBuyGuy(){
var buyScript = GameObject.Find("CharactersandInappPurchases");

print("made a purchase before: " + randomPropCharactArray[randomPropCharact]);


if(randomPropCharactArray[randomPropCharact] == "character"){
if(isRare){
buyScript.SendMessage("buyItemfromGameOver", rareHolder[randomCharac]);
}
else{
buyScript.SendMessage("buyItemfromGameOver", arrayNotPurchasedCharacters[randomCharac]);
}
print("I JUST BOUGHT A FUCKING CHARACTER");
}
if(randomPropCharactArray[randomPropCharact] == "hats"){
buyScript.SendMessage("buyHatsfromGameOver", arrayNotPurchasedHats[randomCharac]);


}
if(randomPropCharactArray[randomPropCharact] == "hands"){
buyScript.SendMessage("buyHandsfromGameOver", arrayNotPurchasedHands[randomCharac]);

}
if(randomPropCharactArray[randomPropCharact] == "feet"){
buyScript.SendMessage("buyFeetfromGameOver", arrayNotPurchasedFeet[randomCharac]);

}
print("made a purchase after: " + randomPropCharactArray[randomPropCharact]);

}

public function didPurchaseguy(){


}

public function rescalebuyGuy(){
if(!isGameOver||!isallowedtoshowBuyGuy)
return;
var freeCam = GameObject.Find("FreeItemCam");
freeCam.GetComponent.<Camera>().enabled =true;

if(arrayNotPurchasedCharacters.Count>0){

buyguy.transform.GetChild(0).gameObject.SetActive(true);
if(isRare){
checkRequiredObjects(rareHolder[randomCharac]);
checkHat(rareHolder[randomCharac]);
}
else{
checkRequiredObjects(arrayNotPurchasedCharacters[randomCharac]);
checkHat(arrayNotPurchasedCharacters[randomCharac]);
}

if(randomPropCharactArray[randomPropCharact] == "character"){
buyguy.transform.GetChild(0).localScale = Vector3(0.2f,0.2f,0.2f);
}
}

if(randomPropCharactArray[randomPropCharact] == "hats"){
if(arrayNotPurchasedHats.Count>0){

buyHatsB.transform.localScale = Vector3(1f,1f,1f);
buyHatsB.transform.GetChild(arrayNotPurchasedHats[randomCharac]).gameObject.SetActive(true);
}
}
if(randomPropCharactArray[randomPropCharact] == "hands"){
if(arrayNotPurchasedHands.Count>0){

buyHandsB.transform.localScale = Vector3(1f,1f,1f);
buyHandsB.transform.GetChild(arrayNotPurchasedHands[randomCharac]).gameObject.SetActive(true);
}
}
if(randomPropCharactArray[randomPropCharact] == "feet"){
if(arrayNotPurchasedFeet.Count>0){

buyFeetB.transform.localScale = Vector3(1f,1f,1f);
buyFeetB.transform.GetChild(arrayNotPurchasedFeet[randomCharac]).gameObject.SetActive(true);
}
}
print("did my scaling");

}

public function enablebuyguy(){

if(randomPropCharactArray.Count == 0)
return;


if(randomPropCharactArray[randomPropCharact] == "character"){
if(arrayNotPurchasedCharacters.Count>0){

buyguy.transform.GetChild(0).gameObject.SetActive(true);
if(isRare){
checkRequiredObjects(rareHolder[randomCharac]);
checkHat(rareHolder[randomCharac]);
}
else{
checkRequiredObjects(arrayNotPurchasedCharacters[randomCharac]);
checkHat(arrayNotPurchasedCharacters[randomCharac]);
}

	LeanTween.scale(buyguy.transform.GetChild(0).gameObject, new Vector3(0.2,0.2,0.2), myTime).setEase(LeanTweenType.easeOutSine);
//print("array count buy guy: " +arrayNotPurchasedCharacters.Count);


}
}
//HATS

if(randomPropCharactArray[randomPropCharact] == "hats"){
if(arrayNotPurchasedHats.Count>0){

	LeanTween.scale(buyHatsB, new Vector3(1,1,1), myTime).setEase(LeanTweenType.easeOutSine);
}
}
//HANDS

if(randomPropCharactArray[randomPropCharact] == "hands"){
if(arrayNotPurchasedHands.Count>0){

	LeanTween.scale(buyHandsB, new Vector3(1,1,1), myTime).setEase(LeanTweenType.easeOutSine);
}
}
//FEET

if(randomPropCharactArray[randomPropCharact] == "feet"){
if(arrayNotPurchasedFeet.Count>0){

	LeanTween.scale(buyFeetB, new Vector3(1,1,1), myTime).setEase(LeanTweenType.easeOutSine);
}
}


}
public function scaledownbuyguy(){

buyguy.transform.GetChild(0).gameObject.SetActive(false);

buyguy.transform.GetChild(0).localScale = new Vector3(0.0000001f,0.0000001f,0.0000001f);
buyHatsB.transform.localScale = new Vector3(0.0000001f,0.0000001f,0.0000001f);
buyHandsB.transform.localScale = new Vector3(0.0000001f,0.0000001f,0.0000001f);
buyFeetB.transform.localScale = new Vector3(0.0000001f,0.0000001f,0.0000001f);


	for(var i : int = 0 ; i < buyHatsB.transform.childCount; i++)
		{
			buyHatsB.transform.GetChild (i).gameObject.SetActive(false);
		}
	for(var j : int = 0; j < buyHandsB.transform.childCount; j++)
		{
			buyHandsB.transform.GetChild (j).gameObject.SetActive(false);
		}
		for(var k : int = 0; k < buyFeetB.transform.childCount; k++)
		{
			buyFeetB.transform.GetChild (k).gameObject.SetActive(false);
		}


}

		
		
		public function checkRequiredObjects(characterint:int){


		var requiredBody = GameObject.Find ("RequiredBody_B");
		var theCape =GameObject.Find ("cape_B");

if(requiredBody==null || theCape == null)
return;

		for(var i:int = 0; i < requiredBody.transform.childCount; i++)
		{
			requiredBody.transform.GetChild(i).gameObject.GetComponent.<SkinnedMeshRenderer>().enabled = false;
			requiredBody.transform.GetChild(i).gameObject.GetComponent.<Cloth>().enabled = false;

		}
		if(characterTextures[characterint].name == "SIDEKICK_BUDDY"){
//			var theCape =GameObject.Find ("cape_B");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.yellow);
		}
		if(characterTextures[characterint].name == "superhawkbuddy"){
//			var theCape =GameObject.Find ("cape_B");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);

		}
		if(characterTextures[characterint].name == "EvilEmperor"){
//			var theCape =GameObject.Find ("cape_B");
			theCape.GetComponent.<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent.<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);
			
		}
		if(characterTextures[characterint].name == "SuperGasBuddy"){
//			var theCape =GameObject.Find ("cape_B");
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
	

		var requiredHead = GameObject.Find ("RequiredHead_B");
if(requiredHead==null)
return;
		for(var j:int = 0; j <  requiredHead.transform.childCount; j++)
		{
			requiredHead.transform.GetChild(j).gameObject.GetComponent.<MeshRenderer>().enabled = false;
		}
		//makes sure that if a hat is selected doesnt add jason mask and robomask

		if(characterTextures[characterint].name == "johnson"){
				var theMask =GameObject.Find ("JohnsonMask_B");

			theMask.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[characterint].name  == "robotBuddy"){
			var theMask2 =GameObject.Find ("robothelmet_B");
			theMask2.GetComponent.<MeshRenderer>().enabled = true;
		}
		
		
		
		
		
		
	}
public function checkHat(charact:int){
//for(var i : int = 2 ; i < totalObjSize  ; i++)
var mainCharacter_Hat_Parent = GameObject.Find("Hat_jnt_B");
var mainCharacter_Hat :GameObject;

if(mainCharacter_Hat_Parent==null)
return;
		for(var i:int = 0; i < mainCharacter_Hat_Parent.transform.childCount; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild(i).gameObject.SetActive(true);
			if(mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent.<MeshRenderer>()){
			mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent.<MeshRenderer>().enabled = false;
			}
		}
		
		if(characterTextures[charact].name == "SimplePeople_RoadWorker_White"){
		
			mainCharacter_Hat = GameObject.Find ("Hat_Helmet_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}

		if(characterTextures[charact].name == "SimplePeople_Punk_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_B");
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
//		if(characterTextures[charact].name == "princess"){
//			
//			mainCharacter_Hat = GameObject.Find ("Hair_Princess_B");
//			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
//		}
		if(characterTextures[charact].name == "SimplePeople_Pimp_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Pimp_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Policeman_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Police_B");
	
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_RiotCop_Brown"){
			
			mainCharacter_Hat = GameObject.Find ("RiotHelmet_B");
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_FireFighter_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_FireFighter_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_StreetMan_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_Backwards_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Redneck_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Hobo_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Robber_White"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Hobo_B");
	
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "SimplePeople_Sheriff_Black"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "tom"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}

		if(characterTextures[charact].name == "PlumberBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "CarpenterBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "BMXBillyBuddy"){
			
			mainCharacter_Hat = GameObject.Find ("Hair_Punk_B");
		
			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
		if(characterTextures[charact].name == "mrbaseball"){
			
			mainCharacter_Hat = GameObject.Find ("Hat_Cap_B");

			mainCharacter_Hat.GetComponent.<MeshRenderer>().enabled = true;
		}
	
		
		

	}
	
	
		public function receivePrice(mynewPrice : String){
		
buyGuyTxt.text = mynewPrice;
			
		}

	











