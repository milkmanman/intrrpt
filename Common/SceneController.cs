using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : SingletonMonoBehaviourFast<SceneController> {

	public string StartScene;

	void Start(){
		LoadCityScene();
	}

	public void LoadCityScene(){
		StartCoroutine (LoadSceneAndSetActive (StartScene));
	}

	private IEnumerator LoadSceneAndSetActive (string sceneName)
	{
			yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
			Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

			SceneManager.SetActiveScene (newlyLoadedScene);
	}
}
