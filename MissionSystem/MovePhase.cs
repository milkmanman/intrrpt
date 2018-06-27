using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : MissionPhase {

	public string Type = "Move";
	public string Destination;
	//public int Distance; //10 - 100
	//public int Obstacle; // 0 - 100
	public string BridgeMsg;
	//public string MustMsg;
	public string Log;

	public override IEnumerator PhaseCoroutine(MissionClass mc) {  
		PrintLog(mc, mc.AppliedHero.Name + " : Moving to " + Destination);
		int count = 0;

		while (count <= 100){

			if (count % 25 == 0){
				PrintLog(mc, "Moving to " + Destination + ", at least " + count.ToString() + "%");
			}

			yield return new WaitForSeconds (0.1f);  
			count++;
		}
		
		PrintLog(mc, "Arrieved at " + Destination + ", " + BridgeMsg);
	}


	private void PrintLog(MissionClass missioncls, string log){
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}


}
