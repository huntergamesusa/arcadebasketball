#pragma strict
import UnityEngine.UI;
private var AnimStopped:boolean =false;
function Awake () {
GetComponent.<Image>().enabled= false;
GetComponent.<Animator>().enabled = false;
AnimStopped = false;
}

function Update () {
 if(Input.GetMouseButton (0)){
 StopCoroutine("showTutorial");
GetComponent.<Image>().enabled= false;
GetComponent.<Animator>().enabled = false;
AnimStopped =true;
 }

}

public function stopShowTutorial(stop:boolean){
AnimStopped = stop;
}

public function showTutorial(){
AnimStopped=false;
if(PlayerPrefs.GetInt("highscore")<1){
yield WaitForSeconds(2);
if(!AnimStopped){
GetComponent.<Image>().enabled = true;
GetComponent.<Animator>().enabled = true;
GetComponent.<Animator>().playbackTime =0;

}
}
else{
yield WaitForSeconds(8);
if(!AnimStopped){
GetComponent.<Image>().enabled = true;
GetComponent.<Animator>().enabled = true;
GetComponent.<Animator>().playbackTime =0;
}
}

}