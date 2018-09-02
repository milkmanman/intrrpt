using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityFreeroamUI : MonoBehaviour {

	public Text missionLog;
	private Text HeroName;
	public GameObject MissionInfo;
	private int maxHealth;
	private Image HPBar;
	public GameObject button;
	public Button ComeBackButton;
	public FreeRoamClass frc;

	void Awake () {
		if(MissionManager.Instance.FreeRoamList[0] != null){
			Debug.Log("FreeRoamList[0]" + " - isnotnull");
			frc = MissionManager.Instance.FreeRoamList[0];
			HeroName = MissionInfo.transform.Find("HeroName").GetComponent<Text>();
			HPBar = MissionInfo.transform.Find("HP/Bar").GetComponent<Image>();
			HPBar.fillAmount = 1.0f;
			HeroName.text = frc.AppliedHero.Name;
			maxHealth = frc.AppliedHero.MaxHealth;

			ComeBackButton.onClick.AddListener(delegate{OnClickComeBack();});
		}

	}

	public void DisplayMissionInfo(FreeRoamClass setfrc){
		frc = setfrc;
		this.gameObject.SetActive(true);
		this.transform.Find("MissionInfo").gameObject.SetActive(true);
		this.transform.Find("MissionResult").gameObject.SetActive(false);
		Debug.Log(setfrc.AppliedHero.Name + " : setfrc");
		HeroName.text = setfrc.AppliedHero.Name;
		missionLog.text = setfrc.MissionLog;
		IEnumerator routine = RefleshDisplay(setfrc);
		StartCoroutine(routine);
	}

	IEnumerator RefleshDisplay (FreeRoamClass mctest) {
		while(mctest.ActiveFlg == true){
			missionLog.text = mctest.MissionLog;
			HPBar.fillAmount = (float)mctest.AppliedHero.Health / (float)mctest.AppliedHero.MaxHealth;
			yield return new WaitForSeconds(1);
		}
		DisplayButton();
	}

	private void DisplayButton(){
		button.SetActive(true);
		button.GetComponent<Button>().onClick.AddListener(delegate{showResult();});
	}

	private void showResult(){
		GameObject Result = this.transform.Find("MissionResult").gameObject;
		Result.SetActive(true);
		string resultmsg;
		if(frc.Success == true){
			resultmsg = "Mission Success";
		} else {
			resultmsg = "Mission Fail";
		}
		Result.transform.Find("Result").gameObject.GetComponent<Text>().text = resultmsg;
		Result.transform.Find("HP/Bar").gameObject.GetComponent<Image>().fillAmount = (float)frc.AppliedHero.Health / (float)frc.AppliedHero.MaxHealth;
		Result.transform.Find("Reward/Reward1").gameObject.GetComponent<Text>().text =  "Reward Space 1";
		Result.transform.Find("Reward/Reward2").gameObject.GetComponent<Text>().text = "Reward Space 2";

	}

	public void OnClickCloseButton(){
		this.gameObject.SetActive(false);
	}

	private void OnClickComeBack(){
		frc.IsBackFlag = true;
		Debug.LogWarning("Push Back Button");
	}

}
