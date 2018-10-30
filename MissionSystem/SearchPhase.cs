using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPhase : MissionPhase {

	private string type = "Search";
	public string Log;
	public string Object;
	public string BetweenLine;
	public int Difficulty;

	public override string Type {
		get{ return type; }
		set{ type = value; }
	}

	public override IEnumerator PhaseCoroutine(BaseMissionClass mc) {  

		if(mc.PhaseMoveAction != null){
			mc.PhaseMoveAction();
		};


		for(int i = 0; i <= 3; i++){
            PrintLog(mc, mc.AppliedHero.Name + " : " + Object + "を捜索中");
            yield return new WaitForSeconds (3.0f);  
        }

		int count = 1;

        while(count <= 5){
			if(count == 5){
				PrintLog(mc, "Search Phase Failed");
				mc.FailtureFlag = true;
				break;
			}else if(Random.Range(12, 20) <= 10){
				PrintLog(mc, mc.AppliedHero.Name + " : " + Object + "を発見！");
				break;
			} else {
				PrintLog(mc, mc.AppliedHero.Name + " : " + Object + "を捜索中");
			}

			count++;
			yield return new WaitForSeconds (2f); 
		}

	}
}
