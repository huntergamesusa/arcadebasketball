#pragma strict
private var charJoint:CharacterJoint;
private var yardSaleBool:boolean =false;
var theBreakForce:float;
function Start () {
charJoint = GetComponent.<CharacterJoint>();
GetComponent.<Rigidbody>().mass =.001;
}

function Update () {

}
//function EnableYardSale(){
//yardSaleBool =true;
//
//}
//function DisableYardSale(){
//yardSaleBool =false;
//
//}

function OnTriggerEnter(coll:Collider){
if(coll.gameObject.tag == "camCollider" && charJoint!=null){
print("enabling breakjoint");
charJoint.breakForce = theBreakForce;
GetComponent.<Rigidbody>().mass =.25;
}
}