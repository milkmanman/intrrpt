using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivatorCity : MonoBehaviour {

	/* [SerializeField]
	 GameObject ShortMissionUI = null;

	 [SerializeField]
 	 GameObject ShortMissionResultUI = null;

	 [SerializeField]
		 GameObject MissionUI = null;*/


	public Canvas ShortMissionUIcanvas;
	public Canvas ShortMissionResultUIcanvas;
	public Canvas MissionUIcanvas;
	public Canvas FreeroamUIcanvas;

	public Action OnEnabledFreeroamUI;



		public void Activator(int a){
			if(a == 1){
				ShortMissionUIcanvas.enabled = true;
				ShortMissionResultUIcanvas.enabled = false;
				MissionUIcanvas.enabled = false;
				FreeroamUIcanvas.enabled = false;

				/* ShortMissionUI.SetActive(true);
				ShortMissionResultUI.SetActive(false);
				MissionUI.SetActive(false);*/
			} else if (a == 2){
				ShortMissionUIcanvas.enabled = false;
				ShortMissionResultUIcanvas.enabled = true;
				MissionUIcanvas.enabled = false;
				FreeroamUIcanvas.enabled = false;

				/*ShortMissionUI.SetActive(false);
				ShortMissionResultUI.SetActive(true);
				MissionUI.SetActive(false);*/
			} else if (a == 3){
				ShortMissionUIcanvas.enabled = false;
				ShortMissionResultUIcanvas.enabled = false;
				MissionUIcanvas.enabled = true;
				FreeroamUIcanvas.enabled = false;

				/*ShortMissionUI.SetActive(false);
				ShortMissionResultUI.SetActive(false);
				MissionUI.SetActive(true);*/
			} else {
				ShortMissionUIcanvas.enabled = false;
				ShortMissionResultUIcanvas.enabled = false;
				MissionUIcanvas.enabled = false;
				FreeroamUIcanvas.enabled = false;

				/*ShortMissionUI.SetActive(false);
				ShortMissionResultUI.SetActive(false);
				MissionUI.SetActive(false);*/
			}
		}
}
