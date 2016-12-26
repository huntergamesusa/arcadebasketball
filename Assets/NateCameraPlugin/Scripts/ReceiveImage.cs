using UnityEngine;
using System.Collections;
public class ReceiveImage : MonoBehaviour {

	byte[] b64_bytes;
//	Texture2D tex;
//	Sprite currentSprite;

	public delegate void imageBytesEventHandler (byte[] mybytes);
	public static event imageBytesEventHandler passBytes;

	void ReceiveImageString (string baseS) {

		b64_bytes = System.Convert.FromBase64String(baseS);
//		tex = new Texture2D(1,1);
//		tex.LoadImage(b64_bytes);
//		Rect rec = new Rect(0, 0, tex.width, tex.height);
		
//		currentSprite=Sprite.Create(tex,rec,new Vector2(0.5f,0.5f),100);
		passBytes (b64_bytes);
	}
	

}
