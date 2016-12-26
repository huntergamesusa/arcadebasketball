using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class adjustTextBtnC : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler{
	private bool isDown;
	private bool isEnter;
	public float downAmount = 5.5f;
	// Use this for initialization
	void Start () {
		isEnter =false;
		isDown =false;
		print ("started");
	}

	void OnEnable()
	{
		isEnter =false;
		isDown =false;
//		print ("started");
	}
	public void OnPointerDown(PointerEventData data)
	{
		isDown = true;

		if(isEnter){
		for(int i = 0; i < transform.childCount; i++)
		{
				transform.GetChild (i).position = new Vector3(transform.GetChild (i).position.x,transform.GetChild (i).position.y - downAmount, transform.GetChild (i).position.z);
		}
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		isEnter =true;
		if(isDown){
		for(int i = 0; i < transform.childCount; i++)
		{
				transform.GetChild (i).position = new Vector3(transform.GetChild (i).position.x,transform.GetChild (i).position.y - downAmount, transform.GetChild (i).position.z);
		}
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		isDown =false;
//		isEnter = false;
		if(isEnter){
		for(int i = 0; i < transform.childCount; i++)
		{
				transform.GetChild (i).position = new Vector3(transform.GetChild (i).position.x,transform.GetChild (i).position.y + downAmount, transform.GetChild (i).position.z);
		}
		}
	}

	public void OnPointerExit(PointerEventData data)
	{
		isEnter =false;
		if(isDown){
		for(int i = 0; i < transform.childCount; i++)
		{
				transform.GetChild (i).position = new Vector3(transform.GetChild (i).position.x,transform.GetChild (i).position.y + downAmount, transform.GetChild (i).position.z);
		}
		}
	}
		
		// Update is called once per frame
	void Update () {
	
	}
}
