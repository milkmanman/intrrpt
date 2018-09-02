using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class RestPhaseListSO : ScriptableObject {

	public List<RestPhaseProp> RestPhasePropList;

	[System.Serializable]
	public struct RestPhaseProp	{
		public List<RestLines> BeforeRestLines;
		public List<RestLines> AfterRestLines;
		public bool DoesEat;
		public string Take;
		public int Value;
	}

	[System.Serializable]
	public struct RestLines	{
		public string Who;
		public string What;
	}
}
