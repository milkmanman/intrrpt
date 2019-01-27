using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Xml;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class MissionManager : SingletonMonoBehaviourFast<MissionManager> {

	public TextAsset MissionDB_Phase1;
	public XmlDocument MissionDoc;
	public TextAsset FreeRoamDB;
	public XmlDocument FreeRoamDoc;
	public TextAsset VillainInfoData;
	public XmlDocument VillainDoc;
	public RestPhaseListSO RestPhaseDB;

	public List<int> completedMission;
	public List<int> selectableMission;
	public List<MissionClass> selectableMissionClassList;
	public List<IEnumerator> routineList;
	public List<MissionClass> MissionList;
	public List<FreeRoamClass> FreeRoamList;

	public TextAsset FreeroamDBJson;


	void Awake () {

		routineList = new List<IEnumerator>();
		MissionList = new List<MissionClass>();
		FreeRoamList = new List<FreeRoamClass>();

		MissionDoc = new XmlDocument();
		MissionDoc.LoadXml(MissionDB_Phase1.text);
		FreeRoamDoc = new XmlDocument();
		FreeRoamDoc.LoadXml(FreeRoamDB.text);
		VillainDoc = new XmlDocument();
		VillainDoc.LoadXml(VillainInfoData.text);

		completedMission = SetCompletedMission();
		selectableMission = SetSelectableMission();
		selectableMissionClassList = setSelectableMissionList(selectableMission);

		//SetFreeroamPhaseFromJson();
	}



	public List<int> SetCompletedMission (){
		//Playerprefs
		List<int> returnList = new List<int>() {0, 1, 3};
		return returnList;
	}

	public List<int> SetSelectableMission (){
		List<int> returnList = new List<int>();
		int MissionQty = MissionDoc.FirstChild.ChildNodes.Count;

		for(int i = 1; i <= MissionQty; i++){
			XmlNode node0 = MissionDoc.SelectSingleNode(@"//mission[@id=" + i + "]/requireCompleted");

			if(completedMission.Contains(int.Parse(node0.InnerText)) == true  && completedMission.Contains(i) == false){

				returnList.Add(i);
			}
		}
		return returnList;
	}

	public List<MissionClass> setSelectableMissionList(List<int> list){
		List<MissionClass> returnList = new List<MissionClass>();

		for(int i = 1; i <= list.Count; i++){

			int missionNo = list[i-1];
			MissionClass MisInfo = new MissionClass();

			XmlNode node0 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/name");
			XmlNode node1 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/level");
			XmlNode node2 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/type");
			XmlNode node3 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward1Type");
			XmlNode node4 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward1Value");
			XmlNode node5 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward2Type");
			XmlNode node6 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward2Value");
			XmlNode node7 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/description");
			XmlNode node8 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/villains");
			XmlNode node11 = MissionDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/villainInfo");
			if(node11 != null){
			int villainInfo = int.Parse(node11.InnerText);
			XmlNode node9 = VillainDoc.SelectSingleNode(@"//villain[@id=" + villainInfo + "]/name");
			XmlNode node10 = VillainDoc.SelectSingleNode(@"//villain[@id=" + villainInfo + "]/description");
			MisInfo.VillainName = node9.InnerText;
			MisInfo.VillainDescription = node10.InnerText;
			}

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

			MisInfo.PhaseList = setPhaseList(i);

			returnList.Add(MisInfo);

		}

		return returnList;
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

	private void setFreeroamHoldResource(FreeRoamClass frc){
		if ( frc.HoldResources.ContainsKey(frc.MissionResourceType) ){
            frc.HoldResources[frc.MissionResourceType] = frc.MissionResourceValue + frc.HoldResources[frc.MissionResourceType];
        } else {
            frc.HoldResources.Add(frc.MissionResourceType, frc.MissionResourceValue);
			Debug.LogWarning("resource : " + frc.PhaseList.Count());
		}

		//for reward log
		frc.MissionLog = frc.MissionLog + "!! get reward !!" + "\n";
		frc.PhaseListHistory.Last().Log = frc.PhaseListHistory.Last().Log + "!! get reward !!" + "\n";

		if(frc.PushMissionLogAction != null){
			frc.PushMissionLogAction();
		};

	}

	 private void setFreeroamExp(FreeRoamClass frc){
		frc.AppliedHero.Exp = frc.AppliedHero.Exp + frc.MissionExp;
		frc.MissionLog = frc.MissionLog + "!! get exp :" + frc.MissionExp.ToString() + "\n";
		frc.PhaseListHistory.Last().Log = frc.PhaseListHistory.Last().Log + "!! get exp :" + frc.MissionExp.ToString() + "\n";
		frc.SumIncreaseExp += frc.MissionExp;
		Debug.LogWarning("SumIncreaseExp : " + frc.SumIncreaseExp.ToString());
	}

	private Dictionary<string, int> InstantiateVillainDic(XmlNode villainsNode){
		Dictionary<string, int> ret = new Dictionary<string, int> ();
		foreach(XmlNode node in villainsNode.ChildNodes){
			ret.Add(node.Name, int.Parse(node.InnerText));
		}
		return ret;
	}

	private Dictionary<string, int> InstantiateVillainDicJson(JArray villainsJArray){
		Dictionary<string, int> ret = new Dictionary<string, int> ();

		foreach (JObject jobj in villainsJArray) {
			string villainType = (string)jobj["type"];
			int villainCount = (int)jobj["count"];
			ret.Add(villainType, villainCount);
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

	public void StartFreeRoam(HeroStatusClass hc){
		Debug.Log("StartFreeRoam");
		hc.Status = 1;
		FreeRoamClass frc = new FreeRoamClass();
		frc.AppliedHero = hc;
		HeroManager.Instance.SetParamsByCostume(hc);
		//MissionList.Add(frc);
		FreeRoamList.Add(frc);
		IEnumerator frmRoutine = FreeroamProgress(frc);
		StartCoroutine(frmRoutine);

	}

	private IEnumerator FreeroamProgress (FreeRoamClass frc) {

		frc.ActiveFlg = true;
		frc.FailtureFlag = false;
		frc.IsBackFlag = false;
		//mc.Success = false;
		frc.PhaseList = new List<MissionPhase>();
		frc.HoldResources = new Dictionary<string, int>();
		List<MissionPhase> phaseList = frc.PhaseList;
		int countPhases = 0;
		int countPatrol = 0;
		frc.PhaseListHistory = new List<MissionPhase>();
		while(frc.ActiveFlg == true){
			phaseList.Clear();


			if(frc.IsBackFlag == true){
				BackPhase bp = new BackPhase();
				//rp = setRestPhase();
				phaseList.Add(bp);
				frc.PhaseListHistory.Add(bp);
				IEnumerator back = phaseList[0].PhaseCoroutine(frc);
				yield return StartCoroutine(back);
				yield return new WaitForSeconds (1.0f);

				frc.ActiveFlg = false;
				if(frc.FinishMissionAction != null){
					frc.FinishMissionAction();
				};
				
				break;
			}


			if(frc.AppliedHero.Health <= 50){

				RestPhase rp = new RestPhase();
				rp = setRestPhase();
				phaseList.Add(rp);
				frc.PhaseListHistory.Add(rp);
				IEnumerator rest = phaseList[0].PhaseCoroutine(frc);
				yield return StartCoroutine(rest);
				yield return new WaitForSeconds (1.0f);

			} else if(countPatrol <= 2) {

				PatrolPhase pp = new PatrolPhase();
				phaseList.Add(pp);
				frc.PhaseListHistory.Add(pp);
				IEnumerator patrol = phaseList[0].PhaseCoroutine(frc);
				yield return StartCoroutine(patrol);
				yield return new WaitForSeconds (1.0f);
				countPatrol++;

			} else {

				//SetFreeroamPhase(frc);
				SetFreeroamPhaseFromJson(frc);
				yield return new WaitForSeconds (1.0f);
				while(phaseList.Count > 0　&& frc.AppliedHero.Health > 0 && frc.FailtureFlag == false) {
				//for(int i = 0; i <= (phaseList.Count -1); i++){
					//IEnumerator mission = phaseList[i].PhaseCoroutine(frc);
					//frc.PhaseListHistory.Add(phaseList[i]);
					IEnumerator mission = phaseList.First().PhaseCoroutine(frc);
					frc.PhaseListHistory.Add(phaseList.First());
					yield return StartCoroutine(mission);
					yield return new WaitForSeconds (1.0f);
					phaseList.RemoveAt(0);
					
				}
				countPatrol = 0;

				if(frc.AppliedHero.Health <= 0){

					Debug.Log("Automatic Back");
					BackPhase bp = new BackPhase();
					bp.doesSendMedic = true;
					//rp = setRestPhase();
					phaseList.Clear();
					phaseList.Add(bp);
					frc.PhaseListHistory.Add(bp);
					IEnumerator back = phaseList[0].PhaseCoroutine(frc);
					yield return StartCoroutine(back);
					yield return new WaitForSeconds (1.0f);
					break;
				} else if(frc.AppliedHero.Health > 0 && frc.FailtureFlag == true) {
					frc.FailtureFlag = false;

				} else {

					setFreeroamExp(frc);
					setFreeroamHoldResource(frc);
					frc.CountSuccessedMission++;
					Debug.LogWarning("SuccessedMission : " + frc.CountSuccessedMission.ToString());
				}

			}
		Debug.Log("mission history count : " + frc.PhaseListHistory.Count);
		Debug.Log("mission phaselist count : " + phaseList.Count);

		countPhases++;
		}
		frc.ActiveFlg = false;
		if(frc.FinishMissionAction != null){
			frc.FinishMissionAction();
		};
		/*while(true){
			RestPhase pp = new RestPhase();
			//PatrolPhase pp = new PatrolPhase();
			phaseList.Add(pp);
			IEnumerator patrol = phaseList[0].PhaseCoroutine(frc);
			yield return StartCoroutine(patrol);
			yield return new WaitForSeconds (1.0f);
			phaseList = new List<MissionPhase>();

			if(phaseList.Count == 0 || phaseList == null){
				phaseList = SetFreeroamPhase();
				yield return new WaitForSeconds (1.0f);

				for(int i = 0; i <= (phaseList.Count -1); i++){
					IEnumerator mission = phaseList[i].PhaseCoroutine(frc);
					yield return StartCoroutine(mission);
					yield return new WaitForSeconds (1.0f);
					countPhases++;
					Debug.Log("phases" + countPhases);
				}
				phaseList = new List<MissionPhase>();
			}
		}*/
		//frc.ActiveFlg = false;
	}

/////////////////
////////////////
	private void SetFreeroamPhase(FreeRoamClass frc){

		List<MissionPhase> rtnList = frc.PhaseList;

		int missionCount = FreeRoamDoc.SelectSingleNode("missions").ChildNodes.Count;
		int randomCount = UnityEngine.Random.Range(1, missionCount + 1);

		XmlNode node0 = FreeRoamDoc.SelectSingleNode(@"//mission[@id=" + randomCount + "]/phases");

		XmlNode rewardkey = FreeRoamDoc.SelectSingleNode(@"//mission[@id=" + randomCount + "]/rewardType");
		XmlNode rewardval = FreeRoamDoc.SelectSingleNode(@"//mission[@id=" + randomCount + "]/rewardValue");
		frc.MissionResourceType = rewardkey.InnerText;
		frc.MissionResourceValue = int.Parse(rewardval.InnerText);

		//XmlNode phase1 = node0.SelectSingleNode("./phase[@id=1]/type");
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
					Dictionary<string, int> villaindir = InstantiateVillainDic(vln);
					battle.villainList = AddVillanByDict(villaindir);					
					rtnList.Add(battle);

					break;

				case "Search" :
					SearchPhase search = new SearchPhase();
					XmlNode srch1 = node0.SelectSingleNode("./phase[@id=" + i + "]/object");
					search.Object = srch1.InnerText;
					rtnList.Add(search);
					break;

				default :
					break;

			}

		}
	}

	public void SetFreeroamPhaseFromJson(FreeRoamClass frc){
		List<MissionPhase> rtnList = frc.PhaseList;

		JObject jdb = (JObject)JsonConvert.DeserializeObject(FreeroamDBJson.text);
        JArray jarr = (JArray)jdb["missions"];
		int missionCount = jarr.Count;
		int randomCount = UnityEngine.Random.Range(1, missionCount + 1);
		
		JObject item = (JObject)jarr[randomCount - 1];
		Debug.Log(item["rewardType"] + " : " + item["rewardValue"]);

		frc.MissionResourceType = (string)item["rewardType"];
		frc.MissionResourceValue = (int)item["rewardValue"];
		frc.MissionExp = (int)item["exp"];

		JArray phase_item_count = (JArray)item["phases"];

		for(int i = 1; i <= (int)phase_item_count.Count; i++){

			JObject phase_item = (JObject)item["phases"][i - 1];

			string phaseType = (string)phase_item["type"];
			Debug.LogWarning("PHASETYPE : " + phaseType);

			switch(phaseType){
				case "Move" : 
					MovePhase move = new MovePhase();
					move.Destination = (string)phase_item["destination"];
					move.BridgeMsg = (string)phase_item["bridgemsg"];
					rtnList.Add(move);
					break;

				case "Talk" :
					TalkPhase talk = new TalkPhase();
					List<Line> lines = new List<Line>();
					JArray line_item = (JArray)item["phases"][i - 1]["lines"];
					Debug.LogWarning("TALKLINE COUNT : " + line_item.Count);
					for(int ino = 1; ino <= (int)line_item.Count; ino++){
						Line lineclass = new Line();
						lineclass.who = (string)line_item[ino - 1]["who"];
						lineclass.what = (string)line_item[ino - 1]["what"];
						lines.Add(lineclass);
					}
					talk.lines = lines;
					rtnList.Add(talk);
					break;


				case "Battle" :
					BattlePhase battle = new BattlePhase();
					JArray vln_item = (JArray)item["phases"][i - 1]["villains"];
					Dictionary<string, int> villaindir = InstantiateVillainDicJson(vln_item);
					battle.villainList = AddVillanByDict(villaindir);					
					rtnList.Add(battle);
					break;


				case "Search" :
					SearchPhase search = new SearchPhase();
					search.Object = (string)phase_item["object"];
					search.FindMsg = (string)phase_item["FindMsg"];
					rtnList.Add(search);
					break;


				default :
					break;
			}

		}

		
	}


	public void FinishFreeroam(FreeRoamClass frc){
		ResourceManager.Instance.VulkResource(frc.HoldResources);
		frc.AppliedHero.Status = 0;
		FreeRoamList.Remove(frc);
	}

	private RestPhase setRestPhase(){
		RestPhase ret = new RestPhase(); 

		int countlist = RestPhaseDB.RestPhasePropList.Count;
		int picknum = UnityEngine.Random.Range(0, countlist);

		ret.recoveryValue = RestPhaseDB.RestPhasePropList[picknum].Value;

		if(RestPhaseDB.RestPhasePropList[picknum].BeforeRestLines.Count > 0){
			ret.beforeLines = new List<Line>();
			for(int blines = 1; blines <= RestPhaseDB.RestPhasePropList[picknum].BeforeRestLines.Count; blines++){
				Line aLine = new Line();
				aLine.who = RestPhaseDB.RestPhasePropList[picknum].BeforeRestLines[blines - 1].Who;
				aLine.what = RestPhaseDB.RestPhasePropList[picknum].BeforeRestLines[blines - 1].What;
				ret.beforeLines.Add(aLine);
			}
		}

		if(RestPhaseDB.RestPhasePropList[picknum].AfterRestLines.Count > 0){
			ret.afterLines = new List<Line>();
			for(int alines = 1; alines <= RestPhaseDB.RestPhasePropList[picknum].AfterRestLines.Count; alines++){
				Line cLine = new Line();
				cLine.who = RestPhaseDB.RestPhasePropList[picknum].AfterRestLines[alines - 1].Who;
				cLine.what = RestPhaseDB.RestPhasePropList[picknum].AfterRestLines[alines - 1].What;
				ret.afterLines.Add(cLine);
			}
		}

		if(RestPhaseDB.RestPhasePropList[picknum].DoesEat == true){
			ret.restType = "eat";
			ret.food = RestPhaseDB.RestPhasePropList[picknum].Take;
		} else {
			ret.restType = "nap";
		}

		return ret;
	}


	public void StartMission(HeroStatusClass hc, MissionClass missioncls){

		int i = 1;
		hc.Status = 1;

		if(MissionList.Count == 0){
			missioncls.AppliedHero = hc;
			HeroManager.Instance.SetParamsByCostume(hc);
			MissionList.Add(missioncls);

			//MissionList[i-1] = missioncls;
			//MissionList[i-1].AppliedHero = hc;
			//IEnumerator misRoutine = MissionProgless(MissionList[i-1]);
			IEnumerator misRoutine = MissionProgless(missioncls);
			StartCoroutine(misRoutine);
			routineList.Add(misRoutine);
			Debug.Log("StartMissionClass" + "[" + i.ToString() + "]");

		} else {

			while(true){
				if(MissionList[i-1].ActiveFlg == false || MissionList[i-1].ActiveFlg ==null){
					MissionList[i-1] = missioncls;
					MissionList[i-1].AppliedHero = hc;
					HeroManager.Instance.SetParamsByCostume(hc);

					IEnumerator misRoutine = MissionProgless(MissionList[i-1]);
					StartCoroutine(misRoutine);
					routineList.Add(misRoutine);
					Debug.Log("StartMissionClass" + "[" + i.ToString() + "]");
					break;
					if(MissionList.Count == i) break;
				}
				i++;
			}
		}

	}


	private IEnumerator MissionProgless (MissionClass mc) {

		mc.ActiveFlg = true;
		mc.Success = false;

		List<MissionPhase> phaseList = mc.PhaseList;

		for(int i = 0; i <= (phaseList.Count - 1); i++){
			//mc.PhaseHistory = new List<string>();
			//mc.PhaseHistory = setPhaseList(mc.MissionNo);
			Debug.Log(mc.PhaseHistory);
			if(mc.ActiveFlg == true){
				IEnumerator mission = phaseList[i].PhaseCoroutine(mc);
				yield return StartCoroutine(mission);

				if(mc.AppliedHero.Health <= 0){
					mc.ActiveFlg = false;
				}
			}

			yield return new WaitForSeconds (1.0f);  

		
		}

		mc.ActiveFlg = false;

		if(mc.Success == true){
			Debug.Log("MissionComplete!");
		} else {
			Debug.Log("MissionFailture...");
		}

	}


	public static void SaveList<T>(string key , List<T> value){
//		string serizlizedList = key;
			string serizlizedList = Serialize<List<T>> (value);
			PlayerPrefs.SetString (key, serizlizedList);
	}

	private int SaveBool(bool intbool){
		int a = 0;
		if(intbool == true)a = 1;
		return a;
	}

	private bool LoadBool(string index){
		bool a = false;
		int b = PlayerPrefs.GetInt(index);
		if(b == 1)a = true;
		return a;
	}

	public static List<T> LoadList<T> (string key){
			//keyがある時だけ読み込む
			if (PlayerPrefs.HasKey (key)) {
					string serizlizedList = PlayerPrefs.GetString (key);
//					return Deserialize<List<T>> (serizlizedList);
					return Deserialize<List<T>> (serizlizedList);

			}

			return new List<T> ();
	}

	private static string Serialize<T> (T obj){
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			MemoryStream    memoryStream    = new MemoryStream ();
			binaryFormatter.Serialize (memoryStream , obj);
			return Convert.ToBase64String (memoryStream   .GetBuffer ());
	}

	private static T Deserialize<T> (string str){
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			MemoryStream    memoryStream    = new MemoryStream (Convert.FromBase64String (str));
			return (T)binaryFormatter.Deserialize (memoryStream);
	}

}
