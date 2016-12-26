#pragma strict
private var charJoint:FixedJoint;
private var yardSaleBool:boolean =false;
private var attachInt:int;
var theBreakForce:float;
var isBoard:boolean=false;
function Start () {

if(isBoard){

}
else{
if(GetComponent.<FixedJoint>()){
charJoint = GetComponent.<FixedJoint>();
}
if(GetComponent.<Rigidbody>()){
GetComponent.<Rigidbody>().mass =.001;
}
//GetComponent.<Rigidbody>().centerOfMass = GetComponent.<CharacterJoint>().connectedAnchor;
}
}

function Update () {

}
function EnableYardSale(){
yardSaleBool =true;
print("yardsaleisActive");
}
function DisableYardSale(){
yardSaleBool =false;

}

function OnTriggerEnter(coll:Collider){

//fixing flailing around fixed joint on flick
if(attachInt<1&&coll.gameObject.tag == "camCollider" ){
attachInt++;

if(isBoard){
gameObject.GetComponent.<Rigidbody>().mass = 1;

var fixedJnts : FixedJoint[];
	fixedJnts = gameObject.GetComponents.<FixedJoint>();
	for (var joint : FixedJoint in fixedJnts) {
		joint.breakForce = theBreakForce*2;
	}
	GetComponent.<Collider>().isTrigger = false;
gameObject.GetComponent.<Rigidbody>().useGravity = true;

}
else{

if(transform.parent.name == "Shoes_Left"){
gameObject.AddComponent.<Rigidbody>();
gameObject.GetComponent.<Rigidbody>().mass = 1;
gameObject.GetComponent.<Rigidbody>().angularDrag = 0;
gameObject.AddComponent.<FixedJoint>();
GetComponent.<FixedJoint>().breakForce = theBreakForce*2;
GetComponent.<FixedJoint>().connectedBody =transform.parent.transform.parent.transform.parent.GetComponent.<Rigidbody>();
GetComponent.<Collider>().isTrigger = false;


}
if(transform.parent.name == "Shoes_Right" ){
gameObject.AddComponent.<Rigidbody>();
gameObject.GetComponent.<Rigidbody>().mass = 1;
gameObject.GetComponent.<Rigidbody>().angularDrag = 0;
gameObject.AddComponent.<FixedJoint>();
GetComponent.<FixedJoint>().breakForce = theBreakForce*2;
GetComponent.<FixedJoint>().connectedBody =transform.parent.transform.parent.transform.parent.GetComponent.<Rigidbody>();
GetComponent.<Collider>().isTrigger = false;

}

if(gameObject.name == "Hat_jnt"){
GetComponent.<Collider>().isTrigger = false;
gameObject.AddComponent.<Rigidbody>();
gameObject.GetComponent.<Rigidbody>().mass = 1;
gameObject.GetComponent.<Rigidbody>().angularDrag = 0;
gameObject.AddComponent.<FixedJoint>();
GetComponent.<FixedJoint>().breakForce = theBreakForce;
GetComponent.<FixedJoint>().connectedBody =transform.parent.GetComponent.<Rigidbody>();
}
if(transform.parent.name == "Purchasable_Hats"){
GetComponent.<Collider>().isTrigger = false;
gameObject.AddComponent.<Rigidbody>();
gameObject.GetComponent.<Rigidbody>().mass = 1;
gameObject.GetComponent.<Rigidbody>().angularDrag = 0;
gameObject.AddComponent.<FixedJoint>();
GetComponent.<FixedJoint>().breakForce = theBreakForce;
GetComponent.<FixedJoint>().connectedBody =transform.parent.transform.parent.GetComponent.<Rigidbody>();
}


}
}

//if(gameObject.name == "Hat_jnt"){
//if(coll.gameObject.tag == "camCollider" && charJoint && yardSaleBool){
//charJoint.breakForce = theBreakForce;
//GetComponent.<Rigidbody>().mass =1;
//}
//}
//else{
////purchased hats
//if(coll.gameObject.tag == "camCollider" && charJoint){
//charJoint.breakForce = theBreakForce;
//GetComponent.<Rigidbody>().mass =1;
//}
//
//}

}