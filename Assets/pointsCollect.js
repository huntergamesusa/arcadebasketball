#pragma strict
import UnityEngine.UI;
var pointstxt : Text;
var endPointsTxt : Text;
var levelTNTTxt : RectTransform;
private var currentTNTLvl:int;
//var wooshClip:AudioClip;
//var endPointsTotalTxt : Text;
var points : int;
static var staticPoints:int;
var isWindMode : boolean = false;
var creditDivider : int;
var creditEarnedInt:int;
var creditsEarned :Text;
var islandMesh:GameObject;
var kegCups:GameObject;
var timeUI:Text;
var cupsLeftUI:Text;
var islandGreen:Material[];
var islandBullseye:Material[];

private var startKegTimer:boolean=false;
private var cupsLeft:String;

private var min : int;
private var sec : int;
private var fraction : int;
private var timecount : float;
private var starttime : float;
private var scoredCups:int;
private var finalTime:float;
private var multiplier:int;

static var bullseyeMultiplier:int;

private var levelString:String;
var kegCupSpawnPos:Transform;

function translateCupsLeft(translation:String){
cupsLeft=translation;
}

function Start () {

points = 0;
pointstxt.text = points.ToString("f0");
currentTNTLvl =1;

//IF YOU HAVE GALACTIC MAN MULTIPLY ALL COINS
if(PlayerPrefs.GetInt("64")==1){
multiplier = 2;
}
else{
multiplier=1;
}

levelString=levelTNTTxt.gameObject.GetComponent.<Text>().text;

}
function Awake(){
PlayerPrefs.SetInt("selectedLevel",0);
resetKegCups();
bullseyeMultiplier=-1;

}

function Update () {
if(startKegTimer){
timecount = Time.time - starttime;
 min = (timecount/60f);
 sec = (timecount % 60f);
 fraction = ((timecount * 10) %10);
 timeUI.text = String.Format("{00:00}:{1:00}:{2:00}",min,sec,fraction);
}
if(Input.GetKeyUp(KeyCode.J)){
endGamePong();
}
}

public function UpdateLevelString(){
levelString=levelTNTTxt.gameObject.GetComponent.<Text>().text;

}

public function whichMode(level:int){

//IF YOU HAVE GALACTIC MAN MULTIPLY ALL COINS
if(PlayerPrefs.GetInt("64")==1){
multiplier = 2;
}
else{
multiplier=1;
}

PlayerPrefs.SetInt("selectedLevel",level);

if(level == 0){
isWindMode = true;
}

else{
isWindMode = false;
}
if(PlayerPrefs.GetInt("selectedLevel")<2 && PlayerPrefs.GetInt("selectedLevel")>=0){
islandMesh.SendMessage("restartRotate");
var kegCupsParent2 = GameObject.Find( "KegCups");
if(kegCupsParent2!=null){
Destroy(kegCupsParent2);
}
islandMesh.GetComponent.<MeshRenderer>().materials = islandBullseye;

startKegTimer = false;
}
else{


iTween.Stop(islandMesh);
islandMesh.transform.localEulerAngles.y = 360;
islandMesh.GetComponent.<MeshRenderer>().materials= islandGreen;

//if(PlayerPrefs.GetInt("selectedLevel")==-1){
//var fgObject = GameObject.Find( "goal_posts_bigger");
//
//}

if(PlayerPrefs.GetInt("selectedLevel")==2){
var kegCupsParent = GameObject.Find( "KegCups");
if(kegCupsParent!=null){
Destroy(kegCupsParent);

}


 var kegCupInst =  Instantiate(kegCups,kegCupSpawnPos.position,Quaternion.identity);
kegCupInst.name = "KegCups";
startKegTimer = true;
starttime = Time.time;
cupsLeftUI.text = cupsLeft+": " + (10 - scoredCups);
}
}



}


public function resetKegCups(){
var kegCupsParent = GameObject.Find("KegCups");
if(kegCupsParent!=null){
Destroy(kegCupsParent);
print("destorying kegcups");
}
if(PlayerPrefs.GetInt("selectedLevel")==2){
scoredCups=0;
islandMesh.GetComponent.<MeshRenderer>().materials = islandGreen;
 var kegCupInst =  Instantiate(kegCups,kegCupSpawnPos.position,Quaternion.identity);
kegCupInst.name = "KegCups";
cupsLeftUI.text = cupsLeft+": " + (10 - scoredCups);
startKegTimer = true;
starttime = Time.time;
}
}



