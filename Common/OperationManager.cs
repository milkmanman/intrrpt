using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationManager : SingletonMonoBehaviourFast<OperationManager> {

	public List<OperationMemberClass> OperationMembers;

	void Awake(){
		OperationMembers = LoadOperationMembers();
		Debug.Log("operation name : " + OperationMembers[0].Name);
		
	}

	List<OperationMemberClass> LoadOperationMembers () {
		OperationMembers = new List<OperationMemberClass>();
		for(int i = 1; i <= 6; i++){
			if(PlayerPrefs.GetString ("Operation" + i.ToString() + ".Name") != ""){
				OperationMemberClass Member = new OperationMemberClass();
				Member.Name = PlayerPrefs.GetString ("Operation" + i.ToString() + ".Name");
				Member.Gender = PlayerPrefs.GetString ("Operation" + i.ToString() + ".Gender");
				Member.Skin = PlayerPrefs.GetInt ("Operation" + i.ToString() + ".Skin");
				OperationMembers.Add(Member);
			}
		}
		return OperationMembers;
	}

}
