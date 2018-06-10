using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FacilityNode : MonoBehaviour {

	public FacilityClass facilitycls;
	public Text Name;
	public Text Type;

	//void OnEnable () {
	//	RefleshDisplay();
	//}

	public void RefleshDisplay () {
		Name.text = facilitycls.Name;
		Type.text = facilitycls.Type;
	}

}
