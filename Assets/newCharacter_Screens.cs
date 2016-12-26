using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class newCharacter_Screens : MonoBehaviour  {

	public Material mainCharacterMtl;
	public Texture[] newGuysTextures;

//	NamedBooleans.Add("This is the name of the first boolean", false);

	public MeshRenderer [] myRenderers ;
	public List<bool> myArray  = new List<bool>();


//	public bool[] myArray = new bool[];
	// Use this for initialization

//	SerializedProperty mybools;
	void Start () {
//		EditorGUILayout.PropertyField(list);


		for(int i = 0; i < newGuysTextures.Length; i++){
if(myArray[i]){
				Material newMaterial = myRenderers[i].material;
				newMaterial.mainTexture = newGuysTextures[i];

				for(int j = 0; j < myRenderers.Length; j++){
					myRenderers[j].material = newMaterial;

				}
			}
//			
		}
	}


}
