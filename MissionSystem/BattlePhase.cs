using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : MissionPhase {

	private string type = "Battle";
	public string BridgeMsg;
	public string Log;
	public List<VillainStatusClass> villainList;
	public int RemainVillains;

	public override string Type {
		get{ return type; }
		set{ type = value; }
	}

	public override IEnumerator PhaseCoroutine (BaseMissionClass missioncls) {

		if(missioncls.PhaseMoveAction != null){
			missioncls.PhaseMoveAction();
		};

		Debug.Log("villian list : " + villainList.Count);
		//missioncls.AppliedHero.Health = 300;
		//missioncls.AppliedHero.Atk = 20f;
		//missioncls.AppliedHero.Def = 0f;
		RemainVillains = villainList.Count;

		for(int i = 0; i <= villainList.Count - 1; i++){
			yield return new WaitForSeconds(1);
			if(missioncls.AppliedHero.Health > 0){
				while(villainList[i].Health > 0 && missioncls.AppliedHero.Health > 0){
					int a = UnityEngine.Random.Range(0,2);
					string log = Attack(a, missioncls.AppliedHero, villainList[i]);
					PrintLog(missioncls, log);
					yield return new WaitForSeconds(1);
				}
				if(missioncls.AppliedHero.Health > 0){
					RemainVillains = villainList.Count - (i + 1);
					Debug.Log("remainvillain : " + RemainVillains);
					if(villainList.Count - (i + 1) != 0){
						PrintLog(missioncls, "!! LEFT " + (villainList.Count - (i + 1)).ToString() + "VILLANS !!");
					} else {
						RemainVillains = 0;
						PrintLog(missioncls, "全ての敵を倒した");
						missioncls.Success = true;

					}
				} else {
					PrintLog(missioncls, "ヒーローダウン！");
					missioncls.Success = false;
					//missioncls.ActiveFlg = false; //instant for free-roam

					yield break;
				}
			}
		}

	}

	private string Attack(int a, HeroStatusClass hero, VillainStatusClass villain){
		string ret = "";
		if(a == 0){
			int damageByVillain = (int)(villain.Atk * ((100 - hero.Def)/ 100) );
			hero.Health = hero.Health - damageByVillain;
			if(hero.Health > 0){
				ret = "敵の攻撃! ヒーロー残りHP : " + hero.Health.ToString();
			} else {
				ret = "敵の攻撃! Hero down!";
			}
		} else {
			int damageByVillain = (int)(hero.Atk * ((100 - villain.Def)/ 100) );
			villain.Health = villain.Health - damageByVillain;
			if(villain.Health > 0){
				ret = "ヒーローの攻撃! 敵の残りHP : " + villain.Health.ToString();
			} else {
				ret = "ヒーローの攻撃! 敵ダウン!";
			}
		}
		return ret;
	}




	/*private void PrintLog(BaseMissionClass missioncls, string log){
		if(log.Contains("Villain")){
			log = log.Replace("Villain", "<color=#ff0000>Villain</color>");
		} else if (log.Contains("Hero")) {
			log = log.Replace("Hero", "<color=#0000ff>Hero</color>");
		}
		missioncls.MissionLog = missioncls.MissionLog + log + "\n";
		Log = Log + log + "\n";
		Debug.Log(log);
	}*/
}
