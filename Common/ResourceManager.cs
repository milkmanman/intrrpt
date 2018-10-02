using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonMonoBehaviourFast<ResourceManager> {

	public enum Type {
		Cash, Develop, Medic, Tech
	}

	public int Cash = 1000;
	public int Intel = 200;
	public int Tech = 1000;
	private int Medic = 3000;
	private int Tailor = 4000;
	private List<string> ResourceLog = new List<string>();


	public void VulkResource(Dictionary<string, int> HoldResources){
		foreach(KeyValuePair<string, int> pair in HoldResources){
			var Values = Enum.GetValues(typeof(Type));
    		foreach (Type value in Values){
        		var strValue = value.ToString();
        		if(strValue ==  pair.Key){
					IncreaseResource(pair.Key, pair.Value);
        		}
    		}
		}
		Debug.LogWarning("Vulked Increase Resource!");
	}


	public void IncreaseResource(string ResourceType, int value) {
		if (ResourceType == "Cash") {
			Cash += value;
		} else if (ResourceType == "Intel"){
			Intel += value;
		} else if (ResourceType == "Tech"){
			Tech += value;
		} else if (ResourceType == "Medic"){
			Medic += value;
		} else if (ResourceType == "Tailor"){
			Tailor += value;
		} else {
			Debug.LogWarning("Invalid ResourceType : " + ResourceType);
		}

		ShortResourceLog(ResourceType, value, true);

	}

	public void DecreaseResource(string ResourceType, int value) {
		if (ResourceType == "Cash") {
			Cash -= value;
		} else if (ResourceType == "Intel"){
			Intel -= value;
		} else if (ResourceType == "Tech"){
			Tech -= value;
		} else if (ResourceType == "Medic"){
			Medic -= value;
		} else if (ResourceType == "Tailor"){
			Tailor -= value;
		} else {
			Debug.LogWarning("Invalid ResourceType : " + ResourceType);
		}

		ShortResourceLog(ResourceType, value, false);

	}

	public void ShowonConsole () {
		Debug.Log("Cash : " + Cash  + "\nIntel : "+ Intel +"\nTech : "+ Tech +"\nMedic : "+ Medic + "\nTailor : " + Tailor);
	}

	public int[] HUD () {
		int[] a = new int[5];
		a[0] = Cash;
		a[1] = Intel;
		a[2] = Tech;
		a[3] = Medic;
		a[4] = Tailor;
		return a;
	}

	public int showResource(string Type){
		int rtnInt;
		switch(Type){
			case "Cash":
				rtnInt = Cash;
				break;
			case "Tech":
				rtnInt = Tech;
				break;
			default:
				rtnInt = 0;
				break;
		}

		return rtnInt;
	}

	private void ShortResourceLog (string ResourceType, int value, bool plus) {
		string posinega = "+";
		if(plus == false) posinega = "-" ;
		ResourceLog.Add(ResourceType + " : " + posinega + value);
		if(ResourceLog.Count == 21) ResourceLog.RemoveAt(1);
	}

	public void LogonConsole () {
		int count = ResourceLog.Count;
		string logs = "";
		if(ResourceLog.Count != 0){
			for(int i = 0; count - 1 >= i; i++){
				logs += ResourceLog[i];
				logs += "\n";
			}
		} else {
			logs += "No Logs";
		}
		Debug.Log(logs);
	}

}
