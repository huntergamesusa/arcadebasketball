#pragma strict
import UnityEngine.UI;

var theOutline:Outline;

var randColor: Color[];
 var colorint:int;
 var colorBool:boolean=false;
var speed :float;
var outlineColor:Color;

function resetToFalse(){
colorBool =false;
}

	function OnApplicationPause (paused:boolean) {
		if (paused) {
		resetToFalse();
//		print("paused reset attempt");
		}
		else {
		resetToFalse();
//				print("unpaused reset attempt");


		}
	}
	
		function OnEnable () {
resetToFalse();
	}

function Update () {
randomColorFunc();

outlineColor=Color.Lerp(outlineColor,randColor[colorint], Time.deltaTime*speed);

theOutline.effectColor=outlineColor;

}

function randomColorFunc(){
if(!colorBool){
colorBool=true;
colorint++;
if(colorint<5){

}
else{
colorint=0;
}

yield WaitForSeconds (.1);

colorBool=false;

}

}


