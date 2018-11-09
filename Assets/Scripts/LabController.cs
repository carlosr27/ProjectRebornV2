using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;




public class LabController : MonoBehaviour {

    //variavel que recebera as variaveis converrtidas em json
    JsonData playerJson;

    public lastMonster_load savechanges;

    public string fileName;

    public string lastMonster;                                                                                                                                        

    // Use this for initialization
    void Start () {

        //VERIFICAR SE O ARQUIVO EXISTE
        if (File.Exists(Application.persistentDataPath +"/lastMonster.json"))
        {
           
            Debug.Log("Existe");
        }
        else
        {
            Debug.Log("Não Existe");
     
        }

        fileName = "lastMonster.json";

        //String "master" It contains all the text for json file.
        string str = Read();

        //Still do not get it right what this means, but it is the variable that understands that the string is a json object.
        JSONNode json = JSON.Parse(str);

        //Debug
        Debug.Log(json["monster_choose"]);

        //Get a string of monster choose, in laboratory
        lastMonster = json["monster_choose"];




    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.M))
        {
            lastMonster = "Mamute";
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastMonster = "Aguia";
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            lastMonster = "Peixe";
        }

        //Button to save and switch scene

        if (Input.GetKeyDown(KeyCode.Space))
        {

            savechanges = new lastMonster_load(lastMonster);
            playerJson = JsonMapper.ToJson(savechanges);
           
            //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
            File.WriteAllText((Application.persistentDataPath + "/lastMonster.json"), playerJson.ToString());

            Application.LoadLevel("Battle");
        }

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

}

public class lastMonster_load{

    public string monster_choose;

    public lastMonster_load(string monster)
    {
        this.monster_choose = monster;
    }

}


