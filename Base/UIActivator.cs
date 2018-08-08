using System;
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

	public Canvas HeroUIcanvas;
	public Canvas MissionUIcanvas;
	public Canvas PoliceUIcanvas;
	public Canvas DevelopUIcanvas;
	public Canvas WholeUIcanvas;
	public Canvas MedicUIcanvas;
	public Canvas OptionUIcanvas;
	public Canvas RecruitUIcanvas;

	public Action OnEnabledWholeUI;
	public Action OnEnabledMedicUI;
	public Action OnEnabledDevelopUI;


		/*public void Activator(int a){

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
			}*/

		public void Activator(int a){

			HeroUIcanvas.enabled = false;
			MissionUIcanvas.enabled = false;
			PoliceUIcanvas.enabled = false;
			DevelopUIcanvas.enabled = false;
			WholeUIcanvas.enabled = false;
			MedicUIcanvas.enabled = false;
			OptionUIcanvas.enabled = false;
			RecruitUIcanvas.enabled = false;

			switch(a){
				case 1:
					HeroUIcanvas.enabled = true;
					break;
				case 2:
					MissionUIcanvas.enabled = true;
					break;
				case 3:
					PoliceUIcanvas.enabled = true;
					break;
				break;
				case 4:
					DevelopUIcanvas.enabled = true;
					OnEnabledDevelopUI();
					break;
				case 5:
					WholeUIcanvas.enabled = true;
					OnEnabledWholeUI();
					break;
				case 6:
					MedicUIcanvas.enabled = true;
					OnEnabledMedicUI();
					break;
				case 7:
					OptionUIcanvas.enabled = true;
					break;
				case 8:
					RecruitUIcanvas.enabled = true;
				break;
				default:
					break;
			}
		}
}
