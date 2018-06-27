using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class TestMissionSystem : MonoBehaviour {

	public TextAsset NM_DB_Phase1;
	public XmlDocument MissionDoc;
/* 
	// Use this for initialization
	void Start () {
		//NewMissionClass test = new NewMissionClass();
		//StartMission(test);
		MissionDoc = new XmlDocument();
		MissionDoc.LoadXml(NM_DB_Phase1.text);
		




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
		//mc1.PhaseList.Add(test1);
		//mc1.PhaseList.Add(test2);
		//mc1.PhaseList.Add(test3);
		mc1.PhaseList = setPhaseList(1);

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
			mc.PhaseHistory = new List<string>();
			mc.PhaseHistory = setPhaseList(mc);
			Debug.Log(mc.PhaseHistory);
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

	public MissionClass setSelectableMissionList(int missionNo){

		MissionClass MisInfo = new MissionClass();

		XmlNode node0 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/name");
		XmlNode node1 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/level");
		XmlNode node2 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/type");
		XmlNode node3 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward1Type");
		XmlNode node4 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward1Value");
		XmlNode node5 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward2Type");
		XmlNode node6 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward2Value");
		XmlNode node7 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/description");
		//XmlNode node8 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/villains");
		//XmlNode node11 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/villainInfo");
		//if(node11 != null){
		//	int villainInfo = int.Parse(node11.InnerText);
		//	XmlNode node9 = VillainDoc.SelectSingleNode(@"//villain[@id=" + villainInfo + "]/name");
		//	XmlNode node10 = VillainDoc.SelectSingleNode(@"//villain[@id=" + villainInfo + "]/description");
		//	MisInfo.VillainName = node9.InnerText;
		//	MisInfo.VillainDescription = node10.InnerText;
		//}

		//if(node8 != null){
		//	Dictionary<string, int> villaindir = InstantiateVillainDic(node8);
		//	MisInfo.VillainList = AddVillanByDict(villaindir);
		//}

		MisInfo.MissionNo = missionNo;
		MisInfo.Name = node0.InnerText;
		MisInfo.Level = int.Parse(node1.InnerText);
		MisInfo.Type = int.Parse(node2.InnerText);
		MisInfo.Reward1 = node3.InnerText;
		MisInfo.Reward1val = int.Parse(node4.InnerText);
		if(node5 != null){
			MisInfo.Reward2 = node5.InnerText;
			MisInfo.Reward2val = int.Parse(node6.InnerText);
		}
		MisInfo.Description = node7.InnerText;


		return MisInfo;
	}


	public List<MissionPhase> setPhaseList(int missionNo){
		List<MissionPhase> rtnList = new List<MissionPhase>();

		XmlNode node0 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/phases");
		XmlNode phase1 = node0.SelectSingleNode("./phase[@id=1]/type");
		for(int i = 1; i <= node0.ChildNodes.Count; i++){
			XmlNode TypeNode = node0.SelectSingleNode("./phase[@id=" + i + "]/type");
			switch(TypeNode.InnerText){
				case "Move" : 
					MovePhase move = new MovePhase();
					XmlNode mv1 = node0.SelectSingleNode("./phase[@id=" + i + "]/destination");
					XmlNode mv2 = node0.SelectSingleNode("./phase[@id=" + i + "]/bridgemsg");
					move.Destination = mv1.InnerText;
					move.BridgeMsg = mv2.InnerText;
					rtnList.Add(move);

					break;

				case "Talk" :
					TalkPhase talk = new TalkPhase();
					List<Line> lines = new List<Line>();
					XmlNode lns = node0.SelectSingleNode("./phase[@id=" + i + "]/lines");
					for(int ino = 1; ino <= lns.ChildNodes.Count; ino++){

						Line lineclass = new Line();
						XmlNode tlk1 = node0.SelectSingleNode("./phase[@id=" + i + "]/lines/line[@id=" + ino + "]/who");
						XmlNode tlk2 = node0.SelectSingleNode("./phase[@id=" + i + "]/lines/line[@id=" + ino + "]/what");
						lineclass.who = tlk1.InnerText;
						lineclass.what = tlk2.InnerText;
						lines.Add(lineclass);
					}
					talk.lines = lines;
					rtnList.Add(talk);

					break;

				case "Battle" :
					BattlePhase battle = new BattlePhase();
					List<VillainStatusClass> villainList = new List<VillainStatusClass>();
					XmlNode vln = node0.SelectSingleNode("./phase[@id=" + i + "]/villains");
					Debug.Log("villainnode : " + vln.InnerText);
					Dictionary<string, int> villaindir = InstantiateVillainDic(vln);
					battle.villainList = AddVillanByDict(villaindir);

					
					rtnList.Add(battle);

					break;

				default :
					break;

			}

		}

		return rtnList;

	}


	private Dictionary<string, int> InstantiateVillainDic(XmlNode villainsNode){
		Dictionary<string, int> ret = new Dictionary<string, int> ();
		foreach(XmlNode node in villainsNode.ChildNodes){
			ret.Add(node.Name, int.Parse(node.InnerText));
		}
		return ret;
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

	private List<string> setPhaseList(NewMissionClass mc){

		List<string> PhaseList = new List<string>();

		List<MissionPhase> phaseList = mc.PhaseList;
		for(int i = 1; i <= mc.PhaseList.Count; i++){
			PhaseList.Add(mc.PhaseList[mc.PhaseList.Count - 1].Type);
		}

		return PhaseList;

	}
*/

}
