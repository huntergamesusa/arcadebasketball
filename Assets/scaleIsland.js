#pragma strict

function Start () {

}

function Update () {

}

public function adjustScale(thisScale:String){
transform.localScale=Vector3(parseFloat(thisScale),parseFloat(thisScale),parseFloat(thisScale));



}

public function adjustTimeScale(thisTime:String){
Time.timeScale =parseFloat(thisTime);


}

public function adjustPlacement(Pos:float){

transform.position.z = Pos;

}

public function adjustGravity(Grav:float){

Physics.gravity.y = Grav;

}
public function adjustPlacementY(PosY:float){

transform.position.y = PosY;

}