using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;

public class MenuComprarBehaviour : MonoBehaviour {

	//variavel que recebera as variaveis converrtidas em json
	JsonData playerJson;
	
	public PlayerP playerSafe;
	public Itens saveItens;
	
	public string fileName;

	private GameObject itemText1,itemText2,itemText3,itemText4,itemText5,itemText6,
	Item1,Item2,Item3,Item4,Item5,Item6, LojaController;
	public GameObject custototal, currentCash, AtualCash,DeNome, Detalhes;
	public int QuanItem1=0, QuanItem2=0, QuanItem3=0, QuanItem4=0, QuanItem5=0, QuanItem6=0, currentMoney,fakeExp,
		item1Preço = 60, item2Preço = 100, item3Preço = 160, item4Preço = 200, item5Preço = 230, item6Preço = 320,
	totalPay = 0;
	public string LastItemChoose;



	// Use this for initialization
	void Start () {
		ListGameobjects ();
		GetCurrentMoney ();
		DeNome.SetActive (false);
		Detalhes.SetActive (false);
	}
	
	void OnEnable() {
		if (currentCash == null) {
			ListGameobjects ();
		}
		GetCurrentMoney ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void GetCurrentMoney()
	{
		fakeExp = LojaController.GetComponent<LojaConstroller>().expfake;
		currentMoney = LojaController.GetComponent<LojaConstroller>().currentMoney;
		currentCash.GetComponent<TextMesh> ().text = currentMoney.ToString ();
		AtualCash.GetComponent<TextMesh> ().text = currentMoney.ToString ();
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
		AtualCash = GameObject.Find ("AtualCash");
		currentCash = GameObject.Find ("currentCash");
		custototal = GameObject.Find ("custototal");
		LojaController = GameObject.Find ("LojaController");
		DeNome = GameObject.Find ("DeNome");
		Detalhes = GameObject.Find ("Detalhes");
	}

	public void atualizaItens(GameObject obj, int quant)
	{
		obj.GetComponent<TextMesh> ().text = quant.ToString ();
		ValorGastar ();
	}

	public void ValorGastar()
	{
		totalPay = (item1Preço * QuanItem1) + (item2Preço * QuanItem2) + (item3Preço * QuanItem3) 
			+ (item4Preço * QuanItem4) + (item5Preço * QuanItem5) + (item6Preço * QuanItem6);
		if (totalPay > currentMoney) {
			custototal.GetComponent<TextMesh> ().color = Color.red;
		} else {
			custototal.GetComponent<TextMesh> ().color = Color.yellow;
		}
		custototal.GetComponent<TextMesh> ().text = totalPay.ToString ();

	}

	public void ZeraItens()
	{
		QuanItem1 = 0; QuanItem2 = 0;QuanItem3 = 0; QuanItem4 = 0; QuanItem5 = 0; QuanItem6 = 0;
		itemText1.GetComponent<TextMesh> ().text = QuanItem1.ToString ();
		itemText2.GetComponent<TextMesh> ().text = QuanItem2.ToString ();
		itemText3.GetComponent<TextMesh> ().text = QuanItem3.ToString ();
		itemText4.GetComponent<TextMesh> ().text = QuanItem4.ToString ();
		itemText5.GetComponent<TextMesh> ().text = QuanItem5.ToString ();
		itemText6.GetComponent<TextMesh> ().text = QuanItem6.ToString ();
	}

	public void Comprar()
	{
		if (totalPay <= currentMoney) {

			currentMoney -= totalPay  ;
			LojaController.GetComponent<LojaConstroller>().currentMoney = currentMoney;
			LojaController.GetComponent<LojaConstroller> ().Pitem1 += QuanItem1;
			LojaController.GetComponent<LojaConstroller> ().Pitem2 += QuanItem2;
			LojaController.GetComponent<LojaConstroller> ().Pitem3 += QuanItem3;
			LojaController.GetComponent<LojaConstroller> ().Pitem4 += QuanItem4;
			LojaController.GetComponent<LojaConstroller> ().Pitem5 += QuanItem5;
			LojaController.GetComponent<LojaConstroller> ().Pitem6 += QuanItem6;

			playerSafe = new PlayerP(fakeExp, currentMoney);
			playerJson = JsonMapper.ToJson(playerSafe);
			File.WriteAllText ((Application.persistentDataPath + "/Player.json"), playerJson.ToString ());
			
			saveItens = new Itens(LojaController.GetComponent<LojaConstroller> ().Pitem1
			                            , LojaController.GetComponent<LojaConstroller> ().Pitem2
			                            ,LojaController.GetComponent<LojaConstroller> ().Pitem3
			                            ,LojaController.GetComponent<LojaConstroller> ().Pitem4 
			                            ,LojaController.GetComponent<LojaConstroller> ().Pitem5
			                            ,LojaController.GetComponent<LojaConstroller> ().Pitem6);
			playerJson = JsonMapper.ToJson(saveItens);
			File.WriteAllText ((Application.persistentDataPath + "/Materiais.json"), playerJson.ToString ());

			currentCash.GetComponent<TextMesh> ().text = currentMoney.ToString ();
			AtualCash.GetComponent<TextMesh> ().text = currentMoney.ToString ();
			ZeraItens();
			ValorGastar();
		} else {  
			LojaController.GetComponent<LojaConstroller> ().warnning();
		}
	}



	public void BP1()
	{
		if (QuanItem1 < 100) {
			QuanItem1 += 1;
			atualizaItens (itemText1, QuanItem1);
			LastItemChoose = "item1";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BP2()
	{
		if (QuanItem2 < 100) {
			QuanItem2 += 1;
			atualizaItens (itemText2, QuanItem2);
			LastItemChoose = "item2";
			ItemDescricao (LastItemChoose);
		}

	}
	public void BP3()
	{
		if (QuanItem3 < 100) {
			QuanItem3 += 1;
			atualizaItens (itemText3, QuanItem3);
			LastItemChoose = "item3";
			ItemDescricao (LastItemChoose);
		}

	}
	public void BP4()
	{
		if (QuanItem4 < 100) {
			QuanItem4 += 1;
			atualizaItens (itemText4, QuanItem4);
			LastItemChoose = "item4";
			ItemDescricao (LastItemChoose);
		}

	}
	public void BP5()
	{
		if (QuanItem5 < 100) {
			QuanItem5 += 1;
			atualizaItens (itemText5, QuanItem5);
			LastItemChoose = "item5";
			ItemDescricao (LastItemChoose);
		}

	}
	public void BP6()
	{
		if (QuanItem6 < 100) {
			QuanItem6 += 1;
			atualizaItens (itemText6, QuanItem6);
			LastItemChoose = "item6";
			ItemDescricao (LastItemChoose);
		}

	}

	public void BL1()
	{
		if (QuanItem1 > 0) {
			QuanItem1 -= 1;
			atualizaItens (itemText1, QuanItem1);
			LastItemChoose = "item1";
			ItemDescricao (LastItemChoose);
		}

	}
	public void BL2()
	{
		if (QuanItem2 > 0) {
			QuanItem2 -= 1;
			atualizaItens (itemText2, QuanItem2);
			LastItemChoose = "item2";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL3()
	{
		if (QuanItem3 > 0) {
			QuanItem3 -= 1;
			atualizaItens (itemText3, QuanItem3);
			LastItemChoose = "item3";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL4()
	{
		if (QuanItem4 > 0) {
			QuanItem4 -= 1;
			atualizaItens (itemText4, QuanItem4);
			LastItemChoose = "item4";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL5()
	{
		if (QuanItem5 > 0) {
			QuanItem5 -= 1;
			atualizaItens (itemText5, QuanItem5);
			LastItemChoose = "item5";
			ItemDescricao (LastItemChoose);
		}
	}
	public void BL6()
	{
		if (QuanItem6 > 0) {
			QuanItem6 -= 1;
			atualizaItens (itemText6, QuanItem6);
			LastItemChoose = "item6";
			ItemDescricao (LastItemChoose);
		}
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
			Detalhes.GetComponent<TextMesh> ().text = "Resistencia(+20)"+"\n"+"Toxina(+15)";
		}else if (itemClicked == "item6") {
			DeNome.GetComponent<TextMesh> ().text = "Whey Protein"+"\n"+"Liquido";
			Detalhes.GetComponent<TextMesh> ().text = "Forca(+5)"+"\n"+"Constituicao(+1)"+"\n"+"Toxina(+70)";
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