public function resetHome(){
//NEED TO REMOVE CUPS AND BRING BACK THE PLATFORM
var kegCupsParent = GameObject.Find( "KegCups");
if(kegCupsParent!=null){
Destroy(kegCupsParent);
}
islandMesh.GetComponent.<MeshRenderer>().materials = islandBullseye;
islandMesh.SendMessage("restartRotate");

}



public function animateTNTLevelTxt(){
    if(!isWindMode&&PlayerPrefs.GetInt("selectedLevel")==1){
   currentTNTLvl++;
      levelTNTTxt.gameObject.SetActive(false);
levelTNTTxt.transform.localPosition = Vector3(-625f,0f,0f);
levelTNTTxt.gameObject.SetActive(true);
   

levelTNTTxt.gameObject.GetComponent.<Text>().text = levelString +" "+ currentTNTLvl;
if(currentTNTLvl==6){
var MissionsGOSix = GameObject.Find("Missions");
MissionsGOSix.SendMessage("receiveTNTSix");
}

if(currentTNTLvl==12){
var gameCenter = GameObject.Find("GameCenterBT");
gameCenter.SendMessage("Level12");
var MissionsGOTwelve = GameObject.Find("Missions");
MissionsGOTwelve.SendMessage("receiveTNTTwelve");
}

LeanTween.move(levelTNTTxt, new Vector3(625f,0f,0f), 1.25).setDelay(1.2f);

//yield WaitForSeconds(.2);
//levelTNTTxt.gameObject.GetComponent.<AudioSource>().PlayOneShot(wooshClip);
}
}

public function animateTNTLevelTxtStart(){
    if(!isWindMode&&PlayerPrefs.GetInt("selectedLevel")==1){
    
    LeanTween.cancel (levelTNTTxt.gameObject);
    
    currentTNTLvl =1;
   
   levelTNTTxt.gameObject.SetActive(false);
levelTNTTxt.transform.localPosition = Vector3(-625f,0f,0f);
levelTNTTxt.gameObject.SetActive(true);
   
levelTNTTxt.gameObject.GetComponent.<Text>().text = levelString +" " + currentTNTLvl;

LeanTween.move(levelTNTTxt, new Vector3(625f,0f,0f), 1.25);

//yield WaitForSeconds(.2);
//levelTNTTxt.gameObject.GetComponent.<AudioSource>().PlayOneShot(wooshClip);
}
}



function receivePoints(recPoints:int){
//points += recPoints;

//Everyplay.SetMetadata("buddyScore", points);



animateTNTLevelTxt();


if(recPoints == 5){


bullseyeMultiplier++;

recPoints=recPoints+(recPoints*bullseyeMultiplier);

}else{
bullseyeMultiplier=-1;
}

staticPoints+=recPoints;

yield WaitForSeconds(1.2);
   for(var i : int = 0; i < recPoints; i++)
    {
    pointstxt.transform.localScale = new Vector3(1.25f,1.25f,1.25f);
    points ++;
    pointstxt.text = points.ToString("f0");
//    pointstxt.transform.localScale = Vector3.zero;
	LeanTween.scale(pointstxt.gameObject, new Vector3(1f,1f,1f), .1f).setEase(LeanTweenType.easeInOutElastic);

        GetComponent.<AudioSource>().Play();
        yield WaitForSeconds(.1);
    }
    
    if(points>= 25){
    var gameCenter2 = GameObject.Find("GameCenterBT");
gameCenter2.SendMessage("twentyfivepointAchievement");

    }
       if(points>= 50){
    var gameCenter3 = GameObject.Find("GameCenterBT");
gameCenter3.SendMessage("fiftyfivepointAchievement");

    }


    
if(recPoints == 5){
if(PlayerPrefs.GetInt("hitbullseyeacheivement")<1){
PlayerPrefs.SetInt("hitbullseyeacheivement",1);
var gameCenter = GameObject.Find("GameCenterBT");

gameCenter.SendMessage("bullseyeAchievement");
}
}



}



