using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPhase : MissionPhase {

	private string type = "Patrol";
	public string log;
	public List<Line> lines;
	private int blankLogCount;
	private int _blankLogCount;

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

		yield return new WaitForSeconds (0.5f);		


		//blankLogCount = Random.Range(3, 5);
		blankLogCount = Random.Range(1, 2);

		for(int i = 1; i <= blankLogCount; i++){
			PrintLog(mc, mc.AppliedHero.Name + " : I just walking to find something...");
			//yield return new WaitForSeconds (2f);
			yield return new WaitForSeconds (0.5f);		
		
		}

		lines = SetLine(mc);
		for(int i = 0; i <= (lines.Count - 1); i++ ) {
			PrintLog(mc, lines[i].who + " : " + lines[i].what);
			yield return new WaitForSeconds (1f);  
		}

		//_blankLogCount = Random.Range(2, 5);
		_blankLogCount = Random.Range(1, 2);

		for(int i = 1; i <= _blankLogCount; i++){
			PrintLog(mc, mc.AppliedHero.Name + " : I just walking to find something...");
			//yield return new WaitForSeconds (2f);	
			yield return new WaitForSeconds (0.5f);		
		}

	}

	private List<Line> SetLine(BaseMissionClass mc) {
		List<Line> rtnlist = new List<Line>();
		Line l1 = new Line();
		l1.who = mc.AppliedHero.Name;
		l1.what = "I left some homework tonight.";
		rtnlist.Add(l1);

		return rtnlist;
	}

}
