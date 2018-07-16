using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicMemberNode : MonoBehaviour {

	public MedicMemberClass med;

	public Text Name;
	public Image LvBar;
	public Image MotivBar;

	public void RefleshNode(){

		Name.text = med.Name;

		float LevelPercentage = (float)(med.SkillLv) / 100f;
		LvBar.fillAmount = LevelPercentage;

		float MvPercentage = (float)(med.Motivation) / 100f;
		MotivBar.fillAmount = MvPercentage;
	}

}
