#pragma strict

var spring = 50.0;
var damper = 5.0;
var drag = 10.0;
var angularDrag = 5.0;
var distance = 0.2;
var minDist :float ;
var maxDist :float ;
var attachToCenterOfMass = false;
var maxXY:float;
var spinforce:float=10.0;
var maxAngVel:float=10.0;
var backcam:Camera;
private var difficultyfloat:float;
private var springJoint : SpringJoint;
private var conversionray:float;
private var conversionray2:float;
private var mouseylock:float;
private var mousezlock:float;

var yplayerpower:float;
var zplayerpower:float;
var forceMult : float;
var mainCharacter:GameObject;
var ragdoll:Rigidbody[];
var enableRag:boolean=false;

var spawnLocation : Transform;
var ragdollPrefab:GameObject;

//resetgame variables
var cameraAction:GameObject;
var miniPlatform:GameObject;

var yClamp:float;

//wind for flags
var windPos:Transform;
var flags : GameObject[];
private	 var ySense : float;
private var startRot: Vector3;

private var touching:boolean=false;
private var startTime:float;
private var initialTouchPos:float;
private var endPos:float;

var tntScript:GameObject;

function Awake(){
spawnPlayer();
}
function Start(){
//wind.windMPH = Random.Range(0,13);
windFlags();
startRot = transform.localEulerAngles;
}

function Update ()
{

//print(Input.GetAxis("Mouse Y"));
//print(conversionray);
if(Time.timeScale==1){
	// Make sure the user pressed the mouse down
	if (Input.touchCount<1)
		return;
 
	var mainCamera = FindCamera();
 
	// We need to actually hit an object
	var hit : RaycastHit;
	if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),  hit, 220,9))
		return;
	// We need to hit a rigidbody that is not kinematic
	if (!hit.rigidbody || hit.transform.tag == "camCollider" )
		return;
 enablingRagdoll();
	if (!springJoint)
	{
		var go = new GameObject("Rigidbody dragger");
		var body : Rigidbody = go.AddComponent.<Rigidbody>();
		body.mass = 500;
		springJoint = go.AddComponent.<SpringJoint>();
		body.isKinematic = true;
		
	}
 
	springJoint.transform.position = hit.point;
	if (attachToCenterOfMass)
	{
		var anchor = transform.TransformDirection(ragdoll[1].centerOfMass) + ragdoll[1].transform.position;
		anchor = springJoint.transform.InverseTransformPoint(anchor);
		springJoint.anchor = anchor;
	}
	else
	{
		springJoint.anchor = Vector3.zero;
	}
 
	springJoint.spring = spring;
	springJoint.damper = damper;
	springJoint.maxDistance = distance;
	springJoint.connectedBody = ragdoll[1];
 	
	StartCoroutine ("DragObject", hit.distance);
	}
}

public function adjustZ(z:float){
zplayerpower = z;
PlayerPrefs.SetFloat("zplayerpower",zplayerpower);
}
public function adjustY(y:float){
yplayerpower = y;
PlayerPrefs.SetFloat("yplayerpower",yplayerpower);
}

public function adjustYString(y:String){
yplayerpower = parseFloat(y);
PlayerPrefs.SetFloat("yplayerpower",yplayerpower);
}
public function adjustZString(z:String){
zplayerpower = parseFloat(z);
PlayerPrefs.SetFloat("zplayerpower",zplayerpower);
}

public function adjustMouseClamp(sens:String){
yClamp =parseFloat(sens);
PlayerPrefs.SetFloat("yClamp",yClamp);
//var ClampMouseTXT = GameObject.Find("ClampMouseTXT");
//ClampMouseTXT.GetComponent.<UI.Text>().text = yClamp.ToString("f1");
}
public function adjustSensitivity(sensitivity:String){
//yClamp =;
PlayerPrefs.SetFloat("Sensitivity",parseFloat(sensitivity));
//var ClampMouseTXT = GameObject.Find("ClampMouseTXT");
//ClampMouseTXT.GetComponent.<UI.Text>().text = yClamp.ToString("f1");
}

public function adjustX(x:float){

forceMult =x;

}

public function adjustSens(sens:float){

yClamp =sens;
PlayerPrefs.SetFloat("yClamp",yClamp);
var ClampMouseTXT = GameObject.Find("ClampMouseTXT");
ClampMouseTXT.GetComponent.<UI.Text>().text = yClamp.ToString("f1");
}

//sending info to wind.js attached to body that spawns
public function setWindSpeed (speed:float){
ragdoll[1].SendMessage("setWindSpeed",speed);

}

