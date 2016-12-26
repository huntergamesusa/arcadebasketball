using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class FDViewController {

	[DllImport ("__Internal")]
	
	
	private static extern void _NateTest();
	
	
	public static void PhotoCamPicker()
	{
		_NateTest();
	}
}
