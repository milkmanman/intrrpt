using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortMissionClass {

	private int type;
	private string missionName;
	private int requiredTime;

	private int rewardType;
	private int rewardValue;

	private string reward;
	private string description;
	private string difficulty;

	public int Type{
		get {return type;}
		set {type = value;}
	}

	public string MissionName{
		get {return missionName;}
		set {missionName = value;}
	}

	public int RequiredTime{
		get {return requiredTime;}
		set {requiredTime = value;}
	}

	public string Reward{
		get {return reward;}
		set {reward = value;}
	}

	public int RewardType{
		get {return rewardType;}
		set {rewardType = value;}
	}

	public int RewardValue{
		get {return rewardValue;}
		set {rewardValue = value;}
	}

	public string Description{
		get {return description;}
		set {description = value;}
	}

	public string Difficulty{
		get {return difficulty;}
		set {difficulty = value;}
	}

	public void ShortMissionGenerator (){

		int mt = Random.Range (0, 5);
		string mn;

		switch (mt){
			case 0:
				mn = "kidnapping";
			break;

			case 1:
				mn = "bank robber";
			break;

			case 2:
				mn = "store robber";
			break;

			case 3:
				mn = "drug dealing";
			break;

			case 4:
				mn = "murder";
			break;

			default:
				mn = "error";
			break;
		}

		Type = mt;
		MissionName = mn;
		RequiredTime = TimeGenerator(mt);
		Reward = "Cash : 1000";
		RewardType = 0;
		RewardValue = 1000;
		Description = DescriptionGenerator(mt);
		Difficulty = "E";
	}

	private int TimeGenerator (int t) {

			int time_;
			int type;
			int zeroisminus = Random.Range (0, 2);
			if (zeroisminus == 0) zeroisminus = -1;
			int randomize = Random.Range (0, 4);

			if      (t == 0) {type = 4;}
			else if (t == 1) {type = 5;}
			else if (t == 2) {type = 3;}
			else if (t == 3) {type = 3;}
			else if (t == 4) {type = 4;}
			else {type = 0;}

			time_ = (10 * type) + (10 * randomize * zeroisminus);
			return time_;

	}

	private string DescriptionGenerator (int t) {

		string resultString;
		int pattern;

		pattern = Random.Range (0, 2);

		if (t == 0 || pattern == 0){
			resultString = "the girl was kidnapped";
		}else if (t == 0 ||  pattern == 1){
			resultString = "";
		}
		else {
			resultString = "boolstrings!!!";
		}

		return resultString;
	}

	public string MissionTypetoString() {
		string type;
		if      (RewardType == 0) {type = "Cash";}
		else if (RewardType == 1) {type = "Intel";}
		else if (RewardType == 2) {type = "Tech";}
		else if (RewardType == 3) {type = "Medic";}
		else if (RewardType == 4) {type = "Tailor";}
		else {type = "";}
		return type;
	}

	public void ShowonConsole(){
		Debug.Log("MissionType : " + Type  + "\nMissionName : "+ MissionName +"\nRequiredTime(seconds) : "+ RequiredTime +"\nRewards : "+ Reward);
	}
}
