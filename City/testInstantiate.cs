using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInstantiate : MonoBehaviour {

	public GameObject prefabMIB;

	// Use this for initialization
	void Start () {
		string scene = Application.loadedLevelName;
		Debug.Log(scene);
		if(scene == "City"){
			InstantiateInfobar();
		}
	}

	void InstantiateInfobar(){
		if(MissionManager.Instance.slot1 != null){
			GameObject heroObj = Instantiate(prefabMIB, new Vector3(-10, 40, 0), Quaternion.identity);
			heroObj.GetComponent<MissionInfoBar>().mc = MissionManager.Instance.slot1;
			heroObj.GetComponent<MissionInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("MissionUI").gameObject;
		}
		if (MissionManager.Instance.slot2.ActiveFlg != false) {
			Debug.Log("slot 2 : " + MissionManager.Instance.slot2);
			GameObject heroObj2 = Instantiate(prefabMIB, new Vector3(40, 40, 0), Quaternion.identity);
			heroObj2.GetComponent<MissionInfoBar>().mc = MissionManager.Instance.slot2;
			heroObj2.GetComponent<MissionInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("MissionUI").gameObject;
		}

	}

}
