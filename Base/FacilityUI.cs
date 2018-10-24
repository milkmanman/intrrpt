using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using TMPro;

public class FacilityUI : MonoBehaviour {

	public UIActivator UiActive;

	public GameObject FacilityNodeField;
	public GameObject DevedFacilityNodeField;
	public GameObject FacilityDetailField;
	public GameObject HUD;

	public Text GranadeCount;
	public TextMeshProUGUI Productivity_tmp;

	public List<GameObject> FacilityObject;

	private string xmldir;
	private string selectedxmldir;

	[SerializeField]
		RectTransform prefab = null;

	public Text Resource;
	public GameObject MemberNodeField;
	[SerializeField]
		RectTransform MemberNodePrefab = null;

	public GameObject WIPNodeField;
	[SerializeField]
		RectTransform WIPNodePrefab = null;


	void Start () {


			List<FacilityClass> FacilityList = new List<FacilityClass>();
			FacilityList = FacilityManager.Instance.FacilityList;
			List<FacilityClass> DevedFacilityList = new List<FacilityClass>();
			DevedFacilityList = FacilityManager.Instance.DevedFacilityList;

			InstantiateFacilityNode(FacilityList, FacilityNodeField);
			InstantiateFacilityNode(DevedFacilityList, DevedFacilityNodeField);


			/*for(int i = 0; i<= FacilityList.Count - 1; i++){
				var item = GameObject.Instantiate(prefab) as RectTransform;
				FacilityClass fc = FacilityList[i];
				item.SetParent(FacilityNodeField.transform, false);
				item.GetComponent<FacilityNode>().facilitycls = fc;
				item.GetComponent<FacilityNode>().RefleshDisplay();

				Button button = item.GetComponent<Button>();
				button.onClick.AddListener(delegate{OnFacilityNodeClicked( fc );});

			}*/

			CanvasOnEnabled();
			UiActive.OnEnabledDevelopUI += CanvasOnEnabled;

	}

	void CanvasOnEnabled () {
		RefreshStuff();
		RefreshProductivity();
		RefreshGranade();
	}

	public void RefleshFacilityList(){

		//List<FacilityClass> FacilityList = new List<FacilityClass>();
		//FacilityList = FacilityManager.Instance.FacilityList;
		//List<FacilityClass> DevedFacilityList = new List<FacilityClass>();
		//DevedFacilityList = FacilityManager.Instance.DevedFacilityList;
		FacilityManager.Instance.SetFacilityList();
		InstantiateFacilityNode(FacilityManager.Instance.FacilityList, FacilityNodeField);
		InstantiateFacilityNode(FacilityManager.Instance.DevedFacilityList, DevedFacilityNodeField);
	}


	private void InstantiateFacilityNode(List<FacilityClass> FacilityList, GameObject parent){

		foreach (Transform child in parent.transform){
			if(child.name.Contains("FacilityNode")){
				GameObject.Destroy (child.gameObject);
　			}
		}

		Debug.LogWarning("debug count : " + FacilityList.Count);

		for(int i = 0; i<= FacilityList.Count - 1; i++){
			var item = GameObject.Instantiate(prefab) as RectTransform;
			FacilityClass fc = FacilityList[i];
			Debug.LogWarning("Facility count count count : " + fc.Name);
			item.SetParent(parent.transform, false);
			item.GetComponent<FacilityNode>().facilitycls = fc;
			item.GetComponent<FacilityNode>().RefleshDisplay();

			Button button = item.GetComponent<Button>();
			button.onClick.AddListener(delegate{OnFacilityNodeClicked( fc );});
			//button.onClick.AddListener(delegate{OnFacilityNodeClicked(tmpxmldir, iconPass);});

		}
	}

	public void OnFacilityNodeClicked(FacilityClass fc) {
		RefreshFacilityDetail(fc);

	}

	public void OnAgreeButtonClicked(FacilityClass fc){

		FacilityManager.Instance.SpendResource(fc);
		GameObject node = InstantiateWIPNode(fc);
		FacilityManager.Instance.StartDevelop(fc, node);

		InstantiateFacility();

	}

	private void InstantiateFacility(){
//		Vector3 placePosition = new Vector3(0,0,0);
		Vector3 placePosition = FacilityManager.Instance.deskpos;


		GameObject test_prefab = (GameObject)Instantiate(
			FacilityObject[0],
			placePosition,
			Quaternion.identity
		);
		test_prefab.name = "InstantiateTable";
	}

	private void RefreshFacilityDetail(FacilityClass fc){
		//selectedxmldir = xmldir;
		string cost;
		//Debug.Log("xmldir : " + xmldir);

		FacilityDetailField.transform.Find("Name_tmp").GetComponent<TextMeshProUGUI>().text = fc.Name;
		FacilityDetailField.transform.Find("Description_tmp").GetComponent<TextMeshProUGUI>().text = fc.Description;

		cost = fc.Cost1Type + " : " + fc.Cost1Value;
		if(fc.Cost2Type != null){
			cost += "\n" + fc.Cost2Type + " : " + fc.Cost2Value;
		}

		FacilityDetailField.transform.Find("Cost").GetComponent<Text>().text = cost;
		if(Resources.Load<Sprite>("UI/FacilityIcons/" + fc.IconPass) != null){
			FacilityDetailField.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/FacilityIcons/" + fc.IconPass);
		}

		int nowResource = ResourceManager.Instance.Cash - fc.Cost1Value;
		string resourceChanges = "Cash : " + ResourceManager.Instance.Cash.ToString() + " -> " + nowResource.ToString();
		FacilityDetailField.transform.Find("Resource").GetComponent<Text>().text = resourceChanges;

		Button button = FacilityDetailField.transform.Find("AgreeButton").GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate{OnAgreeButtonClicked(fc);});
		if(nowResource < 0){
			button.interactable = false;
		} else {
			button.interactable = true;
		}

	}

	private void RefreshStuff(){
		 foreach (Transform child in MemberNodeField.transform) {
     		GameObject.Destroy(child.gameObject);
 		}

		List<DevelopMemberClass> MemberList =  FacilityManager.Instance.DevelopMembers;
		for(int i = 1; i <= MemberList.Count; i++){
			var item = GameObject.Instantiate(MemberNodePrefab) as RectTransform;
			item.SetParent(MemberNodeField.transform, false);
			DevelopMemberClass test =  new DevelopMemberClass();
			test = MemberList[i-1];
			item.GetComponent<DevelopMemberNode>().dev = test;
			item.GetComponent<DevelopMemberNode>().RefleshNode();
			//item.GetComponent<RecruitNode>().RefleshRecruit();
			//Button button = item.GetComponent<RecruitNode>().SelectButton;
			//RecruitNode node = item.GetComponent<RecruitNode>();
			//button.onClick.AddListener(delegate{HeroManager.Instance.HoldApplicant = test;});
			//button.onClick.AddListener(delegate{ShowApplicantDetail();});
			Resource.text = ResourceManager.Instance.showResource("Tech").ToString();
		}
	}

	private GameObject InstantiateWIPNode(FacilityClass fc){
		var item = GameObject.Instantiate(WIPNodePrefab) as RectTransform;
		item.SetParent(WIPNodeField.transform, false);
		return item.gameObject;
	}

	public void RefreshGranade(){
		GranadeCount.text = "x " + FacilityManager.Instance.BuffSlot["flashGranade"].ToString();

	}

	private void RefreshProductivity(){
		Productivity_tmp.text = FacilityManager.Instance.Productivity.ToString();

	}

}
