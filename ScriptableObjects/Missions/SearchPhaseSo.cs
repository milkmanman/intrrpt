using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(
	fileName = "SearchPhaseSo", 
	menuName = "ScriptableObject/SearchPhaseSo")
]
public class SearchPhaseSo : MissionPhaseSo {

	//public string Type = "Battle";

	public string Object;

}
