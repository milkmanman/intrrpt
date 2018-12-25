using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(
  fileName = "MovePhaseSo", 
  menuName = "ScriptableObject/MovePhaseSo")
]
public class MovePhaseSo : MissionPhaseSo {

  //public string Type = "Move";
	public string Destination;
	public string BridgeMsg;

}
