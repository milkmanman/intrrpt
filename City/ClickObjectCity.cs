using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickObjectCity : MonoBehaviour {

	GameObject GUI;

/*	[SerializeField, Range(-200f, 0.1f)]
	private float transForward = -100f;

	[SerializeField, Range(0.1f, 10f)]
	private float transUp = 0.3f;

	[SerializeField, Range(0.1f, 10f)]
	private float transRight = 0.3f;
*/


	void Start () {
		GUI = GameObject.Find("GUI");

	}

	void Update () {

		if (Input.GetMouseButtonDown(0)) {
  			if (EventSystem.current.IsPointerOverGameObject()) return;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();

			if (Physics.Raycast(ray, out hit)){
				GameObject obj = hit.collider.gameObject;
				Debug.Log("collider : " + obj);

				if (obj.tag == "Arrow") {
					ShortMissionUI.HoldArrow = obj;
					obj.GetComponent<Arrow>().GetClicked();
				} else if (obj.tag == "MissionBar") {

				} else {
					GUI.GetComponent<UIActivatorCity>().Activator(0);
				}
			}
		}
	}

/*	public void FocusCamera (GameObject objTarget){

		Vector3 pos = objTarget.transform.position;
		pos += transform.forward * transForward;
		pos += transform.right * transRight;
		pos += transform.up * transUp;

		var time = 0.5f;

		iTween.MoveTo (this.gameObject, iTween.Hash(
		"position", pos,
			"time", time,
			"oncomplete", "AnimationEnd",
			"oncompletetarget", this.gameObject,
			"easeType", "easeInOutQuad"
		));
	}*/

}
