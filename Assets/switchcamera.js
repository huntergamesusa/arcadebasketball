#pragma strict
var cam1 : GameObject;
var cam2 : GameObject;
var characterScript : GameObject;
//var cam_screenshot : GameObject;
private var mult:int;
function Start () {
lookforHat();
}

function Update () {

}

function OnTriggerEnter(coll:Collider){

if(coll.tag == "ragdoll"){

if(mult<1){
mult++;

cam1.GetComponent.<Camera>().enabled =false;
cam1.GetComponent.<AudioListener>().enabled =false;
cam2.SendMessage("findTarget");
cam2.GetComponent.<AudioListener>().enabled =true;

cam2.GetComponent.<Camera>().enabled =true;
//cam_screenshot.SendMessage("findTarget");
//cam_screenshot.GetComponent.<Camera>().enabled =true;

//cam2.GetComponent.<Camera>().enabled = true;

}

}



}

function returnCamera(){

mult=0;
cam1.GetComponent.<AudioListener>().enabled =true;

cam1.GetComponent.<Camera>().enabled =true;
cam1.GetComponent.<AudioListener>().enabled =false;

cam2.GetComponent.<Camera>().enabled =false;
lookforHat();
//cam_screenshot.GetComponent.<Camera>().enabled =false;

}

public function lookforHat(){
yield WaitForFixedUpdate;
characterScript.SendMessage("resetScriptStart");
}