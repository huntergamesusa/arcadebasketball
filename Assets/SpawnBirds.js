#pragma strict
var gull:GameObject;
var birdParent:GameObject;
private var spawnTrigger:boolean=false;
function Start () {

}

function Update () {
if(birdParent.transform.childCount >4)
return;
randomSpawnBird();
//print("birdparent: "+ birdParent.transform.childCount);
}

function randomSpawnBird(){
if(!spawnTrigger){
spawnTrigger=true;



var randomInt = Random.Range(0,transform.childCount);

var gullSpawn = Instantiate(gull,transform.GetChild(randomInt).transform.position,Quaternion.Euler(0,-90,0));
gullSpawn.transform.parent = birdParent.transform;
yield WaitForSeconds(Random.Range(4,8));
spawnTrigger=false;

}

}