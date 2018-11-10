using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PhaseLogNode : MonoBehaviour {

	//public Text phaseName;
	//public Text phaseLog;
	public TextMeshProUGUI phaseName_tmp;
	public TextMeshProUGUI phaseLog_tmp;

	public Color32 PatrolColor;
	public Color32 MoveColor;
	public Color32 TalkColor;

	public void Refresh(MissionPhase mp){
		//phaseName.text = mp.Type;
		//phaseLog.text = mp.Log;
		phaseName_tmp.text = mp.Type;
		if(mp.Type == "Patrol"){
			phaseName_tmp.color = PatrolColor;
		} else if (mp.Type == "Move"){
			phaseName_tmp.color = MoveColor;
		} else if (mp.Type == "Talk"){
			phaseName_tmp.color = TalkColor;
		}
		
		phaseLog_tmp.text = mp.Log;
	}

}
