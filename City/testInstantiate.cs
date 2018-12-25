using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInstantiate : MonoBehaviour {

	public UIActivatorCity UIActivator;
	public GameObject prefabMIB;
	public GameObject prefabFRIB;

	public List<GameObject> FRIBs;

	// Use this for initialization
	void Start () {
		string scene = Application.loadedLevelName;
		Debug.Log(scene);
		if(scene == "City"){
		FRIBs = new List<GameObject>();
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
				heroObj.transform.parent = GameObject.Find("InfoBars").transform;
				heroObj.name = MissionManager.Instance.FreeRoamList[i - 1].AppliedHero.Name + ":freeroam";
				heroObj.GetComponent<FreeroamInfoBar>().frc = MissionManager.Instance.FreeRoamList[i - 1];
				heroObj.GetComponent<FreeroamInfoBar>().MissionUI = GameObject.Find("GUI").transform.Find("FreeroamUI").gameObject;
				heroObj.GetComponent<FreeroamInfoBar>().BarText.text = MissionManager.Instance.FreeRoamList[i - 1].AppliedHero.Name;
				FRIBs.Add(heroObj);
			}
		}

	}

	public void DeleteInfobar(FreeRoamClass frc){
		for(int i = 0; i < FRIBs.Count; i++) {
			FreeRoamClass BarsFrm = FRIBs[i].GetComponent<FreeroamInfoBar>().frc;
			if(BarsFrm == frc){
				Destroy(FRIBs[i]);
				Debug.LogWarning("Destroy!!!!");
				FRIBs.Remove(FRIBs[i]);
			}
		}
	}

}
