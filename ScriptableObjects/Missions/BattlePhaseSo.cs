using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(
	fileName = "BattlePhaseSo", 
	menuName = "ScriptableObject/BattlePhaseSo")
]
public class BattlePhaseSo : MissionPhaseSo {

	//public string Type = "Battle";

	public List<VillainList> Villains;

	[System.Serializable]
	public struct VillainList	{
		public string Type;
		public string Count;
	}
}
