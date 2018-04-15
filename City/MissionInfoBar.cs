using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionInfoBar : MonoBehaviour {

	public MissionClass mc;
	public GameObject MissionUI;

	void Start(){
		this.transform.Find("Text").gameObject.GetComponent<Button>().onClick.AddListener(delegate{MissionUI.GetComponent<CityMissionUI>().DisplayMissionInfo(mc);});
		MissionUI.transform.Find("MissionResult/CloseButton").gameObject.GetComponent<Button>().onClick.AddListener(delegate{DestroyThis();});

	}

	public void DestroyThis(){
		Destroy(gameObject);
	}

}
