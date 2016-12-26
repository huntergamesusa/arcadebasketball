#pragma strict
var rigidParts:Rigidbody[];

var speed:float;
var poIn:GameObject;
var isFlappy:boolean=false;
private var dead:boolean=false;
private var collCount:int;
function Start () {

}

function Update () {
//GetComponent.<Rigidbody>().velocity=(Vector3.right * -1*speed);
if(!dead){
transform.Translate(Vector3.forward *speed);
}

//if(Input.GetKeyUp(KeyCode.Space)&&isFlappy){
//var po = Instantiate(poIn,transform.position,Quaternion.identity);
//}

if(Input.GetKeyUp(KeyCode.D)){
GetComponent.<Animator>().enabled =false;
GetComponent.<Rigidbody>().isKinematic =false;
GetComponent.<Rigidbody>().useGravity =true;


dead =true;
 for(var i : int = 0; i < rigidParts.Length; i++)
    {
    rigidParts[i].useGravity =true;
     rigidParts[i].isKinematic =false;
     rigidParts[i].gameObject.GetComponent.<Collider>().isTrigger = false;
    
    }
}

}

function OnTriggerEnter(coll:Collider){
collCount++;

if(coll.tag=="endGame"){
Destroy(gameObject);

}

if(coll.tag=="rigidbody" && collCount<=1){
collCount++;
//sends bird hit acheivement
var gamecenter = GameObject.Find("GameCenterBT");
gamecenter.SendMessage("birdacheivement");
var MissionsGO = GameObject.Find("Missions");
MissionsGO.SendMessage("HitBird");
Destroy(GetComponent.<CapsuleCollider>());
GetComponent.<Rigidbody>().isKinematic =false;
GetComponent.<Rigidbody>().useGravity =true;

GetComponent.<Animator>().enabled =false;
GetComponent.<Rigidbody>().isKinematic =false;
dead =true;
 for(var i : int = 0; i < rigidParts.Length; i++)
    {
    rigidParts[i].useGravity =true;
     rigidParts[i].isKinematic =false;
     rigidParts[i].gameObject.GetComponent.<Collider>().isTrigger = false;
    
    }
    
    yield WaitForSeconds(3);
    
    Destroy(gameObject);
}



}