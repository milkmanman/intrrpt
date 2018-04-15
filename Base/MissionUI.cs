using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

	public TextAsset MissionData;
	public XmlDocument xmlDoc;
	public TextAsset VillainInfoData;
	public XmlDocument xmlDoc2;
	public GameObject MissionNodeField;
	public GameObject MissionDetailField;
	public GameObject SadMissionDetailField;
	public GameObject SadMissionDetail2Field;
	public GameObject SelectHeroDropdown;
	public GameObject SadSelectHeroDropdown;
	public GameObject SubmitButton;
	public GameObject SadSubmitButton;
	public GameObject HoldMissionNode;
	public GameObject GoonButton;

	private string SelectedHeroName;


	[SerializeField]
		RectTransform prefab = null;

	void Awake () {
		xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(MissionData.text);

		xmlDoc2 = new XmlDocument();
		xmlDoc2.LoadXml(VillainInfoData.text);

		int [] MissionArray = SelectMission();
		int misnum = MissionArray.Length;

		for(int i = 1; i <= misnum; i++) {
			int missionNo = i;
			MissionClass test = new MissionClass();

			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(MissionNodeField.transform, false);
			test = MissionInfoClass(i);

			string MissionName = test.Name;
			var name = item.Find("Name").GetComponent<Text>();
			name.text = MissionName;

			item.GetComponent<MissionNode>().missioncls = test;

			Button button = item.GetComponent<Button>();
			button.onClick.AddListener(delegate{OnMissionNodeClicked(test);});
			button.onClick.AddListener(delegate{HoldMissionNode = item.gameObject;});

			}
			SelectHero(SelectHeroDropdown);
	}


	public int [] SelectMission () { //return mission id number in Array

		int MissionQty = xmlDoc.FirstChild.ChildNodes.Count;
		int [] Missions = new int [MissionQty];

		for(int i = 1; i <= MissionQty; i++){
			Missions[i - 1] = i;
		}

		return Missions;
		//array mission ids
	}

	public MissionClass MissionInfoClass (int missionNo) {

		MissionClass MisInfo = new MissionClass();

		XmlNode node0 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/name");
		XmlNode node1 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/level");
		XmlNode node2 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/type");
		XmlNode node3 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward1Type");
		XmlNode node4 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward1Value");
		XmlNode node5 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward2Type");
		XmlNode node6 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/reward2Value");
		XmlNode node7 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/description");
		XmlNode node8 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/villains");
		XmlNode node11 = xmlDoc.SelectSingleNode(@"//mission[@id=" + missionNo + "]/villainInfo");
		if(node11 != null){
		int villainInfo = int.Parse(node11.InnerText);
		Debug.Log(villainInfo);
		XmlNode node9 = xmlDoc2.SelectSingleNode(@"//villain[@id=" + villainInfo + "]/name");
		XmlNode node10 = xmlDoc2.SelectSingleNode(@"//villain[@id=" + villainInfo + "]/description");
		MisInfo.VillainName = node9.InnerText;
		MisInfo.VillainDescription = node10.InnerText;
		}


		if(node8 != null){
			Dictionary<string, int> villaindir = InstantiateVillainDic(node8);
			MisInfo.VillainList = AddVillanByDict(villaindir);
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

		return MisInfo;

	}

	public void OnMissionNodeClicked (MissionClass mc) {
		bool ismissionSAD = false;
		if(mc.Type == 4) ismissionSAD =true;
		this.gameObject.GetComponent<UITransition>().UITransitioner(ismissionSAD); //transition switch sad or not
		if(ismissionSAD == false){
			MissionDescription(mc);
			SelectHero(SelectHeroDropdown);
		} else {
			SAD_MissionDescription(mc);
		}
	}

	public void MissionDescription (MissionClass mc) {

		string rewardstr = mc.Reward1 + " Reward : " + mc.Reward1val.ToString();
		if(mc.Reward2 != null){
			rewardstr += "\n" + mc.Reward2 + " Reward : " + mc.Reward2val.ToString();
		}

		MissionDetailField.transform.Find("Name").GetComponent<Text>().text = mc.Name;
		MissionDetailField.transform.Find("Description").GetComponent<Text>().text = mc.Description;
		MissionDetailField.transform.Find("Reward").GetComponent<Text>().text = rewardstr;

		GameObject villainInfo = MissionDetailField.transform.Find("VillainInfo").gameObject;
		villainInfo.transform.Find("Name").GetComponent<Text>().text = mc.VillainName;
		villainInfo.transform.Find("Description").GetComponent<Text>().text = mc.VillainDescription;
		villainInfo.transform.Find("Icon").GetComponent<Image>().sprite = VillainIcon();

	}

	private Sprite VillainIcon(){
		Sprite test = Resources.Load<Sprite>("UI/VillainIcons/ios_emoji_neutral_face");
		return test;
	}

	public void SAD_MissionDescription (MissionClass mc) {
//		mc.Route = new List<int>();
//		mc.Route.Add(11);
//		mc.Route.Add(21);
//		mc.Route.Add(31);

		SadMissionDetailField.transform.Find("Name").GetComponent<Text>().text = mc.Name;
		SadMissionDetailField.transform.Find("Description").GetComponent<Text>().text = mc.Description;
		//if(mc.Route[3].Contain == false)Debug.Log("list is null");
		if (mc.Route.Count == 3){
			XmlNode node0 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + mc.Route[0] + "]/type");
			SadMissionDetailField.transform.Find("Node1").transform.Find("Text").GetComponent<Text>().text = SADMissionType(int.Parse(node0.InnerText));
			SadMissionDetailField.transform.Find("Node1").transform.Find("Image").GetComponent<Image>().color = new Color(0f, 1.0f, 0f, 1.0f);
			XmlNode node1 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + mc.Route[1] + "]/type");
			SadMissionDetailField.transform.Find("Node2").transform.Find("Text").GetComponent<Text>().text = SADMissionType(int.Parse(node1.InnerText));
			SadMissionDetailField.transform.Find("Node2").transform.Find("Image").GetComponent<Image>().color = new Color(0f, 1.0f, 0f, 1.0f);
			XmlNode node2 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + mc.Route[2] + "]/type");
			SadMissionDetailField.transform.Find("Node3").transform.Find("Text").GetComponent<Text>().text = SADMissionType(int.Parse(node2.InnerText));
			SadMissionDetailField.transform.Find("Node3").transform.Find("Image").GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

		} else if (mc.Route.Count == 2){
			XmlNode node0 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + mc.Route[0] + "]/type");
			SadMissionDetailField.transform.Find("Node1").transform.Find("Text").GetComponent<Text>().text = SADMissionType(int.Parse(node0.InnerText));
			SadMissionDetailField.transform.Find("Node1").transform.Find("Image").GetComponent<Image>().color = new Color(0f, 1.0f, 0f, 1.0f);
			XmlNode node1 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + mc.Route[1] + "]/type");
			SadMissionDetailField.transform.Find("Node2").transform.Find("Text").GetComponent<Text>().text = SADMissionType(int.Parse(node1.InnerText));
			SadMissionDetailField.transform.Find("Node2").transform.Find("Image").GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		} else {
			SadMissionDetailField.transform.Find("Node1").transform.Find("Text").GetComponent<Text>().text = "Search";
			SadMissionDetailField.transform.Find("Node1").transform.Find("Image").GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		}
		GoonButton.GetComponent<Button>().onClick.AddListener(delegate{SAD_MissionDescription2(mc);});

	}

	public void SAD_MissionDescription2 (MissionClass mc) {
		SelectHero(SadSelectHeroDropdown);

		int a = mc.Route[mc.Route.Count - 1];

		SadMissionDetail2Field.transform.Find("Name").GetComponent<Text>().text = mc.Name;
		XmlNode node0 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + a + "]/type");
		SadMissionDetail2Field.transform.Find("TypeStr").GetComponent<Text>().text = SADMissionType(int.Parse(node0.InnerText));
		XmlNode node1 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + a + "]/description");
		SadMissionDetail2Field.transform.Find("Description").GetComponent<Text>().text = node1.InnerText;

	}

	public void SelectHero (GameObject dp) {
		dp.GetComponent<Dropdown>().ClearOptions();
		List<string> listhero = HeroManager.Instance.listReadyHeroName();
		listhero.Insert(0, "Select Hero");
		dp.GetComponent<Dropdown>().AddOptions(listhero);
		dp.GetComponent<Dropdown>().value = 0;

	}

	public void SH_OnValueChange () {
		if(SelectHeroDropdown.GetComponent<Dropdown>().value != 0){
			SubmitButton.GetComponent<Button>().interactable = true;
			SelectedHeroName = SelectHeroDropdown.transform.Find("Label").GetComponent<Text>().text;
		} else {
			SubmitButton.GetComponent<Button>().interactable = false;
			SelectedHeroName = "";
		}
		//SelectedHeroName = SelectHeroDropdown.transform.Find("Label").GetComponent<Text>().text;
		RefleshStatusBars(SelectedHeroName);
	}

	private void RefleshStatusBars(string heroName){
		float HpPercentage = 1.0f;
		float AtkPercentage = 1.0f;
		float DefPercentage = 1.0f;
		GameObject ParentBars = MissionDetailField.transform.Find("Bars").gameObject;
		if(heroName != ""){
		HeroStatusClass hero = HeroManager.Instance.SearchByName(SelectedHeroName);
		HeroManager.Instance.SetParamsByCostume(hero);
		HpPercentage = (float)(hero.Health) / 500f;
		AtkPercentage = hero.Atk / 150f;
		DefPercentage = hero.Def / 150f;
		}

		ParentBars.transform.Find("HP/Bar").GetComponent<Image>().fillAmount = HpPercentage;
		ParentBars.transform.Find("ATK/Bar").GetComponent<Image>().fillAmount = AtkPercentage;
		ParentBars.transform.Find("DEF/Bar").GetComponent<Image>().fillAmount = DefPercentage;


	}

	public void SAD_SH_OnValueChange () {///////////////
		if(SadSelectHeroDropdown.GetComponent<Dropdown>().value != 0){
			SadSubmitButton.GetComponent<Button>().interactable = true;
		} else {
			SadSubmitButton.GetComponent<Button>().interactable = false;
		}
		SelectedHeroName = SadSelectHeroDropdown.transform.Find("Label").GetComponent<Text>().text;
	}

	public void SubmitButtonOnClicked() {
		this.gameObject.GetComponent<UITransition>().UITransitioner(true);
		HeroStatusClass selectedhero = HeroManager.Instance.SearchByName(SelectedHeroName);
		HoldMissionNode.GetComponent<MissionNode>().SetHero(selectedhero);
		HoldMissionNode.GetComponent<MissionNode>().StartMission();
	}

	public void SAD_SubmitButtonOnClicked() {
		this.gameObject.GetComponent<UITransition>().UITransitionerTwice(false);
		HeroStatusClass selectedhero = HeroManager.Instance.SearchByName(SelectedHeroName);
		HoldMissionNode.GetComponent<MissionNode>().SetHero(selectedhero);
		HoldMissionNode.GetComponent<MissionNode>().StartMission();

	}

	public void OnMissionButtonClickedFns(MissionClass miscls, HeroStatusClass appliedHero) {
		DisplayResult(miscls, appliedHero);
		appliedHero.Status = 0;

		HoldMissionNode.GetComponent<MissionNode>().InitButton();
		AddNextPhase(miscls);


		if(miscls.Success == true){
			ResourceManager.Instance.IncreaseResource(miscls.Reward1, miscls.Reward1val);
			if(miscls.Reward2 != null || miscls.Reward2 == ""){
				ResourceManager.Instance.IncreaseResource(miscls.Reward2, miscls.Reward2val);
			}
		}

	}

	private void DisplayResult(MissionClass miscls, HeroStatusClass appliedHero) {
		GameObject result = this.gameObject.transform.Find("Result").gameObject;
		result.SetActive(true);
		GameObject Default = result.transform.Find("Default").gameObject;
		GameObject SAD = result.transform.Find("SAD").gameObject;


		switch(miscls.Type){

			case 4:
			SAD.SetActive(true);
			Default.SetActive(false);

			int phase = miscls.Route[miscls.Route.Count - 1] / 1;
			XmlNode node0 = xmlDoc.SelectSingleNode(@"//mission[@id=" + miscls.MissionNo + "]/missionNode[@id=" + phase + "]/type");

			SAD.transform.Find("MissionPhase").GetComponent<Text>().text = "MissionPhase " + phase.ToString().Substring(0, 1) + " : " + SADMissionType(int.Parse(node0.InnerText));
			SAD.transform.Find("MissionHero").GetComponent<Text>().text = "HeroName : " + appliedHero.Name;


			if(miscls.Success == true){

				SAD.transform.Find("MissionName").GetComponent<Text>().text = miscls.Name + " : Mission Complete";

				XmlNode node1 = xmlDoc.SelectSingleNode(@"//mission[@id=" + miscls.MissionNo + "]/missionNode[@id=" + phase + "]/rewardtype");
				XmlNode node2 = xmlDoc.SelectSingleNode(@"//mission[@id=" + miscls.MissionNo + "]/missionNode[@id=" + phase + "]/resultmsg");

				if(node1 == null){
					SAD.transform.Find("MissionReport").GetComponent<Text>().text = node2.InnerText;
					SAD.transform.Find("MissionReward_Static").GetComponent<Text>().text = "";
					SAD.transform.Find("MissionReward").GetComponent<Text>().text = "";
				} else{
					XmlNode node3 = xmlDoc.SelectSingleNode(@"//mission[@id=" + miscls.MissionNo + "]/missionNode[@id=" + phase + "]/rewardtype");
					XmlNode node4 = xmlDoc.SelectSingleNode(@"//mission[@id=" + miscls.MissionNo + "]/missionNode[@id=" + phase + "]/rewardvalue");
					SAD.transform.Find("MissionReport").GetComponent<Text>().text = node2.InnerText;
					SAD.transform.Find("MissionReward").GetComponent<Text>().text = node3.InnerText + " : " + node4.InnerText;
				}
			} else{
				SAD.transform.Find("MissionName").GetComponent<Text>().text = miscls.Name + " : Mission Failure";

			}

				break;

			default:
			SAD.SetActive(false);
			Default.SetActive(true);

			if(miscls.Success == true){
				Default.transform.Find("MissionName").GetComponent<Text>().text = miscls.Name + " : Mission Complete";
				Default.transform.Find("MissionHero").GetComponent<Text>().text = "HeroName : " + appliedHero.Name;
				string RewardStr =  miscls.Reward1 + " : " + miscls.Reward1val.ToString();
				if(miscls.Reward2 != null || miscls.Reward2 == ""){
					RewardStr += "\n" + miscls.Reward2 + " : " + miscls.Reward2val.ToString();
				}
				Default.transform.Find("MissionReward").GetComponent<Text>().text = RewardStr;

			} else if (miscls.Success == false){
				Default.transform.Find("MissionName").GetComponent<Text>().text = miscls.Name + " : Mission Failed";
				Default.transform.Find("MissionHero").GetComponent<Text>().text = "HeroName : " + appliedHero.Name;
			}
				break;

		}

	}

	private void AddNextPhase(MissionClass mc){
		if(mc.Success == true){
			int route = mc.Route[mc.Route.Count - 1];
			Debug.Log("current route : " + route.ToString());
			XmlNode node0 = xmlDoc.SelectSingleNode(@"//mission[@id=" + mc.MissionNo + "]/missionNode[@id=" + route + "]/morethan1move");
			int lastroute = int.Parse(node0.InnerText);
			mc.Route.Add(lastroute);
			MissionManager.Instance.SaveSADMissionProgress(mc);
		}
	}

	private string SADMissionType(int a){
		string rtn = "";
		if(a == 1){
			rtn = "Examination";
		} else if (a == 2){
			rtn = "Search";
		} else if (a == 4){
			rtn = "Eliminate";
		}
		return rtn;
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
						villan.Atk = 15f;
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
