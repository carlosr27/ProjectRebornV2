using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    //Vars for UI TEXT
    public TextMesh UI_HEALTH, UI_DAMAGE, UI_RESIST, UI_LEVEL, UI_HEALTH_TOTAL;
    public BattleController battleController;

    //Vars for lifebar
    public GameObject CurrentLifeBar;
    public float PercentualOfLifeBar;

    //Vars for buttons
    public GameObject button_atk, button_def;

    //Aniamtor 
    Animator animator;

    //Type atk
    public string type;

    void Start()
    {
        battleController = GameObject.Find("BattleController").GetComponent<BattleController>();

        //Att UI[ATT]
        UI_HEALTH.text = battleController.health.ToString();
        UI_DAMAGE.text = battleController.damage.ToString();
        UI_RESIST.text = battleController.resistance.ToString();
        UI_HEALTH_TOTAL.text = "/ " +battleController.total_health.ToString();
        UI_LEVEL.text = "Level " + battleController.level.ToString();

        //HEALTH BAR
        //Calculo do percentual de vida, 1 de vida equivale ao PercentualOfLifeBar
        PercentualOfLifeBar = 1 / battleController.total_health ;
        attLifeBar();

        //FindAnimator
        animator = gameObject.GetComponentInChildren<Animator>();

        EnableDisableAtkButtons();

    }

    /* MAMUTE HAB */
    public void MamuteAtk()
    {
        battleController.ControleBarras.gameObject.SetActive(true);
        battleController.ControleBarras.processo = true;
        DisableAllButtons();
        Debug.Log("Ataque");
    }
    public void MamuteDef()
    {
        // A BARRINHA PRECISA ME RETORNAR ISSO
        int N_type;
        N_type = Random.Range(0, 3);

        if (N_type == 0)
        {
            type = "Miss";
        }
        else if (N_type == 1)
        {
            type = "Critical";
        }
        else
        {
            type = "Normal";
        }
        //COMEÇA A ANIMAÇÃO
        //faz o atk(damage, desativa a UI, espera e Ativa de novo)  BARRINHA CHAMA ISSO APOS SER CLICADA
        battleController.DoDef(type);
        Debug.Log("Defesa");

    }

    /* Aguia HAB */
    public void BirdAtk()
    {
        battleController.ControleBarras.gameObject.SetActive(true);
        battleController.ControleBarras.processo = true;
        DisableAllButtons();

        Debug.Log("Ataque");
    }
    public void BirdDef()
    {
        // A BARRINHA PRECISA ME RETORNAR ISSO
        int N_type;
        N_type = Random.Range(0, 3);

        if (N_type == 0)
        {
            type = "Miss";
        }
        else if (N_type == 1)
        {
            type = "Critical";
        }
        else
        {
            type = "Normal";
        }
        //COMEÇA A ANIMAÇÃO
        //faz o atk(damage, desativa a UI, espera e Ativa de novo)  BARRINHA CHAMA ISSO APOS SER CLICADA
        battleController.DoDef(type);
        Debug.Log("Defesa");
    }

    /* Peixe HAB */
    public void FishAtk()
    {
        Debug.Log("Ataque");
    }
    public void FishDef()
    {
        Debug.Log("Defesa");
    }

    public void attUI()
    {

        //Att UI[ATT]
        UI_HEALTH.text = battleController.health.ToString();
        UI_DAMAGE.text = battleController.damage.ToString();
        UI_RESIST.text = battleController.resistance.ToString();
        attLifeBar();

    }
    //Metodo que atualiza a barra, no caso a escala, chamando um outro metodo que calcula a escala, e retorna um float da mesma.
    public void attLifeBar()
    {
        CurrentLifeBar.transform.localScale = new Vector3(AttValueScale((battleController.health)), 1, 1);
    }
    //Calcula a escala atual da barra, e retorna a mesma.
    public float AttValueScale(float health)
    {
        float currentHeath = health;
        float scale;
        scale = currentHeath * PercentualOfLifeBar;
        return scale;
    }

    public void EnableDisableAtkButtons()
    {
        if (battleController.turno)
        {
            button_def.SetActive(false);
            button_atk.SetActive(true);
        }
        else
        {
            button_def.SetActive(true);
            button_atk.SetActive(false);
        }
    }

    public void DisableAllButtons()
    {
        button_atk.gameObject.SetActive(false);
        button_def.gameObject.SetActive(false);
    }

    public void StartAnim(int StateOfAnim)
    {
        animator.SetInteger("TipoAnim", StateOfAnim);
        StartCoroutine(waitForDesactive(1f));
    }

    IEnumerator waitForDesactive(float time)
    {
        Debug.Log("IGUAL A 0");
        yield return new WaitForSeconds(time);
        Debug.Log("Foi");
        animator.SetInteger("TipoAnim", 0);
    }



}
