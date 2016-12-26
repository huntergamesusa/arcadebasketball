#pragma strict

 import System.Collections.Generic;
 

var mainLight:Light;
var snow:GameObject;


function Awake () {
if(PlayerPrefs.GetInt("Volume")==0){
AudioListener.volume = 1;
if(gameObject.name == "Mute"){
gameObject.SetActive(true);
}
if(gameObject.name == "UnMute"){
gameObject.SetActive(false);
}
}
else{
AudioListener.volume = 0;
if(gameObject.name == "UnMute"){
gameObject.SetActive(true);
}
if(gameObject.name == "Mute"){
gameObject.SetActive(false);
}
}


}

function Start () {

if(PlayerPrefs.HasKey("Shadows")==false){
PlayerPrefs.SetInt("Shadows",1);

     if(gameObject.name == "Shadows"){

gameObject.SetActive(true);

}
if(gameObject.name == "NoShadows"){
gameObject.SetActive(false);
}
}

if(PlayerPrefs.GetInt("Shadows")==1){



     if(gameObject.name == "Shadows"){

gameObject.SetActive(true);

}
if(gameObject.name == "NoShadows"){
gameObject.SetActive(false);


}
}
else{

if(gameObject.name == "NoShadows"){

gameObject.SetActive(true);
}
if(gameObject.name == "Shadows"){

gameObject.SetActive(false);
}
}

}


function setVolumePrefs(volume:int){
PlayerPrefs.SetInt("Volume",volume);
if(PlayerPrefs.GetInt("Volume")==0){
AudioListener.volume = 1;
}
else{
AudioListener.volume = 0;
}


}

function shadowsOnOff(shadows:int){


if(PlayerPrefs.GetInt("Shadows")==1){
     PlayerPrefs.SetInt("Shadows",0);
     mainLight.shadows = LightShadows.None;
     snow.SetActive(false);

}
else{
        PlayerPrefs.SetInt("Shadows",1);
             mainLight.shadows = LightShadows.Hard;
                  snow.SetActive(true);



}
}