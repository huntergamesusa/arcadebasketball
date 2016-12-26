
var spring = 50.0;
var damper = 5.0;
var drag= 10.0;
var angularDrag = 5.0;
var distance= 0;
var throwForce = 50000;
var throwRange = 100000;
var attachToCenterOfMass = false;
//var spinforce:float=10.0;
var maxAngVel:float=10.0;
//var backcam:Camera;
//var difficultyfloat:float;
private var springJoint : SpringJoint;
var conversionray:float;
var conversionray2:float;
var mousezlock:float;
var mouseylock:float;
var horseshoecount:int;
//	var hs_prefab:GameObject;
//	var hs_prefab_cpu:GameObject;
//var windspeed:int;
//var windtxt:GUIText;
var speedz:GUIText;
var speedy:GUIText;
//var scoretxt:GUIText;
//var cpuscoretxt:GUIText;
//var playersturn:int;
//var cpueasy:boolean=false;
//var cpumedium:boolean=false;
//var cpuhard:boolean=false;

var yplayerpower:float;
var zplayerpower:float;
//var yplayerpowertxt:GUIText;
//var zplayerpowertxt:GUIText;

//static var checkscore:boolean=false;
//static var changewinddirection:boolean=false;
//var multiplier:int;
//var shoesleft:int;
//static var roundoverbool:boolean=false;
function Start(){


//StartCoroutine("spawnhorseshoe");
	mousezlock=0;
	
//	windspeed=(Random.Range(0,12));
//	windtxt.text=windspeed+" MPH";
//	changewinddirection=false;
//Application.targetFrameRate=30;

	yplayerpower=1200;
	zplayerpower=20.5;
// PlayerPrefs.SetString("style","left");
}

//function OnGUI(){
//yplayerpower=GUI.HorizontalSlider(Rect(25,60,100,30),yplayerpower,0.1000, 2500);
//zplayerpower=GUI.HorizontalSlider(Rect(25,120,100,30),zplayerpower,0.0, 100);
//yplayerpowertxt.text="ypower: "+yplayerpower.ToString("f1");
//zplayerpowertxt.text="zpower: "+zplayerpower.ToString("f1");
//}
 	
function LateUpdate ()
{


//		 var radians:float = wind.wind * (Mathf.PI / 180);

// var degreeVector:Vector3 = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
//	print(degreeVector);
//	var shoefind:GameObject=gameObject.Find("shoe"+horseshoecount);
//	if(shoefind){
//	shoefind.GetComponent.<Rigidbody>().AddForce(windspeed*2*degreeVector.x,0,windspeed*degreeVector.z);
	
//	}

//if(Input.GetKeyUp(KeyCode.Space)){
// var  hs_prefab3=Instantiate(hs_prefab_cpu,Vector3(0,5,40),Quaternion.Euler(0,90,360));
//hs_prefab3.GetComponent.<Rigidbody>().AddForce(0,0,8000);
////hs_prefab3.transform.GetChild(0).rigidbody.AddForce(0,0,4000);
//}

//	speedz.text="speedz "+mousezlock.ToString("f2");
//	speedy.text="speedy "+mouseylock.ToString("f2");
//		scoretxt.text="P1 Score: " +score.score.ToString("f0");
//		if(PlayerPrefs.GetString("gamemode")=="passnplay"){
//			cpuscoretxt.text="P2 Score: " +score.cpuscore.ToString("f0");
//}
//	if(PlayerPrefs.GetString("gamemode")=="playcpu"){
//			cpuscoretxt.text="CPU Score: " +score.cpuscore.ToString("f0");
//}

//if(PlayerPrefs.GetString("gamemode")=="standardplay"){
//cpuscoretxt.text="Shoes: "+shoesleft;
//}

//if(Time.timeScale==0)
//return;

	// Make sure the user pressed the mouse down
//	if(playersturn==2&&PlayerPrefs.GetString("gamemode")=="playcpu")
//	return;
	if (!Input.GetMouseButtonDown (0))
		return;
		
 		var mainCamera = FindCamera();
 
	// We need to actually hit an object
	var hit : RaycastHit;
	if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),  hit, 220))
		return;
