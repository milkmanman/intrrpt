﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class SceneController : SingletonMonoBehaviourFast<SceneController> {

	public string StartScene;
	public string CurrentScene;
	public Action ChangeScene;

	void Start(){
		StartCoroutine (LoadStartScene());
		CurrentScene = StartScene;
	}

	private IEnumerator LoadStartScene(){
		yield return StartCoroutine (LoadSceneAndSetActive (StartScene));
		if(ChangeScene != null){
			ChangeScene();
		}
	}

	public void LoadingScene (string sceneName){
		StartCoroutine (LoadScene(sceneName));
		CurrentScene = sceneName;
	}

	public IEnumerator LoadScene(string sceneName){ //usually use this
		yield return StartCoroutine(Fade());
		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));
		if(ChangeScene != null){
			ChangeScene();
		}

	}

	private IEnumerator LoadSceneAndSetActive (string sceneName)
	{


			yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
			Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

			SceneManager.SetActiveScene (newlyLoadedScene);
	}

	private IEnumerator Fade ()
	{
		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
	}
}
