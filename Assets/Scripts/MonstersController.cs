using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using SimpleJSON;


public class MonstersController : MonoBehaviour {

	//variavel que recebera as variaveis converrtidas em json
	JsonData playerJson;
	
	public lastMonster_Create saveUltimoM;
	public Itens itenssave;
	public Monster_Create savechanges;
	
	public string fileName;
	
	public string lastMonster;

	[Header("AUDIO")]
	public AudioSource audio;

	public GameObject Aguia,Mamute,Fish,
	MenuInject, btnInjection, ItemInject,BtnInject, Aviso, Warnning, close, btnStatus, cArrows, 
	Choose, MenuStatus, Monstros, Bars, leftArrow, rightArrow;
	public ItensBehaviour itensbehaviour;
	public string ItemSelect, LastMonster, LastMonsterChoose;
	public int tmpEner, tmpForca, tmpResis, tmpConst, tmpTox, tmpVida;

	// Use this for initialization
	void Start () {

		audio.Play();
		 // Liste todos os games objects
		ListGameobjects ();



		DisableGameObjects (MenuStatus.GetComponent<StatsBehaviour> ().TMamute);
		DisableGameObjects (MenuStatus.GetComponent<StatsBehaviour> ().TPeixe);
		DisableGameObjects (MenuStatus.GetComponent<StatsBehaviour> ().TAguia);
		DisableGameObjects (MenuInject);
		DisableGameObjects (Aviso);
		DisableGameObjects (MenuStatus);

		fileName = "lastMonster.json";
		
		//String "master" It contains all the text for json file.
		string str = Read();
		
		//Still do not get it right what this means, but it is the variable that understands that the string is a json object.
		JSONNode json = JSON.Parse(str);
		
		//Get a string of monster choose, in laboratory
		LastMonster = json["monster_choose"];

		if (LastMonster == "Aguia") {
			LstAguia();
		} else if (LastMonster == "Mamute") {
			LstMamute();
		} else if (LastMonster == "Peixe") {
			LstPeixe();
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



	private void ListGameobjects()
	{
		MenuInject = GameObject.Find ("MenuInject");
		btnInjection = GameObject.Find("btnInjection");
		ItemInject = GameObject.Find("ItemInject");
		BtnInject = GameObject.Find ("BtnInject");
		Aviso = GameObject.Find ("Aviso");
		Warnning = GameObject.Find("Warnning");
		btnStatus = GameObject.Find ("btnStatus"); 
		cArrows = GameObject.Find ("cArrows"); 
		leftArrow = GameObject.Find ("Left"); 
		rightArrow = GameObject.Find ("Right"); 
		Choose = GameObject.Find ("Choose'"); 
		MenuStatus = GameObject.Find ("MenuStatus"); 
		Monstros = GameObject.Find ("Monstros"); 
		Bars = GameObject.Find ("Bars"); 
		itensbehaviour = MenuInject.GetComponent<ItensBehaviour>();
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

	public void Status()
	{
		DisableGameObjects (btnInjection);
		DisableGameObjects (btnStatus);
		CollidersDisable (rightArrow);
		CollidersDisable (leftArrow);
		
		EnableGameObjects (MenuStatus);
		if (LastMonster == "Aguia") {
			MenuStatus.GetComponent<StatsBehaviour> ().AguiaValues ();
		} else if (LastMonster == "Mamute") {
			MenuStatus.GetComponent<StatsBehaviour> ().MamuteValues ();
		} else if (LastMonster == "Peixe") {
			MenuStatus.GetComponent<StatsBehaviour>().PeixeValues();
		}

		MenuStatus.GetComponent<StatsBehaviour>().ShowValues();
	
	}

	public void ativaInje ()
	{
		itensbehaviour.UpdateItens ();
		MenuInject.SetActive (true);
		CollidersDisable (BtnInject);
		CollidersDisable (btnInjection);
		CollidersDisable (btnStatus);
		DisableGameObjects (Monstros);
		if (LastMonster == "Aguia") {
			DisableGameObjects(Aguia);		
		} else if (LastMonster == "Peixe") {
			DisableGameObjects(Fish);	
		} else if (LastMonster == "Mamute") {
			DisableGameObjects(Mamute);
		}

	}

	public void itemClicked1 ()	{ 	
		GameObject item = itensbehaviour.Item1;
		if (itensbehaviour.item1 > 0) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
			ItemSelect = "item1";
			itensbehaviour.ItemDescricao(ItemSelect);
			CollidersEnable(BtnInject);
		} else {
			warnning();
		}
	}
	public void itemClicked2 ()	{ 	
		GameObject item = itensbehaviour.Item2;
		if (itensbehaviour.item2 > 0) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
			ItemSelect = "item2";
			itensbehaviour.ItemDescricao(ItemSelect);
			CollidersEnable(BtnInject);
		} else {
			warnning();
		}	
	}
	public void itemClicked3 ()	{ 		
		GameObject item = itensbehaviour.Item3;
		if (itensbehaviour.item3 > 0) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
			ItemSelect = "item3";
			itensbehaviour.ItemDescricao(ItemSelect);
			CollidersEnable(BtnInject);
		} else {
			warnning();
		}	
	}
	public void itemClicked4 ()	{ 		
		GameObject item = itensbehaviour.Item4;
		if (itensbehaviour.item4 > 0) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
			ItemSelect = "item4";
			itensbehaviour.ItemDescricao(ItemSelect);
			CollidersEnable(BtnInject);
		} else {
			warnning();
		}
	}
	public void itemClicked5 ()	{ 		
		GameObject item = itensbehaviour.Item5;
		if (itensbehaviour.item5 > 0) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
			ItemSelect = "item5";
			itensbehaviour.ItemDescricao(ItemSelect);
			CollidersEnable(BtnInject);
		} else {
			warnning();
		}
	}
	public void itemClicked6 ()	{ 		
		GameObject item = itensbehaviour.Item6;
		if (itensbehaviour.item6 > 0) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = item.GetComponent<SpriteRenderer> ().sprite;
			ItemSelect = "item6";
			itensbehaviour.ItemDescricao(ItemSelect);
			CollidersEnable(BtnInject);
		} else {
			warnning();
		}
	}



	public void Inject()
	{
		if (LastMonster == "Aguia") {
			MenuStatus.GetComponent<StatsBehaviour> ().AguiaValues ();
		} else if (LastMonster == "Mamute") {
			MenuStatus.GetComponent<StatsBehaviour> ().MamuteValues ();
		} else if (LastMonster == "Peixe") {
			MenuStatus.GetComponent<StatsBehaviour>().PeixeValues();
		}

		if(ItemSelect == "item1")
		{
			itensbehaviour.item1 -= 1;
			tmpEner = 50;
			tmpForca = 0;
			tmpResis = 0;
			tmpConst = 0;
			tmpTox = 5;
			tmpVida = 0;
		}
		else if(ItemSelect == "item2")
		{
			itensbehaviour.item2 -= 1;
			tmpEner = 50;
			tmpForca = 1;
			tmpResis = 3;
			tmpConst = 0;
			tmpTox = 40;
			tmpVida = 0;
		}
		else if(ItemSelect == "item3")
		{
			itensbehaviour.item3 -= 1;
			tmpEner = 0;
			tmpForca = 1;
			tmpResis = 3;
			tmpConst = 0;
			tmpTox = 0;
			tmpVida = 5;
		}
		else if(ItemSelect == "item4")
		{
			itensbehaviour.item4 -= 1;
			tmpEner = 0;
			tmpForca = 0;
			tmpResis = 0;
			tmpConst = 0;
			tmpTox = -20;
			tmpVida = 0;
		}
		else if(ItemSelect == "item5")
		{
			itensbehaviour.item5 -= 1;
			tmpEner = 0;
			tmpForca = 0;
			tmpResis = 0;
			tmpConst = 0;
			tmpTox = 10;
			tmpVida = 20;
		}
		else if(ItemSelect == "item6")
		{
			itensbehaviour.item6 -= 1;
			tmpEner = 0;
			tmpForca = 5;
			tmpResis = 0;
			tmpConst = 1;
			tmpTox = 70;
			tmpVida = 0;


		}

		int damage = MenuStatus.GetComponent<StatsBehaviour>().forc + tmpForca ;
		int health = MenuStatus.GetComponent<StatsBehaviour>().cons + tmpConst;
		int resistance = MenuStatus.GetComponent<StatsBehaviour>().resis + tmpResis;;
		int energy = MenuStatus.GetComponent<StatsBehaviour>().ener + tmpEner;
		int toxi = MenuStatus.GetComponent<StatsBehaviour>().tox + tmpTox;
		int level = MenuStatus.GetComponent<StatsBehaviour> ().level;
		int total_damage = damage;
		int total_health = MenuStatus.GetComponent<StatsBehaviour>().vida;
		int total_resistance = resistance;
		int total_energy = energy;
		
		savechanges = new Monster_Create(total_damage, total_health, total_resistance,total_energy, damage, health, resistance,level,energy, toxi);
		playerJson = JsonMapper.ToJson(savechanges);
		
		if (LastMonster == "Aguia") {
			File.WriteAllText ((Application.persistentDataPath + "/Aguia.json"), playerJson.ToString ());
		} else if (LastMonster == "Mamute") {
			File.WriteAllText ((Application.persistentDataPath + "/Mamute.json"), playerJson.ToString ());
		} else if (LastMonster == "Peixe") {
			File.WriteAllText ((Application.persistentDataPath + "/Peixe.json"), playerJson.ToString ());
		}


		itenssave = new Itens(itensbehaviour.item1,itensbehaviour.item2,itensbehaviour.item3,itensbehaviour.item4,itensbehaviour.item5,itensbehaviour.item6);
		playerJson = JsonMapper.ToJson(itenssave);
		File.WriteAllText ((Application.persistentDataPath + "/Itens.json"), playerJson.ToString ());

		CollidersEnable (btnInjection);
		CollidersEnable (btnStatus);;
		EnableGameObjects (Monstros);
		if (LastMonster == "Aguia") {
			EnableGameObjects(Aguia);		
		} else if (LastMonster == "Peixe") {
			EnableGameObjects(Fish);	
		} else if (LastMonster == "Mamute") {
			EnableGameObjects(Mamute);
		}
		ItemInject.GetComponent<SpriteRenderer> ().sprite = null;
		ItemSelect = null;
		MenuInject.SetActive (false);	


	}

	public void warnning()
	{
		Aviso.SetActive(true);
		Warnning.GetComponent<TextMesh>().text = "Voce nao tem"+"\n"+" nenhum item desses!";
		StartCoroutine(fecharAviso(1.5F));
	}


	IEnumerator fecharAviso(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		Aviso.SetActive(false);
	}

	public void Close()
	{
		 if (MenuInject.activeInHierarchy) {
			ItemInject.GetComponent<SpriteRenderer> ().sprite = null;
			ItemSelect = null;
			MenuInject.SetActive(false);
			CollidersEnable(btnInjection);
			CollidersEnable(btnStatus);
			EnableGameObjects (Monstros);
			if (LastMonster == "Aguia") {
				EnableGameObjects(Aguia);		
			} else if (LastMonster == "Peixe") {
				EnableGameObjects(Fish);	
			} else if (LastMonster == "Mamute") {
				EnableGameObjects(Mamute);
			}
		}else if (MenuStatus.activeInHierarchy) {


			ItemInject.GetComponent<SpriteRenderer> ().sprite = null;
			MenuStatus.GetComponent<StatsBehaviour>().Cancelar();
			ItemSelect = null;
			DisableGameObjects(MenuStatus);
			EnableGameObjects(btnStatus);
			EnableGameObjects(btnInjection);
			CollidersEnable (rightArrow);
			CollidersEnable (leftArrow);
		}

	}

	public void LstAguia ()
	{
		if(LastMonster == "Peixe")
		{
			Destroy (Fish);
			DisableGameObjects(MenuStatus.GetComponent<StatsBehaviour> ().TPeixe);
		}
		else if(LastMonster == "Mamute")
		{
			Destroy (Mamute);
			DisableGameObjects(MenuStatus.GetComponent<StatsBehaviour> ().TMamute);
		}

		Aguia = Instantiate (Resources.Load ("AnimalsAdmPREFABS/Aguia[LAB]")) as GameObject;
		Aguia.SetActive (true);
		MenuStatus.GetComponent<StatsBehaviour>().AguiaValues();

		if(!MenuStatus.activeInHierarchy && !MenuInject.activeInHierarchy)
		{	
			EnableGameObjects (MenuStatus.GetComponent<StatsBehaviour> ().TAguia);
		}
		else if(MenuStatus.activeInHierarchy)
		{
			MenuStatus.GetComponent<StatsBehaviour>().ShowValues();
		}
		LastMonster = "Aguia";
	}

	public void LstPeixe ()
	{
		if(LastMonster == "Aguia")
		{
			Destroy (Aguia);
			DisableGameObjects(MenuStatus.GetComponent<StatsBehaviour> ().TAguia);
		}
		else if(LastMonster == "Mamute")
		{
			Destroy (Mamute);
			DisableGameObjects(MenuStatus.GetComponent<StatsBehaviour> ().TMamute);
		}

		Fish = Instantiate (Resources.Load ("Prefabs/Peixe2")) as GameObject;
		Fish.SetActive (true);
		MenuStatus.GetComponent<StatsBehaviour>().PeixeValues();

		if(!MenuStatus.activeInHierarchy && !MenuInject.activeInHierarchy)
		{	
			EnableGameObjects (MenuStatus.GetComponent<StatsBehaviour> ().TPeixe);
		}
		else if(MenuStatus.activeInHierarchy)
		{
			MenuStatus.GetComponent<StatsBehaviour>().ShowValues();
		}

		LastMonster = "Peixe";
	}

	public void LstMamute ()
	{
		if(LastMonster == "Aguia")
		{
			Destroy (Aguia);
			DisableGameObjects(MenuStatus.GetComponent<StatsBehaviour> ().TAguia);
		}
		else if(LastMonster == "Peixe")
		{
			Destroy (Fish);
			DisableGameObjects(MenuStatus.GetComponent<StatsBehaviour> ().TPeixe);
		}

		Mamute = Instantiate (Resources.Load ("AnimalsAdmPREFABS/Mamute[LAB]")) as GameObject;
		Mamute.SetActive (true);
		MenuStatus.GetComponent<StatsBehaviour>().MamuteValues();
		if(!MenuStatus.activeInHierarchy && !MenuInject.activeInHierarchy)
		{
			EnableGameObjects (MenuStatus.GetComponent<StatsBehaviour> ().TMamute);
		}
		else if(MenuStatus.activeInHierarchy)
		{
			MenuStatus.GetComponent<StatsBehaviour>().ShowValues();
		}

		LastMonster = "Mamute";

	}

	public void ChangeRight()
	{
		if (LastMonster == "Aguia") {
			LstMamute();
		} else if (LastMonster == "Mamute") {
			LstPeixe();
		} else if (LastMonster == "Peixe") {
			LstAguia();
		}

		MenuStatus.GetComponent<StatsBehaviour> ().AtualBars ();
	}

	public void ChangeLeft()
	{
		if (LastMonster == "Aguia") {
			LstPeixe();			
		} else if (LastMonster == "Peixe") {
			LstMamute();
		} else if (LastMonster == "Mamute") {
			LstAguia();
		}
		MenuStatus.GetComponent<StatsBehaviour> ().AtualBars ();
	}

	public void Escolhido()
	{
		LastMonsterChoose = LastMonster;
		Debug.Log (LastMonsterChoose);

		saveUltimoM = new lastMonster_Create(LastMonsterChoose);
		playerJson = JsonMapper.ToJson(saveUltimoM);
		
		//File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
		File.WriteAllText((Application.persistentDataPath + "/lastMonster.json"), playerJson.ToString());
	}


}


