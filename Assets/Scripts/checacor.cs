using UnityEngine;
using System.Collections;

public class checacor : MonoBehaviour {

    //Variaveis basicas para montar raycast
	RaycastHit PontoDeColisao; // Raycasthit Vai devolver onde o Raio bateu(Collider)
    
	Ray TiroRaio; // Raio, vai "Criar uma linha" que vai servir para nossa verificação

    int Corzinha; /* Layermask = vai fazer com que ignore coliders de outras layers, 
                           tem que setar isso na Cena(Para cada barra),Crie uma layerMask e a chame
                                      obs: Em baixo do nome do GameObject no Inspector */



	bool Desativa;


	//Para a animação

	public Animator anime;
	float iniciaAnim;
	float AntesDeLimpar;
	int  DesAnim = 0;

    public bool tapped;


	public static bool GoAnim;

    public BattleController battleController;
    public GameObject barratouch;

    public bool jaDeuDano;

	// Use this for initialization
	void Awake ()
    {

	 anime = this.GetComponent<Animator> ();
     Corzinha = LayerMask.GetMask("GUI"); // Setando a layer que você quer atingir
     

    }
	
	// Update is called once per frame
	void Update ()
    {

        
		Debug.DrawRay(transform.position, transform.forward, Color.blue); 
		/*Desenha uma linha para verificar se o Ray(Raio)
		           esta indo na direção das imagens(Aproxime da Barra de checagem que ela é visivel) 
		                           pode apagar essa linha após ter certeza que esta na direção certa*/

        
		Verifica();


		// Usa uma variavel do script "ControleDeBarras" para limpar a tela
		if(Desativa == true)
		{




			//ControleBarras.LimpaTela = true;
			//Desativa = false;

			AntesDeLimpar += Time.deltaTime;

			if(AntesDeLimpar >= 1)
			{

				ControleBarras.LimpaTela = true;
				AntesDeLimpar = 0;
				DesAnim = 0;
				anime.SetBool ("Move",false);
				anime.speed = 1;
				iniciaAnim = 0;
				Desativa = false;

			}



		}


		if(GoAnim == true)
		{

			iniciaAnim += Time.deltaTime;

			if(iniciaAnim >= 3)
			{
            barratouch.SetActive(true);
            anime.SetBool ("Move",true);
			GoAnim = false;

			}

		}


        

    }



	/*Observações: o Raycast Sai da linha de Pivot da imagem(Pèlo o que pude constatar) */





    void Verifica()
	{


		if ((tapped  || DesAnim == 1) && jaDeuDano==false) 
		{

            anime.speed = 0;

            TiroRaio.origin = transform.position;
            TiroRaio.direction = transform.forward;

            if (DesAnim == 1)
            {
                battleController.DoAtk("Miss");
                tapped = false;
                jaDeuDano = true;
                Debug.Log("Miss");  // Aqui você retorna a informaçãodo tipo do ataque para o combate
                Desativa = true;

            }
            else
            {

                if (Physics.Raycast(TiroRaio, out PontoDeColisao, 10, Corzinha))
                {

                    if (PontoDeColisao.collider.CompareTag("Critico"))
                    {

                        battleController.DoAtk("Critical");
                        tapped = false;
                        jaDeuDano = true;
                        Debug.Log("Critico"); // Aqui você retorna a informaçãodo tipo do ataque para o combate
                        Desativa = true;


                    }

                    if (PontoDeColisao.collider.CompareTag("Miss"))
                    {

                        battleController.DoAtk("Miss");
                        tapped = false;
                        jaDeuDano = true;
                        Debug.Log("Miss");  // Aqui você retorna a informaçãodo tipo do ataque para o combate
                        Desativa = true;

                    }

                    if (PontoDeColisao.collider.CompareTag("Normal"))
                    {

                        battleController.DoAtk("Normal");
                        tapped = false;
                        jaDeuDano = true;
                        Debug.Log("Normal"); // Aqui você retorna a informaçãodo tipo do ataque para o combate
                        Desativa = true;

                    }

                }
            }
			

            tapped = false;



        }

	}





	public void Dst(int atv)
	{

		DesAnim = atv;
       

    }




}