if (!hit.rigidbody )
		return;
 
	if (!springJoint)
	{
		var go = new GameObject("Rigidbody dragger");
		var body : Rigidbody = go.AddComponent.<Rigidbody>() as Rigidbody;
		springJoint = go.AddComponent.<SpringJoint>();
go.transform.position=Vector3(0,9,3.27);
		body.isKinematic = true;
	}
	
//var dragger:GameObject=gameObject.Find("Rigidbody dragger");
//dragger.transform.position=Vector3(0,9,-60);
//var shoe:GameObject=gameObject.Find("shoe"+horseshoecount);
// var shoe : GameObject = hit.gameObject;
//shoe.GetComponent.<Rigidbody>().isKinematic=false;
	
		var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
		anchor= springJoint.transform.InverseTransformPoint(anchor);
		springJoint.anchor = anchor;

	springJoint.connectedBody = hit.rigidbody;
	springJoint.spring = spring;
	springJoint.damper = damper;
	springJoint.maxDistance = .1;

 	print(hit.distance);
	StartCoroutine ("DragObject", hit.distance);	

	
}

  function DragObject ()
{
yield new WaitForFixedUpdate ();

	var oldDrag = springJoint.connectedBody.drag;
	var oldAngularDrag = springJoint.connectedBody.angularDrag;
	springJoint.connectedBody.drag = drag;
	springJoint.connectedBody.angularDrag = angularDrag;
	var mainCamera = FindCamera();

distance=.2;

	
	

	
	while (Input.GetMouseButton (0))
	{
//var dragger:GameObject=gameObject.Find("Rigidbody dragger");
//springJoint.connectedBody.transform.position=dragger.transform.position;
		var ray = mainCamera.ScreenPointToRay (Input.mousePosition);
springJoint.GetComponent.<Rigidbody>().isKinematic=true;
conversionray=ray.direction.y+1.3;

conversionray2=(152-100)/(1-ray.direction.y+.24);

//	springJoint.transform.position.x = ray.origin.x;
// springJoint.transform.position.y = ray.origin.y;
//	springJoint.transform.position.z = .2;
//		print(ray.origin.x);
//springJoint.connectedBody.transform.position.y=springJoint.transform.position.y;
		springJoint.transform.position = ray.GetPoint(12);

springJoint.connectedBody.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY |RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezeRotationX;

var mousez = Input.GetAxis("Mouse Y")*zplayerpower ;
var mousey = Input.GetAxis("Mouse Y")*yplayerpower /conversionray;
var newypower:float;
		var mousex = Input.GetAxis("Mouse X");
		newypower=yplayerpower/(conversionray+1);
//print(Input.GetAxis("Mouse Y"));		
		
		if(mousez>mousezlock){
		
		mousezlock=mousez;
		//yield;
	
		}
			if(mousey>mouseylock){
		
		mouseylock=mousey;
		//yield;
	
		}
		if(mousezlock>1){
			mousezlock=Mathf.Lerp(mousezlock,0,Time.deltaTime*4);
			}
//adjust 206 to lock max height force
		//mouseylock=Mathf.Clamp(mouseylock,0,(250-conversionray));
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
//	print(windspeed);
		
			yield new WaitForFixedUpdate ();
			springJoint.connectedBody.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.None;
			
	      springJoint.connectedBody.AddForce(0,0,mousezlock*6);
//adjust (0,0,z) to throw z direction
		      
		      	  #if UNITY_EDITOR
 //springJoint.connectedBody.AddForce(0,mouseylock ,0);
  springJoint.connectedBody.AddForce(0,newypower ,0);
  #endif

  #if UNITY_IPHONE
 //springJoint.connectedBody.AddForce(0,mouseylock*4 ,0);
 springJoint.connectedBody.AddForce(0,newypower ,0);
 
  #endif
		      
		      
		       springJoint.connectedBody.AddForce(-mousex,0,0);
		       
		     if(  PlayerPrefs.GetString("style")=="left"){
		       springJoint.connectedBody.AddTorque(Vector3.left*15*115);
		       }
		        if(  PlayerPrefs.GetString("style")=="up"){
		       springJoint.connectedBody.AddTorque(Vector3.up*15*225);
		       }
		   // return;

		    springJoint.connectedBody.drag = oldDrag;
		   springJoint.connectedBody.angularDrag = oldAngularDrag;

		    springJoint.connectedBody = null;
		
//yield WaitForSeconds(4.9);


 
//  if(playersturn==1){
//  if(PlayerPrefs.GetString("gamemode")=="passnplay"||PlayerPrefs.GetString("gamemode")=="playcpu"){
//  playersturn=2;
//  }
//StartCoroutine("spawnhorseshoe");
//
//}
//if(playersturn==2&&PlayerPrefs.GetString("gamemode")=="passnplay"){
// StartCoroutine("spawnhorseshoe_player2");
// }
//if(PlayerPrefs.GetString("gamemode")=="standardplay"){
//  countpoints();
// destroyhs_std();
// }



//changewinddirection=true;
//checkscore=true;
// windspeed=(Random.Range(0,12));
// 	windtxt.text=windspeed+" MPH";
 
//yield WaitForEndOfFrame;

// changewinddirection=false;
// checkscore=false;
//
// StartCoroutine("aithrow");


 
		}
	
				
	}
	
