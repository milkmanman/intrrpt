﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityClass {

	private string name;
	private string type;
	private string category;
	private int phase;
	private string cost1Type;
	private int cost1Value;
	private string cost2Type;
	private int cost2Value;
	private string description;
	private Vector3 pos;
	private int time;
	private int remainTime;
	private string iconPass;

	public string Name{
		get {return name;}
		set {name = value;}
	}

	public string Type{
		get {return type;}
		set {type = value;}
	}

	public string Category{
		get {return category;}
		set {category = value;}
	}

	public int Phase{
		get {return phase;}
		set {phase = value;}
	}

	public string Cost1Type{
		get {return cost1Type;}
		set {cost1Type = value;}
	}

	public int Cost1Value{
		get {return cost1Value;}
		set {cost1Value = value;}
	}

	public string Cost2Type{
		get {return cost2Type;}
		set {cost2Type = value;}
	}

	public int Cost2Value{
		get {return cost2Value;}
		set {cost2Value = value;}
	}

	public string Description{
		get {return description;}
		set {description = value;}
	}

	public Vector3 Pos{
		get {return pos;}
		set {pos = value;}
	}

	public int Time{
		get {return time;}
		set {time = value;}
	}

	public int RemainTime{
		get {return remainTime;}
		set {remainTime = value;}
	}

	public string IconPass{
		get {return iconPass;}
		set {iconPass = value;}
	}

	public FacilityClass Clone(){
		FacilityClass retFacility = new FacilityClass();
		retFacility.Name = Name;
		retFacility.Type = Type;
		retFacility.Category = Category;
		retFacility.Phase = Phase;
		retFacility.Cost1Type = Cost1Type;
		retFacility.Cost1Value = Cost1Value;
		retFacility.Cost2Type = Cost2Type;
		retFacility.Cost2Value = Cost2Value;
		retFacility.Description = Description;
		retFacility.Pos = Pos;
		retFacility.Time = Time;
		retFacility.RemainTime = RemainTime;
		retFacility.IconPass = IconPass;
		return retFacility;
	}

	public void SetTime(int prod){
		float newtime_float = (float)(Time) - (float)(Time) / 3 * ((float)(prod) / 108000);
		Time = (int)newtime_float;
		RemainTime = (int)newtime_float;
	}

}