function gameOver(){
currentTNTLvl =1;
//var TNTObject = GameObject.Find("spawn_TNT");
//TNTObject.GetComponent

//isWindMode

var HeyPlay = GameObject.Find("HeyPlayManager");

if(PlayerPrefs.GetInt("selectedLevel")==0){
if(PlayerPrefs.GetInt("highscore") < points){
PlayerPrefs.SetInt("highscore",points);

}
var gameCenter = GameObject.Find("GameCenterBT");
gameCenter.SendMessage("updateLeaderBoard",points);
var highScore = GameObject.Find("HighScore");
if(highScore!=null){ 
highScore.GetComponent.<Text>().enabled = true;

highScore.GetComponent.<Text>().text =  "HI " + PlayerPrefs.GetInt("highscore").ToString("f0");
}
HeyPlay.SendMessage("EndingSessionWind",points);
}
var MissionsScore= GameObject.Find("Missions");
MissionsScore.SendMessage("receiveEndScore",points);

MissionsScore.SendMessage("playAnimationCredits");

if(PlayerPrefs.GetInt("selectedLevel")==1){
if(PlayerPrefs.GetInt("highscoreTNT") < points){
PlayerPrefs.SetInt("highscoreTNT",points);

}
var gameCenter2 = GameObject.Find("GameCenterBT");
gameCenter2.SendMessage("updateLeaderBoardTNT",points);


var highScore2 = GameObject.Find("HighScore");
if(highScore2!=null){
highScore2.GetComponent.<Text>().enabled = true;

highScore2.GetComponent.<Text>().text =  "HI " + PlayerPrefs.GetInt("highscoreTNT").ToString("f0");
}


HeyPlay.SendMessage("EndingSessionTNT",points);



}


if(PlayerPrefs.GetInt("selectedLevel")==-1){
if(PlayerPrefs.GetInt("highscoreFG") < points){
PlayerPrefs.SetInt("highscoreFG",points);


}
var gameCenter3 = GameObject.Find("GameCenterBT");
gameCenter3.SendMessage("updateLeaderBoardFG",points);
var highScore3 = GameObject.Find("HighScore");
if(highScore3!=null){ 
highScore3.GetComponent.<Text>().enabled = true;

highScore3.GetComponent.<Text>().text =  "HI " + PlayerPrefs.GetInt("highscoreFG").ToString("f0");
}
HeyPlay.SendMessage("EndingSessionFG",points);

}





endPointsTxt.text = points.ToString("f0");

//GIVES CREDITS AT END
var credits = GameObject.Find ("Credits UI").GetComponent.<Text>();

creditEarnedInt = (points/creditDivider)*multiplier *PlayerPrefs.GetInt("NetworkScoreMult");
if(creditEarnedInt<1){
creditEarnedInt =1;
}
else{
creditEarnedInt = Mathf.Ceil(creditEarnedInt);
}
if(points ==0){
creditEarnedInt =0;
}

creditsEarned.text  = creditEarnedInt.ToString("f0");

yield WaitForSeconds(1.4);

   for(var i : int = 0; i <= creditEarnedInt; i++)
    {
if(i < creditEarnedInt){
PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")+1);
GetComponent.<AudioSource>().Play();

}
credits.text = PlayerPrefs.GetInt("myCredits").ToString("f0");
var creditsSubtracted : int = creditEarnedInt-i;
creditsEarned.text = creditsSubtracted.ToString("f0");
        yield WaitForSeconds(.05);
    }

creditEarnedInt = 0;


//endPointsTotalTxt.text = points.ToString("f0");

}

function resetPoints(){
points = 0;
pointstxt.text = points.ToString("f0");
bullseyeMultiplier=-1;
staticPoints = 0;

}

function cupScored(){
scoredCups++;
cupsLeftUI.text = cupsLeft+": " + (10 - scoredCups);

print("Destroyed Cup");
//score the time

if(scoredCups >=10){
endGamePong();


}
else{
//resets player psoition
var findCam2 = GameObject.Find("Main Camera");
findCam2.SendMessage("resetPlayer");
}

}