function FindCamera ()
{
	if (GetComponent.<Camera>())
		return GetComponent.<Camera>();
	else
		return Camera.main;

		
		}
	
//function spawnhorseshoe(){
//if(playersturn==1){
//yield new WaitForFixedUpdate ();
//
//	horseshoecount++;
//shoesleft--; 
// 	 var  hs_prefab2=Instantiate(hs_prefab,Vector3(0,9,-60),Quaternion.Euler(0,90,360));
// hs_prefab2.name="shoe"+horseshoecount;
// hs_prefab2.GetComponent.<Rigidbody>().isKinematic=true;
//
//
//
//if(!PlayerPrefs.GetString("gamemode")=="standardplay"){
//playersturn=2;
// }
// if(PlayerPrefs.GetString("gamemode")=="passnplay"){
//playersturn=2;
// }
// 
//
// 
//
//
//  if(PlayerPrefs.GetString("gamemode")=="passnplay"||PlayerPrefs.GetString("gamemode")=="playcpu"){
// if(horseshoecount==5){
//
// print("hit5");
//  countpoints();
// destroyhs();
//
// multiplier++;
// 
// }
//  if(multiplier>=2&&horseshoecount>=4*multiplier){
//
// print("hit5");
//  countpoints();
// destroyhs();
// multiplier++;
// }
// }
// }
//}
//
//function spawnhorseshoe_player2(){
//yield new WaitForFixedUpdate ();
//	horseshoecount++;
//  		
// 
// 	 var  hs_prefab3=Instantiate(hs_prefab_cpu,Vector3(0,9,-60),Quaternion.Euler(0,90,360));
// hs_prefab3.name="shoe"+horseshoecount;
// hs_prefab3.GetComponent.<Rigidbody>().isKinematic=true;
//playersturn=1;
//
//}

