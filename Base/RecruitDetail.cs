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
	public Button ConfirmButton;
	public Button BackButton;

	void Start(){
		OnBackButtonClick();
	}


	public void Reflesh(ApplicantClass AC){
		Name.text = AC.Name;
		Msg.text = AC.Message;
	}

	public void OnConfirmButtonClick(){

	}

	public void OnBackButtonClick(){
		BackButton.onClick.AddListener(delegate{HeroUI.GetComponent<HeroUI>().CloseApplicantDetail();});
	}

}
