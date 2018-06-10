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

	public List<ApplicantClass> RecruitHeroList;
	public ApplicantClass HoldApplicant;

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

				RefleshRecruitList();
	}

	void instantiateHeroes (HeroStatusClass heroClass, int n) {
		int a = Random.Range(1, 2);

		GameObject heroObj = Instantiate(prefabHero, new Vector3(n, 0, a), Quaternion.identity);
		heroObj.transform.parent = GameObject.Find("Characters").transform;
		heroObj.AddComponent<HeroStatus>();
		heroObj.GetComponent<HeroStatus>().SetValue(heroClass);
	}

	public void addHero (ApplicantClass test) {
		HeroStatusClass Hero = test;
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

		/*if (Hero1 != null && Hero1.Status == 0){
			rtnList.Add(Hero1.Name);
		}
		if (Hero2 != null && Hero2.Status == 0){
			rtnList.Add(Hero2.Name);
		}
		if (Hero3 != null && Hero3.Status == 0){
			rtnList.Add(Hero3.Name);
		}
		if (Hero4 != null && Hero4.Status == 0){
			rtnList.Add(Hero4.Name);
		}
		if (Hero5 != null && Hero5.Status == 0){
			rtnList.Add(Hero5.Name);
		}
		if (Hero6 != null && Hero6.Status == 0){
			rtnList.Add(Hero6.Name);
		}*/

		return rtnList;
	}

	public List<string> wholeUI() {
		List<string> rtnList = new List<string>();

		foreach(HeroStatusClass HSC in HeroList){
			rtnList.Add(HSC.Name + " - Status : " + convertStatus(HSC.Status));
		}

		Debug.Log("listcount: " + rtnList.Count);

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

		/*if (Hero1 != null && Hero1.Status == 1){
			rtnList.Add(Hero1.Name);
		}
		if (Hero2 != null && Hero2.Status == 1){
			rtnList.Add(Hero2.Name);
		}
		if (Hero3 != null && Hero3.Status == 1){
			rtnList.Add(Hero3.Name);
		}
		if (Hero4 != null && Hero4.Status == 1) rtnList.Add(Hero4.Name);
		if (Hero5 != null && Hero5.Status == 1) rtnList.Add(Hero5.Name);
		if (Hero6 != null && Hero6.Status == 1) rtnList.Add(Hero6.Name);*/

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

	public void RefleshRecruitList(){
		//Debug.Log(test[0] + " : " + test[1]);
		RecruitHeroList = new List<ApplicantClass>();
		var heroList = new List<string>(){"Peter", "Banner", "Gwen", "Tony", "Sheldon", "Takahiro", "Sayaka"};
		List<int> a = RandomIntList(0,6,4);
		//string prsnl1 = "likes godzilla";
		//string prsnl2 = "80's Movie fan";
		for(int i = 1; i <= 4; i++){

			ApplicantClass hero = new ApplicantClass();
			hero.Name = heroList[ a[i-1] ];
			hero.Heroism = Random.Range(15, 21)*5;
			hero.Motivation = Random.Range(15, 21)*5;
			hero.Message = "HERO? I JUST WANT TO BE IT. \nWANT TO DEFEND MYSELF FROM KICKASS.";
			List<string> prsnl = SetPersonality();
			hero.Personality1 = prsnl[0];
			hero.Personality2 = prsnl[1];
			RecruitHeroList.Add(hero);
		}
	}

	private List<string> SetPersonality () {
		List<string> returnList = new List<string>();

		Debug.Log(xmlDoc2.SelectSingleNode(@"//" + "category[@id" + "1" + "]/node[@id" + "1" + "]"));
//@"//mission[@id=" + missionNo + "]/name"
		var personality1 = new List<string>(){
			"like 90's hip-hop",
			"like post apocalyptic movies",
			"like playing FPS",
			"NOEL GALLAGHER FREAK"
		};

		var personality2 = new List<string>(){
			"born in poor family",
			"high school student",
			"HIGH SCHOOL TEACHER",
			"have two girl-friends"
		};

		returnList.Add(personality1[Random.Range(0, 4)]);
		returnList.Add(personality2[Random.Range(0, 4)]);

		return returnList;
	}


	private List<int> RandomIntList(int min, int max, int count) {
		List<int> returnInt = new List<int>();
		List<int> numbers = new List<int>();

		for (int i = min; i <= max; i++) {
				numbers.Add(i);
		}
		while (count-- > 0) {
				int index = Random.Range(0, numbers.Count);
				int ransu = numbers[index];
				returnInt.Add(ransu);
				numbers.RemoveAt(index);
		}
		return returnInt;
	}

}
