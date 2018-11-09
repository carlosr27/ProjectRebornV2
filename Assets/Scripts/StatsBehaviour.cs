using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;

public class StatsBehaviour : MonoBehaviour {

	JsonData playerJson;
	
	public Monster_Create savechanges;

	public PlayerP saveExp;
	public string fileName;



	private GameObject 	MonstersCon,Mname, Mlevel, Mvida, Mener, Mtox, Mcons, Mfor, Mresis,
		LvlShow, LifeShow, EnerShow, ToxShow,Ppontos;
	public GameObject TMamute, TPeixe, TAguia;
	public string nome;	
	public int level, vida, ener, tox, cons, forc, resis, playerPoints;
	private int tmpCons, tmpFor, tmpResis, TTpoints, moneyP;
	public float escalaTotal = 1;

	// Use this for initialization
	void Start () {

		ListGameobjects ();
		GetPlayerPoint ();

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

	public void GetPlayerPoint()
	{
		fileName = "Player.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);
		moneyP = int.Parse(json["money"]);
		TTpoints = int.Parse(json["Exp"]);
		if (TTpoints > 100) 
		{
			while(TTpoints > 100)
			{
				TTpoints -= 100;
				playerPoints +=1;
			}
		}
	}

	private void ListGameobjects()
	{
		MonstersCon = GameObject.Find ("MonstersController");
		Mname = GameObject.Find ("Mname"); 
		Mlevel = GameObject.Find ("Mlevel"); 
		Mvida = GameObject.Find ("Mvida"); 
		Mener = GameObject.Find ("Mener"); 
		Mtox = GameObject.Find ("Mtox"); 
		Mcons = GameObject.Find ("Mcons"); 
		Mfor = GameObject.Find ("Mfor"); 
		Mresis = GameObject.Find ("Mresis");  
		LifeShow = GameObject.Find ("LifeShow"); 
		EnerShow = GameObject.Find ("EnerShow"); 
		ToxShow = GameObject.Find ("ToxShow"); 
		TAguia = GameObject.Find ("TAguia"); 
		TMamute = GameObject.Find ("TMamute"); 
		TPeixe = GameObject.Find ("TPeixe"); 
		Ppontos = GameObject.Find ("Ppontos"); 

	}


	public void AguiaValues()
	{
		fileName = "Aguia.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);

		nome = "Aguia";
		level = int.Parse(json["level"]);
		vida = int.Parse(json["total_health"]);
		ener = int.Parse(json["energy"]);
		tox = int.Parse(json["toxi"]);
		cons = int.Parse(json["health"]);;
		forc = int.Parse(json["damage"]);
		resis = int.Parse(json["resistance"]);
	}

	public void MamuteValues()
	{
		fileName = "Mamute.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);

		nome = "Mamute";
		level = int.Parse(json["level"]);
		vida = int.Parse(json["total_health"]);
		ener = int.Parse(json["energy"]);
		tox = int.Parse(json["toxi"]);
		cons = int.Parse(json["health"]);;
		forc = int.Parse(json["damage"]);
		resis = int.Parse(json["resistance"]);
	}
	public void PeixeValues()
	{
		fileName = "Peixe.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);

		nome = "Peixe";
		level = int.Parse(json["level"]);
		vida = int.Parse(json["total_health"]);
		ener = int.Parse(json["energy"]);
		tox = int.Parse(json["toxi"]);
		cons = int.Parse(json["health"]);;
		forc = int.Parse(json["damage"]);
		resis = int.Parse(json["resistance"]);
	}

	public void ShowValues()
	{
		Mname.GetComponent<TextMesh> ().text = nome.ToString ();
		Mlevel.GetComponent<TextMesh> ().text = level.ToString ();
		Mvida.GetComponent<TextMesh> ().text = vida.ToString ();
		Mener.GetComponent<TextMesh> ().text = ener.ToString ();
		Mtox.GetComponent<TextMesh> ().text = tox.ToString ();
		Mcons.GetComponent<TextMesh> ().text = cons.ToString ();
		Mresis.GetComponent<TextMesh> ().text = resis.ToString ();
		Mfor.GetComponent<TextMesh> ().text = forc.ToString ();
		Ppontos.GetComponent<TextMesh> ().text = playerPoints.ToString ();
	}


	public void AtualBars ()
	{
		ShowBars(LifeShow, vida);
		ShowBars(EnerShow, ener);
		ShowBars(ToxShow, tox);
		
	}

	private void ShowBars(GameObject obj,int bars )
	{

		float newScale = Mathf.Round(bars * 1)/100;
		Debug.Log (newScale);
		if(newScale < 0) {newScale = 0;}else if (newScale > 1) { newScale = 1; }
        obj.transform.localScale = new Vector3(newScale, 1,1);
	}


	public void AdicionaPcons()
	{
		if (playerPoints > 0) {
			tmpCons +=  1;
			playerPoints -= 1;
			Mcons.GetComponent<TextMesh>().text = (tmpCons + cons).ToString();
		}

		Ppontos.GetComponent<TextMesh>().text = playerPoints.ToString();
	}

	public void RemovePcons()
	{
		if (tmpCons > 0) {
			tmpCons -= 1;
			playerPoints += 1;
			if(tmpCons == 0)
			{
				Mcons.GetComponent<TextMesh>().text = cons.ToString();
			}else
			{
				Mcons.GetComponent<TextMesh>().text = (tmpCons + cons).ToString();

			}
			Ppontos.GetComponent<TextMesh>().text = playerPoints.ToString();
		}

	}

	public void AdicionaPfor()
	{
		if (playerPoints > 0) {
			tmpFor +=  1;
			playerPoints -= 1;
			Mfor.GetComponent<TextMesh>().text = (tmpFor + forc).ToString();
		}
		
		Ppontos.GetComponent<TextMesh>().text = playerPoints.ToString();
	}
	
	public void RemovePfor()
	{
		if (tmpFor > 0) {
			tmpFor -= 1;
			playerPoints += 1;
			if(tmpFor == 0)
			{
				Mfor.GetComponent<TextMesh>().text = forc.ToString();
			}else
			{
				Mfor.GetComponent<TextMesh>().text = (tmpFor + forc).ToString();
				
			}
			Ppontos.GetComponent<TextMesh>().text = playerPoints.ToString();
		}
		
	}

	public void AdicionaPres()
	{
		if (playerPoints > 0) {
			tmpResis +=  1;
			playerPoints -= 1;
			Mresis.GetComponent<TextMesh>().text = (tmpResis + resis).ToString();
		}
		
		Ppontos.GetComponent<TextMesh>().text = playerPoints.ToString();
	}
	
	public void RemovePres()
	{
		if (tmpResis > 0) {
			tmpResis -= 1;
			playerPoints += 1;
			if(tmpResis == 0)
			{
				Mresis.GetComponent<TextMesh>().text = resis.ToString();
			}else
			{
				Mresis.GetComponent<TextMesh>().text = (tmpResis + resis).ToString();
				
			}
			Ppontos.GetComponent<TextMesh>().text = playerPoints.ToString();
		}
		
	}

	public void Aplicar()
	{
		Ppontos.GetComponent<TextMesh> ().text = playerPoints.ToString ();

		int damage = tmpFor;
		int health = tmpCons;
		int resistance = tmpResis;
		int energy = ener;
		
		int total_damage = damage;
		int total_health = vida;
		int total_resistance = resistance;
		int total_energy = energy;

		savechanges = new Monster_Create(total_damage, total_health, total_resistance,total_energy, damage, health, resistance,level,energy, tox);
		playerJson = JsonMapper.ToJson(savechanges);

		if (MonstersCon.GetComponent<MonstersController> ().LastMonster == "Aguia") {
			File.WriteAllText ((Application.persistentDataPath + "/Aguia.json"), playerJson.ToString ());
		} else if (MonstersCon.GetComponent<MonstersController> ().LastMonster == "Mamute") {
			File.WriteAllText ((Application.persistentDataPath + "/Mamute.json"), playerJson.ToString ());
		} else if (MonstersCon.GetComponent<MonstersController> ().LastMonster == "Peixe") {
			File.WriteAllText ((Application.persistentDataPath + "/Peixe.json"), playerJson.ToString ());
		}
		
		saveExp = new PlayerP(TTpoints, moneyP);
		playerJson = JsonMapper.ToJson(saveExp);
		File.WriteAllText ((Application.persistentDataPath + "/Player.json"), playerJson.ToString ());


		resis += tmpResis;
		forc += tmpFor;
		cons += tmpCons;

		tmpCons = 0;
		tmpFor = 0;
		tmpResis = 0;

	}

	public void Cancelar()
	{
		playerPoints += tmpCons;
		playerPoints += tmpFor;
		playerPoints += tmpResis;

		Ppontos.GetComponent<TextMesh> ().text = playerPoints.ToString ();

		Mresis.GetComponent<TextMesh>().text = resis.ToString();
		Mcons.GetComponent<TextMesh>().text = cons.ToString();
		Mfor.GetComponent<TextMesh>().text = forc.ToString();
		
		tmpCons = 0;
		tmpFor = 0;
		tmpResis = 0;
	}

}


