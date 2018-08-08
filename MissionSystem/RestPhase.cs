using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPhase : MissionPhase {

	public string Type = "Rest";
	public List<Line> lines;
	public string restType;
	public string food;
	public int recoveryValue;
	public string Log;

	/*void Awake(){
		restType = "eat";
		food = "a burger and a shake";
		recoveryValue = 10;
	}*/

	public override IEnumerator PhaseCoroutine(MissionClass mc) {  

		restType = "eat";
		food = "a burger and a shake";
		recoveryValue = 10;


	    if(restType == "eat"){
			PrintLog(mc, mc.AppliedHero.Name + " : oh.. Mcdonnald. i buy a burger and shake" );
			yield return new WaitForSeconds (2f);  
			PrintLog(mc, mc.AppliedHero.Name + " : yeah, i bought. find a somewhere to eat" );
			yield return new WaitForSeconds (5f);
			PrintLog(mc, mc.AppliedHero.Name + " : arrive a ordinary roof. burger is still warm." );
			yield return new WaitForSeconds (2f);
			PrintLog(mc, mc.AppliedHero.Name + " : burger and shake : health " + mc.AppliedHero.Health + " -> " + (mc.AppliedHero.Health + recoveryValue) );
			RecoverHero(mc);
		}

	}

	private void PrintLog(MissionClass missioncls, string log){
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}

	private void RecoverHero(MissionClass mc){
		mc.AppliedHero.Health += recoveryValue;
	}

}
