using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    //Vars for UI TEXT
    public TextMesh UI_HEALTH, UI_DAMAGE, UI_RESIST, UI_LEVEL, UI_HEALTH_TOTAL;
    public BattleController battleController;

    //Vars for lifebar
    public GameObject CurrentLifeBar;
    public float PercentualOfLifeBar;

    //Aniamtor 
    Animator animator;

    // Use this for initialization
    void Start () {

        //FindAnimator
        animator = gameObject.GetComponentInChildren<Animator>();

    }

    //CRIEI ESSE METODO PORQUE ELE ESTA INICIANDO MUITO RAPIDO, ELE SERÁ CHAMADO NO BATTLE CONTROLLER, DEPOIS QUE GERAR OS ATRIBUTOS DO INIMIGO
    public void InicializarAsConfiguracoes()
    {
        battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
        //Att UI[ATT]
        UI_HEALTH.text = battleController.e_health.ToString();
        UI_DAMAGE.text = battleController.e_damage.ToString();
        UI_RESIST.text = battleController.e_resistance.ToString();
        UI_HEALTH_TOTAL.text = "/ " + battleController.e_total_health.ToString();
        // UI_LEVEL.text = "Level " + battleController.level.ToString();

        //HEALTH BAR
        //Calculo do percentual de vida, 1 de vida equivale ao PercentualOfLifeBar
        PercentualOfLifeBar = 1 / battleController.e_total_health;
        attLifeBar();

    }


    public void attUI()
    {

        //Att UI[ATT]
        UI_HEALTH.text = battleController.e_health.ToString();
        UI_DAMAGE.text = battleController.e_damage.ToString();
        UI_RESIST.text = battleController.e_resistance.ToString();
        attLifeBar();

    }
    //Metodo que atualiza a barra, no caso a escala, chamando um outro metodo que calcula a escala, e retorna um float da mesma.
    public void attLifeBar()
    {
        CurrentLifeBar.transform.localScale = new Vector3(AttValueScale(battleController.e_health), 1, 1);
    }
    //Calcula a escala atual da barra, e retorna a mesma.
    public float AttValueScale(float health)
    {
        float currentHeath = health;
        float scale;
        scale = currentHeath * PercentualOfLifeBar;
        return scale;
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
