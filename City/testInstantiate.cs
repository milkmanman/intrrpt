using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInstantiate : MonoBehaviour {

	public GameObject prefabMIB;
	public GameObject prefabFRIB;

	// Use this for initialization
	void Start () {
		string scene = Application.loadedLevelName;
		Debug.Log(scene);
		if(scene == "City"){
		InstantiateInfobar();
		}
	}

	void InstantiateInfobar(){
		for(int i = 1; i <= MissionManager.Instance.MissionList.Count; i++){
			if(MissionManager.Instance.MissionList[i-1] != null){
				GameObject heroObj = Instantiate(prefabMIB, new Vector3(i * (-100), 40, 0), Quaternion.identity);
				heroObj.GetComponent<MissionInfoBar>().mc = MissionManager.Instance.MissionList[0];
				heroObj.GetComponent<MissionInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("MissionUI").gameObject;
			}
		}

		for(int i = 1; i <= MissionManager.Instance.FreeRoamList.Count; i++){
			if(MissionManager.Instance.FreeRoamList[i-1] != null){
				GameObject heroObj = Instantiate(prefabFRIB, new Vector3(i * (-100), 80, 0), Quaternion.identity);
				heroObj.GetComponent<FreeroamInfoBar>().frc = MissionManager.Instance.FreeRoamList[0];
				heroObj.GetComponent<FreeroamInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("FreeroamUI").gameObject;
			}
		}

		/*if(MissionManager.Instance.MissionList[0] != null){
		//if(MissionManager.Instance.slot1 != null){

			GameObject heroObj = Instantiate(prefabMIB, new Vector3(-10, 40, 0), Quaternion.identity);
			heroObj.GetComponent<MissionInfoBar>().mc = MissionManager.Instance.MissionList[0];
			heroObj.GetComponent<MissionInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("MissionUI").gameObject;
		}*/
		/*if (MissionManager.Instance.slot2.ActiveFlg != false) {
			Debug.Log("slot 2 : " + MissionManager.Instance.slot2);
			GameObject heroObj2 = Instantiate(prefabMIB, new Vector3(40, 40, 0), Quaternion.identity);
			heroObj2.GetComponent<MissionInfoBar>().mc = MissionManager.Instance.slot2;
			heroObj2.GetComponent<MissionInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("MissionUI").gameObject;
		}*/

	}

}
