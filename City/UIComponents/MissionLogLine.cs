﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionLogLine : MonoBehaviour {

	public FreeRoamClass frc;
	public GameObject NewLogField;
	public RectTransform NewLogNode;

	public void RefreshNewMissionLog(){
		foreach ( Transform n in NewLogField.transform ) {
			GameObject.Destroy(n.gameObject);
		}
		for(int i = 0; (frc.PhaseListHistory.Count() - 1) >= i; i++){
			var item = GameObject.Instantiate(NewLogNode) as RectTransform;
			item.SetParent(NewLogField.transform, false);
			item.GetComponent<PhaseLogNode>().Refresh(frc.PhaseListHistory[i]);
		}
	}

	public void RefleshLog(){
		Transform lastChild = NewLogField.transform.GetChild(NewLogField.transform.childCount - 1);
		Text logtext = lastChild.Find("PhaseLogs/Text").GetComponent<Text>();
		logtext.text = frc.PhaseList.First().Log;
	}

}
