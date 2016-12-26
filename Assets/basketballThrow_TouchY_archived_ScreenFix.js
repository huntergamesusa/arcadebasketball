#pragma strict
var yPowerFlick:float;
var zPower:float;
var xPower:float;
var velocityClamp:float;

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

private var  screenSize:Vector2;

private var difficultyfloat:float;
private var springJoint : SpringJoint;
private var conversionray:float;
private var conversionray2:float;
private var mouseylock:float;
private var mousezlock:float;
private var inputCombine:Vector2;

var yplayerpower:float;
var zplayerpower:float;
var forceMult : float;
var mainCharacter:GameObject;
var ragdoll:Rigidbody[];
var enableRag:boolean=false;

private var headRigid : Rigidbody;
private var purchasableheadRigid : GameObject;
private var rightHandRigid : GameObject;
private var leftHandRigid : GameObject;


private var rightFootRigid : GameObject;
private var leftFootRigid : GameObject;

var spawnLocation : Transform;
var ragdollPrefab:GameObject;

//resetgame variables
var cameraAction:GameObject;
var miniPlatform:GameObject;

var yClamp:float;

var windPos:Transform;
var flags : GameObject[];
private	 var ySense : float;
private var startRot: Vector3;
var tntScript : GameObject;
static var TNTMode :boolean = false;

public var startTouchPos:Vector2;
private var startTime:float;
private var endPos:Vector2;
private var flickspeed:Vector2;
private var flickspeedz:float;

 var characterPrefsScript:GameObject;
 var positionTest:Transform;
 var forceTest:float;

 var layerMask = ~( (1 << 28));

public function enableTNTMode(){
TNTMode = true;
}

public function disableTNTMode(){
TNTMode = false;
}



function Awake(){
spawnPlayer();

adjustthePower();

screenSize = new Vector2(Screen.width,Screen.height);

}


function adjustthePower(){
if(PlayerPrefs.GetInt("selectedLevel")>=0){

zPower = 80000;

velocityClamp =7350;
}
else{
zPower = 120000;

velocityClamp =8000;
}


PlayerPrefs.SetFloat("yplayerpower",yPowerFlick);
PlayerPrefs.SetFloat("zplayerpower",zPower);
PlayerPrefs.SetFloat("XValue",xPower);
PlayerPrefs.SetFloat("maxVelocity",velocityClamp);
PlayerPrefs.SetFloat("startPoFlick",.8);
PlayerPrefs.SetFloat("windSpeed",40);


}


function Start(){
//windFlags();
startRot = transform.localEulerAngles;
}

function FixedUpdate ()
{
//REMOVE THIS
//if(Input.GetKeyUp(KeyCode.E)){
//ragdoll[0].gameObject.transform.position = positionTest.position;
//enablingRagdoll();
//ragdoll[1].AddForce(0,forceTest,0);
//
//}

//print(Time.time.ToString("f7"));
//print("update");

//print(Input.GetAxis("Mouse Y"));
//print(conversionray);
if(Time.timeScale>0){
	// Make sure the user pressed the mouse down
	if (!Input.GetMouseButtonDown (0))
		return;
 
	var mainCamera = FindCamera();
 
	// We need to actually hit an object
	var hit : RaycastHit;
	if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),  hit, 40,layerMask))
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
//		springJoint.transform.position = hit.point;
		
		
	}
    ragdoll[1].transform.position= hit.point + Vector3(0,-.35,0);
	springJoint.transform.position = hit.point ;
	if (attachToCenterOfMass)
	{
		

		var anchor = transform.TransformDirection(ragdoll[1].centerOfMass + ragdoll[1].transform.position);
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
//	ragdoll[1].position = hit.point;
	springJoint.connectedBody = ragdoll[1];
 	
	StartCoroutine ("DragObject", hit.distance);
	}
}

public function zPowerFunc(damps:String){
PlayerPrefs.SetFloat("zplayerpower",parseFloat(damps));
}

public function adjustForgiveness(forgive:String){
//damper = parseFloat(damps);
PlayerPrefs.SetFloat("forgiveness",parseFloat(forgive));
}


public function adjustStart(start:String){
PlayerPrefs.SetFloat("startPoFlick",parseFloat(start));
}

public function adjustSensitivity(sense:String){
PlayerPrefs.SetFloat("yplayerpower",parseFloat(sense));
}

