using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMissionClass {

	private int type;
	private bool activeflg;
	private bool success;
	private List<MissionPhase> phaseList;
	private List<string> phaseHistory;
	private HeroStatusClass appliedHero;
	private string missionLog;

	public int Type{
		get {return type;}
		set {type = value;}
	}

	public bool ActiveFlg{
		get {return activeflg;}
		set {activeflg = value;}
	}

	public bool Success{
		get {return success;}
		set {success = value;}
	}

	public List<MissionPhase> PhaseList{
		get {return phaseList;}
		set {phaseList = value;}
	}

	public List<string> PhaseHistory{
		get {return phaseHistory;}
		set {phaseHistory = value;}
	}

	public HeroStatusClass AppliedHero{
		get {return appliedHero;}
		set {appliedHero = value;}
	}

	public string MissionLog{
		get {return missionLog;}
		set {missionLog = value;}
	}

}
