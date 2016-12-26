#pragma strict
import UnityEngine.UI;
var g : Gradient;
var gck : GradientColorKey[];
var gak : GradientAlphaKey[];
function Start () {
  gck = new GradientColorKey[2];
    gck[0].color = Color.red;
    gck[0].time = 0.0f;
    gck[1].color = Color.blue;
    gck[1].time = 1.0f;
    
     gak = new GradientAlphaKey[2];
    gak[0].alpha = 1.0f;
    gak[0].time = 0.0f;
    gak[1].alpha = 0.0f;
    gak[1].time = 1.0f;
    
     g.SetKeys(gck,gak);
     
//     GetComponent.<Text>().color = g;
}

function Update () {

}