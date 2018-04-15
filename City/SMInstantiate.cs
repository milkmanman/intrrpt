using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMInstantiate : MonoBehaviour {

	public GameObject Arrow;
	private int count = 0;

	void Start () {
		StartCoroutine("ShortMissionCircle");
		StartCoroutine("ShortMissionCircle");
		StartCoroutine("ShortMissionCircle");
	}

	public void refresh () {
		StartCoroutine("ShortMissionCircle");
	}

	IEnumerator ShortMissionCircle () {

			count++;
			string prefabname = "Arrow" + count.ToString();
			int randomtime = Random.Range (4, 6);

			yield return new WaitForSeconds(randomtime);
			appearArrow(prefabname);
	}

	private void appearArrow(string prefabName) {
//		Vector3 placePosition = new Vector3(-8,22,-25);
		int xposition = Random.Range (-3, 5);
		int zposition = Random.Range (-3, 5);
		Vector3 placePosition = new Vector3(xposition * 5,22,-25 + (zposition * 2));

		GameObject test_prefab = (GameObject)Instantiate(
			Arrow,
			placePosition,
			Quaternion.Euler(-90, 90, 0)
		);
		test_prefab.name = prefabName;
	}

}
