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

			PrintLog(mc, "Base : 終了だ。戻ってきてくれ。");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, mc.AppliedHero.Name + " : 了解。寂しかったよ");
			yield return new WaitForSeconds (1.5f);  

			PrintLog(mc, mc.AppliedHero.Name + " : 冗談だよ。今から向かう。");
			yield return new WaitForSeconds (4.0f);  

			PrintLog(mc, mc.AppliedHero.Name +　"ただいま！");
			yield return new WaitForSeconds (1.5f);  

		} else {

			PrintLog(mc, "Medic : 大丈夫か！今から助けに行く");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Medic : GPS情報を送信してくれ。急いで向かう");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, mc.AppliedHero.Name + " : ありがとう。。今、位置情報を送ったよ");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Medic : 了解。耐えていてくれ。");
			yield return new WaitForSeconds (1.5f);

			PrintLog(mc, "Medic : 到着した。ベースに戻るぞ。");
			yield return new WaitForSeconds (3.0f);

			PrintLog(mc, "ヒーロー救出完了。フリーローム終了");
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
