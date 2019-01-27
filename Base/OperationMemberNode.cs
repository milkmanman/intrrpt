using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OperationMemberNode : MonoBehaviour {

	public OperationMemberClass opr;
	public TextMeshProUGUI Name;

	public void RefleshNode(){
		Name.text = opr.Name;
	}

	
}
