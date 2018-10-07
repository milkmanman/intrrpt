using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityInstantiate : MonoBehaviour {

	public GameObject FacilityField;

	public List<GameObject> WalkDrone;
	public List<GameObject> Drone;

	void Start(){
		//StartCoroutine ("Sample"); 
	}

	 private IEnumerator Sample() {  
        InstantiateFacilities("WalkDrone", 0);

		yield return new WaitForSeconds (4.0f);  

		InstantiateFacilities("WalkDrone", 1);

    }  


	public void InstantiateFacilities(string type, int phase){
		foreach ( Transform n in FacilityField.transform ) {
			if(n.gameObject.name == type){
				GameObject.Destroy(n.gameObject);
			}
		}

		GameObject facilityObj = null;
		
		if(type == "jumpingdrone"){
			facilityObj = Instantiate(WalkDrone[phase], new Vector3(0, 0, 0), Quaternion.identity);
		} else if(type == "drone"){
			facilityObj = Instantiate(WalkDrone[phase], new Vector3(0, 0, 0), Quaternion.identity);
		}

		facilityObj.name = type;
		facilityObj.transform.parent = FacilityField.transform;
	}
}
