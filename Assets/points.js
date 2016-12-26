#pragma strict
import UnityEngine.UI;
//import ChartBoostSDK;

public var greenEmit: Texture;
public var lYellowEmit: Texture;
public var rYellowEmit: Texture;
public var lRedEmit: Texture;
public var rRedEmit: Texture;
public var fgMaterial:Material;

//public var rippleEffect :  GameObject;

private var myfg:int;
private var endFG:boolean = false;

private var points:int;
var pointCollect:GameObject;
private var mult:int;
var timeUpBool : boolean = false;
var inPlayArea : boolean = false;
private var mainRigid:Rigidbody;
private var pointEffectTXT:Text;
var singlePointSound:AudioClip;
var bullseyePointSound:AudioClip;
var cheerPointSound:AudioClip;

private var inCup:boolean=false;
public var cupNeedsDestroying:GameObject;
private var bullseyeMult:int;
private var bullseyeTrans:String;
private var stopresetGameCheckBool;
//var doubleCheckGameOver:boolean = FalseString;
function Start () {
fgMaterial.SetTexture("_MKGlowTex",null);

stopresetGameCheckBool =false;
mult =0;
mainRigid = GetComponent.<Rigidbody>();
pointEffectTXT = GameObject.Find("PointsEffectTXT").GetComponent.<Text>();
inCup =false;
bullseyeMult = pointsCollect.bullseyeMultiplier;
//needed to do this because can't get string from smartlocalization because of C# vs JS
 var BullseyeTranslationGO = GameObject.Find("BullseyeTranslation");
bullseyeTrans = BullseyeTranslationGO.GetComponent.<Text>().text;

}

//function Update () {
//if(Input.GetKeyUp(KeyCode.E)){
//pointsAnimationBullseye();
//}
//if(Input.GetKeyUp(KeyCode.M)){
//minipointsAnimation();
//}
//}

function FixedUpdate(){

//SELECTED LEVEL 2 ARE KEG CUPS
if(PlayerPrefs.GetInt("selectedLevel")<2 && PlayerPrefs.GetInt("selectedLevel")>=0){
if(mainRigid.useGravity ==true && mainRigid.velocity.magnitude <.5 && inPlayArea){
if(points>0){
resetPlay();
}
else{
gameOver_1();
}
if(!stopresetGameCheckBool){
stopresetGameCheckBool=true;
gameObject.SendMessage("stopresetGameDoubleCheck");
}



}

}
else{
if(PlayerPrefs.GetInt("selectedLevel")==2 ){
//reset for keg cups 
if(mainRigid.useGravity ==true && mainRigid.velocity.magnitude <.5 && inPlayArea && !inCup && transform.parent.position.y<-28){
var findCam2 = GameObject.Find("Main Camera");
findCam2.SendMessage("resetPlayer");
}
}
}

if(PlayerPrefs.GetInt("selectedLevel")>=0 ){

if(mainRigid.useGravity ==true && mainRigid.velocity.magnitude <.3 && inPlayArea && !inCup && transform.parent.position.y>=-28){
var findCam3 = GameObject.Find("Main Camera");
findCam3.SendMessage("resetPlayer");
}
}

if(PlayerPrefs.GetInt("selectedLevel")==-1 ){
if(transform.position.y <-19.5f&&points==0){
if(!endFG){
endFG=true;
endFGLevel();
}
}
if(transform.position.y <-19.5f&&points>0){
if(!endFG){
endFG=true;
resetFGLevel();
}

}
if(mainRigid.useGravity ==true && mainRigid.velocity.magnitude <.3&& inPlayArea&&points==0){
if(!endFG){
endFG=true;
endFGLevel();
}
}


}

}

function endFGLevel(){
yield WaitForSeconds(1);
gameOver_1();

}

function resetFGLevel(){
yield WaitForSeconds(1);
resetPlay();
//fgMaterial.SetTexture("_MKGlowTex",null);


}




function doubleCheckEnd(){
//SELECTED LEVEL 2 ARE KEG CUPS
if(PlayerPrefs.GetInt("selectedLevel")<2 &&PlayerPrefs.GetInt("selectedLevel")>=0){
if(mainRigid.useGravity ==true && inPlayArea){
if(points>0){
resetPlay();
}
else{
gameOver_1();
}
}
}
else{
if(PlayerPrefs.GetInt("selectedLevel")>=0 ){

//reset for keg cups 
if(!inCup){
var findCam2 = GameObject.Find("Main Camera");
findCam2.SendMessage("resetPlayer");
}
}
}



}


