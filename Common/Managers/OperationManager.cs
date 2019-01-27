using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OperationManager : SingletonMonoBehaviourFast<OperationManager> {

	public List<OperationMemberClass> OperationMembers;
	public List<ResearchClass> ResearchList;

	//public List<IEnumerator> routineList;

	public int TechInfo;
	public Action ChangeInfosAction;



	public int Tech;

	void Awake(){
		ResearchList = new List<ResearchClass>();
		//routineList = new List<IEnumerator>();
		OperationMembers = LoadOperationMembers();
		Debug.Log("operation name : " + OperationMembers[0].Name);
		LoadInfos();
	}

	List<OperationMemberClass> LoadOperationMembers () {
		OperationMembers = new List<OperationMemberClass>();
		for(int i = 1; i <= 6; i++){
			if(PlayerPrefs.GetString ("Operation" + i.ToString() + ".Name") != ""){
				OperationMemberClass Member = new OperationMemberClass();
				Member.Name = PlayerPrefs.GetString ("Operation" + i.ToString() + ".Name");
				Member.Gender = PlayerPrefs.GetString ("Operation" + i.ToString() + ".Gender");
				Member.Skin = PlayerPrefs.GetInt ("Operation" + i.ToString() + ".Skin");
				OperationMembers.Add(Member);
			}
		}
		return OperationMembers;
	}

	public void StartResearch(){

		ResearchClass initResearch = new ResearchClass();
		initResearch.Coroutine = ResearchProgless(initResearch);
		ResearchList.Add(initResearch);
		StartCoroutine(initResearch.Coroutine);

		//IEnumerator misRoutine = ResearchProgless();
		//StartCoroutine(misRoutine);
		//routineList.Add(misRoutine);

	}

	private IEnumerator ResearchProgless (ResearchClass initResearch){

		initResearch.is_active = true;	
		initResearch.Time = 10;
		initResearch.InfoType = "TechInfo";
		initResearch.InfoValue = 100;

		while(initResearch.Time > 0){
			yield return new WaitForSeconds(1f);
			initResearch.Time--;
		}
		initResearch.is_successed = true;
		initResearch.is_active = false;

		Debug.Log("ENDED");

	}

	public void LoadInfos(){
		if(PlayerPrefs.GetInt("Operation.TechInfo") != 0){
			TechInfo = PlayerPrefs.GetInt("Operation.TechInfo");
		} else {
			TechInfo = 0;
		}

	}

	public void IncreaseInfos(string InfoType, int value) {

		if (InfoType == "TechInfo") {
			TechInfo += value;
		}
		if(ChangeInfosAction != null){
			ChangeInfosAction();
		};
	}

	public void RemoveOneResearch(ResearchClass rc){
		ResearchList.Remove(rc);

	}

}
