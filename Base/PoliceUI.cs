using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class PoliceUI : MonoBehaviour {

	public TextAsset NameData;
	public XmlDocument xmlDoc;
	public GameObject PoliceNodeField;
	public GameObject HiredPoliceNodeField;


	[SerializeField]
		RectTransform prefab = null;

	void Awake () {
		xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(NameData.text);

		for(int i = 1; i <= 10; i++) {

			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(PoliceNodeField.transform, false);

			var nameField = item.Find("Name").GetComponent<Text>();
			string name = NameGenerator();
			nameField.text = name;

			var rankField = item.Find("Role").GetComponent<Text>();
			string rank = RankGenerator();
			rankField.text = rank;

			var costField = item.Find("Cost").GetComponent<Text>();
			int cost = CostGenerator(rank);
			costField.text = cost.ToString();

			PoliceClass policeNodeClass = new PoliceClass();
			policeNodeClass.Name = name;
			policeNodeClass.Role = rank;
			policeNodeClass.Cost = cost;



			Button button = item.GetComponent<Button>();
			button.onClick.AddListener(delegate{PoliceManager.Instance.RegisterPolice(policeNodeClass);});
			button.onClick.AddListener(delegate{RefreshPolices();});


			}

	}

	void Start () {
//		Debug.Log(NameGenerator());
		RefreshPolices();
	}


	private void RefreshPolices () {

		for( int i=0; i < HiredPoliceNodeField.transform.childCount; ++i ){
			GameObject.Destroy( HiredPoliceNodeField.transform.GetChild( i ).gameObject );
		}

		if(PoliceManager.Instance.PoliceMember1 != null){
			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(HiredPoliceNodeField.transform, false);

			var nameField = item.Find("Name").GetComponent<Text>();
			nameField.text = PoliceManager.Instance.PoliceMember1.Name;

			var roleField = item.Find("Role").GetComponent<Text>();
			roleField.text = PoliceManager.Instance.PoliceMember1.Role;
		}

		if(PoliceManager.Instance.PoliceMember2 != null){
			var item2 = GameObject.Instantiate(prefab) as RectTransform;
			item2.SetParent(HiredPoliceNodeField.transform, false);

			var nameField2 = item2.Find("Name").GetComponent<Text>();
			nameField2.text = PoliceManager.Instance.PoliceMember2.Name;

			var roleField2 = item2.Find("Role").GetComponent<Text>();
			roleField2.text = PoliceManager.Instance.PoliceMember2.Role;
		}
	}

	private string NameGenerator () {

		xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(NameData.text);

		int random = Random.Range(1,3);
		XmlNode node0 = null;

		switch(random){
			case 1 :
				XmlNode nodeMalename = xmlDoc.SelectSingleNode(@"//malename");
				int MalenameNo = nodeMalename.ChildNodes.Count;
				int ramdomMaleName= Random.Range(1, MalenameNo);
				node0 = xmlDoc.SelectSingleNode(@"//malename/name[@id=" + ramdomMaleName + "]");
				break;

			case 2 :
				XmlNode nodeFemalename = xmlDoc.SelectSingleNode(@"//femalename");
				int FemalenameNo = nodeFemalename.ChildNodes.Count;
				int ramdomFemaleName= Random.Range(1, FemalenameNo);
				node0 = xmlDoc.SelectSingleNode(@"//femalename/name[@id=" + ramdomFemaleName + "]");
				break;

			default :
				break;
		}
		XmlNode nodeSurname = xmlDoc.SelectSingleNode(@"//surname");
		int SurnameNo = nodeSurname.ChildNodes.Count;

		int ramdomSurName= Random.Range(1, SurnameNo);
		XmlNode node1 = xmlDoc.SelectSingleNode(@"//surname/name[@id=" + ramdomSurName + "]");

		string fullName = node0.InnerText + " " + node1.InnerText;

		return fullName;
	}

	private string RankGenerator(){
		int Police_Phase = 2;
		float[] PoliceArray = new float[5];
		string[] Rank_str = new string[] {"Police Officer", "Detective", "Sergeant", "Lieutenant", "Captain"};

		switch(Police_Phase){
			case 1 :
				PoliceArray[0] = 10;
				PoliceArray[1] = 0;
				PoliceArray[2] = 0;
				PoliceArray[3] = 0;
				PoliceArray[4] = 0;
				break;

			default :
			PoliceArray[0] = 10;
			PoliceArray[1] = 5;
			PoliceArray[2] = 8;
			PoliceArray[3] = 2;
			PoliceArray[4] = 4;
				break;
		}

		float random = Choose(PoliceArray);
		int randomInt = (int)random;
		string ChoosenRank = Rank_str[randomInt];

		return ChoosenRank;

	}

	private int CostGenerator(string Role){
		int PoliceCost;

		switch(Role){
			case "Police Officer" :
				PoliceCost = 1000;
				break;
			case "Detective" :
				PoliceCost = 2000;
				break;
			case "Sergeant" :
				PoliceCost = 3000;
				break;
			case "Lieutenant" :
				PoliceCost = 4000;
				break;
			case "Captain" :
				PoliceCost = 5000;
				break;
			default :
				PoliceCost = 0;
				break;
		}

		return PoliceCost;
	}

	private float Choose (float[] probs) {

		float total = 0;
		foreach (float elem in probs) {
				total += elem;
		}
		float randomPoint = Random.value * total;
		for (int i= 0; i < probs.Length; i++) {
				if (randomPoint < probs[i]) {
						return i;
				}
				else {
						randomPoint -= probs[i];
				}
		}
		return probs.Length - 1;
	}

}
