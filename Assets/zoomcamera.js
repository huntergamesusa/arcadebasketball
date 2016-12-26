#pragma strict
var cam1 : GameObject;
var speed :float;

static var zoomCamBool:boolean=false;
static var zoomCamBoolFG:boolean=false;

private var target:GameObject;

private var characterPastBool : boolean = false;
private var characterPastBoolFG : boolean = false;


var camPosActionRandom:Transform[];

var cam2 : GameObject;
var characterScript : GameObject;
private var mult:int;

function Start () {
lookforHat();

}

function FixedUpdate () {
//
if(zoomCamBool&&target!=null){
if(PlayerPrefs.GetInt("selectedLevel")>=0){
cam2.transform.position = Vector3.Lerp(cam2.transform.position ,Vector3(target.transform.position.x+12,cam2.transform.position.y-1 ,target.transform.position.z  - 12),Time.deltaTime *speed);
//}
//else{
//if(zoomCamBoolEnded){
////cam1.transform.position = Vector3.Lerp(cam1.transform.position ,Vector3(camPosPost.x,camPosPost.y + 7 ,camPosPost.z + camPos.z - 13),Time.deltaTime *(speed));
//
//}
//
//}

//
//}

}



}
if(target != null){
if(target.transform.position.z >cam2.transform.position.z){
characterisPast();
}
}



if(zoomCamBoolFG&&target!=null){
if(PlayerPrefs.GetInt("selectedLevel")<0){
cam2.GetComponent.<Camera>().fieldOfView = Mathf.Lerp(cam2.GetComponent.<Camera>().fieldOfView,30,Time.deltaTime*.85);

}
}

if(target != null){
if(target.transform.position.z >cam2.transform.position.z){
characterPastFG();
}
}


}
//
function characterisPast(){
if(!characterPastBool){

characterPastBool = true;
zoomCamBool=true;
//disableCamFollow();

}

}

function characterPastFG(){
if(!characterPastBoolFG){

characterPastBoolFG = true;
yield WaitForSeconds(.5);
zoomCamBoolFG=true;
//disableCamFollow();

}
}

function OnTriggerEnter(coll:Collider){

if(coll.tag == "ragdoll"){

if(mult<1){
mult++;
//camPos = cam1.transform.position;

target = GameObject.Find("Body_jnt");
//zoomCamBool = true;


cam1.GetComponent.<Camera>().enabled =false;
cam2.SendMessage("findTarget");
cam2.transform.position = camPosActionRandom[Random.Range(0,0)].position;
cam2.GetComponent.<Camera>().enabled =true;

if(PlayerPrefs.GetInt("selectedLevel")<0){
cam2.transform.position = Vector3( 30,camPosActionRandom[Random.Range(0,0)].position.y, 13);
}

}

}



}



function returnCamera(){

mult=0;
//multexit=0;
//newspeed=0;
//characterLowBool =false;
characterPastBool =false;
zoomCamBool =false;
characterPastBoolFG =false;
zoomCamBoolFG =false;
lookforHat();
cam2.GetComponent.<Camera>().fieldOfView =60;
//yield WaitForEndOfFrame();
cam2.GetComponent.<Camera>().enabled =false;
cam1.GetComponent.<Camera>().enabled =true;
//cam_screenshot.GetComponent.<Camera>().enabled =false;

}

public function lookforHat(){
yield WaitForFixedUpdate;
characterScript.SendMessage("resetScriptStart");
}



