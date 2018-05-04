using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

	public GameObject MissionNodeField;
	public GameObject MissionDetailField;
	public GameObject SelectHeroDropdown;
	public GameObject SubmitButton;
	public GameObject HoldMissionNode;

	private string SelectedHeroName;


	[SerializeField]
		RectTransform prefab = null;

	void Awake () {

		List<MissionClass> misList = MissionManager.Instance.selectableMissionClassList;

		for(int i = 1; i <= misList.Count; i++) {

			MissionClass misClass = new MissionClass();

			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(MissionNodeField.transform, false);
			misClass = misList[i-1];


			string MissionName = misClass.Name;
			var name = item.Find("Name").GetComponent<Text>();
			name.text = MissionName;

			item.GetComponent<MissionNode>().missioncls = misClass;

			Button button = item.GetComponent<Button>();
			button.onClick.AddListener(delegate{OnMissionNodeClicked(misClass);});
			button.onClick.AddListener(delegate{HoldMissionNode = item.gameObject;});

			}

			SelectHero(SelectHeroDropdown);
	}


	public void OnMissionNodeClicked (MissionClass mc) {
		bool ismissionSAD = false;
		this.gameObject.GetComponent<UITransition>().UITransitioner(ismissionSAD); //transition switch sad or not
			MissionDescription(mc);
			SelectHero(SelectHeroDropdown);

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

	public void SubmitButtonOnClicked() {
		this.gameObject.GetComponent<UITransition>().UITransitioner(true);
		HeroStatusClass selectedhero = HeroManager.Instance.SearchByName(SelectedHeroName);
		HoldMissionNode.GetComponent<MissionNode>().SetHero(selectedhero);
		HoldMissionNode.GetComponent<MissionNode>().StartMission();
	}


	public void OnMissionButtonClickedFns(MissionClass miscls, HeroStatusClass appliedHero) {
		DisplayResult(miscls, appliedHero);
		appliedHero.Status = 0;

		HoldMissionNode.GetComponent<MissionNode>().InitButton();


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




	}


}
