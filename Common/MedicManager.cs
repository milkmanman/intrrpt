using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicManager : SingletonMonoBehaviourFast<MedicManager> {

	public int MedicPower;
	IEnumerator routine1;
	IEnumerator routine2;
	IEnumerator routine3;
	IEnumerator routine4;
	IEnumerator routine5;
	IEnumerator routine6;

	public List<IEnumerator> RoutineList;

	public MedicClass slot1;
	public MedicClass slot2;
	public MedicClass slot3;
	public MedicClass slot4;
	public MedicClass slot5;
	public MedicClass slot6;

	public List<MedicClass> MedicSlot;

	public List<MedicMemberClass> MedicMembers;


	void Awake(){

		LoadMedicMembers();
		Debug.Log("load medic : " + MedicMembers[0].Name);

		slot1 = new MedicClass();
		slot2 = new MedicClass();
		slot3 = new MedicClass();
		MedicSlot = new List<MedicClass>();
		RoutineList = new List<IEnumerator>();
	}


	public void SendMedic(MedicClass mc, GameObject node){
		

		//MedicProgless (MedicClass mc, GameObject Node)

		routine1 = MedicProgless(mc, node);
		StartCoroutine(routine1);
		MedicSlot.Add(mc);
		RoutineList.Add(routine1);


		/*if(slot1.ActiveFlag == false){
			slot1 = new MedicClass();
			routine1 = MedicProgless(hero, slot1);
			StartCoroutine(routine1);
		} else if(slot2.ActiveFlag == false){
			slot2 = new MedicClass();
			routine2 = MedicProgless(hero, slot2);
			StartCoroutine(routine2);
		} else if(slot3.ActiveFlag == false){
			slot3 = new MedicClass();
			routine3 = MedicProgless(hero, slot3);
			StartCoroutine(routine3);
		}*/
	}

	public bool IsAnyoneThere(){
		bool rtn = false;
		for(int i = 0; i < MedicSlot.Count; i++) {
			if(MedicSlot[i].ActiveFlag == true){
				rtn = true;
			}
		}
		/*if(slot1.ActiveFlag == true || slot2.ActiveFlag == true || slot3.ActiveFlag == true){
			rtn = true;
		}*/
		return rtn;
	}

	private int CalculateTime(){
		int a = 10;
		return a;
	}

	/*private IEnumerator MedicProgless (HeroStatusClass hc, MedicClass slot) {
		slot.ActiveFlag = true;
		slot.Time = CalculateTime();
		while (slot.Time > 0){
			yield return new WaitForSeconds(1);
			slot.Time--;
		}
		hc.Status = 0;
	}*/

	private IEnumerator MedicProgless (MedicClass mc, GameObject Node) {
		mc.ActiveFlag = true;
		mc.Time = CalculateTime();
		while (mc.Time > 0){
			Node.GetComponent<MedicNode>().Refresh(mc);
			yield return new WaitForSeconds(1);
			mc.Time--;
		}
		mc.ActiveFlag = false;
		mc.hsc.Status = 0;
		Debug.Log("Complete Medic");
	}


	public void addMedicMember (RecruitClass test) {
		MedicMemberClass MedMember = test.CloneMedic();
		if(MedicMembers.Count <= 5){
			MedicMembers.Add(MedMember);
			Debug.Log("Adding Hero is Success");

		} else {
			Debug.LogError("Error : Over Capacity of heroes");
		}
		SaveMedicMembers ();
	}


	public List<MedicMemberClass> LoadMedicMembers () {
		MedicMembers = new List<MedicMemberClass>();
		for(int i = 1; i <= 6; i++){
			if(PlayerPrefs.GetString ("Medic" + i.ToString() + ".Name") != ""){
				MedicMemberClass Member = new MedicMemberClass();
				Member.Name = PlayerPrefs.GetString ("Medic" + i.ToString() + ".Name");
				Member.Gender = PlayerPrefs.GetString ("Medic" + i.ToString() + ".Gender");
				Member.Skin = PlayerPrefs.GetInt ("Medic" + i.ToString() + ".Skin");
				Member.SkillExp = PlayerPrefs.GetInt ("Medic" + i.ToString() + ".SkillExp");
				Member.Motivation = PlayerPrefs.GetInt ("Medic" + i.ToString() + ".Motivation");
				MedicMembers.Add(Member);
			}
		}
		return MedicMembers;
	}

	public void SaveMedicMembers () {
		for(int i = 1; i <= MedicMembers.Count; i++){
					PlayerPrefs.SetString("Medic" + i.ToString() + ".Name", MedicMembers[i-1].Name);
					PlayerPrefs.SetString("Medic" + i.ToString() + ".Gender", MedicMembers[i-1].Gender);
					PlayerPrefs.SetInt("Medic" + i.ToString() + ".Skin", MedicMembers[i-1].Skin);
					PlayerPrefs.SetInt("Medic" + i.ToString() + ".SkillExp", MedicMembers[i-1].SkillExp);
					PlayerPrefs.SetInt("Medic" + i.ToString() + ".Motivation", MedicMembers[i-1].Motivation);
					PlayerPrefs.Save ();
		}
	}

	public int Productivity {
		get {
			int total = 0;

			for(int i = 1; i <= MedicMembers.Count; i++){
				total += MedicMembers[i-1].Productivity;
			}

			return total;
		} 

	}

}
