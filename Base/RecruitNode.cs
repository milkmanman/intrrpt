using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitNode : MonoBehaviour {

	public ApplicantClass Hero;

	public Text RecruitName;
	public Image Heroism;
	public Image Motivation;
	public Button SelectButton;

	// Use this for initialization
	void Start () {
		//Hero = new HeroStatusClass();
		//Hero.Name = "Sheldon";

		//RefleshRecruit();
	}

	public void RefleshRecruit(){
		RecruitName.text = Hero.Name;

		float HpPercentage = (float)(Hero.Heroism) / 100f;
		Heroism.fillAmount = HpPercentage;

		float MvPercentage = (float)(Hero.Motivation) / 100f;
		Motivation.fillAmount = MvPercentage;

	}
}
