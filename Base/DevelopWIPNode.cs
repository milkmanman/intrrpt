using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DevelopWIPNode : MonoBehaviour {

	public Text Name;
	public Text Time;
	public Image TimeBar;

	public void Refresh(FacilityClass fc){
		Name.text = fc.Name;
		Time.text = fc.RemainTime.ToString();
		float DevelopProgress = ( (float)(fc.Time) - (float)(fc.RemainTime) ) / (float)(fc.Time);
		TimeBar.fillAmount = DevelopProgress;
	}

	public void Complete(FacilityClass fc){
		Time.text = "Complete!";
		TimeBar.fillAmount = 1.0f;
		Button completeBtn = this.gameObject.GetComponent<Button>();
		completeBtn.onClick.AddListener(delegate{FacilityManager.Instance.CompleteDevelopment(fc);});
		if(GameObject.Find("GUI/FacilityUI") != null){
			completeBtn.onClick.AddListener(delegate{GameObject.Find("GUI/FacilityUI").GetComponent<FacilityUI>().RefreshGranade();});
			completeBtn.onClick.AddListener(delegate{StartCoroutine("SelfDestroy");});
		}

	}

	private IEnumerator SelfDestroy (){
		yield return new WaitForSeconds(0.2f);
		Destroy(this.gameObject);
	}


}
