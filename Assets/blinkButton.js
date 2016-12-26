#pragma strict
import UnityEngine.UI;
//var blinkBool:boolean = false;
var blinking :boolean =false;
function Start () {

}

function Update () {

if(GetComponent.<Button>().enabled ==true){
Blinking();
}

}

function Blinking(){
if(!blinking){
blinking =true;
GetComponent.<Image>().color = Color(1,.5,.5);
yield WaitForSeconds(.3);
GetComponent.<Image>().color = Color.white;
yield WaitForSeconds(.3);
blinking =false;

}

}