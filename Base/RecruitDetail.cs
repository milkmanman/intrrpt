﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitDetail : MonoBehaviour {

	public GameObject HeroUI;
	public TextMeshProUGUI Name_tmp;
	public Image Icon;
	public TextMeshProUGUI Msg_tmp;
	public TextMeshProUGUI Personality_tmp;
	public Image Heroism;
	public Image Motivation;
	public Button ConfirmButton;
	public Button BackButton;

	void Start(){
		OnBackButtonClick();
	}


	public void Reflesh(RecruitClass AC){

		Name_tmp.text = AC.Name;

		Msg_tmp.text = AC.Message;
		float HpPercentage = (float)(AC.Status1) / 100f;
		Heroism.fillAmount = HpPercentage;
		float MvPercentage = (float)(AC.Motivation) / 100f;
		Motivation.fillAmount = MvPercentage;
		Personality_tmp.text = "- " + AC.Personality1 + "\n- " + AC.Personality2;

	}

	public void OnConfirmButtonClick(){
		RecruitManager.Instance.AddMember();
		/*if(RecruitManager.Instance.HoldRecruit.Type == "Hero"){
			HeroManager.Instance.addHero(RecruitManager.Instance.HoldRecruit);
		} else if(RecruitManager.Instance.HoldRecruit.Type == "Develop"){
			FacilityManager.Instance.addDevelopMember(RecruitManager.Instance.HoldRecruit);
		} else if(RecruitManager.Instance.HoldRecruit.Type == "Medic"){
			MedicManager.Instance.addMedicMember(RecruitManager.Instance.HoldRecruit);
		}*/
	}

	public void OnBackButtonClick(){
		GameObject RecruitUI = GameObject.Find ("GUI/RecruitUI");
		BackButton.onClick.AddListener(delegate{RecruitUI.GetComponent<RecruitUI>().CloseApplicantDetail();});
	}

}
