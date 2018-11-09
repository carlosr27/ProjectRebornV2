using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using SimpleJSON;

public class ItensBehaviour : MonoBehaviour {

	//variavel que recebera as variaveis converrtidas em json
	JsonData playerJson;
	

	public string fileName;

	public GameObject itemText1,itemText2,itemText3,itemText4,itemText5,itemText6,
	Item1,Item2,Item3,Item4,Item5,Item6, DeNome, Detalhes;
	public int item1,item2,item3,item4,item5,item6;

	// Use this for initialization
	void Start () {
		ListGameobjects ();
		StartItens ();
		UpdateItens ();
		DeNome.SetActive (false);
		Detalhes.SetActive (false);

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

	private void ListGameobjects()
	{
		itemText1 = GameObject.Find ("ItemText1");
		itemText2 = GameObject.Find ("ItemText2");
		itemText3 = GameObject.Find ("ItemText3");
		itemText4 = GameObject.Find ("ItemText4");
		itemText5 = GameObject.Find ("ItemText5");
		itemText6 = GameObject.Find ("ItemText6");
		Item1 = GameObject.Find ("item1");
		Item2 = GameObject.Find ("item2");
		Item3 = GameObject.Find ("item3");
		Item4 = GameObject.Find ("item4");
		Item5 = GameObject.Find ("item5");
		Item6 = GameObject.Find ("item6");
		DeNome = GameObject.Find ("DeNome");
		Detalhes = GameObject.Find ("Detalhes");
	}


	public void StartItens ()
	{
		//CARREGAR ITENS
		fileName = "Itens.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);

		item1 = int.Parse(json["item1Qnt"]); item2 = int.Parse(json["item2Qnt"]); item3 = int.Parse(json["item3Qnt"]); 
		item4 = int.Parse(json["item4Qnt"]); item5 = int.Parse(json["item5Qnt"]); item6 = int.Parse(json["item6Qnt"]);
	}

	public void UpdateItens()
	{
		itemText1.GetComponent<TextMesh> ().text = item1.ToString();
		itemText2.GetComponent<TextMesh> ().text = item2.ToString();
		itemText3.GetComponent<TextMesh> ().text = item3.ToString();
		itemText4.GetComponent<TextMesh> ().text = item4.ToString();
		itemText5.GetComponent<TextMesh> ().text = item5.ToString();
		itemText6.GetComponent<TextMesh> ().text = item6.ToString();
	}

	public void ItensDisable()
	{
		Item1.GetComponent<BoxCollider2D> ().enabled = false;
		Item2.GetComponent<BoxCollider2D> ().enabled = false;
		Item3.GetComponent<BoxCollider2D> ().enabled = false;
		Item4.GetComponent<BoxCollider2D> ().enabled = false;
		Item5.GetComponent<BoxCollider2D> ().enabled = false;
		Item6.GetComponent<BoxCollider2D> ().enabled = false;

	}
	public void ItensEnable()
	{
		Item1.GetComponent<BoxCollider2D> ().enabled = true;
		Item2.GetComponent<BoxCollider2D> ().enabled = true;
		Item3.GetComponent<BoxCollider2D> ().enabled = true;
		Item4.GetComponent<BoxCollider2D> ().enabled = true;
		Item5.GetComponent<BoxCollider2D> ().enabled = true;
		Item6.GetComponent<BoxCollider2D> ().enabled = true;
		
	}

	public void ItemDescricao(string itemClicked)
	{
		DeNome.SetActive(true);
		Detalhes.SetActive(true);
		if (itemClicked == "item1") {
			DeNome.GetComponent<TextMesh> ().text = "Adrenalina";
			Detalhes.GetComponent<TextMesh> ().text = "Energia(+50)"+"\n"+"Toxina(+5)";
		}else if (itemClicked == "item2") {
			DeNome.GetComponent<TextMesh> ().text = "Anabolizante";
			Detalhes.GetComponent<TextMesh> ().text = "Resistencia(+3)"+"\n"+"Forca(+1)"+"\n"+"Toxina(+40)";
		}else if (itemClicked == "item3") {
			DeNome.GetComponent<TextMesh> ().text = "Analgesico";
			Detalhes.GetComponent<TextMesh> ().text = "Vida(+5)";
		}else if (itemClicked == "item4") {
			DeNome.GetComponent<TextMesh> ().text = "Anti-Toxina";
			Detalhes.GetComponent<TextMesh> ().text = "Toxina(-20)";
		}else if (itemClicked == "item5") {
			DeNome.GetComponent<TextMesh> ().text = "Endorfina";
			Detalhes.GetComponent<TextMesh> ().text = "Vida(+20)"+"\n"+"Toxina(+10)";
		}else if (itemClicked == "item6") {
			DeNome.GetComponent<TextMesh> ().text = "Proteina"+"\n"+"Liquida";
			Detalhes.GetComponent<TextMesh> ().text = "Forca(+5)"+"\n"+"Constituicao(+1)"+"\n"+"Toxina(+70)";
		}
	}
	

}


