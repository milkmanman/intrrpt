using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GeneralHelper : SingletonMonoBehaviourFast<GeneralHelper>{

	private GameObject NotificationUI;

	void Start(){
		NotificationUI = GameObject.Find("GUI/Notification");
	}


	public void NoticeUI(string title,string body){
		NotificationUI.GetComponent<NotificationUI>().Notice(title, body);
	}


    public void SaveList<T>(string key , List<T> value){
        string serizlizedList = Serialize<List<T>> (value);
        PlayerPrefs.SetString (key, serizlizedList);
    }


    public void SaveDict<Key, Value>(string key , Dictionary<Key, Value> value){
        string serizlizedDict = Serialize<Dictionary<Key, Value>> (value);
        PlayerPrefs.SetString (key, serizlizedDict);
    }


    public List<T> LoadList<T> (string key){
        //keyがある時だけ読み込む
        if (PlayerPrefs.HasKey (key)) {
            string serizlizedList = PlayerPrefs.GetString (key);
            return Deserialize<List<T>> (serizlizedList);
        }

        return new List<T> ();
    }


    public Dictionary<Key, Value> LoadDict<Key, Value> (string key){
        //keyがある時だけ読み込む
        if (PlayerPrefs.HasKey (key)) {
            string serizlizedDict = PlayerPrefs.GetString (key);
            return Deserialize<Dictionary<Key, Value>> (serizlizedDict);
        }

        return new Dictionary<Key, Value> ();
    }


    private string Serialize<T> (T obj){
        BinaryFormatter binaryFormatter = new BinaryFormatter ();
        MemoryStream    memoryStream    = new MemoryStream ();
        binaryFormatter.Serialize (memoryStream , obj);
        return Convert.ToBase64String (memoryStream   .GetBuffer ());
    }

    private T Deserialize<T> (string str){
        BinaryFormatter binaryFormatter = new BinaryFormatter ();
        MemoryStream    memoryStream    = new MemoryStream (Convert.FromBase64String (str));
        return (T)binaryFormatter.Deserialize (memoryStream);
    }

}
