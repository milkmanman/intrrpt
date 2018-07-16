using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitClass {

	private string type;
	private string name;
	private string gender;
	private string message;
	private string personality1;
	private string personality2;
	private int motivation;
	private int status1;

	public string Type{
		get {return type;}
		set {type = value;}
	}

	public string Name{
		get {return name;}
		set {name = value;}
	}

	public string Gender{
		get {return gender;}
		set {gender = value;}
	}

	public string Message{
		get {return message;}
		set {message = value;}
	}

	public string Personality1{
		get {return personality1;}
		set {personality1 = value;}
	}

	public string Personality2{
		get {return personality2;}
		set {personality2 = value;}
	}

	public int Status1{
		get {return status1;}
		set {status1 = value;}
	}

	public int Motivation{
		get {return motivation;}
		set {motivation = value;}
	}

	public HeroStatusClass CloneHero(){
		HeroStatusClass retHero = new HeroStatusClass();
		retHero.Name = Name;
		retHero.Heroism = Status1;
		return retHero;
	}

	public DevelopMemberClass CloneDevelop(){
		DevelopMemberClass retHero = new DevelopMemberClass();
		retHero.Name = Name;
		retHero.Motivation = Motivation;
		return retHero;
	}

	public MedicMemberClass CloneMedic(){
		MedicMemberClass retMember = new MedicMemberClass();
		retMember.Name = Name;
		retMember.Motivation = Motivation;
		retMember.SkillExp = Status1;
		return retMember;
	}



}
