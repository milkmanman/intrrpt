using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityFreeroamUI : MonoBehaviour {

	public UIActivatorCity UiActive;

	public Text missionLog;
	private Text HeroName;
	public GameObject HeroStatus;
	private int maxHealth;
	private Image HealthBar;
	private Text HealthText;
	public GameObject button;
	public Button ComeBackButton;
	public FreeRoamClass frc;
	
	public TextMeshProUGUI ResourceText_tmp;
	public TextMeshProUGUI CurrentPhase_tmp;
	
	public MissionLogLine mll;

	void Awake () {

		HeroName = HeroStatus.transform.Find("HeroName").GetComponent<Text>();
		HealthBar = HeroStatus.transform.Find("Health/Bar").GetComponent<Image>();
		HealthBar.fillAmount = 1.0f;
		HealthText = HeroStatus.transform.Find("Health/Text").GetComponent<Text>();
		ComeBackButton.onClick.AddListener(delegate{OnClickComeBack();});

		/* if(MissionManager.Instance.FreeRoamList[0] != null){
			frc = MissionManager.Instance.FreeRoamList[0];
			mll.frc = MissionManager.Instance.FreeRoamList[0];
		}*/

		frc = null;

	/* 
		if(MissionManager.Instance.FreeRoamList[0] != null){
			Debug.Log("FreeRoamList[0]" + " - isnotnull");
			frc = MissionManager.Instance.FreeRoamList[0];
			HeroName = HeroStatus.transform.Find("HeroName").GetComponent<Text>();
			HealthBar = HeroStatus.transform.Find("Health/Bar").GetComponent<Image>();
			HealthBar.fillAmount = 1.0f;
			HealthText = HeroStatus.transform.Find("Health/Text").GetComponent<Text>();
			//HeroName.text = frc.AppliedHero.Name;
			//maxHealth = frc.AppliedHero.MaxHealth;

			mll.frc = MissionManager.Instance.FreeRoamList[0];
			//frc.PhaseMoveAction += mll.RefreshNewMissionLog;
			//frc.PushMissionLogAction += mll.RefleshLog;

			ComeBackButton.onClick.AddListener(delegate{OnClickComeBack();});
		}
	*/
	}

	public void DisplayMissionInfo(FreeRoamClass setfrc){
		//frc = setfrc;
		RefleshFrc(setfrc);
		HeroName.text = frc.AppliedHero.Name;
		maxHealth = frc.AppliedHero.MaxHealth;
		mll.frc = frc;
		mll.InitMissionLog();
		//this.gameObject.SetActive(true);
		this.gameObject.GetComponent<Canvas>().enabled = true;
		this.transform.Find("MissionInfo").gameObject.SetActive(true);
		this.transform.Find("MissionResult").gameObject.SetActive(false);
		Debug.Log(setfrc.AppliedHero.Name + " : setfrc");
		HeroName.text = setfrc.AppliedHero.Name;
		missionLog.text = setfrc.MissionLog;
		//IEnumerator routine = RefleshDisplay(setfrc);
		//StartCoroutine(routine);
	}

	private void RefleshFrc(FreeRoamClass setfrc){
		if(frc != null){
			frc.PhaseMoveAction -= mll.RefreshNewMissionLog;
			frc.PushMissionLogAction -= mll.RefleshLog;
			frc.PushMissionLogAction -= RefleshDisplays;
		}
		frc = setfrc;

		frc.PhaseMoveAction += mll.RefreshNewMissionLog;
		frc.PushMissionLogAction += mll.RefleshLog;
		frc.PushMissionLogAction += RefleshDisplays;
	}

	IEnumerator RefleshDisplay (FreeRoamClass mctest) {
		while(mctest.ActiveFlg == true){
			missionLog.text = mctest.MissionLog;
			HealthBar.fillAmount = (float)mctest.AppliedHero.Health / (float)mctest.AppliedHero.MaxHealth;
			HealthText.text = "Health : " + mctest.AppliedHero.Health.ToString() + "/" + mctest.AppliedHero.MaxHealth.ToString();
			yield return new WaitForSeconds(1);
			DisplayResource();
			DisplayCurrentMission();
		}
		DisplayButton();
	}

	public void RefleshDisplays () {
		if(frc != null){
			missionLog.text = frc.MissionLog;
			HealthBar.fillAmount = (float)frc.AppliedHero.Health / (float)frc.AppliedHero.MaxHealth;
			HealthText.text = "Health : " + frc.AppliedHero.Health.ToString() + "/" + frc.AppliedHero.MaxHealth.ToString();
			DisplayResource();
			DisplayCurrentMission();
		}
	}

	private void DisplayResource(){
		string resourceTxt = "";
		if(frc.HoldResources != null){
			foreach(KeyValuePair<string, int> pair in frc.HoldResources){
				resourceTxt = resourceTxt + pair.Key + " : " + pair.Value.ToString() + "\n";
			}
		}
		ResourceText_tmp.text = resourceTxt;
	}
	
	private void DisplayCurrentMission(){
		if(frc.PhaseListHistory != null){
			string currentMissionStr = frc.PhaseListHistory.Last().Type;
			CurrentPhase_tmp.text = currentMissionStr;
		}
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
		Result.transform.Find("Result_tmp").gameObject.GetComponent<TextMeshProUGUI>().text = resultmsg;

		Result.transform.Find("HP/Bar").gameObject.GetComponent<Image>().fillAmount = (float)frc.AppliedHero.Health / (float)frc.AppliedHero.MaxHealth;
		Result.transform.Find("Reward/Reward1").gameObject.GetComponent<Text>().text =  "Reward Space 1";
		Result.transform.Find("Reward/Reward2").gameObject.GetComponent<Text>().text = "Reward Space 2";

		MissionManager.Instance.FinishFreeroam(frc);

	}

	public void OnClickCloseButton(){
		this.gameObject.SetActive(false);
	}

	private void OnClickComeBack(){
		frc.IsBackFlag = true;
		Debug.LogWarning("Push Back Button");
	}

}
