using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;

// O QUE FALTA: FAZER A CRIAÇÃO DO ARQUIVO DA AGUIA, E PEIXE
public class LoadFilesController : MonoBehaviour {

	private float changeTimer = 5.0f;
    //FOR CREATE LASTMONSTER.JSON
    public string lastMonster;
    public lastMonster_Create lastMonster_Create;

    //FOR CREATE MONSTERSBEHAVIOUR.json(Mamute.json, Aguia.json)
    public int total_damage;
    public int total_health;
    public int total_resistance;
    public int total_energy;
    public int damage;
    public int energy;
    public int health;
    public int resistance;
    public int level;
	public int toxi;
    public Monster_Create Monster_Create;

    //FOR CREATE ITENS.json
	//itens para injetar
    public int item1Qnt;
	public int item2Qnt;
	public int item3Qnt;
	public int item4Qnt;
	public int item5Qnt;
	public int item6Qnt;

	public Itens Itens;

	//FOR CREATE Materiais.json
	//Materiais
	public int mate1qnt;
	public int mate2qnt;
	public int mate3qnt;
	public int mate4qnt;
	public int mate5qnt;
	public int mate6qnt;
	
	public Materiais Materiais;

	//FOR CREATE Player.json
	//Materiais
	public int Exp;
	public int dindin;

	public PlayerP PlayerP;

    //variavel que recebera as variaveis converrtidas em json
    JsonData playerJson;


	void Start()
	{
		lastMonster_load();
		Mamute_load();
		Peixe_Load();
		Aguia_Load();
		Itens_load();
		Player_load();
		Materiais_load ();
        Application.LoadLevel("MonstersAdm");
		Debug.Log(Application.persistentDataPath);
		
	}
	void Update () {
		
		changeTimer -= Time.deltaTime;
		if(changeTimer <= 0f)
		{
			Application.LoadLevel("MonstersAdm");
		}
	}

	public void Player_load()
	{
		//VERIFICAR SE O ARQUIVO EXISTE
		if (File.Exists(Application.persistentDataPath + "/Player.json"))
		{
			Debug.Log("Player.json exist");
		}
		else
		{
			Debug.Log("Player.json do not exist");
			Exp = 0;
			dindin = 500;
			
			PlayerP = new PlayerP(Exp, dindin);
			playerJson = JsonMapper.ToJson(PlayerP);
			//File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
			File.WriteAllText((Application.persistentDataPath + "/Player.json"), playerJson.ToString());
		}
	}

    public void Mamute_load()
    {
        //VERIFICAR SE O ARQUIVO EXISTE
        if (File.Exists(Application.persistentDataPath + "/Mamute.json"))
        {
            Debug.Log("Mamute.json exist");
        }
        else
        {
            Debug.Log("Mamute.json do not exist");
            damage = 22;
            health = 120;
            resistance = 10;
            energy = 100;
			toxi = 0;
            total_damage = damage;
            total_health = health;
            total_resistance = resistance;
            total_energy = energy;

            level = 1;

			Monster_Create = new Monster_Create(total_damage, total_health, total_resistance, total_energy, damage, health, resistance, level, energy, toxi);
            playerJson = JsonMapper.ToJson(Monster_Create);
            //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
            File.WriteAllText((Application.persistentDataPath + "/Mamute.json"), playerJson.ToString());
        }
    }

    public void Aguia_Load()
    {
        //VERIFICAR SE O ARQUIVO EXISTE
        if (File.Exists(Application.persistentDataPath + "/Aguia.json"))
        {
            Debug.Log("Aguia.json exist");
        }
        else
        {
            Debug.Log("Aguia.json do not exist");
            damage = 25;
            health = 80;
            resistance = 12;
            energy = 100;
			toxi = 0;

            total_damage = damage;
            total_health = health;
            total_resistance = resistance;
            total_energy = energy;

            level = 1;

			Monster_Create = new Monster_Create(total_damage, total_health, total_resistance, total_energy, damage, health, resistance, level, energy, toxi);
            playerJson = JsonMapper.ToJson(Monster_Create);
            //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
            File.WriteAllText((Application.persistentDataPath + "/Aguia.json"), playerJson.ToString());
        }
    }

    public void Peixe_Load()
    {
        //VERIFICAR SE O ARQUIVO EXISTE
        if (File.Exists(Application.persistentDataPath + "/Peixe.json"))
        {
            Debug.Log("Aguia.json exist");
        }
        else
        {
            Debug.Log("Aguia.json do not exist");
            damage = 15;
            health = 120;
            resistance = 30;
            energy = 100;
			toxi = 0;
            total_damage = damage;
            total_health = health;
            total_resistance = resistance;
            total_energy = energy;
            
            level = 1;

			Monster_Create = new Monster_Create(total_damage, total_health, total_resistance,total_energy, damage, health, resistance,level,energy,toxi);
            playerJson = JsonMapper.ToJson(Monster_Create);
            //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
            File.WriteAllText((Application.persistentDataPath + "/Peixe.json"), playerJson.ToString());
        }
    }

