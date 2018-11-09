using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;

public class LojaConstroller : MonoBehaviour {

	//variavel que recebera as variaveis converrtidas em json
	JsonData playerJson;
	
	public string fileName;
	
	[Header("AUDIO")]
	public AudioSource audio;

	private GameObject btnComprar, btnVender, btnFabricar, MenuComprar, MenuVender, Aviso, Warnning, 
	AtualCash;
	public int Pitem1, Pitem2, Pitem3, Pitem4, Pitem5 , Pitem6 ,
		Citem1, Citem2, Citem3, Citem4, Citem5, Citem6;
	public int currentMoney, expfake;

	// Use this for initialization
	void Start () {
		audio.Play();

		listGameobjects ();
		GetCurrentMoney();
		DisableGameObjects (MenuComprar);
		DisableGameObjects (MenuVender);
		DisableGameObjects (Aviso);

		fileName = "Itens.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);

		Pitem1 = int.Parse(json["item1Qnt"]); Pitem2 = int.Parse(json["item2Qnt"]); Pitem3 = int.Parse(json["item3Qnt"]); 
		Pitem4 = int.Parse(json["item4Qnt"]); Pitem5 = int.Parse(json["item5Qnt"]); Pitem6 = int.Parse(json["item6Qnt"]);

		fileName = "Materiais.json";
		
		//String "master" It contains all the text for json file.
		str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		json = JSON.Parse(str);

		Citem1 = int.Parse(json["mate1qnt"]); Citem2 = int.Parse(json["mate2qnt"]); Citem3 = int.Parse(json["mate3qnt"]);
		Citem4 = int.Parse(json["mate4qnt"]); Citem5 = int.Parse(json["mate5qnt"]); Citem6 = int.Parse(json["mate6qnt"]);
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
	
	public void GetCurrentMoney()
	{
		fileName = "Player.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);


		expfake = int.Parse(json["Exp"]);
		currentMoney = int.Parse(json["money"]);
		AtualCash.GetComponent<TextMesh> ().text = currentMoney.ToString ();
	}

	public void listGameobjects()
	{
		btnComprar = GameObject.Find ("btnComprar");
		btnVender = GameObject.Find ("btnVender");
		btnFabricar = GameObject.Find ("btnFabricar");
		MenuComprar = GameObject.Find ("MenuComprar");
		MenuVender = GameObject.Find ("MenuVender");
		Aviso = GameObject.Find ("Aviso");
		Warnning = GameObject.Find("Warnning");
		AtualCash = GameObject.Find("AtualCash");

	}

	private void DisableGameObjects(GameObject Obj)
	{		
		Obj.SetActive (false);
	}
	private void EnableGameObjects(GameObject Obj)
	{		
		Obj.SetActive (true);
	}	
	
	private void CollidersDisable (GameObject objeto)
	{
		BoxCollider2D Coll;
		Coll = objeto.GetComponent<BoxCollider2D> ();
		Coll.enabled = false;
	}
	private void CollidersEnable (GameObject objeto)
	{
		BoxCollider2D Coll;
		Coll = objeto.GetComponent<BoxCollider2D> ();
		Coll.enabled = true;
	}

	public void warnning()
	{
		Aviso.SetActive(true);
		Warnning.GetComponent<TextMesh>().text = "Voce nao tem"+"\n"+" dinheiro suficicente!";
		StartCoroutine(fecharAviso(1.5F));
	}

	public void warnning2()
	{
		Aviso.SetActive(true);
		Warnning.GetComponent<TextMesh>().text = "Voce nao escolheu"+"\n"+" nenhum item p/ vender";
		StartCoroutine(fecharAviso(1.5F));
	}
	
	
	IEnumerator fecharAviso(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		Aviso.SetActive(false);
	}


	public void menuComprar()
	{
		MenuComprar.SetActive (true);

		CollidersDisable (btnComprar);
		CollidersDisable (btnFabricar);
		CollidersDisable (btnVender);
	}
	public void menuVender()
	{
		MenuVender.SetActive (true);

		CollidersDisable (btnComprar);
		CollidersDisable (btnFabricar);
		CollidersDisable (btnVender);
	}


	public void Close()
	{
		if (MenuComprar.activeInHierarchy) {
			MenuComprar.GetComponent<MenuComprarBehaviour>().ZeraItens();
			MenuComprar.GetComponent<MenuComprarBehaviour>().atualizaItens(
				MenuComprar.GetComponent<MenuComprarBehaviour>().custototal, 0);
			MenuComprar.GetComponent<MenuComprarBehaviour>().DeNome.SetActive(false);
			MenuComprar.GetComponent<MenuComprarBehaviour>().Detalhes.SetActive(false);
			MenuComprar.SetActive (false);
			CollidersEnable(btnComprar);
			CollidersEnable(btnFabricar);
			CollidersEnable(btnVender);
		}
		else if (MenuVender.activeInHierarchy) {
			MenuVender.GetComponent<MenuVenderBehaviour>().ZeraItens();
			MenuVender.GetComponent<MenuVenderBehaviour>().atualizaItens(
				MenuVender.GetComponent<MenuVenderBehaviour>().custototalV, 0);
			MenuVender.GetComponent<MenuVenderBehaviour>().DeNomeV.SetActive(false);
			MenuVender.GetComponent<MenuVenderBehaviour>().DetalhesV.SetActive(false);
			MenuVender.SetActive (false);
			CollidersEnable(btnComprar);
			CollidersEnable(btnFabricar);
			CollidersEnable(btnVender);
		}
		
	}



}