function resetPlay(){
if(!timeUpBool){
timeUpBool =true;
//yield WaitForSeconds(1);
print("resetPlay");

var sendPoints = GameObject.Find("PointsCollect");
sendPoints.SendMessage("receivePoints",points);


if(points ==5){
pointsAnimationBullseye();
yield WaitForSeconds(1.1);
}
else{
if(points <5 && points >=1){
minipointsAnimation();
yield WaitForSeconds(1);
}
}
var myCam = GameObject.Find("PointCamera");
myCam.GetComponent.<Camera>().enabled = false;
var findCam = GameObject.Find("Main Camera");
findCam.SendMessage("resetPlayer");



}
}

function pointsAnimationBullseye(){
var toonWork = GameObject.Find("ToonFireWork");
var myCam = GameObject.Find("PointCamera");
myCam.GetComponent.<Camera>().enabled = true;
//yield WaitForSeconds(.1f);
pointEffectTXT.gameObject.GetComponent.<AudioSource>().PlayOneShot(bullseyePointSound);


toonWork.GetComponent.<ParticleSystem>().Play();
for(var i : int = 0; i <toonWork.transform.childCount ; i++)
    {
    toonWork.transform.GetChild(i).GetComponent.<ParticleSystem>().Play();
}
if(PlayerPrefs.GetInt("selectedLevel")>=0){
pointEffectTXT.text = "BULLSEYE " + (bullseyeMult+2)+"X";
var MissionsGO = GameObject.Find("Missions");
MissionsGO.SendMessage("receiveBullseye",bullseyeMult+2);
}
else{


pointEffectTXT.text = "Perfect! " + (bullseyeMult+2)+"X";
pointEffectTXT.gameObject.GetComponent.<AudioSource>().PlayOneShot(cheerPointSound);
var MissionsGOFG = GameObject.Find("Missions");
MissionsGOFG.SendMessage("receiveBullseye",bullseyeMult+2);

if((bullseyeMult+2) == 3){
if(PlayerPrefs.GetInt("triplefg")<1){
PlayerPrefs.SetInt("triplefg",1);
var gameCenter = GameObject.Find("GameCenterBT");
gameCenter.SendMessage("triplefgachievement");
}
}

}

//	LeanTween.rotateZ(pointEffectTXT.gameObject, 360f, .7f);
	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(1f,1f,1f), .7f).setEase(LeanTweenType.easeOutElastic);
    	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(0f,0f,0f), .2f).setEase(LeanTweenType.easeOutExpo).setDelay(.7);

}

function pointsAnimationInCup(){
var toonWork = GameObject.Find("ToonFireWork");
var myCam = GameObject.Find("PointCamera");
myCam.GetComponent.<Camera>().enabled = true;
yield WaitForSeconds(.1f);
pointEffectTXT.gameObject.GetComponent.<AudioSource>().PlayOneShot(bullseyePointSound);

toonWork.GetComponent.<ParticleSystem>().Play();
for(var i : int = 0; i <toonWork.transform.childCount ; i++)
    {
    toonWork.transform.GetChild(i).GetComponent.<ParticleSystem>().Play();
}
pointEffectTXT.text = "IN THE CUP";
//	LeanTween.rotateZ(pointEffectTXT.gameObject, 360f, .7f);
	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(1f,1f,1f), .7f).setEase(LeanTweenType.easeOutElastic);
    	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(0f,0f,0f), .2f).setEase(LeanTweenType.easeOutExpo).setDelay(.7);

}

function minipointsAnimation(){
var myCam = GameObject.Find("PointCamera");
myCam.GetComponent.<Camera>().enabled = true;
if(points == 1){
var toonWorkpurple = GameObject.Find("StarMiniWorkPurple");
toonWorkpurple.GetComponent.<ParticleSystem>().Play();
}
if(points == 2){
var toonWorkblue = GameObject.Find("StarMiniWorkBlue");
toonWorkblue.GetComponent.<ParticleSystem>().Play();
}
if(points == 3){
var toonWorkgreen = GameObject.Find("StarMiniWorkGreen");
toonWorkgreen.GetComponent.<ParticleSystem>().Play();
}

//for(var i : int = 0; i <toonWork.transform.childCount ; i++)
//    {
//    toonWork.transform.GetChild(i).GetComponent.<ParticleSystem>().Play();
//}
pointEffectTXT.text = points.ToString("f0");


	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(1f,1f,1f), .7f).setEase(LeanTweenType.easeOutElastic);
    	LeanTween.scale(pointEffectTXT.gameObject, new Vector3(0f,0f,0f), .2f).setEase(LeanTweenType.easeOutExpo).setDelay(.7f);
yield WaitForSeconds(.1f);
pointEffectTXT.gameObject.GetComponent.<AudioSource>().PlayOneShot(singlePointSound);

}

