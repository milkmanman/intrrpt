using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
  fileName = "FreeroamSo", 
  menuName = "ScriptableObject/FreeroamSo")
]
public class FreeroamSo : ScriptableObject {

	public string RewardType;
	public int RewardValue;
	public List<MissionPhaseSo> MissionList;

}