//public function adjustMouse(sen:float){
//
//Input.get
//
//}

  function DragObject (distance : float)
{
	var oldDrag = springJoint.connectedBody.drag;
	var oldAngularDrag = springJoint.connectedBody.angularDrag;
	springJoint.connectedBody.drag = drag;
	springJoint.connectedBody.angularDrag = angularDrag;
	var mainCamera = FindCamera();
	var rayz:float;
	var flickspeed:float;
	
	
	springJoint.minDistance = minDist;
	springJoint.maxDistance = maxDist;
	var touch = Input.GetTouch(0);

	
	
	// Track a single touch as a direction control.
	if (Input.touchCount > 0) {
	

	
	if(touch.phase == TouchPhase.Began){

	startTime = Time.time;
		initialTouchPos = touch.position.y;
		
		print(initialTouchPos);
		}
		
	while (Input.touchCount > 0)
	{
	
	
	
	
		var ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
		springJoint.transform.position = ray.GetPoint(distance);
		var mousex = Input.GetAxis("Mouse X");
		var speedTxt = GameObject.Find("throwSpeed");	 
	 speedTxt.GetComponent.<UI.Text>().text = "Throw Speed: " + ySense.ToString("f2");
	  ySense = Mathf.Clamp(ySense,0,PlayerPrefs.GetFloat("yClamp"));
//	  if(Input.GetTouch(0).deltaPosition.y>0){
//	
//
//		}
yield;
	}

		

//while(touch.phase == TouchPhase.Ended){
//			touching = false;


//		yield;
//		}
	}
	if(!springJoint.connectedBody)
return;
endPos = touch.position.y;
print(endPos);

flickspeed = endPos - initialTouchPos ;
flickspeed /= (Time.time - startTime);

				ragdoll[0].velocity = Vector3.zero;
		ragdoll[1].velocity = Vector3.zero;
		ragdoll[2].velocity = Vector3.zero;
		ragdoll[3].velocity = Vector3.zero;
		ragdoll[4].velocity = Vector3.zero;
		ragdoll[5].velocity = Vector3.zero;
		ragdoll[6].velocity = Vector3.zero;
		ragdoll[7].velocity = Vector3.zero;
		ragdoll[8].velocity = Vector3.zero;
		ragdoll[9].velocity = Vector3.zero;
		ragdoll[10].velocity = Vector3.zero;


	var mousez = PlayerPrefs.GetFloat("zplayerpower") ;

			var mousey = PlayerPrefs.GetFloat("yplayerpower") ;

	var speedTxt2 = GameObject.Find("throwSpeed");
	 
	 speedTxt2.GetComponent.<UI.Text>().text = "Throw Speed: " + flickspeed;
			
	      springJoint.connectedBody.AddForce(0,mousey*flickspeed,0);
		      springJoint.connectedBody.AddForce(0,0,mousez*flickspeed);

		       springJoint.connectedBody.AddForce(mousex*forceMult,0,0);

		    springJoint.connectedBody.drag = 0;
		   springJoint.connectedBody.angularDrag = .05;

		    springJoint.connectedBody = null;

		    miniPlatform.GetComponent.<Collider>().isTrigger=true;
		    ragdoll[1].SendMessage("activateWind");
		    gameObject.SendMessage("findTarget");
		    GetComponent.<camera_Lookat>().enabled = true;
		  GetComponent.<basketballThrow_newTouch>().enabled = false;
	
//	while (Input.touchCount > 0)
//	{
//
//
//		var ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
//		
//
//
//		springJoint.transform.position = ray.GetPoint(distance);
//		
//	
//
//var newypower:float;
//		var mousex = Input.GetAxis("Mouse X");
//
//			newypower=yplayerpower;
//			
//var speedTxt = GameObject.Find("throwSpeed");
//	 
//	 speedTxt.GetComponent.<UI.Text>().text = "Throw Speed: " + ySense.ToString("f2");
//
//	  ySense = Mathf.Clamp(ySense,0,PlayerPrefs.GetFloat("yClamp"));
//
//
//	  #if UNITY_EDITOR
//
//  #endif
//
//  #if UNITY_IPHONE
//  mousezlock=Mathf.Clamp(mousezlock,300,700);
//  #endif
//if(mouseylock>1){
//			mouseylock=Mathf.Lerp(mouseylock,0,Time.deltaTime*4);
//			}
//
//
//if(Input.GetTouch(0).deltaPosition.y>0){
//startTime = Time.time;
//initialTouchPos = Input.GetTouch(0).position.y;
//
////flickspeed = Input.GetTouch(0).deltaPosition.y;
////flickspeed /= Time.deltaTime;
//}
//
//yield;
//
//	}
//	if(TouchPhase.Ended){
//endPos = Input.GetTouch(0).position.y;
//}
//		if (Input.touchCount < 0){
//if(!springJoint.connectedBody)
//return;
//endPos = Input.GetTouch(0).position.y;
////var endPos = Input.GetTouch(0).position.y;
//
//flickspeed = initialTouchPos - endPos;
//flickspeed /= (Time.time - startTime);
//
//				ragdoll[0].velocity = Vector3.zero;
//		ragdoll[1].velocity = Vector3.zero;
//		ragdoll[2].velocity = Vector3.zero;
//		ragdoll[3].velocity = Vector3.zero;
//		ragdoll[4].velocity = Vector3.zero;
//		ragdoll[5].velocity = Vector3.zero;
//		ragdoll[6].velocity = Vector3.zero;
//		ragdoll[7].velocity = Vector3.zero;
//		ragdoll[8].velocity = Vector3.zero;
//		ragdoll[9].velocity = Vector3.zero;
//		ragdoll[10].velocity = Vector3.zero;
//
//
//	var mousez = PlayerPrefs.GetFloat("zplayerpower") ;
//
//
//			var mousey = PlayerPrefs.GetFloat("yplayerpower") ;
//		
//
//			
//	var speedTxt2 = GameObject.Find("throwSpeed");
//	 
//	 speedTxt2.GetComponent.<UI.Text>().text = "Throw Speed: " + flickspeed;
//			
//	      springJoint.connectedBody.AddForce(0,mousey*flickspeed,0);
//		      springJoint.connectedBody.AddForce(0,0,mousez*flickspeed);
//
//		       springJoint.connectedBody.AddForce(mousex*forceMult,0,0);
//
//		    springJoint.connectedBody.drag = oldDrag;
//		   springJoint.connectedBody.angularDrag = oldAngularDrag;
//
//		    springJoint.connectedBody = null;
//
//		    miniPlatform.GetComponent.<Collider>().isTrigger=true;
//		    ragdoll[1].SendMessage("activateWind");
//		    gameObject.SendMessage("findTarget");
//		    GetComponent.<camera_Lookat>().enabled = true;
//		  GetComponent.<basketballThrow_newTouch>().enabled = false;
//		  
//		}
//	
//				
	}
	
