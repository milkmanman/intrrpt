using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WholeUI : MonoBehaviour {

	public UIActivator UiActive;

	public Text Cash;
	public Text Intel;
	public Text Tech;
	public Text Medic;
	public Text Tailor;

	public Text HeroesField;
	public Text DevsField;


	void Start () {
		CanvasOnEnabled();
		UiActive.OnEnabledWholeUI += CanvasOnEnabled;
	}

	void CanvasOnEnabled () {
		RefreshResource();
		RefreshHeroes();
		RefreshDev();
	}

	public void RefreshResource () {
		int[] Resource = ResourceManager.Instance.HUD();
		Cash.text = "CASH : " + Resource[0].ToString();
		Intel.text = "INTEL : " + Resource[1].ToString();
		Tech.text = "TECH : " + Resource[2].ToString();
		Medic.text = "MEDIC : " + Resource[3].ToString();
		Tailor.text = "TAILOR : " + Resource[4].ToString();

	}

	public void RefreshHeroes(){

		List<string> listhero = HeroManager.Instance.wholeUI();
		string test = listhero[0];
		if(listhero.Count >= 2) test += "\n" + listhero[1];
		if(listhero.Count >= 3) test += "\n" + listhero[2];
		if(listhero.Count >= 4) test += "\n" + listhero[3];
		if(listhero.Count >= 5) test += "\n" + listhero[4];
		if(listhero.Count >= 6) test += "\n" + listhero[5];

		HeroesField.text = test;

	}

		public void RefreshDev(){

		List<string> listhero = FacilityManager.Instance.wholeUI();
		string test = listhero[0];
		if(listhero.Count >= 2) test += "\n" + listhero[1];
		if(listhero.Count >= 3) test += "\n" + listhero[2];
		if(listhero.Count >= 4) test += "\n" + listhero[3];
		if(listhero.Count >= 5) test += "\n" + listhero[4];
		if(listhero.Count >= 6) test += "\n" + listhero[5];

		DevsField.text = test;

	}

}
