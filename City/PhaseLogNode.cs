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

	public void Refresh(MissionPhase mp){
		//phaseName.text = mp.Type;
		//phaseLog.text = mp.Log;
		phaseName_tmp.text = mp.Type;
		phaseLog_tmp.text = mp.Log;
	}

}
