using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPhase : MissionPhase  {

	private string type = "Back";
	public bool doesSendMedic = false;
	public string Log;

	public override string Type {
		get{ return type; }
		set{ type = value; }
	}

	public override IEnumerator PhaseCoroutine(BaseMissionClass mc) {  

		if(mc.PhaseMoveAction != null){
			mc.PhaseMoveAction();
		};

		if(doesSendMedic == false){

			PrintLog(mc, "Base : Come back hero.");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Hero : Okay. just i miss you.");
			yield return new WaitForSeconds (1.5f);  

			PrintLog(mc, "Hero : i'm kidding. so I back to the base.");
			yield return new WaitForSeconds (4.0f);  

			PrintLog(mc, "Hero : Home sweet home!");
			yield return new WaitForSeconds (1.5f);  

		} else {

			PrintLog(mc, "Medic : I rescue you hero.");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Medic : Send your location. I'll go ASAP.");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Hero : Thanks. Now, I send GPS Data.");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Medic : OK. Patient.");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Medic : Arrived. Back to Base.");
			yield return new WaitForSeconds (3.0f);

			PrintLog(mc, "Hero Rescued. Mission Completed.");
			yield return new WaitForSeconds (1.5f);

		}

	}

	/*private void PrintLog(BaseMissionClass missioncls, string log){
		Debug.Log("_print_log_" +  log);
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}*/

}
