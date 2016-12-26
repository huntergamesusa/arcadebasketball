/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/

// The target we are following
var target : Transform;
// The distance in the x-z plane to the target

// the height we want the camera to be above the target
var height = 5.0;
// How much we 
var smoothingValue:float;
var smoothingForward:float;
var xOffset:float;
var zOffset:float;
var rotationalSmooth:float;
private var xValue:float;
private var xValue2:float;

private var zValue:float;
private var zValue2:float;

private var yrotValue:float;
private var yrotValue2:float;

private var pValue : Vector3;
private var pValue2 : Vector3;
private var thrownBool:boolean =false;
// Place the script in the Camera-Control group in the component menu
@script AddComponentMenu("Camera-Control/Smooth Follow")

function Start(){

thrownBool =false;
}

function LateUpdate () {
	// Early out if we don't have a target

	if (!target || !thrownBool)
		return;
	
//xValue = target.position.x-xOffset;

//xValue2 = Mathf.Lerp(xValue2,xValue,Time.deltaTime *smoothingValue);

pValue = Vector3(target.position.x - xOffset,target.position.y - height, target.position.z - zOffset);
pValue2 = Vector3.Lerp(pValue2, pValue, Time.deltaTime * smoothingValue);
	
//	transform.position.x =xValue2;
//	transform.position.y =height;
	
//	transform.position=new Vector3(xValue2,height,target.position.z-zOffset);

transform.position = pValue2;
	
	
//transform.rotation.x = target.rotation.x;
//transform.rotation.y = target.rotation.y;
	
	
//var relativePos = target.position - transform.position;
//var rotationPos = Quaternion.LookRotation(relativePos);
//transform.rotation.y = rotationPos.y;



//transform.localEulerAngles.y = yrotValue2;
	

}
function Update(){


//transform.position.z = target.position.z-zOffset;

}

public function thrown(){

thrownBool = true;
}



	

