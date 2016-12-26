#pragma strict
var speedFade:float;
var maxVolume:float;
var minVolume:float;
private var audioSource : AudioSource;
private var fadeBool:boolean=false;


function Awake(){
audioSource = GetComponent.<AudioSource>();

}

public function fadeInAudio(){
fadeBool = false;

}

public function fadeOutAudio(){
fadeBool = true;

}

function Update(){
if(audioSource.volume <maxVolume && !fadeBool ){
audioSource.volume = Mathf.Lerp(audioSource.volume,maxVolume,Time.deltaTime * speedFade);
}
if(audioSource.volume >minVolume && fadeBool ){
audioSource.volume = Mathf.Lerp(audioSource.volume,minVolume,Time.deltaTime * speedFade);
}
}