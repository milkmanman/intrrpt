﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionClass : BaseMissionClass {

	private int missionNo;
	private string name;
	private int level;
	private string reward1;
	private int reward1val;
	private string reward2;
	private int reward2val;
	private string description;

	private string villainName;
	private string villainDescription;
	private int villainInfo;


	public int MissionNo{
		get {return missionNo;}
		set {missionNo = value;}
	}

	public string Name{
		get {return name;}
		set {name = value;}
	}

	public int Level{
		get {return level;}
		set {level = value;}
	}

	public string Reward1{
		get {return reward1;}
		set {reward1 = value;}
	}

	public int Reward1val{
		get {return reward1val;}
		set {reward1val = value;}
	}

	public string Reward2{
		get {return reward2;}
		set {reward2 = value;}
	}

	public int Reward2val{
		get {return reward2val;}
		set {reward2val = value;}
	}

	public string Description{
		get {return description;}
		set {description = value;}
	}

	public string VillainName{
		get {return villainName;}
		set {villainName = value;}
	}

	public string VillainDescription{
		get {return villainDescription;}
		set {villainDescription = value;}
	}

	public int VillainInfo{
		get {return villainInfo;}
		set {villainInfo = value;}
	}



/* 
	private int missionNo;
	private int type;
	private string name;
	private int level;
	private string reward1;
	private int reward1val;
	private string reward2;
	private int reward2val;
	private string description;
	private bool activeflg;
	private bool success;
	private List<MissionPhase> phaseList;
	private List<string> phaseHistory;

	private string villainName;
	private string villainDescription;
	private int villainInfo;
	private HeroStatusClass appliedHero;
	private string missionLog;


	public int MissionNo{
		get {return missionNo;}
		set {missionNo = value;}
	}

	public int Type{
		get {return type;}
		set {type = value;}
	}

	public string Name{
		get {return name;}
		set {name = value;}
	}

	public int Level{
		get {return level;}
		set {level = value;}
	}

	public string Reward1{
		get {return reward1;}
		set {reward1 = value;}
	}

	public int Reward1val{
		get {return reward1val;}
		set {reward1val = value;}
	}

	public string Reward2{
		get {return reward2;}
		set {reward2 = value;}
	}

	public int Reward2val{
		get {return reward2val;}
		set {reward2val = value;}
	}

	public string Description{
		get {return description;}
		set {description = value;}
	}

	public bool ActiveFlg{
		get {return activeflg;}
		set {activeflg = value;}
	}

	public bool Success{
		get {return success;}
		set {success = value;}
	}

	public string VillainName{
		get {return villainName;}
		set {villainName = value;}
	}

	public string VillainDescription{
		get {return villainDescription;}
		set {villainDescription = value;}
	}

	public int VillainInfo{
		get {return villainInfo;}
		set {villainInfo = value;}
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
 */

}
