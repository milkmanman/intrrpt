using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : SingletonMonoBehaviourFast<FacilityManager> {

	private int dronePhase;
	private int jumpingDronePhase;
	private int monitorPhase;
	private int rescueCar;
	public Vector3 deskpos;

	public List<Facility> FacilityObject;


	public int DronePhase{
		get{return dronePhase;}
		set{dronePhase = value;}
	}

	public int JumpingDronePhase{
		get{return jumpingDronePhase;}
		set{jumpingDronePhase = value;}
	}

	public string[,] facilityarray() {
		string[,] arr = new string[2, 2];
		arr[0, 0] = "drone";
		arr[0, 1] = DronePhase.ToString();
		arr[1, 0] = "jumpingdrone";
		arr[1, 1] = JumpingDronePhase.ToString();

		return arr;
	}


	void Start(){
		InstantiateFacility();
	}

	private void InstantiateFacility(){
		var go = new GameObject();
		go.name = "Drone1";

		Facility test = FacilityObject[0];
		int facilityLength = test.FacilityObject.Count;

		for(int i = 0; i <= facilityLength - 1; i++){
			GameObject test_prefab = (GameObject)Instantiate(
				test.FacilityObject[i].obj,
				test.FacilityObject[i].pos,
				Quaternion.Euler(test.FacilityObject[i].eul)
			);
			test_prefab.name = "Drone_" + i.ToString();
			test_prefab.transform.SetParent(go.transform);
		}
	}
}
