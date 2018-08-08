using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPhase : MissionPhase {

	public string Type = "Patrol";
	public string Log;
	public List<Line> lines;
	private int blankLogCount;
	private int _blankLogCount;


	public override IEnumerator PhaseCoroutine(MissionClass mc) {  

		blankLogCount = Random.Range(3, 5);

		for(int i = 1; i <= blankLogCount; i++){
			PrintLog(mc, mc.AppliedHero.Name + " : I just walking to find something...");
			yield return new WaitForSeconds (2f);		
		}

		lines = SetLine(mc);
		for(int i = 0; i <= (lines.Count - 1); i++ ) {
			PrintLog(mc, lines[i].who + " : " + lines[i].what);
			yield return new WaitForSeconds (1f);  
		}

		_blankLogCount = Random.Range(2, 5);

		for(int i = 1; i <= _blankLogCount; i++){
			PrintLog(mc, mc.AppliedHero.Name + " : I just walking to find something...");
			yield return new WaitForSeconds (2f);		
		}


	}

	private List<Line> SetLine(MissionClass mc) {
		List<Line> rtnlist = new List<Line>();
		Line l1 = new Line();
		l1.who = mc.AppliedHero.Name;
		l1.what = "I left some homework tonight.";
		rtnlist.Add(l1);

		return rtnlist;
	}

	private void PrintLog(MissionClass missioncls, string log){
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}

}
