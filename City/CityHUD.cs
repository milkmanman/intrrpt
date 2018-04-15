using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityHUD : MonoBehaviour {

	public Text Cash;
	public Text Intel;

	void Start () {
		RefreshResource();
	}

	public void RefreshResource () {
		int[] Resource = ResourceManager.Instance.HUD();
		Cash.text = Resource[0].ToString();
		Intel.text = Resource[1].ToString();
	}

	public void BackToBase () {
		Application.LoadLevel("Base");
	}
}
