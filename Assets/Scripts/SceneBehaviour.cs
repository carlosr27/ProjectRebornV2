using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using SimpleJSON;

public class SceneBehaviour : MonoBehaviour {

	JsonData playerJson;
	
	public string fileName, LastMonster;

	public int Stats;

	// Use this for initialization
	void Start () {

		fileName = "lastMonster.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);
		
		//Get a string of monster choose, in laboratory
		LastMonster = json["monster_choose"];
		if (LastMonster == "Aguia") {
			fileName = "Aguia.json";
			
			//String "master" It contains all the text for json file.
			str = Read();
			
			//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
			json = JSON.Parse(str);
			
			//Get a string of monster choose, in laboratory
			Stats = int.Parse(json["energy"]);
		} else if (LastMonster == "Mamute") {
			fileName = "Mamute.json";
			
			//String "master" It contains all the text for json file.
			str = Read();
			
			//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
			json = JSON.Parse(str);
			
			//Get a string of monster choose, in laboratory
			Stats = int.Parse(json["energy"]);

		} else if (LastMonster == "Peixe") {
			fileName = "Peixe.json";
			
			//String "master" It contains all the text for json file.
			str = Read();
			
			//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
			json = JSON.Parse(str);
			
			//Get a string of monster choose, in laboratory
			Stats = int.Parse(json["energy"]);
		}
	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	string Read()
	{
		
		/*Set the JSON file directory (in this case, the file is in Assets/). If you download file, upgrade this line for "/JsonFileReader/" if you paste this folder in
        Assets file. */
		string filePath = (Application.persistentDataPath + "/"+fileName);
		StreamReader sr = new StreamReader(filePath);
		string content = sr.ReadToEnd();
		sr.Close();
		
		return content;
		
	}

	public void SceneBatalha() 
	{
		if (Application.loadedLevelName != "Battle") {
			if(Stats >= 10)
			{
				Application.LoadLevel ("Battle");
			}else
			{
				Debug.Log("Voce nao tem energia suficiente");
			}
		} else {
			Debug.Log("Voce ja esta nessa cena");
		}
	}

	public void SceneLoja()
	{
		if (Application.loadedLevelName != "Loja") {
			Application.LoadLevel("Loja");
		} else {
			Debug.Log("Voce ja esta nessa cena");
		}
	}

	public void SceneAdm()
	{
		if (Application.loadedLevelName != "MonstersAdm") {
			Application.LoadLevel("MonstersAdm");
		} else {
			Debug.Log("Voce ja esta nessa cena");
		}
	}

}
