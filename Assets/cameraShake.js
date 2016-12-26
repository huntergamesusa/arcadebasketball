 var originPosition:Vector3;
 var originRotation:Quaternion;
 
public  var shake_decay: float;
 private var shake_intensity: float = 0;
public var shake_input_intensity:float;

 function Update(){
 
//if(Input.GetKeyUp(KeyCode.B)){
//Shake();
//}

     if(shake_intensity > 0){

//gameObject.SendMessage("changeBlur",.8f);


         transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
         transform.rotation =  Quaternion(
                         originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2,
                         originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2,
                         originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2,
                         originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2);
         shake_intensity -= shake_decay;
     }
     else{
//     gameObject.SendMessage("changeBlur",0f);

     }
 }
 
 public function Shake(){
     originPosition = transform.position;
     originRotation = transform.rotation;
     shake_intensity = shake_input_intensity;
//     shake_decay = 0.002;
 }