    public void lastMonster_load()
    {
        //VERIFICAR SE O ARQUIVO EXISTE
        if (File.Exists(Application.persistentDataPath + "/lastMonster.json"))
        {

            Debug.Log("LastMonster.json exist");
           
        }
        else
        {
            Debug.Log("LastMonster.json do not exist");
            lastMonster = "Aguia";
            lastMonster_Create = new lastMonster_Create(lastMonster);
            playerJson = JsonMapper.ToJson(lastMonster_Create);
            //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
            File.WriteAllText((Application.persistentDataPath + "/lastMonster.json"), playerJson.ToString());
           
        }
    }

    public void Itens_load()
    {

        //VERIFICAR SE O ARQUIVO EXISTE
        if (File.Exists(Application.persistentDataPath + "/Itens.json"))
        {
            Debug.Log("Itens.json exist");
        }
        else
        {
            Debug.Log("Itens.json do not exist");

			item1Qnt = 2;
			item2Qnt = 2;
			item3Qnt = 0;
			item4Qnt = 0;
			item5Qnt = 0;
			item6Qnt = 0;

			Itens = new Itens(item1Qnt, item2Qnt, item3Qnt, item4Qnt, item5Qnt, item6Qnt);
			playerJson = JsonMapper.ToJson(Itens);
            //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
            File.WriteAllText((Application.persistentDataPath + "/Itens.json"), playerJson.ToString());
        }
    }

	public void Materiais_load()
	{
		
		//VERIFICAR SE O ARQUIVO EXISTE
		if (File.Exists(Application.persistentDataPath + "/Materiais.json"))
		{
			Debug.Log("Materiais.json exist");
		}
		else
		{
			Debug.Log("Materiais.json do not exist");
			
			mate1qnt = 0;
			mate2qnt = 0;
			mate3qnt = 0;
			mate4qnt = 0;
			mate5qnt = 0;
			mate6qnt = 0;
			
			Materiais = new Materiais(mate1qnt, mate2qnt, mate3qnt, mate4qnt, mate5qnt, mate6qnt);
			playerJson = JsonMapper.ToJson(Materiais);
			//File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
			File.WriteAllText((Application.persistentDataPath + "/Materiais.json"), playerJson.ToString());
		}
	}

}

public class lastMonster_Create
{
    public string monster_choose;

    public lastMonster_Create(string monster)
    {
        this.monster_choose = monster;
    }
}

public class Monster_Create
{
    public int total_damage;
    public int total_health;
    public int total_resistance;
    public int total_energy;
    public int damage;
    public int health;
    public int resistance;
    public int level;
    public int energy;
	public int toxi;

    public Monster_Create(int t_d, int t_h,int t_r,int t_e, int d, int h, int r, int l,int e,int t)
    {
        this.total_damage = t_d;
        this.total_health = t_h;
        this.total_resistance = t_r;
        this.total_energy = t_e;
        this.damage = d;
        this.health = h;
        this.resistance = r;
        this.energy = e;
        this.level = l;
		this.toxi = t;
    }
}

public class Itens
{
	public int item1Qnt;
	public int item2Qnt;
	public int item3Qnt;
	public int item4Qnt;
	public int item5Qnt;
	public int item6Qnt;

	public Itens(int item1, int item2, int item3, int item4, int item5, int item6)
    {
		this.item1Qnt = item1;
		this.item2Qnt = item2;
		this.item3Qnt = item3;
		this.item4Qnt = item4;
		this.item5Qnt = item5;
		this.item6Qnt = item6;
    }
}

public class Materiais
{
	public int mate1qnt;
	public int mate2qnt;
	public int mate3qnt;
	public int mate4qnt;
	public int mate5qnt;
	public int mate6qnt;
	
	public Materiais(int mat1, int mat2, int mat3, int mat4, int mat5, int mat6)
	{
		this.mate1qnt = mat1;
		this.mate2qnt = mat2;
		this.mate3qnt = mat3;
		this.mate4qnt = mat4;
		this.mate5qnt = mat5;
		this.mate6qnt = mat6;
	}
}

public class PlayerP
{
	public int Exp;
	public int money;
	
	public PlayerP(int Pex, int dindin)
	{
		this.Exp = Pex;
		this.money = dindin;

	}
}



