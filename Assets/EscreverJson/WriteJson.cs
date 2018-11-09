using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class WriteJson : MonoBehaviour {
 
    
    //Classe que irá converter as variaveis locais para o json
    public Character player = new Character(7,"Austin the Wizzard", 1337,false,new int[] { 7,4,8,21,12,1566666 });

    //ClassePlayerTeste (classe não monobehaviour) de fora que vai converter suas proprias variaveis em json IDEM PARA O DE CIMA
    public ClassePlayerTeste scriptTest = new ClassePlayerTeste();

    //variavel que recebera as variaveis converrtidas em json
    JsonData playerJson;

	// Use this for initialization
	void Start () {

        //Atribuindo ao jsondata a classe
        playerJson = JsonMapper.ToJson(scriptTest);
        //debugando
        Debug.Log(playerJson);
        //Salvando em um arquivo(criando um arquivo na raiz dos assets chamado Player.json
        File.WriteAllText(Application.dataPath + "/Player.json", playerJson.ToString());


    }

    // Update is called once per frame
    void Update () {

        //Add Value
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerJson = JsonMapper.ToJson(scriptTest);
            scriptTest.number++;
            File.WriteAllText(Application.dataPath + "/Player.json", playerJson.ToString());
            Debug.Log(playerJson);

        }

    }

    public void SaveJson()
    {

        //Atribuindo ao jsondata a classe
        playerJson = JsonMapper.ToJson(scriptTest);
        //debugando
        Debug.Log(playerJson);
        //Salvando em um arquivo(criando um arquivo na raiz dos assets chamado Player.json
        File.WriteAllText(Application.dataPath + "/Player.json", playerJson.ToString());

    }
}

//Classe Charater (pode ser criada dentro ou fora daqui ela é acessada por tudo e por todos
public class Character
{

    public int id;
    public string name;
    public int health;
    public bool agressive;
    public int[] stats;

    public Character(int id,string name, int health,bool agressive, int[] stats)
    {

        this.id = id;
        this.name = name;
        this.health = health;
        this.agressive = agressive;
        this.stats = stats;

    }

}
