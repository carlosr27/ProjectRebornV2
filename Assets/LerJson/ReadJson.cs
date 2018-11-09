using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

public class ReadJson : MonoBehaviour {


    // file name
    public string fileName;

    void Start() {

        //String "master" It contains all the text for json file.
        string str = Read();

        //Still do not get it right what this means, but it is the variable that understands that the string is a json object.
        JSONNode json = JSON.Parse(str);

        //get simple String

        string nameStr = json["name"].Value;
        Debug.Log("Name: " + nameStr);

        //Take a string, on json file and convert to int.
        int StringToInt = int.Parse(json["age"].Value);

        Debug.Log("Age: " + StringToInt);
        

        //get a simple string or value in array or not the model is (json["name"] + case there other data add "[]" if there vector inside of "[]" add a number of index.

        string val = json["data"][0]["name"].Value;
        Debug.Log(val);

	
	}

        //Function for reader the json and return a string
        string Read()
        {

        /*Set the JSON file directory (in this case, the file is in Assets/). If you download file, upgrade this line for "/JsonFileReader/" if you paste this folder in
        Assets file. */
        StreamReader sr = new StreamReader(Application.dataPath + "/LerJson/" + fileName);
        string content = sr.ReadToEnd();
        sr.Close();

        return content;

        }
}
