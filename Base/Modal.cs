using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modal : MonoBehaviour {

	public Text MessageTitle;
	public Text MEssageDescription;
	public Button AcceptButton;

	public void RefleshMessage(string title, string message){
		MessageTitle.text = title;
		MEssageDescription.text = message;
	}
}
