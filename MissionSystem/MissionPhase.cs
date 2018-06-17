using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPhase {

	public virtual IEnumerator PhaseCoroutine(NewMissionClass mc) {  
		yield return new WaitForSeconds (0.5f);  
		mc.MissionLog += "error in the phase!";

	}
}
