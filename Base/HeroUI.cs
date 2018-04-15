using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Linq;


public class HeroUI : MonoBehaviour {

	public TextAsset CostumeData;
	public XmlDocument xmlDoc;

	private HeroStatusClass selectedHero;

	public Text Name;
	public Text Costume;
	public Text Heroism;
	public Image CostumeIcon;

	public Image HPBar;
	public Image ATKBar;
	public Image DEFBar;

	 public ToggleGroup costumeToggleGroup;

	 public GameObject HiringField;
	 public GameObject RecruitField;
	 public RectTransform RecruitNodePrefab;

	 void Awake () {
		 xmlDoc = new XmlDocument();
		 xmlDoc.LoadXml(CostumeData.text);
	 }

	void Start () {
		RefreshRecruits();
	}

	public void Reflesh (HeroStatusClass hero) {
		selectedHero = hero;

		RefleshBars(hero);
		Name.text = hero.Name;
		Costume.text = costumeString(hero.Costume, hero.CostumeLv);
		Heroism.text = "Heroism : " + hero.Heroism;
		Sprite test = Resources.Load<Sprite>("UI/CostumeIcons/" + hero.Costume);
		if(test != null){
			CostumeIcon.sprite = test;
		} else {
			CostumeIcon.sprite = null;
		}

	}

	public void RefleshBars(HeroStatusClass hero){

		if(hero.Costume == null){

			HPBar.fillAmount = 0.1f;
			ATKBar.fillAmount = 0.1f;
			DEFBar.fillAmount = 0.1f;

		} else{
			HeroManager.Instance.SetParamsByCostume(hero);

			float HpPercentage = (float)(hero.Health) / 500f;
			HPBar.fillAmount = HpPercentage;
			Debug.Log("Reflesh hero health : " + hero.Health.ToString());

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

	public void RefreshRecruits(){
		HeroManager.Instance.RefleshRecruitList();
		foreach (Transform item in RecruitField.transform) {
			Destroy(item.gameObject);
		}
		for(int i = 1; i <= 4; i++){
				var item = GameObject.Instantiate(RecruitNodePrefab) as RectTransform;
				item.SetParent(RecruitField.transform, false);
				ApplicantClass test =  new ApplicantClass();
				test = HeroManager.Instance.RecruitHeroList[i-1];
				item.GetComponent<RecruitNode>().Hero = test;
				item.GetComponent<RecruitNode>().RefleshRecruit();
				Button button = item.GetComponent<RecruitNode>().SelectButton;
				RecruitNode node = item.GetComponent<RecruitNode>();
				button.onClick.AddListener(delegate{HeroManager.Instance.HoldApplicant = test;});
				button.onClick.AddListener(delegate{ShowApplicantDetail();});
		}
	}

	public void ShowApplicantDetail(){
		GameObject Hiring = HiringField.transform.Find("Detail").gameObject;
		Hiring.SetActive(true);
		Hiring.GetComponent<RecruitDetail>().Reflesh(HeroManager.Instance.HoldApplicant);
	}

	public void CloseApplicantDetail(){
		GameObject Hiring = HiringField.transform.Find("Detail").gameObject;
		Hiring.SetActive(false);
	}


}
