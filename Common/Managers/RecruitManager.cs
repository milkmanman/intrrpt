﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitManager : SingletonMonoBehaviourFast<RecruitManager> {

	public List<RecruitClass> RecruitHeroList;
	public List<RecruitClass> RecruitDevelopList;
	public List<RecruitClass> RecruitMedicList;

	public RecruitClass HoldRecruit;


	public void RefleshRecruitList(string type){

		if(type == "Hero"){
			RecruitHeroList = new List<RecruitClass>();
		} else if(type == "Develop") {
			RecruitDevelopList = new List<RecruitClass>();
		} else if(type == "Medic"){
			RecruitMedicList = new List<RecruitClass>();
		}
		var heroList = new List<string>(){"Peter", "Banner", "Gwen", "Tony", "Sheldon", "Takahiro", "Sayaka", "Hank", "Seth", "Jane"};
		List<int> a = RandomIntList(0,9,4);
		for(int i = 1; i <= 4; i++){

			RecruitClass hero = new RecruitClass();
			hero.Type = type;
			hero.Name = heroList[ a[i-1] ];
			hero.Gender = RandomGender();
			hero.Status1 = Random.Range(15, 21)*5;
			hero.Motivation = Random.Range(15, 21)*5;
			hero.Message = "HERO? I JUST WANT TO BE IT. \nWANT TO DEFEND MYSELF FROM KICKASS.";
			List<string> prsnl = SetPersonality();
			hero.Personality1 = prsnl[0];
			hero.Personality2 = prsnl[1];
			if(type == "Hero"){
				RecruitHeroList.Add(hero);
			} else if(type == "Develop"){
				RecruitDevelopList.Add(hero);
			} else if(type == "Medic"){
				RecruitMedicList.Add(hero);
			}
		}
	}

	private List<string> SetPersonality () {
		List<string> returnList = new List<string>();

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

	private string RandomGender(){
		string ret;
		int a = Random.Range(0, 1);
		if(a == 0){
			ret = "Male";
		} else {
			ret = "Female";
		}
		return ret;
	}

	public void AddMember(){
		RecruitClass sendrec = HoldRecruit;
		Debug.Log("RecruitManager : ADDMEMBER " + sendrec.Type);

		if(sendrec.Type == "Hero"){
			HeroManager.Instance.addHero(HoldRecruit);
		} else if(sendrec.Type == "Develop"){
			FacilityManager.Instance.addDevelopMember(HoldRecruit);
		} else if(sendrec.Type == "Medic"){
			MedicManager.Instance.addMedicMember(HoldRecruit);
			Debug.Log("So... MedicMember");
		}
	}


}
