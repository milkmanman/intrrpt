using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecruitUI : MonoBehaviour {

	public GameObject HiringField;
	public GameObject RecruitField;
	public RectTransform RecruitNodePrefab;

	// Use this for initialization
	void Start () {
		RefreshRecruits();
	}

	public void RefreshRecruits(){
		HeroManager.Instance.RefleshRecruitList();
		foreach (Transform item in RecruitField.transform) {
			Destroy(item.gameObject);
		}
		for(int i = 1; i <= 4; i++){
				var item = GameObject.Instantiate(RecruitNodePrefab) as RectTransform;
				item.SetParent(RecruitField.transform, false);
				ApplicantClass test =  new ApplicantClass();
				test = HeroManager.Instance.RecruitHeroList[i-1];
				item.GetComponent<RecruitNode>().Hero = test;
				item.GetComponent<RecruitNode>().RefleshRecruit();
				Button button = item.GetComponent<RecruitNode>().SelectButton;
				RecruitNode node = item.GetComponent<RecruitNode>();
				button.onClick.AddListener(delegate{HeroManager.Instance.HoldApplicant = test;});
				button.onClick.AddListener(delegate{ShowApplicantDetail();});
		}
	}

	public void ShowApplicantDetail(){
		GameObject Hiring = HiringField.transform.Find("Detail").gameObject;
		Hiring.SetActive(true);
		Hiring.GetComponent<RecruitDetail>().Reflesh(HeroManager.Instance.HoldApplicant);
	}

	public void CloseApplicantDetail(){
		GameObject Hiring = HiringField.transform.Find("Detail").gameObject;
		Hiring.SetActive(false);
	}
}
