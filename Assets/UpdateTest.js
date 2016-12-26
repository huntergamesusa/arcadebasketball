#pragma strict
import UnityEngine.UI;

private var myText:Text;
private var updateTime:float;
private var fixedupdateTime:float;
private var deltaTime:float;
private var fixedupdateFloat:float;
private var updateFloat:float;
private var deltaFloat:float;



function Awake () {
myText = GetComponent.<Text>();
}

function Update () {
updateFloat++;
deltaFloat = updateFloat-fixedupdateFloat;
updateTime = Time.time;
deltaTime = updateTime - fixedupdateTime;
myText.text = "Update Time: "+ updateTime +"\n" +  "Fixed Update Time: "+ fixedupdateTime + "\n" + "Delta Time: "+deltaTime + "\n" 
+ "Update Float: "+ updateFloat +"\n" + "Fixed Update Float: "+ fixedupdateFloat +"\n" +"Delta Float: "+ deltaFloat +"\n"
;

}

function FixedUpdate(){

fixedupdateFloat++;
fixedupdateTime = Time.time;

}