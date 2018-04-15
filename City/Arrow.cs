using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {

	private ShortMissionClass shortmission;

	public bool missionstart = false;
//	public bool mssnflg = false;
	public bool missioncomplete = false;
	public float endtime;

	private Text timeUI;
	public string holdDisplaytime;


	void Awake() {
		shortmission = new ShortMissionClass();
		shortmission.ShortMissionGenerator();
		StartCoroutine("DestroyThis");

	}

/*	void Update() {
		if (mssnflg == true){
			float counttime = Time.realtimeSinceStartup;
			float times = endtime - counttime;
			times = Mathf.Max(times, 0);
			string displaytime = FloatToTime(times, "#0:00");
			if (holdDisplaytime != displaytime) timeUI.text = displaytime;
			holdDisplaytime = displaytime;
		}
	}*/

	public void GetClicked(){
		if (missioncomplete == false){
			GameObject.Find("GUI").GetComponent<UIActivatorCity>().Activator(1);
			ShortMissionUI shormissionui = GameObject.Find("ShortMissionUI").GetComponent<ShortMissionUI>();
			shormissionui.SMRefresh(shortmission);
		} else {
			GameObject.Find("GUI").GetComponent<UIActivatorCity>().Activator(2);
		}
	}

	public void MissionProgress () {
		int missiontime = shortmission.RequiredTime;
		timeUI = this.gameObject.transform.Find("MissionProgress/Canvas/Time").gameObject.GetComponent<Text>();

		float starttime = Time.realtimeSinceStartup;
		endtime = Time.realtimeSinceStartup + (float)missiontime;
		Debug.Log("starttime : " + starttime + "endtime : " + endtime);
//		mssnflg = true;
		StartCoroutine("ShortMission", missiontime);
	}

	IEnumerator ShortMission (int a) {
		while(a >= 0){
			float counttime = Time.realtimeSinceStartup;
			float times = endtime - counttime;
			times = Mathf.Max(times, 0);
			string displaytime = FloatToTime(times, "#0:00");
			if (holdDisplaytime != displaytime) timeUI.text = displaytime;
			holdDisplaytime = displaytime;

			yield return new WaitForSeconds(1);
			a--;
		}
//		yield return new WaitForSeconds(a);
		Debug.Log("MISSSION COMPLETE!");
		missioncomplete = true;
	}

	IEnumerator DestroyThis () {
			int randomtime = Random.Range (10,15);

			yield return new WaitForSeconds(randomtime);
			if (missionstart == true) { yield break; }
			GameObject.Find("SMInstantiate").GetComponent<SMInstantiate>().refresh();

			Destroy(this.gameObject);
	}

	private string FloatToTime (float toConvert, string format){
				switch (format){
					case "#0:00":
							return string.Format("{0:#0}:{1:00}",
									Mathf.Floor(toConvert / 60),//minutes
									Mathf.Floor(toConvert) % 60);//seconds
					break;
					case "#00:00":
							return string.Format("{0:#00}:{1:00}",
									Mathf.Floor(toConvert / 60),//minutes
									Mathf.Floor(toConvert) % 60);//seconds
					break;
			}
			return "error";
	}



}
