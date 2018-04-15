using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortMissionUI : MonoBehaviour {

	public static GameObject HoldArrow;
	public GameObject Name;
	public GameObject Description;
	public GameObject Time;
	public GameObject Difficulty;
	public GameObject RewardType;
	public GameObject RewardValue;

	public void SMRefresh (ShortMissionClass test){

		string _rt = minutesseconds(test.RequiredTime);

		Text nt = Name.GetComponent<Text>();
		nt.text = test.MissionName;
		Text dt = Description.GetComponent<Text>();
		dt.text = test.Description;
		Text timetext = Time.GetComponent<Text>();
		timetext.text = _rt;
	}

	public void ClickAccept(){
		HoldArrow.GetComponent<Arrow>().missionstart = true;
		HoldArrow.transform.Find("MissionProgress").gameObject.SetActive(true);
		HoldArrow.GetComponent<Arrow>().MissionProgress();
		GameObject.Find("GUI").GetComponent<UIActivatorCity>().Activator(0);
	}

	private string minutesseconds(int a){
		int minutes = a / 60;
		int seconds = a % 60;
		string rtn = minutes.ToString() + ":" + seconds.ToString();
		if(seconds == 0){
			rtn = minutes.ToString() + ":00";
		}
		return rtn;
	}

}
