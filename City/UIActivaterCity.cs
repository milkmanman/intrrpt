using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivaterCity : MonoBehaviour {

	[SerializeField]
	 GameObject ShortMissionUI = null;


		public void Activater(int a){
			if(a == 1){
				ShortMissionUI.SetActive(true);
			} else {
				ShortMissionUI.SetActive(false);
			}
		}
}
