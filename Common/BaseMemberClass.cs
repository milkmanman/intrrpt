using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMemberClass {

	private string name;
	private string gender;
	private int skin;

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

}
