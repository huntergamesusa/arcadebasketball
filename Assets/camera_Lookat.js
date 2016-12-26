#pragma strict
var target : Transform;
var speed :float;
function findTarget () {
target = GameObject.Find("Body_jnt").transform;
}



function LateUpdate () {
if(gameObject.name == "CameraAction" || gameObject.name == "CameraAction_Screenshot"){
transform.LookAt(target);
}
else{
    var newRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
 newRotation.z = 0.0;
    newRotation.y = 0.0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);

}
}