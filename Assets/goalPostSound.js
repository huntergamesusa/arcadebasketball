#pragma strict
var hit:int;
function Start () {

}

function Update () {

}

function OnCollisionEnter(coll:Collision){

if(coll.gameObject.tag=="rigidbody"){
hit++;
if(hit<2){
//if(coll.GetComponent.<Rigidbody>().velocity.magnitude >=6){
//GetComponent.<AudioSource>().Play();
//}

   var hitVol : float= coll.relativeVelocity.magnitude * .03;
        hitVol = Mathf.Clamp(hitVol,0f,1f);
         GetComponent.<AudioSource>().pitch = Random.Range(.6,1);
        GetComponent.<AudioSource>().volume = hitVol;
        
        GetComponent.<AudioSource>().Play();
        resethitsound();

if(PlayerPrefs.GetInt("upright")<1){

PlayerPrefs.SetInt("upright",1);
var gameCenter = GameObject.Find("GameCenterBT");

gameCenter.SendMessage("uprightacheivement");
}

var MissionsGO = GameObject.Find("Missions");
MissionsGO.SendMessage("receivePostHit");

}
}

}

function resethitsound(){
yield WaitForSeconds(1);
hit=0;

}