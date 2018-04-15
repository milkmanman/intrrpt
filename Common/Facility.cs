using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]

public class Facility : ScriptableObject {

	public string name;
	public Vector3 globalPos;
	public List<Prefabs> FacilityObject;

	[System.Serializable]
	public struct Prefabs	{
		public GameObject obj;
		public Vector3 pos;
		public Vector3 eul;
	}

}