public function adjustX(x:String){

//PlayerPrefs.SetFloat("XValue",parseFloat(x));

}

public function adjustWind(x:String){

PlayerPrefs.SetFloat("windSpeed",parseFloat(x));

}

//public function adjustSensitivity(sensitivity:String){
////yClamp =;
//PlayerPrefs.SetFloat("Sensitivity",parseFloat(sensitivity));
//
//}


public function adjustmaxVelocity(sens:String){

//PlayerPrefs.SetFloat("maxVelocity",sens);

PlayerPrefs.SetFloat("maxVelocity",parseFloat(sens));

}






  function DragObject (distance : float)
{
	var oldDrag = springJoint.connectedBody.drag;
	var oldAngularDrag = springJoint.connectedBody.angularDrag;
	springJoint.connectedBody.drag = drag;
	springJoint.connectedBody.angularDrag = angularDrag;
	var mainCamera = FindCamera();
	var rayz:float;
	
	
	springJoint.minDistance = minDist;
	springJoint.maxDistance = maxDist;
  ySense = 0;
  
  if(Input.GetMouseButton (0)){
  	startTime = Time.time;

  	
		startTouchPos =  Vector2((Input.mousePosition.x/screenSize.x)*750,(Input.mousePosition.y/screenSize.y)*1334);
}

	
	while (Input.GetMouseButton (0))
	{

		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		

conversionray=ray.direction.y+1.3;
conversionray2=(155-100)/(1-ray.direction.y+.24);


		springJoint.transform.position = ray.GetPoint(distance);
		
		

	 

var newypower:float;
//		var mousex = Input.GetAxis("Mouse X");
//		newypower=yplayerpower/(conversionray+1);
			newypower=yplayerpower;

 inputCombine = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
// inputCombine.magnitude.
// print("inputComine: "+ inputCombine.normalized.y);

if(inputCombine.magnitude <= PlayerPrefs.GetFloat("startPoFlick")){
  	startTime = Time.time;
  	
	startTouchPos =  Vector2((Input.mousePosition.x/screenSize.x)*750,(Input.mousePosition.y/screenSize.y)*1334);

//  	 var mouseRatioX = Input.mousePosition.x / Screen.width;
// var mouseRatioY = Input.mousePosition.y / Screen.height;
//  	
//		startTouchPos =  Vector2(mouseRatioX*750,mouseRatioY*1334);
		
		
}
//0.5 IS KEY FOR SLOW DRAGGERS
if(Input.GetAxis("Mouse Y") <= 0){
  	startTime = Time.time;
 
  	startTouchPos =  Vector2((Input.mousePosition.x/screenSize.x)*750,(Input.mousePosition.y/screenSize.y)*1334);

//  	 var mouseRatioX2 = Input.mousePosition.x / Screen.width;
// var mouseRatioY2 = Input.mousePosition.y / Screen.height;
//  	
//		startTouchPos =  Vector2(mouseRatioX2*750,mouseRatioY2*1334);
}

//print("getaxis:  "+Input.GetAxis("Mouse Y"));


//if(Input.GetAxis("Mouse Y")>0){
//	  ySense += Input.GetAxis("Mouse Y") / Time.deltaTime /PlayerPrefs.GetFloat("Sensitivity");
//	  }
//	  
//	  else{
//	  
//	  ySense = Mathf.Lerp(ySense,0,Time.deltaTime*PlayerPrefs.GetFloat("forgiveness"));
//	  }
//	  
// ySense = Mathf.Clamp(ySense,0,PlayerPrefs.GetFloat("maxVelocity"));
	  
//	var speedTxt = GameObject.Find("throwSpeed");
//	 
//	 speedTxt.GetComponent.<UI.Text>().text = "Throw Speed: " + ySense.ToString("f2");
	 
	 
	  #if UNITY_EDITOR
   //mousezlock=Mathf.Clamp(mousezlock,500,700);
  #endif

  #if UNITY_IPHONE
  mousezlock=Mathf.Clamp(mousezlock,300,700);
  #endif
if(mouseylock>1){
			mouseylock=Mathf.Lerp(mouseylock,0,Time.deltaTime*4);
			}


yield;
	}

		if (!Input.GetMouseButton (0)){

	print("released");

	    for(var i : int = 0; i < 11; i++){

ragdoll[i].velocity = Vector3.zero;

		}


			var mousez = PlayerPrefs.GetFloat("zplayerpower") ;



			var mousey = PlayerPrefs.GetFloat("yplayerpower") ;

if(springJoint.connectedBody !=null){

endPos =   Vector2((Input.mousePosition.x/screenSize.x)*750,(Input.mousePosition.y/screenSize.y)*1334);

//  	 var mouseRatioX3 = Input.mousePosition.x / Screen.width;
// var mouseRatioY3 = Input.mousePosition.y / Screen.height;
//
//endPos =  Vector2(mouseRatioX3*750,mouseRatioY3*1334);


flickspeed = endPos - startTouchPos ;
flickspeed /= (Time.time - startTime);

flickspeed.y = Mathf.Clamp(flickspeed.y,0,PlayerPrefs.GetFloat("maxVelocity"));

//slower speeds hides clamp and code
if(flickspeed.magnitude <=2000){
 mousez = flickspeed.magnitude * mousey;
}
else{
mousez =PlayerPrefs.GetFloat("zplayerpower") ;
}

		    springJoint.connectedBody.drag = oldDrag;
		   springJoint.connectedBody.angularDrag = oldAngularDrag;
		   
		   //resently added x was too much could just use Vector2
	      springJoint.connectedBody.AddForce(0,flickspeed.y*mousey,0);
	   
	       springJoint.connectedBody.AddForce(flickspeed.x*mousey,0,0);
		      springJoint.connectedBody.AddForce(0,0,mousez);

		      
		    springJoint.connectedBody = null;

		    miniPlatform.GetComponent.<Collider>().isTrigger=true;
		    if(!TNTMode ||PlayerPrefs.GetInt("selectedLevel")<=0 ){
		    ragdoll[1].SendMessage("activateWind");
		    }
		    gameObject.SendMessage("findTarget");
		    
		    	var speedTxt2 = GameObject.Find("throwSpeed");
	 
	 speedTxt2.GetComponent.<UI.Text>().text = "Throw Speed: " + flickspeed.ToString("f2");
	 
	 
		    GetComponent.<camera_Lookat>().enabled = true;
		    

		   
		    //snaps first pic with action cam
		    snapPicture();
		    
		    
		  GetComponent.<basketballThrow_TouchY>().enabled = false;
		  
		  }
		}
	
				
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
    
    if(headRigid){
    headRigid.isKinematic =false;
    headRigid.useGravity =true;
    }
    purchasableheadRigid = GameObject.Find("Purchasable_Hats");
    
      for(var m : int = 0; m <  purchasableheadRigid.transform.childCount; m++)
		{  
		if(purchasableheadRigid.transform.GetChild(m).GetComponent.<Rigidbody>()){
		purchasableheadRigid.transform.GetChild(m).GetComponent.<Rigidbody>().isKinematic = false;
				purchasableheadRigid.transform.GetChild(m).GetComponent.<Rigidbody>().useGravity = true;
//				rightHandRigid.transform.GetChild(i).GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		}
    	}
    
    rightHandRigid = GameObject.Find("Right_Hand_Acc");
    for(var i : int = 0; i <  rightHandRigid.transform.childCount; i++)
		{  
		rightHandRigid.transform.GetChild(i).GetComponent.<Rigidbody>().isKinematic = false;
				rightHandRigid.transform.GetChild(i).GetComponent.<Rigidbody>().useGravity = true;
//				rightHandRigid.transform.GetChild(i).GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		}
		
		 leftHandRigid = GameObject.Find("Left_Hand_Acc");
    for(var n: int = 0; n <  leftHandRigid.transform.childCount; n++)
		{  
		leftHandRigid.transform.GetChild(n).GetComponent.<Rigidbody>().isKinematic = false;
				leftHandRigid.transform.GetChild(n).GetComponent.<Rigidbody>().useGravity = true;
//				rightHandRigid.transform.GetChild(i).GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		}
    
    
    rightFootRigid = GameObject.Find("Shoes_Right");
       for(var j : int = 0; j <  rightFootRigid.transform.childCount; j++)
		{  
	if(rightFootRigid.transform.GetChild(j).GetComponent.<Rigidbody>()){
		rightFootRigid.transform.GetChild(j).GetComponent.<Rigidbody>().isKinematic = false;
				rightFootRigid.transform.GetChild(j).GetComponent.<Rigidbody>().useGravity = true;
//								rightFootRigid.transform.GetChild(j).GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		}
		}
    
    
       leftFootRigid = GameObject.Find("Shoes_Left");
           for(var k : int = 0; k<  leftFootRigid.transform.childCount; k++)
		{  
		
		
		if(leftFootRigid.transform.GetChild(k).GetComponent.<Rigidbody>()){
		leftFootRigid.transform.GetChild(k).GetComponent.<Rigidbody>().isKinematic = false;
				leftFootRigid.transform.GetChild(k).GetComponent.<Rigidbody>().useGravity = true;
//		leftFootRigid.transform.GetChild(k).GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		}
    }
       
    }
		}
		
		
function spawnPlayer(){
var newragdollPrefab = Instantiate(ragdollPrefab,spawnLocation.position,spawnLocation.rotation);
newragdollPrefab.name = "Player";
mainCharacter = newragdollPrefab;
characterPrefsScript.SendMessage("resetScriptStart");

print(mainCharacter.name);

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
//ragdoll[11] = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/Head_jnt/Head/Hat_jnt").GetComponent.<Rigidbody>();
headRigid = newragdollPrefab.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/Head_jnt/Head/Hat_jnt").GetComponent.<Rigidbody>();


//if(ragdoll[1].transform.GetChild(0).GetComponent.<Renderer>().material.mainTexture.name == "SimplePeople_RoadWorker_White"){
//ragdoll[1].transform.GetChild(0).GetComponent.<MeshRenderer>().enabled = true;
//}

//characterPrefsScript.SendMessage("resetHand");


}


function resetPlayer(){
//resets camera rotation
if(TNTMode){
tntScript.SendMessage("resetTNT");
}
transform.localEulerAngles = startRot;
Destroy(mainCharacter);
spawnPlayer();
cameraAction.SendMessage("returnCamera");
GetComponent.<camera_Lookat>().enabled = false;



GetComponent.<basketballThrow_TouchY>().enabled = true;
enableRag =false;
miniPlatform.GetComponent.<Collider>().isTrigger=false;
//characterPrefsScript.SendMessage("resetScriptStart");

//windFlags();
//yield WaitForFixedUpdate;
//
//characterPrefsScript.SendMessage("resetScriptStart");
//characterPrefsScript.SendMessage("resetHand");

}


public function resetPlayerGameOver(){
//resets camera rotation
if(TNTMode){
tntScript.SendMessage("resetTNT");
}
transform.localEulerAngles = startRot;
Destroy(mainCharacter);
spawnPlayer();
cameraAction.SendMessage("returnCamera");
GetComponent.<camera_Lookat>().enabled = false;


GetComponent.<basketballThrow_TouchY>().enabled = true;
enableRag =false;

var theTutorial = GameObject.Find("Tutorial");
theTutorial.SendMessage("showTutorial");

miniPlatform.GetComponent.<Collider>().isTrigger=false;

//if(PlayerPrefs.GetInt("selectedLevel")==2){
//var kegCupsParent = GameObject.Find( "KegCups");
//if(kegCupsParent!=null){
//Destroy(kegCupsParent);
//}
//}

//yield WaitForFixedUpdate;
//characterPrefsScript.SendMessage("resetScriptStart");
//characterPrefsScript.SendMessage("resetHand");

}



function snapPicture(){
var actionCam = GameObject.Find("CameraAction_Screenshot");
if(actionCam!=null){
//actionCam.SendMessage("findTarget");
yield WaitForSeconds(1.5);

//if(actionCam.GetComponent.<Camera>().enabled == true){
actionCam.SendMessage("newScreen");
//newScreen();
//}
}
}

//function snapPicture(){
//var actionCam = Instantiate(cameraScreenshot,new Vector3(40f,-6.9f,111.4f),Quaternion.Euler(30.62,-86.64,0));
//actionCam.name = "CameraAction_Screenshot";
//if(actionCam!=null){
//actionCam.SendMessage("findTarget");
//yield WaitForSeconds(1.5);
//
//if(actionCam.GetComponent.<Camera>().enabled == true){
//actionCam.SendMessage("newScreen");
////newScreen();
//}
//}
//Destroy(actionCam);
//}


		