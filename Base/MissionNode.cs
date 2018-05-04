using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionNode : MonoBehaviour {

	public MissionClass missioncls;
	public HeroStatusClass appliedHero;

	Text level;
	Image icon;
	Button button;

	IEnumerator routine;

	void OnEnable () {
		if (routine != null){
			StartCoroutine(routine);
		}
	}

	public void StartMission(){
		level = this.gameObject.transform.Find("Level").GetComponent<Text>();
		icon = this.gameObject.transform.Find("Icon").GetComponent<Image>();
		button = this.gameObject.GetComponent<Button>();

		MissionManager.Instance.StartMission(appliedHero, missioncls);
		routine = RefleshDisplay();
		StartCoroutine(routine);
	}

	public void SetHero(HeroStatusClass getHeroClass){
		appliedHero = getHeroClass;
		getHeroClass.Status = 1;
	}

	public void InitButton () {
		button.onClick.RemoveAllListeners();
		icon.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		MissionUI mu = GameObject.Find("GUI/MissionUI").GetComponent<MissionUI>();
		button.onClick.AddListener(delegate{mu.OnMissionNodeClicked(missioncls);});
		button.onClick.AddListener(delegate{mu.HoldMissionNode = this.gameObject;});
	}


	IEnumerator RefleshDisplay () {
		button.onClick.RemoveAllListeners();
		icon.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		level.text = "Applying";
		while(missioncls.ActiveFlg == true){
			yield return new WaitForSeconds(1);
		}
		level.text = "Mssndn";
		icon.color = new Color(0f, 1.0f, 0f, 1.0f);
		MissionUI mu = GameObject.Find("GUI/MissionUI").GetComponent<MissionUI>();
		button.onClick.AddListener(delegate{mu.OnMissionButtonClickedFns(missioncls, appliedHero);});
		button.onClick.AddListener(delegate{mu.HoldMissionNode = this.gameObject;});
	}

}
