#pragma strict
private var charJoint:CharacterJoint;
private var yardSaleBool:boolean =false;
private var hits:int;

var theBreakForce:float;
function Start () {
charJoint = GetComponent.<CharacterJoint>();
GetComponent.<Rigidbody>().mass =.001;
}

function Update () {

}
function EnableYardSale(){
yardSaleBool =true;

}
function DisableYardSale(){
yardSaleBool =false;

}

function OnCollisionEnter(coll:Collision){
if(coll.gameObject.tag == "island"){


hits++;

//GetComponent.<AudioSource>().PlayOneShot(punchClip);
if(hits<2){




        if (coll.relativeVelocity.magnitude > theBreakForce){
        GetComponent.<Rigidbody>().isKinematic =false;
        GetComponent.<Rigidbody>().useGravity =true;
GetComponent.<Rigidbody>().mass =1;
              }
       

}

}
}