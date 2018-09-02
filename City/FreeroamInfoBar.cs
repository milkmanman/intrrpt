using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeroamInfoBar : MonoBehaviour {

	public FreeRoamClass frc;
	public GameObject MissionUI;

	void Start(){
		this.transform.Find("Text").gameObject.GetComponent<Button>().onClick.AddListener(delegate{MissionUI.GetComponent<CityFreeroamUI>().DisplayMissionInfo(frc);});
		//MissionUI.transform.Find("MissionResult/CloseButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate{DestroyThis();});

	}

	public void DestroyThis(){
		Destroy(gameObject);
	}
}
