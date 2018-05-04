using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]

public class Mission : ScriptableObject {

	public int id;
	public int type;
	public string name;
	public int level;
	public int villainInfo;

	public enum RewardType {
	cash,
	medic,
	tech,
};

	public List<Rewards> RewardsList;

	public List<Villains> VillainsList;

	[System.Serializable]
	public struct Rewards	{
		public string rewardType;
		public int rewardValue;
	}

	[System.Serializable]
	public struct Villains	{
		public string villanType;
		public int villanNum;
	}

}
