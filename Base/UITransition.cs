using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour {

	public GameObject canvas1;
	public float y_axis_canvas1;
	public float time1;

	public string easeType;


	public void UITransitioner (bool down) {

		float direction = 1;
		if (down == false) direction = - direction;

		Vector3 pos1 = canvas1.transform.position;
//		Debug.Log(pos1);

		pos1.y += y_axis_canvas1 * direction;

		iTween.MoveTo (canvas1.gameObject, iTween.Hash(
		"position", pos1,
//			"y", y_axis_canvas1,
			"time", time1,
			"oncomplete", "AnimationEnd",
			"oncompletetarget", this.gameObject,
			"easeType", easeType
		));
	}

	public void UITransitionerTwice (bool down) {

		float direction = 2;
		if (down == false) direction = - direction;

		Vector3 pos1 = canvas1.transform.position;
//		Debug.Log(pos1);

		pos1.y += y_axis_canvas1 * direction;

		iTween.MoveTo (canvas1.gameObject, iTween.Hash(
		"position", pos1,
//			"y", y_axis_canvas1,
			"time", time1,
			"oncomplete", "AnimationEnd",
			"oncompletetarget", this.gameObject,
			"easeType", easeType
		));
	}

	public void InitUIPosition () {
		Vector3 InitPosition = new Vector3(0, 0, 0);
		Vector3 pos1 = canvas1.transform.position;
		pos1 = InitPosition;
		Debug.Log(pos1);

	}
}
