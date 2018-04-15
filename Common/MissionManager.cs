using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class MissionManager : SingletonMonoBehaviourFast<MissionManager> {

	IEnumerator routine1;
	IEnumerator routine2;
	IEnumerator routine3;
	IEnumerator routine4;
	IEnumerator routine5;
	IEnumerator routine6;
	public MissionClass slot1;
	public MissionClass slot2;
	public MissionClass slot3;
	public MissionClass slot4;
	public MissionClass slot5;
	public MissionClass slot6;

	void Awake () {
		slot1 = new MissionClass();
		slot2 = new MissionClass();
		slot3 = new MissionClass();
		slot4 = new MissionClass();
		slot5 = new MissionClass();
		slot6 = new MissionClass();

	//example
		MissionClass testmission = new MissionClass();
		testmission.MissionNo = 2;
		testmission.Route = new List<int> (){11, 21};
		testmission.Finished = false;
		SaveSADMissionProgress(testmission); //save

		//MissionClass loadedMission = LoadSADMissionProgress(16);
		//Debug.Log("loadedMissionlist : " + loadedMission.Route[1] + loadedMission.Route[2]);
		//Debug.Log("loadedMissionFinished : " +loadedMission.Finished); //load

		//DeleteSADMissionProgress(16); //delete

	}

	public void SaveSADMissionProgress(MissionClass mc) {
		SaveList<int>("Mission." + mc.MissionNo.ToString() + ".Phase", mc.Route);
		PlayerPrefs.SetInt("Mission." + mc.MissionNo.ToString() + ".Finished",SaveBool(mc.Finished));
		PlayerPrefs.Save ();
	}

	public MissionClass LoadSADMissionProgress(MissionClass rtn ,int MissionNo) {
		rtn.MissionNo = MissionNo;
		rtn.Route = LoadList<int>("Mission." + MissionNo.ToString() + ".Phase");
		rtn.Finished = LoadBool("Mission." + MissionNo.ToString() + ".Finished");
		return rtn;
	}

	public void DeleteSADMissionProgress(int MissionNo){
		PlayerPrefs.DeleteKey("Mission." + MissionNo.ToString() + ".Phase");
		PlayerPrefs.DeleteKey("Mission." + MissionNo.ToString() + ".Finished");
	}


	public void StartMission(HeroStatusClass hc, MissionClass missioncls){
		missioncls.Time = CalculateTime();
		if(slot1.ActiveFlg == false) {
			slot1 = missioncls;
			slot1.AppliedHero = hc;
			routine1 = MissionProgless(hc, slot1);
			StartCoroutine(routine1);
		} else if (slot2.ActiveFlg == false) {
			slot2 = missioncls;
			slot2.AppliedHero = hc;
			routine2 = MissionProgless(hc, slot2);
			StartCoroutine(routine2);
		} else if (slot3.ActiveFlg == false) {
			slot3 = missioncls;
			routine3 = MissionProgless(hc, slot3);
			StartCoroutine(routine3);
		} else if (slot4.ActiveFlg == false) {
			slot4 = missioncls;
			routine4 = MissionProgless(hc, slot4);
			StartCoroutine(routine4);
		} else if (slot5.ActiveFlg == false) {
			slot5 = missioncls;
			routine5 = MissionProgless(hc, slot5);
			StartCoroutine(routine5);
		} else if (slot6.ActiveFlg == false) {
			slot6 = missioncls;
			routine6 = MissionProgless(hc, slot6);
			StartCoroutine(routine6);
		}
	}

	private int CalculateTime(){
		int a = 10;
		return a;
	}

	private IEnumerator MissionProgless (HeroStatusClass hc, MissionClass missioncls) {

		Debug.Log("villian list : " + missioncls.VillainList.Count);
		//hc.Health = 300;
		//hc.Atk = 20f;
		//hc.Def = 0f;
		missioncls.ActiveFlg = true;
		missioncls.Success = false;
		missioncls.RemainVillains = missioncls.VillainList.Count;

		for(int i = 0; i <= missioncls.VillainList.Count - 1; i++){
			yield return new WaitForSeconds(1);
			if(hc.Health > 0){
				while(missioncls.VillainList[i].Health > 0 && hc.Health > 0){
					int a = UnityEngine.Random.Range(0,2);
					string log = Attack(a, hc, missioncls.VillainList[i]);
					PrintBattlelog(missioncls, log);
					yield return new WaitForSeconds(1);
				}
				if(hc.Health > 0){
					missioncls.RemainVillains = missioncls.VillainList.Count - (i + 1);
					Debug.Log("remainvillain : " + missioncls.RemainVillains);
					if(missioncls.VillainList.Count - (i + 1) != 0){
						PrintBattlelog(missioncls, "!! LEFT " + (missioncls.VillainList.Count - (i + 1)).ToString() + "VILLANS !!");
					} else {
						missioncls.RemainVillains = 0;
						PrintBattlelog(missioncls, "!! ALL VILLAINS REMOVED !!");
						missioncls.Success = true;
					}
				} else {
					PrintBattlelog(missioncls, "!! HERO DOWN !!");
				}
			}
		}
		missioncls.ActiveFlg = false;
	}

	private string Attack(int a, HeroStatusClass hero, VillainStatusClass villain){
		string ret = "";
		if(a == 0){
			int damageByVillain = (int)(villain.Atk * ((100 - hero.Def)/ 100) );
			hero.Health = hero.Health - damageByVillain;
			if(hero.Health > 0){
				ret = "Villain attack! Hero's health remain : " + hero.Health.ToString();
			} else {
				ret = "Villain attack! Hero down!";
			}
		} else {
			int damageByVillain = (int)(hero.Atk * ((100 - villain.Def)/ 100) );
			villain.Health = villain.Health - damageByVillain;
			if(villain.Health > 0){
				ret = "Hero attack! villain's health remain : " + villain.Health.ToString();
			} else {
				ret = "Hero attack! Villain down!";
			}
		}
		return ret;
	}

	private void PrintBattlelog(MissionClass missioncls, string log){
		if(log.Contains("Villain")){
			log = log.Replace("Villain", "<color=#ff0000>Villain</color>");
		} else if (log.Contains("Hero")) {
			log = log.Replace("Hero", "<color=#0000ff>Hero</color>");
		}
		missioncls.CombatLog = missioncls.CombatLog + log + "\n";
		Debug.Log(log);
	}




	public static void SaveList<T>(string key , List<T> value){
//		string serizlizedList = key;
			string serizlizedList = Serialize<List<T>> (value);
			PlayerPrefs.SetString (key, serizlizedList);
	}

	private int SaveBool(bool intbool){
		int a = 0;
		if(intbool == true)a = 1;
		return a;
	}

	private bool LoadBool(string index){
		bool a = false;
		int b = PlayerPrefs.GetInt(index);
		if(b == 1)a = true;
		return a;
	}

	public static List<T> LoadList<T> (string key){
			//keyがある時だけ読み込む
			if (PlayerPrefs.HasKey (key)) {
					string serizlizedList = PlayerPrefs.GetString (key);
//					return Deserialize<List<T>> (serizlizedList);
					return Deserialize<List<T>> (serizlizedList);

			}

			return new List<T> ();
	}

	private static string Serialize<T> (T obj){
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			MemoryStream    memoryStream    = new MemoryStream ();
			binaryFormatter.Serialize (memoryStream , obj);
			return Convert.ToBase64String (memoryStream   .GetBuffer ());
	}

	private static T Deserialize<T> (string str){
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			MemoryStream    memoryStream    = new MemoryStream (Convert.FromBase64String (str));
			return (T)binaryFormatter.Deserialize (memoryStream);
	}

}
