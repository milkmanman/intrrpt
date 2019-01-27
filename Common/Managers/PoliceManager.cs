using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : SingletonMonoBehaviourFast<PoliceManager> {

	public PoliceClass PoliceMember1;
	public PoliceClass PoliceMember2;
	public PoliceClass PoliceMember3;
	public PoliceClass PoliceMember4;
	public PoliceClass PoliceMember5;

	public void RegisterPolice (PoliceClass police) {
		if (PoliceMember1 == null){
			PoliceMember1 = police;
		} else if (PoliceMember2 == null){
			PoliceMember2 = police;
		} else if (PoliceMember3 == null){
			PoliceMember3 = police;
		} else if (PoliceMember4 == null){
			PoliceMember4 = police;
		} else if (PoliceMember5 == null){
			PoliceMember5 = police;
		} else {
			Debug.Log("Error : Can't Registing Police");
		}

		ShowonConsole();
	}

	public void ShowonConsole () {
/*		if (PoliceMember1 != null){
			Debug.Log("police1 filled");
		}

		if (PoliceMember2 != null) {
			Debug.Log("police2 filled");
		}*/

//		Debug.Log("PoliceName : " + PoliceMember1.Name + "\nRole : " + PoliceMember1.Role + "\nCost : " + PoliceMember1.Cost + "\nFaith : " + PoliceMember1.Faith + "\nAbility : " + PoliceMember1.Ability);
//		Debug.Log("PoliceName : " + PoliceMember2.Name + "\nRole : " + PoliceMember2.Role + "\nCost : " + PoliceMember2.Cost + "\nFaith : " + PoliceMember2.Faith + "\nAbility : " + PoliceMember2.Ability);

	}


}
