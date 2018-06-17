using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMissionSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//NewMissionClass test = new NewMissionClass();
		//StartMission(test);



		//////////////////////////////////
		// set classdata
		//////////////////////////////////
		MovePhase test1 = new MovePhase();
		test1.Destination = "Central Park";
		test1.BridgeMsg = "So find enemys.";


		TalkPhase test2 = new TalkPhase();
		List<Line> lines = new List<Line>();
		
		Line ln1 = new Line();
		ln1.who = "Hero";
		ln1.what = "youre over.";
		lines.Add(ln1);

		Line ln2 = new Line();
		ln2.who = "Villain";
		ln2.what = "No... not yet!";
		lines.Add(ln2);

		test2.lines = lines;


		BattlePhase test3 = new BattlePhase();
		Dictionary<string, int> villaindir = new Dictionary<string, int>();
		villaindir ["daredevil"] = 2;
		test3.villainList = AddVillanByDict(villaindir);
		Debug.Log("villailist : " + test3.villainList[0]);


		NewMissionClass mc1 = new NewMissionClass();
		mc1.PhaseList = new List<MissionPhase>();
		mc1.PhaseList.Add(test1);
		mc1.PhaseList.Add(test2);
		mc1.PhaseList.Add(test3);


		//////////////////////////////////
		// set hero
		//////////////////////////////////
		HeroStatusClass selectedhero = HeroManager.Instance.SearchByName("Sheldon");
		HeroManager.Instance.SetParamsByCostume(selectedhero);
		mc1.AppliedHero = selectedhero;

		IEnumerator mission = StartMission(mc1);
		StartCoroutine(mission);
	}

	private IEnumerator StartMission(NewMissionClass mc){
		
		mc.ActiveFlg = true;
		mc.Success = false;

		List<MissionPhase> phaseList = mc.PhaseList;

		for(int i = 0; i <= (phaseList.Count - 1); i++){
			if(mc.ActiveFlg == true){
				IEnumerator mission = phaseList[i].PhaseCoroutine(mc);
				yield return StartCoroutine(mission);
			}
		
		}

		mc.ActiveFlg = false;

		if(mc.Success == true){
			Debug.Log("MissionComplete!");
		} else {
			Debug.Log("MissionFailture...");
		}

	}


	private List<VillainStatusClass> AddVillanByDict (Dictionary<string, int> villanDict){
	List<VillainStatusClass> ret = new List<VillainStatusClass>();

		foreach(KeyValuePair<string, int> pair in villanDict){
			for(int i= 1; i <= pair.Value; i++){
				switch(pair.Key){
					case "daredevil":
						VillainStatusClass villan = new VillainStatusClass();
						villan.Name = "shocker";
						villan.Health = 100;
						villan.Atk = 10f;
						villan.Def = 0f;
						ret.Add(villan);
						break;
					case "sniper":
						VillainStatusClass Shocker = new VillainStatusClass();
						Shocker.Name = "shocker";
						Shocker.Health = 90;
						Shocker.Atk = 10f;
						Shocker.Def = 0f;
						ret.Add(Shocker);
						break;
					default :
						break;
				}
			}
		}
		return ret;
	}

}
