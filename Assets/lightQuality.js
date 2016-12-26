#pragma strict

function Awake () {
if(PlayerPrefs.HasKey("Shadows")==false){
PlayerPrefs.SetInt("Shadows",1);
     GetComponent.<Light>().shadows = LightShadows.Hard;

}

if(PlayerPrefs.GetInt("Shadows")==0){

     GetComponent.<Light>().shadows = LightShadows.None;
}
else{
     GetComponent.<Light>().shadows = LightShadows.Hard;

}
}

function Update () {

}