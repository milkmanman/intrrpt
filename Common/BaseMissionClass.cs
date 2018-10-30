using System.Collections;
using System;
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
	private Action phaseMoveAction;
	private Action pushMissionLogAction;
	private bool failtureFlag;

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

	public Action PhaseMoveAction{
		get {return phaseMoveAction;}
		set {phaseMoveAction = value;}
	}

	public Action PushMissionLogAction{
		get {return pushMissionLogAction;}
		set {pushMissionLogAction = value;}
	}

	public bool FailtureFlag{
		get {return failtureFlag;}
		set {failtureFlag = value;}
	}

}
