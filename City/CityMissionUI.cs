using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityMissionUI : MonoBehaviour {

	public Text missionLog;
	private Text HeroName;
	public GameObject MissionInfo;
	private int maxHealth;
	private Image HPBar;
	public GameObject button;
	public MissionClass mc;

	// Use this for initialization
	void Awake () {
		if(MissionManager.Instance.MissionList[0] != null){
			Debug.Log("MissionList[0]" + " - isnotnull");
			Debug.Log(MissionManager.Instance.MissionList[0].GetType());
			mc = MissionManager.Instance.MissionList[0];
			HeroName = MissionInfo.transform.Find("HeroName").GetComponent<Text>();
			HPBar = MissionInfo.transform.Find("HP/Bar").GetComponent<Image>();
			HPBar.fillAmount = 1.0f;
			HeroName.text = mc.AppliedHero.Name;
			maxHealth = mc.AppliedHero.MaxHealth;
		}

		/*if(mc != null){
			battleLog.text = MissionManager.Instance.slot1.CombatLog;
			re = MissionManager.Instance.slot1.RemainVillains;
			remainEnemies.text = "Remain Enemies : " + re.ToString();
			IEnumerator routine = RefleshDisplay(mc);
			StartCoroutine(routine);
		}*/
	}

	public void DisplayMissionInfo(MissionClass setmc){
		mc = setmc;
		this.gameObject.SetActive(true);
		this.transform.Find("MissionInfo").gameObject.SetActive(true);
		this.transform.Find("MissionResult").gameObject.SetActive(false);
		Debug.Log(setmc.AppliedHero.Name + " : setmc");
		HeroName.text = setmc.AppliedHero.Name;
		missionLog.text = setmc.MissionLog;
		IEnumerator routine = RefleshDisplay(setmc);
		StartCoroutine(routine);
	}

	IEnumerator RefleshDisplay (MissionClass mctest) {
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
		if(mc.Success == true){
			resultmsg = "Mission Success : " + mc.Name;
		} else {
			resultmsg = "Mission Fail : " + mc.Name;
		}
		Result.transform.Find("Result").gameObject.GetComponent<Text>().text = resultmsg;
		Result.transform.Find("HP/Bar").gameObject.GetComponent<Image>().fillAmount = (float)mc.AppliedHero.Health / (float)mc.AppliedHero.MaxHealth;
		Result.transform.Find("Reward/Reward1").gameObject.GetComponent<Text>().text =  mc.Reward1 + " : " + mc.Reward1val.ToString();
		if(mc.Reward2 != null){
			Result.transform.Find("Reward/Reward2").gameObject.GetComponent<Text>().text =  mc.Reward2 + " : " + mc.Reward2val.ToString();
		}

	}

	public void OnClickCloseButton(){
		this.gameObject.SetActive(false);
	}

}
