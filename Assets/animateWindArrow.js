#pragma strict
private var windAngle :float;
private var windRect:RectTransform;
private var windTXTRect:RectTransform;

private var startPos:float;
private var startPosText:float;

 var textWind:GameObject;
 
function Start () {
windRect = GetComponent.<RectTransform>();
windTXTRect = textWind.GetComponent.<RectTransform>();
startPos = windRect.localPosition.y;
startPosText = windTXTRect.localPosition.y;
}

function Update () {

}

function receiveWindDirection(receivedDirection:float){
windAngle = receivedDirection;
}


public function animateArrow(){
if(PlayerPrefs.GetInt("selectedLevel")>0)
return;
//GetComponent.<AudioSource>().volume = 1;
windRect.localPosition.y = startPos;
windTXTRect.localPosition.y = startPosText;

//windRect.position.y = 60;

LeanTween.move(windTXTRect, new Vector3(0,-15f,0), 1.25f).setEase(LeanTweenType.easeOutElastic);

LeanTween.move(windRect, new Vector3(0,-85f,0), 1.25f).setEase(LeanTweenType.easeOutElastic);

//scaleWind();

GetComponent.<AudioSource>().Play();

// for(var i : int = 0; i < 3; i++){
//GetComponent.<AudioSource>().Play();
//GetComponent.<AudioSource>().volume -=.25;
//yield WaitForSeconds(.25);
//
//}

//iTween.MoveTo(gameObject, iTween.Hash("y",-85f,"time",1.25f,"easeType",iTween.EaseType.easeOutBounce));

//transform.localEulerAngles.z = 180f;
//LeanTween.rotateLocal(gameObject.GetComponent.<RectTransform>(), windAngle, 1.25f).setEase(LeanTweenType.easeOutElastic);
//iTween.RotateTo(gameObject, iTween.Hash("z",windAngle,"islocal",true,"time",1.25f,"easeType","delay",1.25f,iTween.EaseType.easeOutElastic));


}

//public function scaleWind(){
////for(var i:int = 0; i < 3; i++){
//yield WaitForSeconds(1.25f);
//LeanTween.scale(windRect, new Vector3(1.25f,1.25f,1.25f), 1f).setEase(LeanTweenType.easeOutElastic);
//
//LeanTween.scale(windTXTRect, new Vector3(1.25f,1.25f,1.25f), 1f).setEase(LeanTweenType.easeOutElastic);
//
//LeanTween.scale(windRect, new Vector3(1f,1f,1f), 1f).setEase(LeanTweenType.easeOutElastic).setDelay(.3f);
//
//LeanTween.scale(windTXTRect, new Vector3(1f,1f,1f), 1f).setEase(LeanTweenType.easeOutElastic).setDelay(.3f);
////}
//}

