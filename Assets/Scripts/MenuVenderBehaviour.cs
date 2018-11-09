using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;

public class MenuVenderBehaviour : MonoBehaviour {

	//variavel que recebera as variaveis converrtidas em json
	JsonData playerJson;

	public PlayerP playerSafe;
	public Materiais saveMaterial;
	
	public string fileName;
	
	private GameObject itemText1V,itemText2V,itemText3V,itemText4V,itemText5V,itemText6V,
	Item1V,Item2V,Item3V,Item4V,Item5V,Item6V, LojaController;
	public GameObject custototalV, currentCashV, AtualCash,DeNomeV, DetalhesV;
	public int QuanItem1V, QuanItem2V, QuanItem3V, QuanItem4V, QuanItem5V, QuanItem6V, currentMoneyV, fakeExp,
	item1PreçoV = 10, item2PreçoV = 20, item3PreçoV = 35, item4PreçoV = 50, item5PreçoV = 70, item6PreçoV = 100,
	totalPayV = 0;
	public string LastItemChoose;
	
	
	
	// Use this for initialization
	void Start () {
		ListGameobjects ();
		GetCurrentMoney ();
		DeNomeV.SetActive (false);
		DetalhesV.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable() {
		if (currentCashV == null) {
			ListGameobjects ();
		}
		GetCurrentMoney ();
	}
	
	public void GetCurrentMoney()
	{
		fakeExp = LojaController.GetComponent<LojaConstroller>().expfake;
		currentMoneyV = LojaController.GetComponent<LojaConstroller>().currentMoney;
		currentCashV.GetComponent<TextMesh> ().text = currentMoneyV.ToString ();
		AtualCash.GetComponent<TextMesh> ().text = currentMoneyV.ToString ();
	}

	

	private void ListGameobjects()
	{
		itemText1V = GameObject.Find ("ItemText1V");
		itemText2V = GameObject.Find ("ItemText2V");
		itemText3V = GameObject.Find ("ItemText3V");
		itemText4V = GameObject.Find ("ItemText4V");
		itemText5V = GameObject.Find ("ItemText5V");
		itemText6V = GameObject.Find ("ItemText6V");
		Item1V = GameObject.Find ("item1V");
		Item2V = GameObject.Find ("item2V");
		Item3V = GameObject.Find ("item3V");
		Item4V = GameObject.Find ("item4V");
		Item5V = GameObject.Find ("item5V");
		Item6V = GameObject.Find ("item6V");
		AtualCash = GameObject.Find ("AtualCash");
		currentCashV = GameObject.Find ("currentCashV");
		custototalV = GameObject.Find ("custototalV");
		LojaController = GameObject.Find ("LojaController");
		DeNomeV = GameObject.Find ("DeNomeV");
		DetalhesV = GameObject.Find ("DetalhesV");
	}
	
	public void atualizaItens(GameObject obj, int quant)
	{
		obj.GetComponent<TextMesh> ().text = quant.ToString ();
		ValorGastar ();
	}
	
	public void ValorGastar()
	{
		totalPayV = (item1PreçoV * QuanItem1V) + (item2PreçoV * QuanItem2V) + (item3PreçoV * QuanItem3V) 
			+ (item4PreçoV * QuanItem4V) + (item5PreçoV * QuanItem5V) + (item6PreçoV * QuanItem6V);
		custototalV.GetComponent<TextMesh> ().text = totalPayV.ToString ();	
	}
	
	public void ZeraItens()
	{
		QuanItem1V = 0;	QuanItem2V = 0;	QuanItem3V = 0;	QuanItem4V = 0;	QuanItem5V = 0;	QuanItem6V = 0;
		itemText1V.GetComponent<TextMesh> ().text = QuanItem1V.ToString ();
		itemText2V.GetComponent<TextMesh> ().text = QuanItem2V.ToString ();
		itemText3V.GetComponent<TextMesh> ().text = QuanItem3V.ToString();
		itemText4V.GetComponent<TextMesh> ().text = QuanItem4V.ToString();
		itemText5V.GetComponent<TextMesh> ().text = QuanItem5V.ToString();
		itemText6V.GetComponent<TextMesh> ().text = QuanItem6V.ToString();
	}
	
	public void Vender()
	{
		if (totalPayV > 0) {
			
			currentMoneyV += totalPayV  ;
			LojaController.GetComponent<LojaConstroller>().currentMoney = currentMoneyV;
			LojaController.GetComponent<LojaConstroller> ().Citem1 -= QuanItem1V;
			LojaController.GetComponent<LojaConstroller> ().Citem2 -= QuanItem2V;
			LojaController.GetComponent<LojaConstroller> ().Citem3 -= QuanItem3V;
			LojaController.GetComponent<LojaConstroller> ().Citem4 -= QuanItem4V;
			LojaController.GetComponent<LojaConstroller> ().Citem5 -= QuanItem5V;
			LojaController.GetComponent<LojaConstroller> ().Citem6 -= QuanItem6V;

			playerSafe = new PlayerP(fakeExp, currentMoneyV);
			playerJson = JsonMapper.ToJson(playerSafe);
			File.WriteAllText ((Application.persistentDataPath + "/Player.json"), playerJson.ToString ());

			saveMaterial = new Materiais(LojaController.GetComponent<LojaConstroller> ().Citem1,
			                            LojaController.GetComponent<LojaConstroller> ().Citem2,
			                            LojaController.GetComponent<LojaConstroller> ().Citem3,
			                            LojaController.GetComponent<LojaConstroller> ().Citem4,
			                            LojaController.GetComponent<LojaConstroller> ().Citem5,
			                            LojaController.GetComponent<LojaConstroller> ().Citem6);
			playerJson = JsonMapper.ToJson(saveMaterial);
			File.WriteAllText ((Application.persistentDataPath + "/Materiais.json"), playerJson.ToString ());

			currentCashV.GetComponent<TextMesh> ().text = currentMoneyV.ToString ();
			AtualCash.GetComponent<TextMesh> ().text = currentMoneyV.ToString ();
			ZeraItens();
			ValorGastar();
			currentMoneyV = LojaController.GetComponent<LojaConstroller>().currentMoney;
			
		} else {  
			LojaController.GetComponent<LojaConstroller> ().warnning2();
		}
	}
	
	
	
	public void BP1()
	{
		if (QuanItem1V < LojaController.GetComponent<LojaConstroller> ().Citem1) {
			QuanItem1V += 1;
			atualizaItens (itemText1V, QuanItem1V);
			LastItemChoose = "item1";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BP2()
	{
		if (QuanItem2V < LojaController.GetComponent<LojaConstroller> ().Citem2) {
			QuanItem2V += 1;
			atualizaItens (itemText2V, QuanItem2V);
			LastItemChoose = "item2";
			ItemDescricao (LastItemChoose);
		}
		
	}
	public void BP3()
	{
		if (QuanItem3V < LojaController.GetComponent<LojaConstroller> ().Citem2) {
			QuanItem3V += 1;
			atualizaItens (itemText3V, QuanItem3V);
			LastItemChoose = "item3";
			ItemDescricao (LastItemChoose);
		}
		
	}
	public void BP4()
	{
		if (QuanItem4V < LojaController.GetComponent<LojaConstroller> ().Citem4) {
			QuanItem4V += 1;
			atualizaItens (itemText4V, QuanItem4V);
			LastItemChoose = "item4";
			ItemDescricao (LastItemChoose);
		}
		
	}
	public void BP5()
	{
		if (QuanItem5V < LojaController.GetComponent<LojaConstroller> ().Citem5) {
			QuanItem5V += 1;
			atualizaItens (itemText5V, QuanItem5V);
			LastItemChoose = "item5";
			ItemDescricao (LastItemChoose);
		}
		
	}
	public void BP6()
	{
		if (QuanItem6V < LojaController.GetComponent<LojaConstroller> ().Citem6) {
			QuanItem6V += 1;
			atualizaItens (itemText6V, QuanItem6V);
			LastItemChoose = "item6";
			ItemDescricao (LastItemChoose);
		}
		
	}
	
	public void BL1()
	{
		if (QuanItem1V > 0) {
			QuanItem1V -= 1;
			atualizaItens (itemText1V, QuanItem1V);
			LastItemChoose = "item1";
			ItemDescricao (LastItemChoose);
		}
		
	}
	public void BL2()
	{
		if (QuanItem2V > 0) {
			QuanItem2V -= 1;
			atualizaItens (itemText2V, QuanItem2V);
			LastItemChoose = "item2";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL3()
	{
		if (QuanItem3V > 0) {
			QuanItem3V -= 1;
			atualizaItens (itemText3V, QuanItem3V);
			LastItemChoose = "item3";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL4()
	{
		if (QuanItem4V > 0) {
			QuanItem4V -= 1;
			atualizaItens (itemText4V, QuanItem4V);
			LastItemChoose = "item4";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL5()
	{
		if (QuanItem5V > 0) {
			QuanItem5V -= 1;
			atualizaItens (itemText5V, QuanItem5V);
			LastItemChoose = "item5";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL6()
	{
		if (QuanItem6V > 0) {
			QuanItem6V -= 1;
			atualizaItens (itemText6V, QuanItem6V);
			LastItemChoose = "item6";
			ItemDescricao (LastItemChoose);
		}
	}	
	
	public void ItemDescricao(string itemClicked)
	{
		DeNomeV.SetActive(true);
		//DetalhesV.SetActive(true);
		if (itemClicked == "item1") {
			DeNomeV.GetComponent<TextMesh> ().text = "Bronze";
			//DetalhesV.GetComponent<TextMesh> ().text = "Energia(+50)"+"\n"+"Toxina(+5)";
		}else if (itemClicked == "item2") {
			DeNomeV.GetComponent<TextMesh> ().text = "Ferro";
			//DetalhesV.GetComponent<TextMesh> ().text = "Resistencia(+3)"+"\n"+"Forca(+1)"+"\n"+"Toxina(+40)";
		}else if (itemClicked == "item3") {
			DeNomeV.GetComponent<TextMesh> ().text = "Obsidiana";
			//DetalhesV.GetComponent<TextMesh> ().text = "Vida(+5)";
		}else if (itemClicked == "item4") {
			DeNomeV.GetComponent<TextMesh> ().text = "Chumbo";
			//DetalhesV.GetComponent<TextMesh> ().text = "Toxina(-20)";
		}else if (itemClicked == "item5") {
			DeNomeV.GetComponent<TextMesh> ().text = "Ouro";
			//DetalhesV.GetComponent<TextMesh> ().text = "Resistencia(+20)"+"\n"+"Toxina(+15)";
		}else if (itemClicked == "item6") {
			DeNomeV.GetComponent<TextMesh> ().text = "Uranio";
			//DetalhesV.GetComponent<TextMesh> ().text = "Forca(+5)"+"\n"+"Constituicao(+1)"+"\n"+"Toxina(+70)";
		}
	}
	public void itemClicked1 ()	{ 	
		LastItemChoose = "item1";
		ItemDescricao (LastItemChoose);
	}
	public void itemClicked2 ()	{ 	
		LastItemChoose = "item2";
		ItemDescricao (LastItemChoose);
	}
	public void itemClicked3 ()	{ 		
		LastItemChoose = "item3";
		ItemDescricao (LastItemChoose);
	}
	public void itemClicked4 ()	{ 		
		LastItemChoose = "item4";
		ItemDescricao (LastItemChoose);
	}
	public void itemClicked5 ()	{ 		
		LastItemChoose = "item5";
		ItemDescricao (LastItemChoose);
	}
	public void itemClicked6 ()	{ 		
		LastItemChoose = "item6";
		ItemDescricao (LastItemChoose);
	}
}