function FindCamera ()
{
	if (Camera.main)
		return Camera.main;
	else
		return Camera.main;
		
		
		}
		
function enablingRagdoll(){
if(!enableRag){
enableRag =true;
Destroy(mainCharacter.GetComponent.<Animator>());
for(var r:Rigidbody in ragdoll) // for each limb in the array limbs
    {
       r.isKinematic = false; // set the variable of the Rigidbody component
       r.useGravity =true;
//        r.interpolation = RigidbodyInterpolation.Interpolate;
    }
    
    }
		}
		
		
function spawnPlayer(){
var newragdollPrefab = Instantiate(ragdollPrefab,spawnLocation.position,spawnLocation.rotation);
newragdollPrefab.name = "Player";
mainCharacter = newragdollPrefab;

//print(mainCharacter.name);

ragdoll[0] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt").GetComponent.<Rigidbody>();
ragdoll[1] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt").GetComponent.<Rigidbody>();
ragdoll[2] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/Head_jnt/Head").GetComponent.<Rigidbody>();
ragdoll[3] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/UpperArm_Left_jnt").GetComponent.<Rigidbody>();
ragdoll[4] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/UpperArm_Left_jnt/LowerArm_Left_jnt").GetComponent.<Rigidbody>();
ragdoll[5] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/UpperArm_Right_jnt").GetComponent.<Rigidbody>();
ragdoll[6] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/UpperArm_Right_jnt/LowerArm_Right_jnt").GetComponent.<Rigidbody>();
ragdoll[7] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/UpperLeg_Left_jnt").GetComponent.<Rigidbody>();
ragdoll[8] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/UpperLeg_Left_jnt/LowerLeg_Left_jnt").GetComponent.<Rigidbody>();
ragdoll[9] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/UpperLeg_Right_jnt/").GetComponent.<Rigidbody>();
ragdoll[10] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/UpperLeg_Right_jnt/LowerLeg_Right_jnt").GetComponent.<Rigidbody>();
}


function resetPlayer(){
//resets camera rotation

tntScript.SendMessage("resetTNT");

transform.localEulerAngles = startRot;
//wind.windMPH = Random.Range(0,13);
Destroy(mainCharacter);
spawnPlayer();
cameraAction.SendMessage("returnCamera");
GetComponent.<camera_Lookat>().enabled = false;
GetComponent.<basketballThrow_newTouch>().enabled = true;
enableRag =false;
miniPlatform.GetComponent.<Collider>().isTrigger=false;
windFlags();
}

function windFlags(){
//print(wind.windMPH);
for(var i : int = 0; i < flags.Length  ; i++){
flags[i].GetComponent.<Cloth>().externalAcceleration = windPos.transform.forward*10*wind.windMPH;
flags[i].GetComponent.<Cloth>().randomAcceleration = Vector3(Random.Range(0,15),Random.Range(0,15),Random.Range(0,15));
}



}

		