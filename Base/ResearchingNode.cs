using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchingNode : MonoBehaviour {

	public OperationUI o_ui;
	public ResearchClass rc;
	public TextMeshProUGUI textfield;
	public Button NodeButton;

	public void RefleshNode(){
		if(rc != null){
			if(rc.Time != 0){
				textfield.text = "Researching : " + rc.Time.ToString();
			} else {
				textfield.text = "Researching : " + "Complete!";
				NodeButton.enabled = true;
				//if(o_ui != null){
					NodeButton.onClick.RemoveAllListeners();
					NodeButton.onClick.AddListener(delegate{o_ui.ShowResearchResultUI(rc, this.gameObject);});
					
				//}

			}
		}
	}
}
