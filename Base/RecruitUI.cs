using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecruitUI : MonoBehaviour {

	public GameObject HeroHiringField;
	public GameObject DevelopHiringField;
	public GameObject MedicHiringField;
	public GameObject HeroRecruitField;
	public GameObject DevelopRecruitField;
	public GameObject MedicRecruitField;
	public GameObject Detail;
	public RectTransform RecruitNodePrefab;


	// Use this for initialization
	void Start () {

		RefreshRecruits("Hero");
		RefreshRecruits("Develop");
		RefreshRecruits("Medic");


	}

	public void RefreshRecruits(string type){

		GameObject field = null;

		if(type == "Hero"){
			field = HeroRecruitField;
		} else if(type == "Develop"){
			field = DevelopRecruitField;
		} else if(type == "Medic"){
			field = MedicRecruitField;
		}

		RecruitManager.Instance.RefleshRecruitList(type);
		Debug.Log("test recruit debug : " + RecruitManager.Instance.RecruitHeroList[1].Name);
		foreach (Transform item in field.transform) {
			Destroy(item.gameObject);
		}
		for(int i = 1; i <= 4; i++){
				var item = GameObject.Instantiate(RecruitNodePrefab) as RectTransform;
				item.SetParent(field.transform, false);
				RecruitClass test =  new RecruitClass();
				if(type == "Hero"){
					test = RecruitManager.Instance.RecruitHeroList[i-1];
				} else if(type == "Develop"){
					test = RecruitManager.Instance.RecruitDevelopList[i-1];
				} else if(type == "Medic"){
					test = RecruitManager.Instance.RecruitMedicList[i-1];
				}
				item.GetComponent<RecruitNode>().Hero = test;
				item.GetComponent<RecruitNode>().RefleshRecruit();
				Button button = item.GetComponent<RecruitNode>().SelectButton;
				RecruitNode node = item.GetComponent<RecruitNode>();
				button.onClick.AddListener(delegate{RecruitManager.Instance.HoldRecruit = test;});
				button.onClick.AddListener(delegate{ShowApplicantDetail();});
		}
	}

	public void ShowApplicantDetail(){
		GameObject Hiring = Detail;

		Hiring.SetActive(true);
		Hiring.GetComponent<RecruitDetail>().Reflesh(RecruitManager.Instance.HoldRecruit);
	}

	public void CloseApplicantDetail(){
		GameObject Hiring = Detail;
		Hiring.SetActive(false);
	}

	public void ChangeTab(string type){
		if(type == "Hero"){
			HeroHiringField.SetActive(true);
			DevelopHiringField.SetActive(false);
			MedicHiringField.SetActive(false);
		} else if(type == "Develop"){
			DevelopHiringField.SetActive(true);
			HeroHiringField.SetActive(false);
			MedicHiringField.SetActive(false);
		} else if(type == "Medic"){
			DevelopHiringField.SetActive(false);
			HeroHiringField.SetActive(false);
			MedicHiringField.SetActive(true);
		}
	}

}
