﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour {

	public Button NewGame;
	public Button Option;
	public Button Exit;

	// Use this for initialization
	void Start () {
		SetButtons();
	}

	private void SetButtons(){
		NewGame.onClick.AddListener(delegate{Debug.Log("new game");});
		NewGame.onClick.AddListener(delegate{AudioManager.Instance.PlaySE(4);});
		NewGame.onClick.AddListener(delegate{LoadCityScene();});
		Option.onClick.AddListener(delegate{ShowOptionUI();});
		Option.onClick.AddListener(delegate{AudioManager.Instance.PlaySE(4);});

		Exit.onClick.AddListener(delegate{Application.Quit();});

	}



	public void LoadCityScene(){
		SceneController.Instance.LoadingScene("Base");
	}

	private IEnumerator LoadSceneAndSetActive (string sceneName)
	{
			yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
			Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

			SceneManager.SetActiveScene (newlyLoadedScene);
	}


	public void ShowOptionUI(){
		Debug.Log("OptionUI");
	}


}