function gameOver_1(){
if(!timeUpBool){
timeUpBool =true;
//yield WaitForSeconds(1);
print("gameOver");



var sendPoints2 = GameObject.Find("PointsCollect");
sendPoints2.SendMessage("gameOver");

var gameOverCanvas = GameObject.Find("NewGameOverCanvas");
gameOverCanvas.GetComponent.<Canvas>().enabled = true;
var gameOverScript = GameObject.Find("Script_Controller");
gameOverScript.SendMessage("slideMenu1");
var ads = GameObject.Find("Ads_Nate");
ads.SendMessage("cacheAd");

}
}


function gameOver(){
if(!timeUpBool){
timeUpBool =true;
var sendPoints3 = GameObject.Find("PointsCollect");
sendPoints3.SendMessage("gameOver");

var gameOverCanvas = GameObject.Find("NewGameOverCanvas");
gameOverCanvas.GetComponent.<Canvas>().enabled = true;
var gameOverScript = GameObject.Find("Script_Controller");
gameOverScript.SendMessage("slideMenu1");

var ads = GameObject.Find("Ads_Nate");
ads.SendMessage("cacheAd");

}
}

function OnTriggerEnter(coll:Collider){
//SELECTED LEVEL 2 ARE KEG CUPS
if(PlayerPrefs.GetInt("selectedLevel")<2 &&PlayerPrefs.GetInt("selectedLevel")>=0){

if(coll.gameObject.tag == "5point"){

points+=2;
}
else{
if(coll.gameObject.tag == "3point" ){
points+=1;
}
else{
if(coll.gameObject.tag == "2point" ){
points+=1;
}
else{
if(coll.gameObject.tag == "1point" ){
points+=1;
}
}
}

}
}
if(coll.gameObject.tag == "camCollider"){
inPlayArea =true;
}
//SELECTED LEVEL 2 ARE KEG CUPS
if(PlayerPrefs.GetInt("selectedLevel")<2 &&PlayerPrefs.GetInt("selectedLevel")>=0){
if(coll.gameObject.tag == "endGame"){
gameOver();
}

}

else{
//cup game reset

if(coll.gameObject.tag == "endGame"&&PlayerPrefs.GetInt("selectedLevel")==2){

var findCam = GameObject.Find("Main Camera");
findCam.SendMessage("resetPlayer");
}

if(coll.gameObject.tag == "cup"){
cupNeedsDestroying =coll.gameObject.transform.parent.gameObject;

if(!inCup){
inCup = true;
yield WaitForSeconds(.5);

pointsAnimationInCup();
yield WaitForSeconds(1);
Destroy(cupNeedsDestroying);

//sends info that a cup was scored
var sendPoints4 = GameObject.Find("PointsCollect");
sendPoints4.SendMessage("cupScored");




}
}
}


if(PlayerPrefs.GetInt("selectedLevel")==-1){
if(coll.gameObject.tag == "5pointfg" ||coll.gameObject.tag == "3pointfg"||coll.gameObject.tag == "1pointfg"){
myfg++;
if(myfg<2){
if(coll.gameObject.tag == "5pointfg"){
points=5;
fgMaterial.SetTexture("_MKGlowTex",greenEmit);
fgMaterial.SetColor("_MKGlowTexColor",Color(0,1,0,0.45f));


}
if(coll.gameObject.tag == "3pointfg"){
points=2;
if(coll.gameObject.name == "3-Points_Right"){
fgMaterial.SetTexture("_MKGlowTex",rYellowEmit);
fgMaterial.SetColor("_MKGlowTexColor",Color(1,1,0,0.45f));
}
if(coll.gameObject.name == "3-Points_Left"){
fgMaterial.SetTexture("_MKGlowTex",lYellowEmit);
fgMaterial.SetColor("_MKGlowTexColor",Color(1,1,0,0.45f));
}

}
if(coll.gameObject.tag == "1pointfg"){
points=1;
if(coll.gameObject.name == "1-Point_Left"){

fgMaterial.SetTexture("_MKGlowTex",rRedEmit);
fgMaterial.SetColor("_MKGlowTexColor",Color(1,0,0,0.45f));
}
if(coll.gameObject.name == "1-Point_Right"){
fgMaterial.SetTexture("_MKGlowTex",lRedEmit);
fgMaterial.SetColor("_MKGlowTexColor",Color(1,0,0,0.45f));
}

}
print("fg: "+points);
//var myRipple = Instantiate(rippleEffect,Vector3(transform.position.x,transform.position.y,coll.transform.position.z+2),Quaternion.identity);

}

}

}
}


function OnTriggerExit(colls:Collider){
//SELECTED LEVEL 2 ARE KEG CUPS
if(PlayerPrefs.GetInt("selectedLevel")<2 &&PlayerPrefs.GetInt("selectedLevel")>=0){

if(colls.gameObject.tag == "5point"){
points-=2;

}
if(colls.gameObject.tag == "3point"){
points-=1;

}
if(colls.gameObject.tag == "2point"){
points-=1;

}
if(colls.gameObject.tag == "1point"){
points-=1;

}
}
}
