using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text ResourceSpace;
	int i = 0;
	public string resource;


	void Start () {
//		RefreshResource();
		StartCoroutine(refleshText());
	}

	public void GoToCity () {
		SceneController.Instance.LoadingScene("City");
	}

/*	public void RefreshResource () {
		int[] Resource = ResourceManager.Instance.HUD();
		Cash.text = Resource[0].ToString();
		Intel.text = Resource[1].ToString();
		Tech.text = Resource[2].ToString();
		Medic.text = Resource[3].ToString();
		Tailor.text = Resource[4].ToString();

	}*/

	public void cash1500 () {
		ResourceManager.Instance.IncreaseResource("Cash", 1500);
//		RefreshResource();
	}

	private IEnumerator refleshText () {
//		string resource;

		while(true){
		int[] Resource = ResourceManager.Instance.HUD();

		if(i % 5 == 0){
			resource = "Cash : " + Resource[0].ToString();
		} else if (i % 5 == 1) {
			resource = "Intel : " + Resource[1].ToString();
		} else if (i % 5 == 2) {
			resource = "Tech : " + Resource[2].ToString();
		} else if (i % 5 == 3) {
			resource = "Medic : " + Resource[3].ToString();
		} else if (i % 5 == 4) {
			resource = "Tailor : " + Resource[4].ToString();
		}

		i++;

		ResourceSpace.text = resource;

		yield return new WaitForSeconds (3.0f);

		ResourceSpace.text = "";

		yield return new WaitForSeconds (1.0f);

		}

	}

}
