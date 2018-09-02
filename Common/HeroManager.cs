using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class HeroManager : SingletonMonoBehaviourFast<HeroManager> {

	public TextAsset CostumeData;
	public XmlDocument xmlDoc;

	public TextAsset PersonalityData;
	public XmlDocument xmlDoc2;

	public List<HeroStatusClass> HeroList;

	public GameObject prefabHero;


	void Awake () {
		HeroList = new List<HeroStatusClass>();


		for(int i = 1; i <= 6; i++){
			if(PlayerPrefs.GetString ("Hero" + i.ToString() + ".Name") != ""){
				HeroList.Add(LoadHero("Hero" + i.ToString()));
				instantiateHeroes(HeroList[i-1], i);
			}
		}

		xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(CostumeData.text);

		xmlDoc2 = new XmlDocument();
		xmlDoc2.LoadXml(PersonalityData.text);

	}

	void instantiateHeroes (HeroStatusClass heroClass, int n) {
		int a = Random.Range(1, 2);

		GameObject heroObj = Instantiate(prefabHero, new Vector3(n, 0, a), Quaternion.identity);
		heroObj.transform.parent = GameObject.Find("Characters").transform;
		heroObj.AddComponent<HeroStatus>();
		heroObj.GetComponent<HeroStatus>().SetValue(heroClass);
	}

	public void addHero (RecruitClass test) {
		HeroStatusClass Hero = test.CloneHero();
		if(HeroList.Count <= 5){
			HeroList.Add(Hero);
			Debug.Log("Adding Hero is Success");

		} else {
			Debug.LogError("Error : Over Capacity of heroes");
		}
		SaveHero();
	}

	void deleteHero (string HeroName) {
	}

	HeroStatusClass LoadHero (string heronumber) {
		HeroStatusClass Hero = new HeroStatusClass();
		Hero.Name = PlayerPrefs.GetString (heronumber + ".Name");
		Hero.Gender = PlayerPrefs.GetString (heronumber + ".Gender");
		Hero.Skin = PlayerPrefs.GetInt (heronumber + ".Skin");
		Hero.Costume = PlayerPrefs.GetString (heronumber + ".Costume");
		Hero.CostumeLv = PlayerPrefs.GetInt (heronumber + ".CostumeLv");
		Hero.Heroism = PlayerPrefs.GetInt (heronumber + ".Heroism");
		Hero.Status = PlayerPrefs.GetInt (heronumber + ".Status");
		return Hero;
	}

	void SaveHero () {
		for(int i = 1; i <= HeroList.Count - 1; i++){

					PlayerPrefs.SetString("Hero" + i.ToString() + ".Name", HeroList[i].Name);
					PlayerPrefs.SetString("Hero" + i.ToString() + ".Gender", HeroList[i].Gender);
					PlayerPrefs.SetInt("Hero" + i.ToString() + ".Skin", HeroList[i].Skin);
					PlayerPrefs.SetString("Hero" + i.ToString() + ".Costume", HeroList[i].Costume);
					PlayerPrefs.SetInt("Hero" + i.ToString() + ".CostumeLv", HeroList[i].CostumeLv);
					PlayerPrefs.SetInt("Hero" + i.ToString() + ".Heroism", HeroList[i].Heroism);
					PlayerPrefs.SetInt("Hero" + i.ToString() + ".Status", HeroList[i].Status);
					PlayerPrefs.Save ();

		}
	}

	void RemoveHero (string heronumber) {
		PlayerPrefs.DeleteKey(heronumber + ".Name");
	}

	public HeroStatusClass SearchByName (string HeroName){
		HeroStatusClass rtnHSC = null;
		foreach(HeroStatusClass HSC in HeroList){
			if(HeroName == HSC.Name){
				rtnHSC = HSC;
			}
		}
		/*if (HeroName == Hero1.Name) rtnHSC = Hero1;
		if (HeroName == Hero2.Name) rtnHSC = Hero2;
		if (HeroName == Hero3.Name) rtnHSC = Hero3;
		if (HeroName == Hero4.Name) rtnHSC = Hero4;
		if (HeroName == Hero5.Name) rtnHSC = Hero5;
		if (Hero6 != null && HeroName == Hero6.Name) rtnHSC = Hero6;*/
		return rtnHSC;
	}

	public List<string> listReadyHeroName() {
		List<string> rtnList = new List<string>();

		foreach(HeroStatusClass HSC in HeroList){
			if(HSC.Status == 0){
				rtnList.Add(HSC.Name);
			}
		}

		return rtnList;
	}

	public List<string> wholeUI() {
		List<string> rtnList = new List<string>();

		foreach(HeroStatusClass HSC in HeroList){
			rtnList.Add(HSC.Name + " - Status : " + convertStatus(HSC.Status));
		}

		return rtnList;
	}


	private string convertStatus (int herostatus){
		string retstr = "";
		switch(herostatus) {
			case 0:
				retstr = "Assignable";
				break;
			case 1:
				retstr = "Wounded";
				break;
			case 2:
				retstr = "Recover";
				break;
			default :
				break;
		}
		return retstr;
	}

	public List<string> MedicUI() {
		List<string> rtnList = new List<string>();

		foreach(HeroStatusClass HSC in HeroList){
			if(HSC.Status == 1){
				rtnList.Add(HSC.Name);
			}
		}

		return rtnList;
	}

	public void SetCostume(HeroStatusClass heroclass ,string test){
		heroclass.Costume = test;
	}

	public void SetParamsByCostume(HeroStatusClass heroclass){
		int cos_hlt;
		float cos_atk;
		float cos_def;

		if(xmlDoc.SelectSingleNode(@"//" + heroclass.Costume + "/HP") == null){

			heroclass.Health = 100;
			heroclass.MaxHealth = 100;
			heroclass.Atk = 10f;
			heroclass.Def = 10f;

		} else {

			XmlNode node0 = xmlDoc.SelectSingleNode(@"//" + heroclass.Costume + "/HP");
			XmlNode node1 = xmlDoc.SelectSingleNode(@"//" + heroclass.Costume + "/ATK");
			XmlNode node2 = xmlDoc.SelectSingleNode(@"//" + heroclass.Costume + "/DEF");

			cos_hlt = int.Parse(node0.InnerText);
			cos_atk = float.Parse(node1.InnerText);
			cos_def = float.Parse(node2.InnerText);

			float lv = (float)heroclass.CostumeLv;
			heroclass.Health = cos_hlt + (heroclass.CostumeLv * 10);
			heroclass.MaxHealth = cos_hlt + (heroclass.CostumeLv * 10);
			heroclass.Atk = cos_atk + (lv * 5f);
			heroclass.Def = cos_def + (lv * 5f);
		}

	}


}
