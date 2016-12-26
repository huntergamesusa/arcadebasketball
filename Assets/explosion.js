#pragma strict
var Force : float;
var upMod : float;
var radius : float;
var explosion:GameObject;
var hit:int;
var explosionScale:float;
//var explosionSound:AudioClip;
function Start () {

}

function Update () {



}
function OnTriggerEnter(coll:Collider){

if(coll.tag == "mainBody" ||coll.tag == "rigidbody"){
zoomcamera.zoomCamBool = false;
var gameCenter = GameObject.Find("GameCenterBT");
gameCenter.SendMessage("tntAchievement");

var missionsGO = GameObject.Find("Missions");
missionsGO.SendMessage("hitTNT");

var actioncam = GameObject.Find("CameraAction");
actioncam.SendMessage("Shake");

var explosionPos : Vector3 = transform.position;
		var colliders : Collider[] = Physics.OverlapSphere (explosionPos, radius);
		
		for (var hit2 : Collider in colliders) {
		if(hit2.tag == "mainBody" || hit2.tag == "rigidbody"){
			if (hit2 && hit2.GetComponent.<Rigidbody>()  ){
//			hit2.GetComponent.<Collider>().isTrigger = false;
//			hit2.GetComponent.<Rigidbody>().useGravity =true;
//			
//			if(hit2.transform.GetChild(0).GetComponent.<Collider>()){
//			hit2.transform.GetChild(0).GetComponent.<Collider>().enabled = false;
//			}
//			if(hit2.transform.GetChild(1).GetComponent.<Collider>()){
//			hit2.transform.GetChild(1).GetComponent.<Collider>().enabled = false;
//			}
//			if(hit2.transform.GetChild(2).GetComponent.<Collider>()){
//			hit2.transform.GetChild(2).GetComponent.<Collider>().enabled = false;
//				}		
//			hit2.SendMessage("StopMoving");
				hit2.GetComponent.<Rigidbody>().AddExplosionForce(Force*explosionScale, explosionPos,radius, upMod*explosionScale,ForceMode.Acceleration);
						if(hit<1){
				hit++;
					print("EXPLODE!!!");
				var prefab_exp = Instantiate(explosion,transform.position, Quaternion.identity);
										GetComponent.<MeshRenderer>().enabled =false;

						snapPicture();

				
				}
				}
				}
		}
//		
//			if(hit<1){
//				hit++;
//		GetComponent.<AudioSource>().Play();
//		snapPicture();
//		}
		
		}
	
		}
		
		function snapPicture(){
yield WaitForSeconds(.02);
var actionCam = GameObject.Find("CameraAction_Screenshot");
//if(actionCam.GetComponent.<Camera>().enabled == true){
actionCam.SendMessage("newScreen");
Destroy(gameObject);
//newScreen();
//}
}