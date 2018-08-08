using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopMemberClass {

	private string name;
	private string gender;
	private int skin;
//	private int skillLv;
	private int skillExp;
	private int motivation;

	public string Name{
		get {return name;}
		set {name = value;}
	}

	public string Gender{
		get {return gender;}
		set {gender = value;}
	}

	public int Skin{
		get {return skin;}
		set {skin = value;}
	}

	public int SkillLv{
		get {return (skillExp / 100) + 1;}
		set {skillExp = (value * 100) + 1;}
	}

	public int SkillExp{
		get {return skillExp;}
		set {skillExp = value;}
	}

	public int Motivation{
		get {return motivation;}
		set {motivation = value;}
	}

	public int Productivity{
		//get {return motivation * SkillLv * 10;}
		get {return ((motivation + 100) * SkillLv * 30) / 10;}

	}

	public void debug(){
		Debug.Log("DevelopMember : " + name + ", exp-" + SkillExp + ", lv-" + SkillLv + ", motiv-" + Motivation + ", prdctvt-" + Productivity);
	}

}
