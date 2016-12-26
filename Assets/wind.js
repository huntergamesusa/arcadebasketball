#pragma strict


var activateWindBool:boolean=false;

//var flags:GameObject[];

private var body:Rigidbody;
private var windPos:Transform;
private var windPosUI:GameObject;
private var windTxtUI:GameObject;
static var windMPH : int;
private var windAngle:float;
private var randomAngle:int;
var windFloat:float;
function Awake () {
activateWindBool = false;
body = GetComponent.<Rigidbody>();
windPos = GameObject.Find("Wind").transform;
randomAngle = Random.Range(0,2);
if(randomAngle > 0){
windAngle =90;

}
else{
windAngle = 270;
}

if(PlayerPrefs.GetInt("selectedLevel")==0){
windMPH = Random.Range(0,11);
}
else{
windMPH = Random.Range(0,17);

}

windPos.transform.eulerAngles.y = windAngle;
windPosUI = GameObject.Find("WindUI");
windTxtUI = GameObject.Find("WindTxt");
if(windTxtUI!=null){
windTxtUI.GetComponent.<UI.Text>().text = windMPH.ToString("f0") +" MPH";
}
if(windPosUI!=null){
windPosUI.transform.localEulerAngles.z =180f ;
}


}



function FixedUpdate () {

if(activateWindBool || PlayerPrefs.GetInt("selectedLevel")==0 || PlayerPrefs.GetInt("selectedLevel")==-1){
GetComponent.<Rigidbody>().AddForce(windPos.forward * PlayerPrefs.GetFloat("windSpeed") * (windMPH));
//print(PlayerPrefs.GetFloat("windSpeed") * windMPH);
}

}

public function activateWind(){

activateWindBool = true;


}

public function deactivateWind(){

activateWindBool = false;


}

function Start(){
//setWindSpeed(33);

if(windPosUI!=null){
//windPosUI.transform.localEulerAngles.z =windAngle ;
windPosUI.SendMessage("receiveWindDirection",windAngle);

//LeanTween.rotateLocal(windPosUI.GetComponent.<RectTransform>(), windAngle, 1.25f).setEase(LeanTweenType.easeOutElastic);

iTween.RotateTo(windPosUI, iTween.Hash("z",windAngle,"islocal",true,"time",1.25f,"easeType",iTween.EaseType.easeOutElastic));


}
}


//function setWindSpeed(speed:float){
//windFloat = speed;
//PlayerPrefs.SetFloat("windSpeed",windFloat);
//}
