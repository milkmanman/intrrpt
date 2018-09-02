using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPhase : MissionPhase {

	public string Type = "Talk";
	public List<Line> lines;
	public string firstDesc; //end
	public string endDesc; //end
	public string Log;

	public override IEnumerator PhaseCoroutine(BaseMissionClass mc) {  

		if(firstDesc != null){
			PrintLog(mc, firstDesc);
			yield return new WaitForSeconds (2f);  

		}

		for(int i = 0; i <= (lines.Count - 1); i++ ) {
			PrintLog(mc, lines[i].who + " : " + lines[i].what);
			yield return new WaitForSeconds (1f);  

		}

		if(endDesc != null){
			PrintLog(mc, endDesc);
			yield return new WaitForSeconds (2f);  

		}


	}

	private void PrintLog(BaseMissionClass missioncls, string log){
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}

}