public function endGamePong(){
startKegTimer = false;
//finalTime = timecount;
//PlayerPrefs.SetFloat("BuddyPongHighScore",2);
//had to make highscorebuddypong playerprefs on awake

 min = (timecount/60f);
 sec = (timecount % 60f);
 fraction = ((timecount * 10) %10);
 timeUI.text = String.Format("{00:00}:{1:00}:{2:00}",min,sec,fraction);

if( PlayerPrefs.GetFloat("BuddyPongHighScore") ==0){
PlayerPrefs.SetFloat("BuddyPongHighScore",timecount);

PlayerPrefs.SetFloat("hiMin",min) ;
PlayerPrefs.SetFloat("hiSec",sec) ;
PlayerPrefs.SetFloat("hiFraction",fraction) ;


}

if( timecount<=PlayerPrefs.GetFloat("BuddyPongHighScore")){
PlayerPrefs.SetFloat("BuddyPongHighScore",timecount);
PlayerPrefs.SetFloat("hiMin",min) ;
PlayerPrefs.SetFloat("hiSec",sec) ;
PlayerPrefs.SetFloat("hiFraction",fraction) ;



}



//normalize main time




var highScore3 = GameObject.Find("TimeHighScore");
if(highScore3!=null){
highScore3.GetComponent.<Text>().enabled = true;

var highScoreTimeString = String.Format("{00:00}:{1:00}:{2:00}",PlayerPrefs.GetFloat("hiMin"),PlayerPrefs.GetFloat("hiSec"),PlayerPrefs.GetFloat("hiFraction"));
highScore3.GetComponent.<Text>().text =  "HI " + highScoreTimeString;


}


var gameCenter = GameObject.Find("GameCenterBT");

//includes acheivement
gameCenter.SendMessage("updateLeaderBoardPong",timecount);


var ads = GameObject.Find("Ads_Nate");

ads.SendMessage("autoLoadAd");

var gameOverCanvas = GameObject.Find("NewGameOverCanvas");
gameOverCanvas.GetComponent.<Canvas>().enabled = true;


endPointsTxt.text = String.Format("{00:00}:{1:00}:{2:00}",min,sec,fraction);




///SCRIPT CONTROLLER WILL SHOW AD
var gameOverScript = GameObject.Find("Script_Controller");
gameOverScript.SendMessage("slideMenu1");

//GIVES CREDITS AT END
var credits = GameObject.Find ("Credits UI").GetComponent.<Text>();

var HeyPlay = GameObject.Find("HeyPlayManager");

HeyPlay.SendMessage("EndingSessionPong",timecount);


if(timecount<60*2){
creditEarnedInt = 100*multiplier;
gameCenter.SendMessage("tenCupsUnderTwoMinutes");
var MissionsGOThree = GameObject.Find("Missions");
MissionsGOThree.SendMessage("PongMode",2);

}
else{
if(timecount<60*3){
creditEarnedInt = 60*multiplier;
var MissionsGOFour = GameObject.Find("Missions");
MissionsGOFour.SendMessage("PongMode",3);
}
else{
if(timecount<60*4){
creditEarnedInt = 40*multiplier;
var MissionsGOFive = GameObject.Find("Missions");
MissionsGOFive.SendMessage("PongMode",4);
}
else{
if(timecount<60*5){
creditEarnedInt = 30*multiplier;

}
else{
if(timecount>60*5){
creditEarnedInt = 20*multiplier;
}
}
}
}
}


creditsEarned.text  = creditEarnedInt.ToString("f0");

yield WaitForSeconds(1.4);

   for(var i : int = 0; i <= creditEarnedInt; i++)
    {
if(i < creditEarnedInt){
PlayerPrefs.SetInt("myCredits",PlayerPrefs.GetInt("myCredits")+1);
GetComponent.<AudioSource>().Play();

}
credits.text = PlayerPrefs.GetInt("myCredits").ToString("f0");
var creditsSubtracted : int = creditEarnedInt-i;
creditsEarned.text = creditsSubtracted.ToString("f0");
        yield WaitForSeconds(.05);
    }

creditEarnedInt = 0;



}



