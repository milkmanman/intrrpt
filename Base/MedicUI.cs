using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicUI : MonoBehaviour {

	public Dropdown dropdown;
	Button SubmitButton;
	public Text heroname;
	public Text time;
	public Text Mediclevel;
	public GameObject HealingField;

	void Start() {
		RefleshMedicLevel();
		SelectHero();
	}

	void OnEnable () {
		SelectHero();
	}

	public void SelectHero(){
		dropdown.ClearOptions();
		List<string> listhero = HeroManager.Instance.MedicUI();
		listhero.Insert(0, "Select Hero");
		dropdown.AddOptions(listhero);
		dropdown.value = 0;
	}

	public void OnSubmitButton(){
		string Heroname = dropdown.transform.Find("Label").GetComponent<Text>().text;
		HeroStatusClass hc = HeroManager.Instance.SearchByName(Heroname);
		if(MedicManager.Instance.slot1.ActiveFlag == false){ // later, if in manager
			MedicManager.Instance.SendMedic(hc, 100);
			heroname.text = hc.Name;
			StartCoroutine(MedicProgless());
		} else if (MedicManager.Instance.slot2.ActiveFlag == false){
			MedicManager.Instance.SendMedic(hc, 100);
			HealingField.SetActive(true);
			HealingField.transform.Find("Name").GetComponent<Text>().text = hc.Name;

		}


	}


	void RefleshMedicLevel(){
		Mediclevel.text = "Medic level : " + MedicManager.Instance.MedicPower.ToString();
	}

	IEnumerator MedicProgless () {
		while (MedicManager.Instance.IsAnyoneThere() == true){
			if(MedicManager.Instance.slot1.ActiveFlag == true){
				time.text = MedicManager.Instance.slot1.Time.ToString();
			}
			if(MedicManager.Instance.slot2.ActiveFlag == true){
				Text time2 = HealingField.transform.Find("Time").GetComponent<Text>();
				time2.text = MedicManager.Instance.slot2.Time.ToString();
			}
			yield return new WaitForSeconds(1);
		}
	}
}
