using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivatorCity : MonoBehaviour {

	[SerializeField]
	 GameObject ShortMissionUI = null;

	 [SerializeField]
 	 GameObject ShortMissionResultUI = null;

	 [SerializeField]
		 GameObject MissionUI = null;

		public void Activator(int a){
			if(a == 1){
				ShortMissionUI.SetActive(true);
				ShortMissionResultUI.SetActive(false);
				MissionUI.SetActive(false);
			} else if (a == 2){
				ShortMissionUI.SetActive(false);
				ShortMissionResultUI.SetActive(true);
				MissionUI.SetActive(false);
			} else if (a == 3){
				ShortMissionUI.SetActive(false);
				ShortMissionResultUI.SetActive(false);
				MissionUI.SetActive(true);
			} else {
				ShortMissionUI.SetActive(false);
				ShortMissionResultUI.SetActive(false);
				MissionUI.SetActive(false);
			}
		}
}
