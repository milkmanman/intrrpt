using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamClass : MissionClass {

	
	//private HeroStatusClass appliedHero;
	//private List<MissionPhase> phaseList;
	private int cashReward;
	private List<MissionPhase> phaseListHistory;

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


}
