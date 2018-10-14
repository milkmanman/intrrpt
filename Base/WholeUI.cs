using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WholeUI : MonoBehaviour {

	public UIActivator UiActive;

	public TextMeshProUGUI ResourceField_tmp;
	public TextMeshProUGUI HeroesField_tmp;

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
		string resourcestr = "";
		int[] Resource = ResourceManager.Instance.HUD();
		resourcestr += "CASH : " + Resource[0].ToString();
		resourcestr += "\n" + "TECH : " + Resource[1].ToString();
		resourcestr += "\n" + "MEDIC : " + Resource[2].ToString();

		ResourceField_tmp.text = resourcestr;

	}

	public void RefreshHeroes(){

		List<string> listhero = HeroManager.Instance.wholeUI();
		string test = "";
		if(listhero.Count >= 1) test += "\n" + listhero[0];
		if(listhero.Count >= 2) test += "\n" + listhero[1];
		if(listhero.Count >= 3) test += "\n" + listhero[2];
		if(listhero.Count >= 4) test += "\n" + listhero[3];
		if(listhero.Count >= 5) test += "\n" + listhero[4];
		if(listhero.Count >= 6) test += "\n" + listhero[5];

		HeroesField_tmp.text = test;

	}

	public void RefreshDev(){

		List<string> listhero = FacilityManager.Instance.wholeUI();

		string test = "";

		for(int i = 1; i <= listhero.Count; i++){
			test += listhero[i - 1] + "\n";
		}

		DevsField.text = test;

	}

}
