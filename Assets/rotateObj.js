#pragma strict
var rotateSpeed:float;
function Start () {
 iTween.RotateBy(gameObject, iTween.Hash("y",1,"time", rotateSpeed,"looptype",iTween.LoopType.loop,"easeType", iTween.EaseType.linear, "space", "world"));   
}

function Update () {

}
function restartRotate(){

 iTween.RotateBy(gameObject, iTween.Hash("y",1,"time", rotateSpeed,"looptype",iTween.LoopType.loop,"easeType", iTween.EaseType.linear, "space", "world"));   

}