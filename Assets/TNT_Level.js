#pragma strict
var tnt:GameObject;
private var totalBarrels:int;
var pointsCollect :GameObject;
private var scaleTNT:float = 1;
var islandParent:Transform;

public function scaleExp(thisScale:String){
scaleTNT = parseFloat(thisScale);
}
public function tntSizeStart(startSize:int){
totalBarrels = startSize;

}

function resetTNT(){
if(PlayerPrefs.GetInt("selectedLevel")==1){
System.GC.Collect();

totalBarrels = Mathf.Clamp(totalBarrels,0,12);
totalBarrels++;
 var allTNT : GameObject[]= GameObject.FindGameObjectsWithTag("tnt_Destroy");
for ( var go in allTNT){
Destroy(go);
}

if(basketballThrow_TouchY.TNTMode){

    for(var i : int = 0; i < totalBarrels; i++){
	    var ran = Random.insideUnitSphere * 26.2*scaleTNT;
	    var sphereSpawn = Vector3(transform.position.x + ran.x,islandParent.transform.position.y,transform.position.z + ran.z);


var tnt_spawn = Instantiate(tnt,sphereSpawn,Quaternion.identity);
tnt_spawn.transform.localScale = Vector3(scaleTNT*13.73725,scaleTNT*13.73725,scaleTNT*13.73725);
tnt_spawn.name = "tnt";

}
}
}
}

public function DestroyTNT(){


 var allTNT : GameObject[]= GameObject.FindGameObjectsWithTag("tnt_Destroy");
for ( var go in allTNT){
Destroy(go);
}
}



//var hitColliders : Collider[] = Physics.OverlapSphere(sphereSpawn, 1);
////for (var j = 0; j < hitColliders.Length; j++) {
//for (var hit in hitColliders){
////if(!hit)
////continue;
//
//if(hit.GetComponent.<Collider>()){
//
//if(hit.GetComponent.<Collider>().gameObject.tag == "tnt_Destroy"){
//Destroy(hit.transform.parent.gameObject);
//print("destroyed");
////    var ran2 = Random.insideUnitSphere * 27.28;
////	    var sphereSpawn2 = Vector3(transform.position.x + ran2.x,transform.position.y+2.1,transform.position.z + ran2.z);
////var tnt_spawn2 = Instantiate(tnt,sphereSpawn2,Quaternion.identity);
//		}
//		}
//}
//
//}


