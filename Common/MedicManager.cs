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
	public MedicClass slot1;
	public MedicClass slot2;
	public MedicClass slot3;
	public MedicClass slot4;
	public MedicClass slot5;
	public MedicClass slot6;

	void Awake(){
		slot1 = new MedicClass();
		slot2 = new MedicClass();
		slot3 = new MedicClass();

	}


	public void SendMedic(HeroStatusClass hero, int time){
		if(slot1.ActiveFlag == false){
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
		}
	}

	public bool IsAnyoneThere(){
		bool rtn = false;
		if(slot1.ActiveFlag == true || slot2.ActiveFlag == true || slot3.ActiveFlag == true){
			rtn = true;
		}
		return rtn;
	}

	private int CalculateTime(){
		int a = 10;
		return a;
	}

	private IEnumerator MedicProgless (HeroStatusClass hc, MedicClass slot) {
		slot.ActiveFlag = true;
		slot.Time = CalculateTime();
		while (slot.Time > 0){
			yield return new WaitForSeconds(1);
			slot.Time--;
		}
		hc.Status = 0;
	}

}
