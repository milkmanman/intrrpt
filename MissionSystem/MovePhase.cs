using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : MissionPhase {

	
	private string type = "Move";
	public string Destination;
	//public int Distance; //10 - 100
	//public int Obstacle; // 0 - 100
	public string BridgeMsg;
	//public string MustMsg;
	public string log;

	public override string Type {
		get{ return type; }
		set{ type = value; }
	}

	public override string Log {
		get{ return log; }
		set{ log = value; }
	}

	public override IEnumerator PhaseCoroutine(BaseMissionClass mc) {  

		if(mc.PhaseMoveAction != null){
			mc.PhaseMoveAction();
		};

		PrintLog(mc, mc.AppliedHero.Name + " : " + Destination + "へ移動を開始");
		int count = 0;

		while (count <= 100){

			if (count % 25 == 0){
				PrintLog(mc, Destination + "へ移動中, 到達率" + count.ToString() + "%");
			}

			yield return new WaitForSeconds (0.1f);  
			count++;
		}

		yield return new WaitForSeconds (1.0f);  

		PrintLog(mc, Destination + "に到着。" + ", " + BridgeMsg);

	}

}
