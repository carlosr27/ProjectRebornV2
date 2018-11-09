using UnityEngine;
using System.Collections;

public class ControleBarras : MonoBehaviour {

	//Primeiro Passo1: Pegar as barras que vão ser utilizaas na cena.
	public GameObject[] BarrasUsadas = new GameObject[3];

	//Primeiro Passo2: Criar as variaveis que vão armazenar Ataque e Defesa do Bicho e calcular as formulas.
	public float Ataque, Defesa; // Atributos comparativos, Ataque = Seu monstro, Defesa= Inimigo.

	float TotalBarra,Total10,Total5; //Total da barra, 10% do Total && 5% do total

	float Resto;
	float Critico;
	float Miss;
	//===============================================================================================================


	/*Segundo Passo1: Iniciar o processo geral de função da barra sempre que quiser, ou seja, depois do clique no ataque,
	joga um True nessa variavel*/
	public bool processo;


	/*Segundo Passo2: Aqui será criada as variaveis para armazenar valores do sorteio para futuras comparações
	 P's = Posições check's = auxilio de verificação de repetição de valores */
	public int P1,P2,P3;
	bool check1, check2, check3;
	//=================================================================================================================


	/*Terceiro Passo: Variavel(eis) que possibilita a inicialização do desenho das barras */
	bool PodeDesenhar;
	float A,B,C;  /* Essas float's serão o valor da Scale das barras, neste exemplo, para aumentar as barras verticalmente
	                  tem que modificar o ScaleX(poe variar pro Y ou não) */


	//==============================================================================================================

	/* Quarto Passo1: Limpar a Tela*/
	public static bool LimpaTela;

    public BattleController battleController;


	void Start () 
	{
		
	
    //Primeiro Passo3: Eu escolhi pegas as barras através da TAG, você pode escolher outro método.
		BarrasUsadas[0] = GameObject.FindGameObjectWithTag("Normal");
		BarrasUsadas[1] = GameObject.FindGameObjectWithTag("Critico");
		BarrasUsadas[2] = GameObject.FindGameObjectWithTag("Miss");
	//Uma observação importante é que os sorteios das posições da barra serão feitas através do indie deste array.



	/*Primeiro Passo4: Logo de inicio, ja desativamos a barra para a mesma não ficar desnecessariamente poluindo a tela
		antes de ser necessariamente chamada*/

		for(int i = 0; i < BarrasUsadas.Length; i++)
		{

			BarrasUsadas [i].SetActive (false);

		}



        Ataque = battleController.damage;
        Defesa = battleController.e_resistance;


    }



    // Update is called once per frame
    void Update () 
	{



            //Calcula atributos e monta formula = Primeiro Passo
            CalculaAtributos();
            //==============================================================


        //Com precesso iniciado, Sorteia posições = Segund Passo
        if (processo == true)
		{
			
		  SorteiaPos();

		}

		//================================================================


		//Terceiro Passo
		if(PodeDesenhar == true)
		{

			Desenha ();
			//checacor.GoAnim = true;


		}

		//================================================================

		//Quarto Passo

		if(LimpaTela == true)
		{
			
			Limpa ();


		}
			


	
	}







		void CalculaAtributos()
	{

		if (Ataque <= Defesa) {

			Resto = TotalBarra * 20 / 100;



		} 
		else 
		{

			Resto = (Ataque - Defesa) + Total10;

		}


		TotalBarra = Ataque + Defesa;
		//Total5 = TotalBarra * 5 / 100;
		Total5 = TotalBarra * 2 / 100;
		Total10 = TotalBarra * 10 / 100;

		//Resto = (Ataque - Defesa) + Total10;
		//Critico = Resto / 2 + Total5;
		Critico = Resto / 5f + Total5;
		Miss = TotalBarra - (Critico + Resto);


	}






	/* Aqui se realiza o Sorteio de valores para comparação futura com as posições no Array, como nosso array tem apenas 
	 três posições(0,1,2), o Random.Range tem de parametro 0 e 3 (o 3 não entra no sorteio é tipo: 0 >= X < 3) */
	

	void SorteiaPos()
	{

		check1 = true;

		if(check1 == true)
		{

			P1 = Random.Range (0,3);
			check1 = false;
			check2 = true;

		}



		if(check2 == true)
		{

			P2 = Random.Range (0,3);

			if (P2 == P1) 
			{

				check2 = false;
				check1 = true;


			} 

			else 
			{

				check2 = false;
				check3 = true;

			}



		}



		if(check3 == true)
		{


			P3 = Random.Range (0,3);

			if(P3 == P1 || P3 == P2)
			{

				check3 = false;
				check1 = true;


			}

			else
			{

				check3 = false;
				//RealizaSorteio = false;
				PodeDesenhar = true;
				processo = false;


			}

		}


		checacor.GoAnim = true;


	}



	/*Aqui são realizadas as comparações, o Código sempre vai desenhar na ordem P1,P2,P3; mas o
	valor que estes possuirem após o sorteio determina qual tipo de barra vai ser desenhada */

	/*OBS: Note que em P1,a variavel A, tem um valor determinado, B & C seguem o mesmo padrão;
	Porém, na hora de desenho as barras B e C se somam com as anteriores(A + B, A + B + C)*/

	/* 0.17 é o tamanho da barra desenhada na tela(é o X no Scale das imagens das Barras),
	  Esse valor pode variar quando or incrementar */


