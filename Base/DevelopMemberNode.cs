using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevelopMemberNode : MonoBehaviour {

	public DevelopMemberClass dev;

	public Text Name;
	public Image LvBar;
	public Image MotivBar;

	public void RefleshNode(){

		Name.text = dev.Name;

		float LevelPercentage = (float)(dev.SkillLv) / 30f;
		LvBar.fillAmount = LevelPercentage;

		float MvPercentage = (float)(dev.Motivation) / 100f;
		MotivBar.fillAmount = MvPercentage;

	}

}
