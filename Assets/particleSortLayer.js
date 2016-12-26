#pragma strict
var someOrder:int;
function Start () {
//particleSystem.renderer.sortingLayerName = ";
GetComponent.<ParticleSystem>().GetComponent.<Renderer>().sortingOrder = someOrder;
}

function Update () {

}