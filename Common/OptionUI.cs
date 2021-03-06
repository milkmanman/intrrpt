﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour {

	public AudioMixer test;
	public Slider BGM;
	public Slider SE;

	/*void Start(){
		LoadBGM();
	}*/

	public void SetBGMVol(float bgmVolume) {
		test.SetFloat("BGMVol", bgmVolume);
	}
	public void SetSEVol(float seVolume) {
		test.SetFloat("SEVol", seVolume);
	}

	/*public void SaveBGM(){
		PlayerPrefs.SetFloat("Audio.BGMVol", BGM.value);
		PlayerPrefs.SetFloat("Audio.SEVol", SE.value);
	}*/

	/*public void LoadBGM(){
		test.SetFloat("BGMVol", PlayerPrefs.GetFloat("Audio.BGMVol"));
		test.SetFloat("SEVol", PlayerPrefs.GetFloat("Audio.SEVol"));
	}*/

	public void OnSaveButtonClicked(){
		AudioManager.Instance.SaveSetting(BGM.value, SE.value);
		//SaveBGM();
	}

	public void OnBackButtonClicked(){
		if(SceneController.Instance.CurrentScene == "Base"){
			Debug.Log("testtestestset");
			GameObject.Find("GUI").GetComponent<UIActivator>().Activator(0);
		} else if(SceneController.Instance.CurrentScene == "Start"){
			this.gameObject.GetComponent<Canvas>().enabled = false;	
		}	
	}

}
