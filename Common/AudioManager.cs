using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class AudioManager : SingletonMonoBehaviourFast<AudioManager> {

	public List<AudioClip> BGMList;
  public List<AudioClip> OneshotSEList;
	public List<AudioClip> IntervalList;

	public AudioSource bgmSource;
	public AudioSource seSource;
	public AudioSource intervalSource;

	private int forwardBGMNo = 2017;

	void Start () {

		int a = Random.Range(0, 2);
		if(a == 1){
			PlayBGM();
		} else {
			PlayInterval();
		}
	}

	void OnSceneLoaded( Scene scene, LoadSceneMode mode ){
		if(scene.name == "Base"){
			bgmSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		} else if(scene.name == "City"){

		}
    }



	public void PlayBGM(){
		int bgmNo = Random.Range(0, BGMList.Count);
		while(bgmNo == forwardBGMNo){
			bgmNo = Random.Range(0, BGMList.Count);
		}

		bgmSource.clip = BGMList[bgmNo];
		bgmSource.Play();
		StartCoroutine(IntervalEnd(bgmSource.clip.length, true));
		forwardBGMNo = bgmNo;
	}

	public void PlaySE(int seNo){
		seSource.clip = OneshotSEList[seNo];
		seSource.Play();
	}

	private void PlayInterval(){
		int intervalNo = Random.Range(0, IntervalList.Count);

		intervalSource.clip = IntervalList[intervalNo];
		intervalSource.Play();
		StartCoroutine(IntervalEnd(intervalSource.clip.length, false));

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