	void Desenha ()
	{


		switch(P1)
		{

		case 0:

			{

				A = 1f / TotalBarra * Resto;

				//BarrasUsadas [0].GetComponent<SpriteRenderer> ().color = NormalAmarelo;
				BarrasUsadas [0].SetActive(true);
				BarrasUsadas [0].GetComponent<SpriteRenderer> ().sortingOrder = 1;
				BarrasUsadas [0].transform.localScale = new Vector3 (1, A, 1);
				BarrasUsadas [0].GetComponent<BoxCollider> ().enabled = true;





				break;





			}

		case 1:
			{

				A = 1f / TotalBarra * Critico;

				//BarrasUsadas [1].GetComponent<SpriteRenderer> ().color = CriticoVerde;
				BarrasUsadas [1].SetActive(true);
				BarrasUsadas [1].GetComponent<SpriteRenderer> ().sortingOrder = 1;
				BarrasUsadas [1].transform.localScale = new Vector3 (1, A, 1);
				BarrasUsadas [1].GetComponent<BoxCollider> ().enabled = true;




				break;


			}

		case 2:
			{

				A = 1f / TotalBarra * Miss;

				//BarrasUsadas [1].GetComponent<SpriteRenderer> ().color = MissVermelho;
				BarrasUsadas [2].SetActive(true);
				BarrasUsadas [2].GetComponent<SpriteRenderer> ().sortingOrder = 1;
				BarrasUsadas [2].transform.localScale = new Vector3 (1, A, 1);
				BarrasUsadas [2].GetComponent<BoxCollider> ().enabled = true;





				break;


			}

		default:
			{

				Debug.Log ("Fail");
				break;
			}



		}


		switch (P2) 
		{



		case 0:
			{
				//Debug.Log ("zero");
				B = 1f / TotalBarra * Resto;


				BarrasUsadas [0].SetActive(true);
				BarrasUsadas [0].GetComponent<SpriteRenderer> ().sortingOrder = 0;
				BarrasUsadas [0].transform.localScale = new Vector3 (1, A + B, 1);
				BarrasUsadas [0].GetComponent<BoxCollider> ().enabled = true;





				break;



			}


		case 1:
			{

				//Debug.Log ("um");
				B = 1f / TotalBarra * Critico;

				BarrasUsadas [1].SetActive(true);
				BarrasUsadas [1].GetComponent<SpriteRenderer> ().sortingOrder = 0;
				BarrasUsadas [1].transform.localScale = new Vector3 (1, A + B, 1);
				BarrasUsadas [1].GetComponent<BoxCollider> ().enabled = true;


				break;

			}



		case 2:
			{
				//Debug.Log ("dois");

				B = 1f / TotalBarra * Miss;

				BarrasUsadas [2].SetActive(true);
				BarrasUsadas [2].GetComponent<SpriteRenderer> ().sortingOrder = 0;
				BarrasUsadas [2].transform.localScale = new Vector3 (1, A + B, 1);
				BarrasUsadas [2].GetComponent<BoxCollider> ().enabled = true;


				break;

			}
		default:
			{
				Debug.Log ("nops");
				break;

			}


		}


		switch(P3)
		{


		case 0:
			{


				C = 1f / TotalBarra * Resto;

				BarrasUsadas [0].SetActive(true);
				BarrasUsadas [0].GetComponent<SpriteRenderer> ().sortingOrder = -1;
				BarrasUsadas [0].transform.localScale = new Vector3 (1, A + B + C, 1);
				BarrasUsadas [0].GetComponent<BoxCollider> ().enabled = true;
				break;



			}

		case 1:
			{
				C = 1f / TotalBarra * Critico;

				BarrasUsadas [1].SetActive(true);
				BarrasUsadas [1].GetComponent<SpriteRenderer> ().sortingOrder = -1;
				BarrasUsadas [1].transform.localScale = new Vector3 (1, A + B + C, 1);
				BarrasUsadas [1].GetComponent<BoxCollider> ().enabled = true;
				break;


			}


		case 2:
			{


				C = 1f / TotalBarra * Miss;

				BarrasUsadas [2].SetActive(true);
				BarrasUsadas [2].GetComponent<SpriteRenderer> ().sortingOrder = -1;
				BarrasUsadas [2].transform.localScale = new Vector3 (1, A + B + C, 1);
				BarrasUsadas [2].GetComponent<BoxCollider> ().enabled = true;
				break;


			}

		default:
			{

				Debug.Log("Bugou");
				break;

			}



		}






	}



	void Limpa()
	{


		PodeDesenhar = false;
		processo = false;
		//LimpaTela = false;

		BarrasUsadas [0].SetActive(false);
		BarrasUsadas [0].GetComponent<SpriteRenderer> ().sortingOrder = 0;
		BarrasUsadas [0].transform.localScale = new Vector3 (1, 1, 1);
		BarrasUsadas [0].GetComponent<BoxCollider> ().enabled = false;


		BarrasUsadas [1].SetActive(false);
		BarrasUsadas [1].GetComponent<SpriteRenderer> ().sortingOrder = 0;
		BarrasUsadas [1].transform.localScale = new Vector3 (1, 1, 1);
		BarrasUsadas [1].GetComponent<BoxCollider> ().enabled = false;

		BarrasUsadas [2].SetActive(false);
		BarrasUsadas [2].GetComponent<SpriteRenderer> ().sortingOrder = 0;
		BarrasUsadas [2].transform.localScale = new Vector3 (1, 1, 1);
		BarrasUsadas [2].GetComponent<BoxCollider> ().enabled = false;


        this.gameObject.SetActive(false);
		LimpaTela = false;




	}









}