//function aithrow(){
//if(playersturn==2&&PlayerPrefs.GetString("gamemode")=="playcpu"){
//	horseshoecount++;
//  var  hs_prefab3=Instantiate(hs_prefab_cpu,Vector3(0,9,-60),Quaternion.Euler(0,90,360));
// hs_prefab3.name="shoe"+horseshoecount;
// hs_prefab3.GetComponent.<Rigidbody>().isKinematic=true;
//
//var radians2:float = wind.wind * (Mathf.PI / 180);
//
// var degreeVector2:Vector3 = new Vector3(Mathf.Cos(radians2), 0, Mathf.Sin(radians2));
//	print(-1*degreeVector2);
//	
//	
//yield WaitForSeconds(2);
//print(windspeed);
//var shoe2:GameObject=gameObject.Find("shoe"+horseshoecount);
//var cpubalancewindx:float;
//var cpubalancewindz:float;
//var cpuzpower:float;
//var cpuypower:float;
//cpuzpower=4000;
//cpuypower=2500;
//cpubalancewindx=140;
//cpubalancewindz=55;
//
////if(cpueasy){
////cpuzpower=Random.Range(3000,5000);
////cpuypower=Random.Range(1800,3200);
////}
////if(cpumedium){
////cpuzpower=Random.Range(3500,4500);
////cpuypower=Random.Range(2200,2800);
////}
//
//shoe2.GetComponent.<Rigidbody>().isKinematic=false;
//
//shoe2.GetComponent.<Rigidbody>().AddForce(0,0,cpuzpower);
//shoe2.GetComponent.<Rigidbody>().AddForce(0,cpuypower,0);
//
//shoe2.GetComponent.<Rigidbody>().AddForce(-1*degreeVector2.x*cpubalancewindx*windspeed,0,-1*degreeVector2.z*cpubalancewindz*windspeed);
//
//
//    if(  PlayerPrefs.GetString("style")=="left"){
//		       shoe2.GetComponent.<Rigidbody>().AddTorque(Vector3.left*15*115);
//		       }
//		        if(  PlayerPrefs.GetString("style")=="up"){
//		 shoe2.GetComponent.<Rigidbody>().AddTorque(Vector3.up*15*225);
//		       }
//
//		yield WaitForSeconds(4.9);
//	playersturn=1;
//StartCoroutine("spawnhorseshoe");
//	
//changewinddirection=true;
//checkscore=true;
// windspeed=(Random.Range(0,12));
// 	windtxt.text=windspeed+" MPH";
// 
//yield WaitForEndOfFrame;
//
// changewinddirection=false;
// checkscore=false;
// }
//}
//		
//function destroyhs(){
//var sh1:GameObject=gameObject.Find("shoe"+(horseshoecount-4));
//var sh2:GameObject=gameObject.Find("shoe"+(horseshoecount-3));
//var sh3:GameObject=gameObject.Find("shoe"+(horseshoecount-2));
//var sh4:GameObject=gameObject.Find("shoe"+(horseshoecount-1));
//sh1.GetComponent.<Rigidbody>().drag=20;
//sh2.GetComponent.<Rigidbody>().drag=20;
//sh3.GetComponent.<Rigidbody>().drag=20;
//sh4.GetComponent.<Rigidbody>().drag=20;
//sh1.GetComponent.<Collider>().enabled=false;
//sh2.GetComponent.<Collider>().enabled=false;
//sh3.GetComponent.<Collider>().enabled=false;
//sh4.GetComponent.<Collider>().enabled=false;
//sh1.transform.GetChild(0).GetComponent.<Collider>().enabled=false;
//sh2.transform.GetChild(0).GetComponent.<Collider>().enabled=false;
//sh3.transform.GetChild(0).GetComponent.<Collider>().enabled=false;
//sh4.transform.GetChild(0).GetComponent.<Collider>().enabled=false;
//yield WaitForSeconds(3);
//Destroy(sh1);
//Destroy(sh2);
//Destroy(sh3);
//Destroy(sh4);
//
//}

//function countpoints(){
//
//roundoverbool=true;
//yield WaitForEndOfFrame;
//roundoverbool=false;
//
//}
//
//function destroyhs_std(){
//
//var sh4:GameObject=gameObject.Find("shoe"+(horseshoecount));
//
//sh4.GetComponent.<Rigidbody>().drag=20;
//
//sh4.GetComponent.<Collider>().enabled=false;
//yield WaitForSeconds(3);
//
//Destroy(sh4);
//
//}