using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickObject : MonoBehaviour {

	GameObject GUI;

	[SerializeField, Range(-10f, 0.1f)]
	private float transForward = -130f;

	[SerializeField, Range(0.1f, 10f)]
	private float transUp = 0.3f;

	[SerializeField, Range(-10f, 10f)]
	private float transRight = 0.3f;



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
				AudioManager.Instance.PlaySE(1);

				if (obj.tag == "Character/Hero") {
					FocusCamera(obj);
					GUI.GetComponent<UIActivator>().Activator(1);
					GameObject.Find("HeroUI").GetComponent<HeroUI>().Reflesh(obj.GetComponent<HeroStatus>().heroClass);

				} else if (obj.tag == "Character/Mission") {
					FocusCamera(obj);
					GUI.GetComponent<UIActivator>().Activator(2);

				} else if (obj.tag == "Character/Police") {
					FocusCamera(obj);
					GUI.GetComponent<UIActivator>().Activator(3);

				} else if (obj.tag == "Character/Develop") {
					FocusCamera(obj);
					GUI.GetComponent<UIActivator>().Activator(4);

				} else if (obj.tag == "Character/WholeUI") {
					FocusCamera(obj);
					GUI.GetComponent<UIActivator>().Activator(5);

					} else if (obj.tag == "Character/Medic") {
						FocusCamera(obj);
						GUI.GetComponent<UIActivator>().Activator(6);

				} else if (obj.tag == "Arrow") {
					ShortMissionUI.HoldArrow = obj;
					obj.GetComponent<Arrow>().GetClicked();
				} else {
					GUI.GetComponent<UIActivator>().Activator(0);
				}
			}
		}
	}

	public void FocusCamera (GameObject objTarget){

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
	}

}
