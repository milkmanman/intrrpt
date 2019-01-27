using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OperationUI : MonoBehaviour {

	public IEnumerator RefleshNodeCoroutine;
	public UIActivator UiActive;

	public UITransition UITransite;

	public GameObject MemberNodeField;
	public RectTransform MemberNodePrefab;

	public GameObject ResearchNodeField;
	public RectTransform ResearchNodePrefab;

	public GameObject ResearchResultUI;

	public TextMeshProUGUI TechInfo;

	void Start () {
		//ShowResearchResultUI();

			CanvasOnEnabled();
			UiActive.OnEnabledDevelopUI += CanvasOnEnabled;
			OperationManager.Instance.ChangeInfosAction += refleshReserchedInfo;

	}

	private void CanvasOnEnabled(){
		Debug.Log("test");
		RefreshStuff();
	}

	private void RefreshStuff(){
		 foreach (Transform child in MemberNodeField.transform) {
			Debug.LogWarning("childname : " + child.gameObject.name);
			if(child.gameObject.name != "member_logo"){
     			GameObject.Destroy(child.gameObject);
			}
			refleshReserchedInfo();
 		}

		List<OperationMemberClass> MemberList = OperationManager.Instance.OperationMembers;
		for(int i = 1; i <= MemberList.Count; i++){
			var item = GameObject.Instantiate(MemberNodePrefab) as RectTransform;
			item.SetParent(MemberNodeField.transform, false);
			OperationMemberClass test =  new OperationMemberClass();
			test = MemberList[i-1];
			item.GetComponent<OperationMemberNode>().opr = test;
			item.GetComponent<OperationMemberNode>().RefleshNode();
		}
	}

	private void RemoveResearchNode(){
		foreach (Transform child in ResearchNodeField.transform) {
			 if(child.gameObject.name != "researching_logo"){
				GameObject.Destroy(child.gameObject);
			}
 		}
	}

	private void AddResearchNode () {
		RemoveResearchNode();

		List<ResearchClass> ResearchList = OperationManager.Instance.ResearchList;
		if(ResearchList != null){
		for(int i = 1; i <= ResearchList.Count; i++){
			var item = GameObject.Instantiate(ResearchNodePrefab) as RectTransform;
			item.SetParent(ResearchNodeField.transform, false);
			item.GetComponent<ResearchingNode>().o_ui = this;
			item.GetComponent<ResearchingNode>().rc = ResearchList[i - 1];
			item.GetComponent<ResearchingNode>().rc.nodeObj = item.gameObject;
			//item.GetComponent<ResearchingNode>().RefleshNode();
		}
		RefleshResearchNode();
		if(RefleshNodeCoroutine == null){
			RefleshNodeCoroutine = ResearchProgless();
			StartCoroutine(RefleshNodeCoroutine);
		}
		//RefleshNodeCoroutine = StartCoroutine("ResearchProgless");
		}
	}

	private void RefleshResearchNode () {
		foreach (Transform child in ResearchNodeField.transform) {
			if(child.gameObject.GetComponent<ResearchingNode>()){
    			child.gameObject.GetComponent<ResearchingNode>().RefleshNode();
			}

 		}
	}

	private IEnumerator ResearchProgless (){

		while(true){
			yield return new WaitForSeconds(1f);
			RefleshResearchNode();
		}
	}

	private void refleshReserchedInfo(){
		int techinfo = OperationManager.Instance.TechInfo;
		TechInfo.text = "Tech Info - " + techinfo.ToString();
	}

	public void StartResearchButton(){
		OperationManager.Instance.StartResearch();
		UITransite.UITransitioner(true);
		AddResearchNode();
	}

	public void ShowResearchResultUI(ResearchClass rc, GameObject test){
		ResearchResultUI.SetActive(true);
		ResearchResultUI.GetComponent<OperationUIResult>().Reflesh(rc);
		RemoveOneNode(test);
		//ResearchResultUI.GetComponent<OperationUIResult>().Reflesh();
	}

	public void CloseResearchResultUI(){
		ResearchResultUI.SetActive(false);
		ResearchClass rc = ResearchResultUI.GetComponent<OperationUIResult>().rc_result;
		OperationManager.Instance.IncreaseInfos(rc.InfoType, rc.InfoValue);
		OperationManager.Instance.RemoveOneResearch(rc);
	}

	private void RemoveOneNode(GameObject rc){
		if(rc != null){
			GameObject.Destroy(rc);
		}
	}

}
