using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;



public class AudioManager : SingletonMonoBehaviourFast<AudioManager> {

  public List<AudioClip> OneshotSEList;

	public List<AudioClip> StartBgmList;

	public List<AudioClip> BaseBgmList;
	public List<AudioClip> BaseIntervalList;

	public List<AudioClip> CityBgmList;

	public AudioSource bgmSource;
	public AudioSource seSource;
	public AudioSource intervalSource;

	public AudioMixer MasterMixer;

	private int forwardBGMNo = 2017;

	void Awake () {
		SceneController.Instance.ChangeScene += OnNewSceneLoaded;
		OnNewSceneLoaded();
		LoadSetting();
	}

	void OnDestroy () {
		SceneController.Instance.ChangeScene -= OnNewSceneLoaded;
	}

	public void OnNewSceneLoaded(){

		InitializeSources();		

		if(SceneController.Instance.CurrentScene == "Base"){
			BgmOnBase();
		} else if(SceneController.Instance.CurrentScene == "City"){
			BgmOnCity();
		} else if(SceneController.Instance.CurrentScene == "Start"){
			BgmOnStart();
		}

	
    }

	private void InitializeSources(){
	bgmSource.Stop();
	seSource.Stop();
	intervalSource.Stop();
	}


	public void SaveSetting(float bgm, float se){
		PlayerPrefs.SetFloat("Audio.BGMVol", bgm);
		PlayerPrefs.SetFloat("Audio.SEVol", se);
	}

	public void LoadSetting(){
		MasterMixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("Audio.BGMVol"));
		MasterMixer.SetFloat("SEVol", PlayerPrefs.GetFloat("Audio.SEVol"));
	}



	private void BgmOnBase(){
		Debug.LogWarning("warning");
		int a = Random.Range(0, 2);
		if(a == 1){
			PlayBGM();
		} else {
			PlayInterval();
		}
	}

	private void BgmOnCity(){
		PlayCityBGM();
	}

	private void BgmOnStart(){
		PlayStartBGM();
	}

	private void PlayCityBGM(){
		int bgmNo = Random.Range(0, CityBgmList.Count);
		intervalSource.clip = CityBgmList[bgmNo];
		intervalSource.Play();
	}


	private void PlayStartBGM(){
		int bgmNo = Random.Range(0, StartBgmList.Count);
		intervalSource.clip = StartBgmList[bgmNo];
		intervalSource.Play();
	}

	public void PlaySE(int seNo){
		seSource.clip = OneshotSEList[seNo];
		seSource.Play();
	}

	private void PlayInterval(){
		int intervalNo = Random.Range(0, BaseIntervalList.Count);

		intervalSource.clip = BaseIntervalList[intervalNo];
		intervalSource.Play();
		StartCoroutine(IntervalEnd(intervalSource.clip.length, false));

	}

	public void PlayBGM(){

		int bgmNo = Random.Range(0, BaseBgmList.Count);
		while(bgmNo == forwardBGMNo){
			bgmNo = Random.Range(0, BaseBgmList.Count);
		}

		bgmSource.clip = BaseBgmList[bgmNo];
		bgmSource.Play();
		StartCoroutine(IntervalEnd(bgmSource.clip.length, true));
		forwardBGMNo = bgmNo;
		
	}

	IEnumerator IntervalEnd (float length, bool isbgm) {
		length = length - 0.2f;
		yield return new WaitForSeconds(length);
		if(isbgm == true){
			PlayInterval();
		} else {
			PlayBGM();
		}
	}

}
