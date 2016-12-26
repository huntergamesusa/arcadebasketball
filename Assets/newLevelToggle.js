#pragma strict

function Start () {
if(PlayerPrefs.GetInt("missionNew")==0){
gameObject.SetActive(true);
}
else{
gameObject.SetActive(false);

}
}



public function disableNew(){

PlayerPrefs.SetInt("missionNew",1);
gameObject.SetActive(false);

}