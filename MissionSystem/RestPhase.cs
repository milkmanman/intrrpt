using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPhase : MissionPhase {

	public string Type = "Rest";
	public List<Line> beforeLines;
	public List<Line> afterLines;
	public string restType;
	public string food;
	public int recoveryValue;
	public string Log;

	/*void Awake(){
		restType = "eat";
		food = "a burger and a shake";
		recoveryValue = 10;
	}*/

	public override IEnumerator PhaseCoroutine(BaseMissionClass mc) {  

		//restType = "eat";
		//food = "a burger and a shake";
		recoveryValue = 10;


	    if(restType == "eat"){
			if(beforeLines != null){
				for(int i = 0; i <= (beforeLines.Count - 1); i++ ) {
					PrintLog(mc, beforeLines[i].who + " : " + beforeLines[i].what);
					yield return new WaitForSeconds (2f);  
				}
			}

			/*PrintLog(mc, mc.AppliedHero.Name + " : oh.. Mcdonnald. i buy a burger and shake" );
			yield return new WaitForSeconds (2f);  
			PrintLog(mc, mc.AppliedHero.Name + " : yeah, i bought. find a somewhere to eat" );
			yield return new WaitForSeconds (5f);
			PrintLog(mc, mc.AppliedHero.Name + " : arrive a ordinary roof. burger is still warm." );
			yield return new WaitForSeconds (2f);*/
			PrintLog(mc, mc.AppliedHero.Name + " : " + food + " : health " + mc.AppliedHero.Health + " -> " + (mc.AppliedHero.Health + recoveryValue) );
			RecoverHero(mc);

			if(afterLines != null){
				for(int i = 0; i <= (afterLines.Count - 1); i++ ) {
					PrintLog(mc, afterLines[i].who + " : " + afterLines[i].what);
					yield return new WaitForSeconds (2f);  
				}
			}

			
		}

	}

	private void PrintLog(BaseMissionClass missioncls, string log){
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}

	private void RecoverHero(BaseMissionClass mc){
		mc.AppliedHero.Health += recoveryValue;
	}

}
