using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class FacilityUI : MonoBehaviour {

	public GameObject FacilityNodeField;
	public GameObject FacilityDetailField;
	public GameObject HUD;
	public TextAsset FacilityDatabase;
	public XmlDocument xmlDoc;

	public List<GameObject> FacilityObject;

	private string xmldir;
	private string selectedxmldir;

	[SerializeField]
		RectTransform prefab = null;

	void Awake () {
		//tmp setting
		FacilityManager.Instance.DronePhase = 1;
		FacilityManager.Instance.JumpingDronePhase = 0;

		xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(FacilityDatabase.text);
	}

	void Start () {

			string[,] arr = FacilityManager.Instance.facilityarray();
			int length = arr.GetLength(0);

			for(int i = 0; i<= length - 1; i++){
				var item = GameObject.Instantiate(prefab) as RectTransform;
				item.SetParent(FacilityNodeField.transform, false);

				string tmpxmldir = "//" + arr[i, 0] + "[@id=" + arr[i, 1] +"]";
				Debug.Log(tmpxmldir);
				string iconPass = arr[i, 0] + "_" + arr[i, 1];

				XmlNode node0 = xmlDoc.SelectSingleNode(@"" + tmpxmldir + "/name");
				item.transform.Find("Text").GetComponent<Text>().text = node0.InnerText;

				Button button = item.GetComponent<Button>();
				button.onClick.AddListener(delegate{OnFacilityNodeClicked(tmpxmldir, iconPass);});
			}
	}

	public void OnFacilityNodeClicked(string dir, string iconpass) {
		this.gameObject.transform.Find("Field").GetComponent<UITransition>().UITransitioner(false);
		RefreshFacilityDetail(dir, iconpass);

	}

	public void OnAgreeButtonClicked(){

		XmlNode node0 = xmlDoc.SelectSingleNode(@"" + selectedxmldir + "/cost1");
		XmlNode node1 = xmlDoc.SelectSingleNode(@"" + selectedxmldir + "/cost1type");

		ResourceManager.Instance.DecreaseResource(node0.InnerText, int.Parse(node1.InnerText));

		if(xmlDoc.SelectSingleNode(@"" + selectedxmldir + "/cost2") != null ){
			XmlNode node2 = xmlDoc.SelectSingleNode(@"" + selectedxmldir + "/cost2");
			XmlNode node3 = xmlDoc.SelectSingleNode(@"" + selectedxmldir + "/cost2type");
			ResourceManager.Instance.DecreaseResource(node2.InnerText, int.Parse(node3.InnerText));
		}
		this.gameObject.transform.Find("Field").GetComponent<UITransition>().UITransitioner(true);

		InstantiateFacility();
		Debug.Log("agreebutton values : " + selectedxmldir);

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

	private void RefreshFacilityDetail(string xmldir, string iconPass){
		selectedxmldir = xmldir;
		string cost;

		XmlNode node0 = xmlDoc.SelectSingleNode(@"" + xmldir + "/name");
		FacilityDetailField.transform.Find("Name").GetComponent<Text>().text = node0.InnerText;
		XmlNode node1 = xmlDoc.SelectSingleNode(@"" + xmldir + "/description");
		FacilityDetailField.transform.Find("Description").GetComponent<Text>().text = node1.InnerText;


		XmlNode node2 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost1");
		XmlNode node3 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost1type");
		cost = node2.InnerText + " : " + node3.InnerText;

		if(xmlDoc.SelectSingleNode(@"" + xmldir + "/cost2") != null){
			XmlNode node4 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost2");
			XmlNode node5 = xmlDoc.SelectSingleNode(@"" + xmldir + "/cost2type");
			cost += "\n" + node4.InnerText + " : " + node5.InnerText;
		}

		FacilityDetailField.transform.Find("Cost").GetComponent<Text>().text = cost;

		FacilityDetailField.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/FacilityIcons/" + iconPass);
	}

}
