using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhaseLogNode : MonoBehaviour {

	public Text phaseName;
	public Text phaseLog;

	public void Refresh(MissionPhase mp){
		phaseName.text = mp.Type;
		phaseLog.text = mp.Log;
	}

}
