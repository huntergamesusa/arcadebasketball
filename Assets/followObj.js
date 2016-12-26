#pragma strict
var target : GameObject;	
var smooth : float;
var startPos : Vector3;
var adjustments : Vector3;	
function Awake () {
startPos = transform.position;
}

function LateUpdate () {
transform.position.z= target.transform.position.z + adjustments.z;
transform.position.x = Mathf.Lerp(transform.position.x, target.transform.position.x, Time.deltaTime * smooth);
}

