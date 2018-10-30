using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FreeRoamClass : MissionClass {
public class FreeRoamClass : BaseMissionClass {
	

	//private HeroStatusClass appliedHero;
	//private List<MissionPhase> phaseList;
	private int cashReward;
	private List<MissionPhase> phaseListHistory;
	private bool isBackFlag;
	private string missionResourceType;
	private int missionResourceValue;
	private Dictionary<string, int> holdResources;


	/*
	public HeroStatusClass AppliedHero{
		get {return appliedHero;}
		set {appliedHero = value;}
	}

	public List<MissionPhase> PhaseList{
		get {return phaseList;}
		set {phaseList = value;}
	}
	*/

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

	public Dictionary<string, int> HoldResources{
		get {return holdResources;}
		set {holdResources = value;}
	}


}
