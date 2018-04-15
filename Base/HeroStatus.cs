using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus : MonoBehaviour {

	public HeroStatusClass heroClass;

	void Start () {
/*		heroClass = new HeroStatusClass();
		heroClass.Name = "Alex";
		heroClass.Gender = "Male";
		heroClass.Skin = 3;
		heroClass.Costume = "Gorilla";
		heroClass.CostumeLv = 22;
		heroClass.Heroism = 100;*/
	}

	public void SetValue (HeroStatusClass setvalue) {
		heroClass = setvalue;
	}
}
