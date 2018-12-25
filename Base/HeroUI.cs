using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Linq;
using TMPro;

public class HeroUI : MonoBehaviour {

	public TextAsset CostumeData;
	public XmlDocument xmlDoc;

	private HeroStatusClass selectedHero;

	public TextMeshProUGUI Name_tmp; 
	public TextMeshProUGUI Costume_tmp; 
	public TextMeshProUGUI Heroism_tmp; 
	public Image CostumeIcon;

	public Image EXPBar;
	public Image HPBar;
	public Image ATKBar;
	public Image DEFBar;
	public TextMeshProUGUI Lv_tmp;

	 public ToggleGroup costumeToggleGroup;

	 public GameObject HiringField;
	 public GameObject RecruitField;
	 public RectTransform RecruitNodePrefab;

	 void Awake () {
		 xmlDoc = new XmlDocument();
		 xmlDoc.LoadXml(CostumeData.text);
	 }

	void Start () {
		//RefreshRecruits();
	}

	public void Reflesh (HeroStatusClass hero) {
		selectedHero = hero;

		RefleshBars(hero);
		Name_tmp.text = hero.Name;
		Lv_tmp.text = "Lv." + hero.Lv.ToString();
		Costume_tmp.text = costumeString(hero.Costume, hero.CostumeLv);

		Heroism_tmp.text = "Heroism : " + hero.Heroism;
		Sprite test = Resources.Load<Sprite>("UI/CostumeIcons/" + hero.Costume);
		if(test != null){
			CostumeIcon.sprite = test;
		} else {
			CostumeIcon.sprite = null;
		}

	}

	public void RefleshBars(HeroStatusClass hero){

		if(hero.Exp != null) {
			float ExpPercentage = (float)(hero.Exp) / 500f;
			EXPBar.fillAmount = ExpPercentage;
		}

		if(hero.Costume == null){

			HPBar.fillAmount = 0.1f;
			ATKBar.fillAmount = 0.1f;
			DEFBar.fillAmount = 0.1f;

		} else{
			HeroManager.Instance.SetParamsByCostume(hero);

			float HpPercentage = (float)(hero.Health) / 500f;
			HPBar.fillAmount = HpPercentage;

			float AtkPercentage = hero.Atk / 150f;
			ATKBar.fillAmount = AtkPercentage;

			float DefPercentage = hero.Def / 150f;
			DEFBar.fillAmount = DefPercentage;

		}
	}


	public void TouchButton(bool down){
		this.gameObject.transform.GetComponent<UITransition>().UITransitioner(down);
	}


	public void onClick()
{
		string selectedLabel = costumeToggleGroup.ActiveToggles()
				.First().gameObject.name;

		HeroManager.Instance.SetCostume(selectedHero, selectedLabel);

		this.gameObject.transform.GetComponent<UITransition>().UITransitioner(false);
		Reflesh(selectedHero);

}

	public void onBackButtonClick()	{
		this.gameObject.transform.GetComponent<UITransition>().UITransitioner(false);
	}


	private string costumeString(string costume,int level){
		string ret;
		if(level != 1 || level == null){
			ret = costume + " Mk." + level;
		} else{
			ret = costume;
		}
		return ret;
	}

}
