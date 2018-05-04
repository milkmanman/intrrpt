using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivator : MonoBehaviour {

	public GameObject HeroUI;
	public GameObject MissionUI;
	public GameObject PoliceUI;
	public GameObject DevelopUI;
	public GameObject WholeUI;
	public GameObject MedicUI;
	public GameObject OptionUI;
	public GameObject RecruitUI;

		public void Activator(int a){

			HeroUI.SetActive(false);
			MissionUI.SetActive(false);
			PoliceUI.SetActive(false);
			DevelopUI.SetActive(false);
			WholeUI.SetActive(false);
			MedicUI.SetActive(false);
			OptionUI.SetActive(false);
			RecruitUI.SetActive(false);


			switch(a){
				case 1:
					HeroUI.SetActive(true);
					break;
				case 2:
					MissionUI.SetActive(true);
					break;
				case 3:
					PoliceUI.SetActive(true);
					break;
				break;
				case 4:
					DevelopUI.SetActive(true);
					break;
				case 5:
					WholeUI.SetActive(true);
					break;
				case 6:
					MedicUI.SetActive(true);
					break;
				case 7:
					OptionUI.SetActive(true);
					break;
				case 8:
					RecruitUI.SetActive(true);
				break;
				default:
					break;
			}
		}
}
