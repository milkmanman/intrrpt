using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPhase {

	private string type = "N/A";
	private string log = "";
	
	public virtual string Type {
        get{return type;}
        set{type = value;}
    }

	public virtual string Log {
        get{return log;}
        set{log = value;}
    }

	public virtual IEnumerator PhaseCoroutine(BaseMissionClass mc) {  
		yield return new WaitForSeconds (0.5f);  
		mc.MissionLog += "error in the phase!";

	}

	public void PrintLog(BaseMissionClass missioncls, string logline){
		missioncls.MissionLog = missioncls.MissionLog + logline + "\n";
		Log = Log + logline + "\n";
		Debug.Log(logline);
		
		if(missioncls.PushMissionLogAction != null){
			missioncls.PushMissionLogAction();
		};

	}
}
