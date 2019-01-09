using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationUI : MonoBehaviour {

	public UIActivator UiActive;

	void Start () {

			CanvasOnEnabled();
			UiActive.OnEnabledDevelopUI += CanvasOnEnabled;

	}

	private void CanvasOnEnabled(){
		Debug.Log("test");
	}
}
