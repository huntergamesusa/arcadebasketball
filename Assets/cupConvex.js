#pragma strict

function Start () {

}

function Update () {

}

function OnTriggerEnter(colls:Collider){
if(colls.gameObject.tag == "ragdoll"||colls.gameObject.tag == "rigidbody"){
transform.parent.gameObject.GetComponent.<MeshCollider>().convex =false;
}
}