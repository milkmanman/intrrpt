using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicUI : MonoBehaviour {

	public UIActivator UiActive;

	public Dropdown dropdown;
	Button SubmitButton;
	public Text heroname;
	public Text time;
	public Text Mediclevel;
	public Text Productivity;
	public GameObject HealingField;
	public GameObject MedicNodeField;
	public RectTransform MedicNodePrefab;

	public GameObject MemberNodeField;
	public RectTransform MemberNodePrefab;


	void Start() {
		CanvasOnEnabled();
		UiActive.OnEnabledMedicUI += CanvasOnEnabled;
	}
	
	void CanvasOnEnabled () {
		RefreshStuff();
		RefleshMedicLevel();
		SelectHero();
	}

	public void SelectHero(){
		dropdown.ClearOptions();
		List<string> listhero = HeroManager.Instance.MedicUI();
		listhero.Insert(0, "Select Hero");
		dropdown.AddOptions(listhero);
		dropdown.value = 0;
	}

	public void OnSubmitButton(){
		MedicClass med = new MedicClass();
		string Heroname = dropdown.transform.Find("Label").GetComponent<Text>().text;
		med.hsc = HeroManager.Instance.SearchByName(Heroname);
		GameObject node = InstantiateMedicNode(med);
		MedicManager.Instance.SendMedic(med, node);
		
		
		/*if(MedicManager.Instance.slot1.ActiveFlag == false){ // later, if in manager
			MedicManager.Instance.SendMedic(hc, 100);
			heroname.text = hc.Name;
			StartCoroutine(MedicProgless());
		} else if (MedicManager.Instance.slot2.ActiveFlag == false){
			MedicManager.Instance.SendMedic(hc, 100);
			HealingField.SetActive(true);
			HealingField.transform.Find("Name").GetComponent<Text>().text = hc.Name;

		}*/
	}

	private GameObject InstantiateMedicNode(MedicClass fc){
		var item = GameObject.Instantiate(MedicNodePrefab) as RectTransform;
		item.SetParent(MedicNodeField.transform, false);
		return item.gameObject;
	}


	void RefleshMedicLevel(){
		Mediclevel.text = "Medic level : " + MedicManager.Instance.MedicPower.ToString();
	}

	IEnumerator MedicProgless () {
		while (MedicManager.Instance.IsAnyoneThere() == true){
			if(MedicManager.Instance.slot1.ActiveFlag == true){
				time.text = MedicManager.Instance.slot1.Time.ToString();
			}
			if(MedicManager.Instance.slot2.ActiveFlag == true){
				Text time2 = HealingField.transform.Find("Time").GetComponent<Text>();
				time2.text = MedicManager.Instance.slot2.Time.ToString();
			}
			yield return new WaitForSeconds(1);
		}
	}

	private void RefreshStuff(){
		 foreach (Transform child in MemberNodeField.transform) {
     		GameObject.Destroy(child.gameObject);
 		}

		List<MedicMemberClass> MemberList =  MedicManager.Instance.MedicMembers;
		for(int i = 1; i <= MemberList.Count; i++){
			var item = GameObject.Instantiate(MemberNodePrefab) as RectTransform;
			item.SetParent(MemberNodeField.transform, false);
			MedicMemberClass test =  new MedicMemberClass();
			test = MemberList[i-1];
			item.GetComponent<MedicMemberNode>().med = test;
			item.GetComponent<MedicMemberNode>().RefleshNode();
			
			//item.GetComponent<RecruitNode>().RefleshRecruit();
			//Button button = item.GetComponent<RecruitNode>().SelectButton;
			//RecruitNode node = item.GetComponent<RecruitNode>();
			//button.onClick.AddListener(delegate{HeroManager.Instance.HoldApplicant = test;});
			//button.onClick.AddListener(delegate{ShowApplicantDetail();});
		}
		Productivity.text = MedicManager.Instance.Productivity.ToString();
	}


}
