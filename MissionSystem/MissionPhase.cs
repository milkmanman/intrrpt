using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPhase {

	public string Type = "N/A";

	public virtual IEnumerator PhaseCoroutine(MissionClass mc) {  
		yield return new WaitForSeconds (0.5f);  
		mc.MissionLog += "error in the phase!";

	}
}
