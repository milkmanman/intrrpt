using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(
  fileName = "TalkPhaseSo", 
  menuName = "ScriptableObject/TalkPhaseSo")
]
public class TalkPhaseSo : MissionPhaseSo {

  //public string Type = "Talk";

	public List<TalkLine> TalkLines;

	[System.Serializable]
	public struct TalkLine	{
		public string Who;
		public string What;
	}
}
