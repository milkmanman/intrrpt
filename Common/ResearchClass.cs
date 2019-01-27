using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchClass {

	public int Time;
	public int Possibility;
	public bool is_active;
	public IEnumerator Coroutine;
	public GameObject nodeObj;

	public string InfoType;
	public int InfoValue;
	public bool is_successed;

	public ResearchClass(){
		is_active = false;
		is_successed = false;
		InfoType = null;
		InfoValue = 0;
		nodeObj = null;
	}
}
