using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitDetail : MonoBehaviour {

	public GameObject HeroUI;
	public Text Name;
	public Image Icon;
	public Text Msg;
	public Text Personality;
	public Image Heroism;
	public Image Motivation;
	public Button ConfirmButton;
	public Button BackButton;

	void Start(){
		OnBackButtonClick();
	}


	public void Reflesh(RecruitClass AC){
		Name.text = AC.Name;
		Msg.text = AC.Message;
		float HpPercentage = (float)(AC.Status1) / 100f;
		Heroism.fillAmount = HpPercentage;
		float MvPercentage = (float)(AC.Motivation) / 100f;
		Motivation.fillAmount = MvPercentage;
		Personality.text = "- " + AC.Personality1 + "\n- " + AC.Personality2;

	}

	public void OnConfirmButtonClick(){
		if(RecruitManager.Instance.HoldRecruit.Type == "Hero"){
			HeroManager.Instance.addHero(RecruitManager.Instance.HoldRecruit);
		} else if(RecruitManager.Instance.HoldRecruit.Type == "Develop"){
			FacilityManager.Instance.addDevelopMember(RecruitManager.Instance.HoldRecruit);
		}
	}

	public void OnBackButtonClick(){
		GameObject RecruitUI = GameObject.Find ("GUI/RecruitUI");
		BackButton.onClick.AddListener(delegate{RecruitUI.GetComponent<RecruitUI>().CloseApplicantDetail();});
	}

}
