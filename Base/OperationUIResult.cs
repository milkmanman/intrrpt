using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OperationUIResult : MonoBehaviour {

	public ResearchClass rc_result;
	public TextMeshProUGUI Result;
	public TextMeshProUGUI Reward;

	public void Reflesh(ResearchClass rc){
		rc_result = rc;
		if(rc != null){
			string result_str;
			if(rc.is_successed == true){
				result_str = "Research : Successs";
			} else {
				result_str = "Research : Failure";
			}
			Result.text = result_str;
			Reward.text = rc.InfoType + " : " + rc.InfoValue.ToString();
			//Reward.text = "Reward Type : XXXX";
		}
	}
	
}
