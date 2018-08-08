using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotificationUI : MonoBehaviour {

	public GameObject field;
	public Text title;
	public Text body;

	IEnumerator holdCoroutine;


	/*private IEnumerator testMultiple (){

		yield return new WaitForSeconds(1f);

		Notice("title", "hoge hoge");

		yield return new WaitForSeconds(3f);

		Notice("title2", "hoge hoge");

	}*/


	public void Notice(string title, string body){
		holdCoroutine = NoticeProgless(title, body);
		StartCoroutine(holdCoroutine);
	}

	
	private IEnumerator NoticeProgless (string titlestr, string bodystr){

		title.text = titlestr;
		body.text = bodystr;

		Vector3 pos1 = field.transform.position;
		pos1.x -= 200;

		yield return new WaitForSeconds(1f);

		iTween.MoveTo (field, iTween.Hash(
		"position", pos1,
			"time", 0.5f,
			"oncomplete", "AnimationEnd",
			"oncompletetarget", this.gameObject,
			"easeType", "easeOutCubic"
		));

		yield return new WaitForSeconds(5f);
		pos1.x += 200;


		iTween.MoveTo (field, iTween.Hash(
			"position", pos1,
			"time", 0.7f,
			"oncomplete", "AnimationEnd",
			"oncompletetarget", this.gameObject,
			"easeType", "easeInOutCubic"
		));

	}

	public void ClickUI(){
		 StopCoroutine (holdCoroutine);
		 StartCoroutine("DevelopProgless");
		  
	}

	private IEnumerator DevelopProgless (){

		Vector3 pos1 = field.transform.position;
		pos1.x += 200;

		yield return new WaitForSeconds(0.1f);

		iTween.MoveTo (field, iTween.Hash(
			"position", pos1,
			"time", 0.4f,
			"oncomplete", "AnimationEnd",
			"oncompletetarget", this.gameObject,
			"easeType", "easeInOutCubic"
		));

	}
	
}
