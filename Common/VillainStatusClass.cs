using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainStatusClass {

	private string name;
	private int health;
	private float atk;
	private float def;

	public string Name{
		get {return name;}
		set {name = value;}
	}

	public int Health{
		get {return health;}
		set {health = value;}
	}

	public float Atk{
		get {return atk;}
		set {atk = value;}
	}

	public float Def{
		get {return def;}
		set {def = value;}
	}

	public string StatusMessage (){
		string a = "VillainName : " + Name + "\nHealth : " + Health.ToString() +"\nAttack : "+ Atk.ToString() +"\nDef : "+ Def.ToString();
		return a;
	}
}
