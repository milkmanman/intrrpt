using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitNode : MonoBehaviour {

	public RecruitClass Hero;

	public Text RecruitName;
	public Text Status1Name;
	public Image Status1;
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

		float Status1Percentage = (float)(Hero.Status1) / 100f;
		Status1.fillAmount = Status1Percentage;

		float MvPercentage = (float)(Hero.Motivation) / 100f;
		Motivation.fillAmount = MvPercentage;

	}
}
