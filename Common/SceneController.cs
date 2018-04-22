using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : SingletonMonoBehaviourFast<SceneController> {

	public string StartScene;

	void Start(){
		StartCoroutine (LoadStartScene());
	}

	private IEnumerator LoadStartScene(){
		yield return StartCoroutine (LoadSceneAndSetActive (StartScene));
	}

	public void LoadingScene (string sceneName){
		StartCoroutine (LoadScene(sceneName));
	}

	public IEnumerator LoadScene(string sceneName){ //usually use this
		yield return StartCoroutine(Fade());
		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

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
