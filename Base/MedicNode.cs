using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicNode : MonoBehaviour {

	public Text Name;
	public Text Time;

	public void Refresh(MedicClass mc){
		Debug.Log("dev time : " + mc.Time.ToString());

		Name.text = mc.hsc.Name;
		Time.text = mc.Time.ToString();

	}

}
