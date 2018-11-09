using UnityEngine;
using System.Collections;

public class InicioBehaviour : MonoBehaviour {


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Jogar()
	{
		Application.LoadLevel("PreLoad");
	}
	public void Creditos()
	{
		Application.LoadLevel("Creditos");
	}
	public void Sair()
	{
		Application.Quit();
	}
	public void Voltar()
	{
		Application.LoadLevel("Inicio");
	}

}
