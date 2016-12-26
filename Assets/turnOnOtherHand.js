#pragma strict
private var itemLoc:int;
function Start () {
var LeftItemParent = GameObject.Find("Left_Hand_Acc");
var RightItemParent = GameObject.Find("Right_Hand_Acc");

 for(var i : int = 0; i < RightItemParent.transform.childCount; i++)
    {
    if( RightItemParent.transform.GetChild(i).gameObject.name == gameObject.name){
    itemLoc = i;
    }
    
    }
    LeftItemParent.transform.GetChild(itemLoc).gameObject.SetActive(true);
    
}

function Update () {

}