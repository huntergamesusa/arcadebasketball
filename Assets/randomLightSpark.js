#pragma strict
private var lightBool:boolean=false;
var twinklespeed:float;
function Start () {

}

function Update () {
changeLight();
//GetComponent.<Light>().intensity = Mathf.Lerp(GetComponent.<Light>().intensity,Random.Range(.1,8),Time.deltaTime*1);
}

function changeLight(){
if(!lightBool){
lightBool = true;
GetComponent.<Light>().intensity = Random.Range(.1,5);
yield WaitForSeconds (twinklespeed);
lightBool = false;

}


}