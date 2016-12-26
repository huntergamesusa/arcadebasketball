using UnityEngine;
using UnityEngine.EventSystems;

public class LevelClick : MonoBehaviour,  IPointerClickHandler, IPointerDownHandler, IPointerUpHandler{

	// Use this for initialization

	// Update is called once per frame
	public void OnPointerDown( PointerEventData eventData )
	{
	}

	public void OnPointerUp( PointerEventData eventData )
	{
	}

	public void OnPointerClick( PointerEventData eventData )
	{
	}
	void Update(){
		if (Input.GetKeyUp (KeyCode.A)) {
 ExecuteEvents.Execute<IPointerClickHandler>(ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject), new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
		}

	}
}
