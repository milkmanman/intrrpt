﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class FacilityManager : SingletonMonoBehaviourFast<FacilityManager> {

	private int dronePhase;
	private int jumpingDronePhase;
	private int monitorPhase;
	private int rescueCar;
	public Vector3 deskpos;
	public TextAsset FacilityDatabase;
	public XmlDocument xmlDoc;

	//public int FlashGranadeCount;

	public List<DevelopMemberClass> DevelopMembers;
	public List<FacilityClass> DevedFacilityList;
	public List<FacilityClass> FacilityList;
	public List<Facility> FacilityObject;
	public List<IEnumerator> routineList;

	public Dictionary<string, int> FacilityPhases;
	public Dictionary<string, bool> EnableBuff;
	public Dictionary<string, int> BuffSlot;

	//public int FlashGranade;


	void Awake(){
		//tmp setting
		BuffSlot = new Dictionary<string, int>();
		BuffSlot.Add("flashGranade", 0);

		InstantiateFacility();
		DevelopMembers = LoadDevelopMembers();
		routineList = new List<IEnumerator>();

		xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(FacilityDatabase.text);

		/*DevelopMemberClass dmc = new DevelopMemberClass();
		dmc.Name = "Shuri";
		dmc.Gender = "Female";
		dmc.Skin = 10;
		dmc.SkillLv = 10;
		dmc.SkillExp = 1000;
		dmc.Motivation = 100;
		Debug.Log("productivity test : " + dmc.Productivity);*/
		//DevelopMembers.Add(dmc);
		//SaveDevelopMembers();

		FacilityPhases = new Dictionary<string, int>();
		FacilityPhases.Add("drone", 1);
		FacilityPhases.Add("jumpingdrone", 0);

		FacilityList = new List<FacilityClass>();
		Dictionary<string, int> DevelopPhases = FacilityPhases;
		foreach(KeyValuePair<string, int> item in DevelopPhases) {
			string tmpxmldir = "//" + item.Key.ToString() + "[@id=" + item.Value +"]";
			string iconPass = item.Key.ToString() + "_" + item.Value;
			FacilityClass fclty = setFacilityClass(tmpxmldir);
			fclty.IconPass = iconPass;
			FacilityList.Add(fclty);
		}

		DevedFacilityList = new List<FacilityClass>();
		foreach(KeyValuePair<string, int> item in DevelopPhases) {
			string tmpxmldir = "//" + item.Key.ToString() + "[@id=" + (item.Value - 1) +"]";
			string iconPass = item.Key.ToString() + "_" + item.Value;
			FacilityClass fclty = setFacilityClass(tmpxmldir);
			fclty.IconPass = iconPass;
			DevedFacilityList.Add(fclty);
		}

		EnableBuff = new Dictionary<string, bool>();
		EnableBuff.Add("flashGranade", true);
		SetOnetimeBuffList(EnableBuff);


		//drone[@id=0]

		Debug.Log("Productivity : " + Productivity.ToString());

	}

	/*public void SetFacilityList(Dictionary<string, int> test){
		foreach(KeyValuePair<string, bool> pair in test){
			if(pair.Value == true){
				Debug.Log (pair.Key + " " + pair.Value);
				string onetime = pair.Key; //instant for flash granade
				string onetimedir = "//" + onetime;
				FacilityClass onetimeFc = setFacilityClass(onetimedir);
				onetimeFc.IconPass =  onetime;
				FacilityList.Add(onetimeFc);
			}
		}

	}*/

	public void SetOnetimeBuffList(Dictionary<string, bool> test){
		foreach(KeyValuePair<string, bool> pair in test){
			if(pair.Value == true){
				Debug.Log (pair.Key + " " + pair.Value);
				string onetime = pair.Key; //instant for flash granade
				string onetimedir = "//" + onetime;
				FacilityClass onetimeFc = setFacilityClass(onetimedir);
				onetimeFc.IconPass =  onetime;
				FacilityList.Add(onetimeFc);
			}
		}

	}



	public List<FacilityClass> SetDevedFacilityList(Dictionary<string, int> dic){
		List<FacilityClass> retList = new List<FacilityClass>();

		foreach(KeyValuePair<string, int> item in dic) {
			string tmpxmldir = "//" + item.Key.ToString() + "[@id=" + (item.Value - 1) +"]";
			if((item.Value - 1) == 0){ // excluse phase 0
				Debug.Log("DEVEDEVED : " + tmpxmldir);
				string iconPass = item.Key.ToString() + "_" + item.Value;
				FacilityClass fclty = setFacilityClass(tmpxmldir);
				fclty.IconPass = iconPass;
				retList.Add(fclty);
			}
		}
		return retList;
	}

	public FacilityClass setFacilityClass(string xmldir){
		FacilityClass returnFacility = new FacilityClass();

		XmlNode node0 = xmlDoc.SelectSingleNode(@"" + xmldir + "/name");
		if(node0 != null){
			returnFacility.Name = node0.InnerText;
			XmlNode node1 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost1");
			returnFacility.Cost1Type = node1.InnerText;
			XmlNode node2 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost1type");
			returnFacility.Cost1Value = int.Parse(node2.InnerText);
			XmlNode node3 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost2");
			if(node3 != null){
				returnFacility.Cost2Type = node3.InnerText;
				XmlNode node4 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost2type");
				returnFacility.Cost2Value = int.Parse(node4.InnerText);
			}
			XmlNode node5 = xmlDoc.SelectSingleNode(@"" + xmldir + "/description");
			returnFacility.Description = node5.InnerText;
			XmlNode node6 = xmlDoc.SelectSingleNode(@"" + xmldir + "/time");
			returnFacility.Time = int.Parse(node6.InnerText);
			XmlNode node7 = xmlDoc.SelectSingleNode(@"" + xmldir + "/time");
			returnFacility.RemainTime = int.Parse(node7.InnerText);
			XmlNode node8 = xmlDoc.SelectSingleNode(@"" + xmldir + "/type");
			returnFacility.Type = node8.InnerText;
		}

		return returnFacility;
	}


	private void InstantiateFacility(){
		var go = new GameObject();
		go.name = "Drone1";

		Facility test = FacilityObject[0];
		int facilityLength = test.FacilityObject.Count;

		for(int i = 0; i <= facilityLength - 1; i++){
			GameObject test_prefab = (GameObject)Instantiate(
				test.FacilityObject[i].obj,
				test.FacilityObject[i].pos,
				Quaternion.Euler(test.FacilityObject[i].eul)
			);
			test_prefab.name = "Drone_" + i.ToString();
			test_prefab.transform.SetParent(go.transform);
		}
	}

	public void addDevelopMember (RecruitClass test) {
		DevelopMemberClass DevMember = test.CloneDevelop();
		if(DevelopMembers.Count <= 5){
			DevelopMembers.Add(DevMember);
			Debug.Log("Adding Hero is Success");

		} else {
			Debug.LogError("Error : Over Capacity of heroes");
		}
		SaveDevelopMembers ();
	}

	List<DevelopMemberClass> LoadDevelopMembers () {
		DevelopMembers = new List<DevelopMemberClass>();
		for(int i = 1; i <= 6; i++){
			if(PlayerPrefs.GetString ("Develop" + i.ToString() + ".Name") != ""){
				DevelopMemberClass Member = new DevelopMemberClass();
				Member.Name = PlayerPrefs.GetString ("Develop" + i.ToString() + ".Name");
				Member.Gender = PlayerPrefs.GetString ("Develop" + i.ToString() + ".Gender");
				Member.Skin = PlayerPrefs.GetInt ("Develop" + i.ToString() + ".Skin");
				//Member.SkillLv = PlayerPrefs.GetInt ("Develop" + i.ToString() + ".SkillLv");
				Member.SkillExp = PlayerPrefs.GetInt ("Develop" + i.ToString() + ".SkillExp");
				Member.Motivation = PlayerPrefs.GetInt ("Develop" + i.ToString() + ".Motivation");
				DevelopMembers.Add(Member);
			}
		}
		return DevelopMembers;
	}

	public void SaveDevelopMembers () {
		for(int i = 1; i <= DevelopMembers.Count; i++){
					PlayerPrefs.SetString("Develop" + i.ToString() + ".Name", DevelopMembers[i-1].Name);
					PlayerPrefs.SetString("Develop" + i.ToString() + ".Gender", DevelopMembers[i-1].Gender);
					PlayerPrefs.SetInt("Develop" + i.ToString() + ".Skin", DevelopMembers[i-1].Skin);
					PlayerPrefs.SetInt("Develop" + i.ToString() + ".SkillLv", DevelopMembers[i-1].SkillLv);
					PlayerPrefs.SetInt("Develop" + i.ToString() + ".SkillExp", DevelopMembers[i-1].SkillExp);
					PlayerPrefs.SetInt("Develop" + i.ToString() + ".Motivation", DevelopMembers[i-1].Motivation);
					PlayerPrefs.Save ();
		}
	}

	public int MemberListActiveCount(){
		int returnCount = 0;
		for(int i = 1; i <= DevelopMembers.Count; i++){
			if(DevelopMembers[i-1].Name != null) returnCount++;
		}
		return returnCount;
	}

	public void SpendResource(FacilityClass fc){
		ResourceManager.Instance.DecreaseResource(fc.Cost1Type, fc.Cost1Value);
		if(fc.Cost2Type != null){
			ResourceManager.Instance.DecreaseResource(fc.Cost2Type, fc.Cost2Value);
		}
	}


	public void StartDevelop(FacilityClass fc, GameObject Node){
				FacilityClass cloneFc = new FacilityClass();
				cloneFc = fc.Clone();
				IEnumerator misRoutine = DevelopProgless(cloneFc, Node);
				StartCoroutine(misRoutine);
				routineList.Add(misRoutine);

	}

	private IEnumerator DevelopProgless (FacilityClass fc, GameObject Node){

		while(fc.RemainTime >= 0){
			Node.GetComponent<DevelopWIPNode>().Refresh(fc);
			yield return new WaitForSeconds(1f);
			fc.RemainTime--;
		}

		Node.GetComponent<DevelopWIPNode>().Complete(fc);
		Debug.Log("Complete Development!");
	}

	public void CompleteDevelopment (FacilityClass fc){
		if(fc.Name == "Flash Granade"){
			Debug.Log("Develop Flash Granade");
			IncreaseBuff("flashGranade", 1);

		}
	}


	public int Productivity {
		get {
			int total = 0;

			for(int i = 1; i <= DevelopMembers.Count; i++){
				total += DevelopMembers[i-1].Productivity;
			}

			return total;
		} 

	}


	public List<string> wholeUI() {
		List<string> rtnList = new List<string>();

		foreach(DevelopMemberClass HSC in DevelopMembers){
			rtnList.Add(HSC.Name + " - prdct : " + HSC.Productivity.ToString());
		}

		return rtnList;
	}

	public void IncreaseBuff(string type, int num){

		List<string> keyList = new List<string>(BuffSlot.Keys);

		foreach(string key in keyList){
			if(key == type){
			BuffSlot[key] += num;
			}
		}
	}

	public void DecreaseBuff(string type, int num){
		foreach(KeyValuePair<string, int> item in BuffSlot) {
			if(item.Key == type){
				BuffSlot[item.Key] = item.Value - num;
			}
		}
	}

}
