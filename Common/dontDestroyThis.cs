using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyThis : MonoBehaviour {

	void Start(){
		DontDestroyOnLoad(this.gameObject);
	}
}
