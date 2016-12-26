#pragma strict
var thPo : GameObject;
private var numbColl:int;
function Start () {

}

function Update () {

}

function OnTriggerEnter(coll:Collider){
numbColl++;
if(numbColl<=1){
var thPoInt = Instantiate(thPo, Vector3(transform.position.x,transform.position.y+.5,transform.position.z),Quaternion.Euler(-90,0,0));
Destroy(gameObject);
}


}