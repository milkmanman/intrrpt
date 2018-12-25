using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FreeRoamClass : BaseMissionClass {
	

	private int cashReward;
	private List<MissionPhase> phaseListHistory;
	private bool isBackFlag;
	private string missionResourceType;
	private int missionResourceValue;
	private int missionExp;
	private Dictionary<string, int> holdResources;
	private int sumIncreaseExp;


	public int CashReward{
		get {return cashReward;}
		set {cashReward = value;}
	}

	public List<MissionPhase> PhaseListHistory{
		get {return phaseListHistory;}
		set {phaseListHistory = value;}
	}

	public bool IsBackFlag{
		get {return isBackFlag;}
		set {isBackFlag = value;}
	}

	public string MissionResourceType{
		get {return missionResourceType;}
		set {missionResourceType = value;}
	}

	public int MissionResourceValue{
		get {return missionResourceValue;}
		set {missionResourceValue = value;}
	}

	public int MissionExp{
		get {return missionExp;}
		set {missionExp = value;}
	}

	public Dictionary<string, int> HoldResources{
		get {return holdResources;}
		set {holdResources = value;}
	}

	public int SumIncreaseExp{
		get {return sumIncreaseExp;}
		set {sumIncreaseExp = value;}
	}

	public FreeRoamClass(){
		SumIncreaseExp = 0;
	}

}
