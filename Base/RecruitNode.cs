﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitNode : MonoBehaviour {

	public RecruitClass Hero;

	public TextMeshProUGUI RecruitName_tmp;

	public Image Status1;
	public Image Motivation;
	public Button SelectButton;
	public TextMeshProUGUI Param1Name;
	public TextMeshProUGUI Param2Name;

	// Use this for initialization
	void Start () {
		//Hero = new HeroStatusClass();
		//Hero.Name = "Sheldon";

		//RefleshRecruit();
	}

	public void RefleshRecruit(){

		switch(Hero.Type){
			case "Hero" :
				Param1Name.text = "Heroism";
				Param2Name.text = "Motivation";
				break;
			case "Develop" :
				Param1Name.text = "Dev skill";
				Param2Name.text = "Motivation";
				break;
			case "Medic" :
				Param1Name.text = "Med skill";
				Param2Name.text = "Motivation";
				break;
			default :
				break;
		}


		RecruitName_tmp.text = Hero.Name;

		float Status1Percentage = (float)(Hero.Status1) / 100f;
		Status1.fillAmount = Status1Percentage;

		float MvPercentage = (float)(Hero.Motivation) / 100f;
		Motivation.fillAmount = MvPercentage;

	}
}
