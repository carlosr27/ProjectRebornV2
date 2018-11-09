using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using SimpleJSON;

//Criar as habilidades, e a IA(breve).

public class BattleController : MonoBehaviour {

    // file name for json.
    public string fileName;

    //variavel que recebera as variaveis converrtidas em json
    JsonData playerJson;

    //Array for load the monsters prefabs.
    public GameObject[] monster_array;

    //Array for load the enemy monsters prefab
    public GameObject[] e_monster_array;

    //current active Monster GameObject FOR MUTATE THE ANIMATOR or XYZ or Scale
    public GameObject current_Monster;

    //current active ENEMY MONSTER
    public GameObject e_current_Monster;

    //Variable of HUD GAME
    public GameObject typeAtk;
    public GameObject damage_HUD_player, damage_HUD_enemy;

    //Variables for Player
    public float health, total_health;
    public float damage, total_damage;
    public float resistance, total_resistance;
    public float energy, total_energy;
    public float toxina;
    public int level;


    //Variables for Enemy
    public float e_health, e_total_health;
    public float e_damage, e_total_damage;
    public float e_resistance, e_total_resistance;
    public float LevelDamage_increase;
    public string enemyMonster;

    //VARIAVEIS PARA CONTROLE DE BATALHA
    //turno, true = atk | false = def
    public bool turno = true;

    //Variaveis da barrinha
    public ControleBarras ControleBarras;
    public checacor ChecaCor;

    public GameObject barraTouch;

    public bool finished;

    public Animator cameraAnim;

    public string lastMonster;

    public GameObject gameover_lose, gameover_win, game_Over;
    public TextMesh TXT_Cobre, TXT_Ferro, TXT_Chumbo, TXT_Niquel, TXT_URANIO, TXT_OURO;

    void Start () {

        getLastMonster();
        CriarAtributoInimigo();
        RandomEnemy();
        getEnemyMonster(enemyMonster);
        //Inicialização do monstro inimigo. CHAMANDO O "START" GENERICO DO ENEMYBEHAVIOUR 
        e_current_Monster.GetComponent<EnemyBehaviour>().InicializarAsConfiguracoes();

    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space)){

            health--;
            current_Monster.GetComponent<PlayerBehaviour>().attUI();
            e_health--;
            e_current_Monster.GetComponent<EnemyBehaviour>().attUI();

        }
       
    }

    //FUNÇÃO DO PROPRIO JSON READER
    string Read()
    {

        /*Set the JSON file directory (in this case, the file is in Assets/). If you download file, upgrade this line for "/JsonFileReader/" if you paste this folder in
        Assets file. */
        string filePath = (Application.persistentDataPath + "/" + fileName);
        StreamReader sr = new StreamReader(filePath);
        string content = sr.ReadToEnd();
        sr.Close();

        return content;

    }

    //RECEBER O VALOR DO MONSTRO ESCOLHIDO, INSTANCIA-LO e receber também as variaveis do mesmo
    public void getLastMonster()
    {
        //Atribuir um arquivo ao file name para achar o arquivo na pasta de json's
        fileName = "lastMonster.json";

        //Essa string str vai receber um valor retornado do metodo read, que le o arquivo .json e transforma tudo numa string, 
        //no caso srt
        string str = Read();

        //Converter a string em um JSONNode, ou seja ela supostamente pega a string e transforma numa variavel json
        JSONNode json = JSON.Parse(str);

        //Debug
        Debug.Log(json["monster_choose"]);

        //Pega a variavel de dentro do arquivo json chamada monster_choose que é o monstro escolhido no lab
        lastMonster = json["monster_choose"];

        //Carregar todos os prefabs de dentro da pasta resources/Prefabs, que são os monstros
        monster_array = Resources.LoadAll<GameObject>("Prefabs");

        //Verificar cada indice do monster_array e comparar com o nome do monster_choose, afim de instanciar o monstro escolhido
        for (int i = 0; i < monster_array.Length; i++)
        {
            if (monster_array[i].name == lastMonster)
            {
                //Instanciar o monstro numa variavel suporte do proprio metodo, e depois atribuir essa variavel 
                //suporte a variavel master do battle controller
                GameObject instantiateMonster = Instantiate(monster_array[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                current_Monster = instantiateMonster;
            }
        }

        //RECEBER AS VARIAVEIS DO MONSTRO, DE DENTRO DO JSON

            fileName = lastMonster + ".json";
            str = Read();
            json = JSON.Parse(str);


            //get values of monster in json file with the parameter
            int _damage = int.Parse(json["damage"].Value);
            int _health = int.Parse(json["health"].Value);
            int _resistance = int.Parse(json["resistance"].Value);
            int _level = int.Parse(json["level"].Value);
            int _energy = int.Parse(json["energy"].Value);

            int _total_damage = int.Parse(json["total_damage"].Value);
            int _total_health = int.Parse(json["total_health"].Value);
            int _total_resistance = int.Parse(json["total_resistance"].Value);
            int _total_energy = int.Parse(json["total_energy"].Value);

            Debug.Log(_damage);
            Debug.Log(_health);
            Debug.Log(_resistance);
            Debug.Log(_level);
            Debug.Log(_energy);

            total_damage = _total_damage;
            total_health = _total_health;
            total_resistance = _total_resistance;
            total_energy = _total_energy;

            health = _health;
            damage = _damage;
            resistance = _resistance;
            energy = _energy;
            level = _level;
            toxina = int.Parse(json["toxi"].Value);
    }

    //Gerar o dano/resistencia/vida do inimigo
    public void CriarAtributoInimigo()
    {
        LevelDamage_increase = level / 10f;

        e_total_damage += Mathf.RoundToInt((total_damage * LevelDamage_increase) + total_damage);
        e_total_health += Mathf.RoundToInt((total_health * LevelDamage_increase) + total_health);
        e_total_resistance += Mathf.RoundToInt((total_resistance * LevelDamage_increase) + total_resistance);

        e_damage = e_total_damage;
        e_resistance = e_total_resistance;
        e_health = e_total_health;
    }

    //Randomizar um monstro inimigo
    public void RandomEnemy()
    {
       
        int enemy;
        enemy = Random.Range(0, 2);
        if(enemy == 0)
        {
            enemyMonster = "Aguia";
        }
        else if(enemy == 1)
        {
            enemyMonster = "Mamute";
        }
   
    }

    //Instanciar o monstro inimigo após a randomizaçao
    public void getEnemyMonster(string enemy)
    {

        //Carregar todos os prefabs de dentro da pasta resources/Prefabs, que são os monstros
        e_monster_array = Resources.LoadAll<GameObject>("Prefabs_bot");


        //Verificar cada indice do monster_array e comparar com o nome do monster_choose, afim de instanciar o monstro escolhido
        for (int i = 0; i < e_monster_array.Length; i++)
        {
            if (e_monster_array[i].name == enemy+"_BOT")
            {
                //Instanciar o monstro numa variavel suporte do proprio metodo, e depois atribuir essa variavel 
                //suporte a variavel master do battle controller
                GameObject instantiateMonster = Instantiate(e_monster_array[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                e_current_Monster = instantiateMonster;
            }
        }
    }

    //Fazer atk
    public void DoAtk(string type)
    {
        ChecaCor.jaDeuDano = true;

        if (type == "Miss")
        {
            Debug.Log("Faz nada");
            if (turno)
            {
                turno = false;
            }
            else
            {
                turno = true;
            }

            typeAtk.gameObject.SetActive(true);
            typeAtk.GetComponent<TextMesh>().text = "Errou o ataque";
            typeAtk.GetComponent<TextMesh>().color = new Color(255, 0, 0);


            WinOrLose();
            e_current_Monster.GetComponent<EnemyBehaviour>().StartAnim(2);

            damage_HUD_enemy.gameObject.SetActive(true);
            damage_HUD_enemy.GetComponent<TextMesh>().text = "errou";

            //Atualiza a UI do inimigo
            e_current_Monster.GetComponent<EnemyBehaviour>().attUI();
            //Espera um tempo para ligar a UI de novo
            StartCoroutine(WaitTimeAndShowUI(5f));
        }
        else if(type == "Critical")
        {
            e_health -= Mathf.RoundToInt(damage * 0.5f) + damage;
            if (turno)
            {
                turno = false;
            }
            else
            {
                turno = true;
            }

            typeAtk.gameObject.SetActive(true);
            typeAtk.GetComponent<TextMesh>().text = "Ataque critico";
            typeAtk.GetComponent<TextMesh>().color = new Color(0, 255, 0);

            WinOrLose();

            if (finished)
            {
                gameOver();

                //Disabilita todos os botões(para n clicar de novo)
                current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
                //Atualiza a UI do inimigo
                e_current_Monster.GetComponent<EnemyBehaviour>().attUI();
                typeAtk.gameObject.SetActive(false);
            }
            else
            {
                if (lastMonster == "Mamute")
                {
                    cameraAnim.SetBool("Ativa", true);
                }

                current_Monster.GetComponent<PlayerBehaviour>().StartAnim(3);
                e_current_Monster.GetComponent<EnemyBehaviour>().StartAnim(4);
                damage_HUD_enemy.gameObject.SetActive(true);
                damage_HUD_enemy.GetComponent<TextMesh>().text = "-" + (Mathf.RoundToInt(damage * 0.5f) + damage);

                //Atualiza a UI do inimigo
                e_current_Monster.GetComponent<EnemyBehaviour>().attUI();
                //Espera um tempo para ligar a UI de novo
                StartCoroutine(WaitTimeAndShowUI(5f));
            }
            
        }
        else
        {
            e_health -= damage;
            if (turno)
            {
                turno = false;
            }
            else
            {
                turno = true;
            }

            typeAtk.gameObject.SetActive(true);
            typeAtk.GetComponent<TextMesh>().text = "Ataque comum";
            typeAtk.GetComponent<TextMesh>().color = new Color(255, 255, 0);

            WinOrLose();

            if (finished)
            {
                gameOver();

                ///Disabilita todos os botões(para n clicar de novo)
                current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
                //Atualiza a UI do inimigo
                e_current_Monster.GetComponent<EnemyBehaviour>().attUI();
                typeAtk.gameObject.SetActive(false);
            }
            else
            {
                current_Monster.GetComponent<PlayerBehaviour>().StartAnim(1);
                e_current_Monster.GetComponent<EnemyBehaviour>().StartAnim(2);
                damage_HUD_enemy.gameObject.SetActive(true);
                damage_HUD_enemy.GetComponent<TextMesh>().text = "-" + damage;

                //Atualiza a UI do inimigo
                e_current_Monster.GetComponent<EnemyBehaviour>().attUI();
                //Espera um tempo para ligar a UI de novo
                StartCoroutine(WaitTimeAndShowUI(5f));
            }
            
        }
    }

    //Fazer def
    public void DoDef(string type)
    {
        if (type == "Miss")
        {
            Debug.Log("Faz nada");
            if (turno)
            {
                turno = false;
            }
            else
            {
                turno = true;
            }
            typeAtk.gameObject.SetActive(true);
            typeAtk.GetComponent<TextMesh>().text = "Inimigo errou";
            typeAtk.GetComponent<TextMesh>().color = new Color(255, 0, 0);


            current_Monster.GetComponent<PlayerBehaviour>().StartAnim(2);

            //Disabilita todos os botões(para n clicar de novo)
            current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
            //Atualiza a UI do inimigo
            current_Monster.GetComponent<PlayerBehaviour>().attUI();
            //Espera um tempo para ligar a UI de novo
            StartCoroutine(WaitTimeAndShowUI(5f));

            damage_HUD_player.gameObject.SetActive(true);
            damage_HUD_player.GetComponent<TextMesh>().text = "errou";

        }
        else if (type == "Critical")
        {
            health -= Mathf.RoundToInt(e_damage * 0.5f) + e_damage;
            if (turno)
            {
                turno = false;
            }
            else
            {
                turno = true;
            }

            typeAtk.gameObject.SetActive(true);
            typeAtk.GetComponent<TextMesh>().text = "Critico do inimigo";
            typeAtk.GetComponent<TextMesh>().color = new Color(0, 255, 0);

            WinOrLose();

            if (finished)
            {
                gameOver();

                //Disabilita todos os botões(para n clicar de novo)
                current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
                //Atualiza a UI do inimigo
                current_Monster.GetComponent<PlayerBehaviour>().attUI();
                typeAtk.gameObject.SetActive(false);
            }
            else
            {
                if(enemyMonster == "Mamute")
                {
                    cameraAnim.SetBool("Ativa", true);
                }

                e_current_Monster.GetComponent<EnemyBehaviour>().StartAnim(3);
                current_Monster.GetComponent<PlayerBehaviour>().StartAnim(4);

                //Disabilita todos os botões(para n clicar de novo)
                current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
                //Atualiza a UI do inimigo
                current_Monster.GetComponent<PlayerBehaviour>().attUI();
                //Espera um tempo para ligar a UI de novo
                StartCoroutine(WaitTimeAndShowUI(5f));

                damage_HUD_player.gameObject.SetActive(true);
                damage_HUD_player.GetComponent<TextMesh>().text = "-" + (Mathf.RoundToInt(e_damage * 0.5f) + e_damage);
            }

        }
        else
        {
            
            health -= e_damage;
            if (turno)
            {
                turno = false;
            }
            else
            {
                turno = true;
            }

            typeAtk.gameObject.SetActive(true);
            typeAtk.GetComponent<TextMesh>().text = "Ataque comum";
            typeAtk.GetComponent<TextMesh>().color = new Color(255, 255, 0);

            WinOrLose();

            if (finished)
            {
                gameOver();

                //Disabilita todos os botões(para n clicar de novo)
                current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
                //Atualiza a UI do inimigo
                current_Monster.GetComponent<PlayerBehaviour>().attUI();
                typeAtk.gameObject.SetActive(false);
            }
            else
            {
                e_current_Monster.GetComponent<EnemyBehaviour>().StartAnim(1);
                current_Monster.GetComponent<PlayerBehaviour>().StartAnim(2);
                damage_HUD_player.gameObject.SetActive(true);
                damage_HUD_player.GetComponent<TextMesh>().text = "-" + e_damage;

                //Disabilita todos os botões(para n clicar de novo)
                current_Monster.GetComponent<PlayerBehaviour>().DisableAllButtons();
                //Atualiza a UI do inimigo
                current_Monster.GetComponent<PlayerBehaviour>().attUI();
                //Espera um tempo para ligar a UI de novo
                StartCoroutine(WaitTimeAndShowUI(5f));
            }

            

        }

    }

    IEnumerator WaitTimeAndShowUI( float time)
    {
        Debug.Log("Desapareceu");
        yield return new WaitForSeconds(time);

        cameraAnim.SetBool("Ativa", false);
        typeAtk.gameObject.SetActive(false);
        damage_HUD_player.gameObject.SetActive(false);
        damage_HUD_enemy.gameObject.SetActive(false);
        current_Monster.GetComponent<PlayerBehaviour>().EnableDisableAtkButtons();
        ChecaCor.jaDeuDano = false;
    }

    IEnumerator WaitDeath(float time, bool result)
    {
        Debug.Log("Morreu");
        yield return new WaitForSeconds(time);

        if (result)
        {

            Destroy(current_Monster);
            game_Over.SetActive(true);
            gameover_win.SetActive(true);
            GenerateItens(true);
        }
        else
        {

            Destroy(current_Monster);
            game_Over.SetActive(true);
            gameover_lose.SetActive(true);
            GenerateItens(false);
        }
        


    }

    public void ClicouBarra()
    {
        ChecaCor.tapped = true;
        barraTouch.SetActive(false);
    }

    public void WinOrLose()
    {
        if(health <= 0)
        {
            health = 0;
            finished = true;
        }
        else if(e_health <= 0)
        {
            e_health = 0;
            finished = true;
        }
        else
        {

        }
    }

    public void gameOver()
    {
        if(e_health == 0)
        {
            e_current_Monster.GetComponent<EnemyBehaviour>().StartAnim(5);
            StartCoroutine(WaitDeath(6f, true));

        }
        else
        {
            current_Monster.GetComponent<PlayerBehaviour>().StartAnim(5);
            StartCoroutine(WaitDeath(6f, false));

        }
    }

    public void GenerateItens(bool result)
    {
        int mate1qnt, mate2qnt, mate3qnt, mate4qnt, mate5qnt, mate6qnt;

        fileName = "Materiais.json";

        string str = Read();

        JSONNode json = JSON.Parse(str);

        mate1qnt = int.Parse(json["mate1qnt"]);
        mate2qnt = int.Parse(json["mate2qnt"]);
        mate3qnt = int.Parse(json["mate3qnt"]);
        mate4qnt = int.Parse(json["mate4qnt"]);
        mate5qnt = int.Parse(json["mate5qnt"]);
        mate6qnt = int.Parse(json["mate6qnt"]);

        Debug.Log("Bronze = "+ mate1qnt);
        Debug.Log("Ferro = "+ mate2qnt);
        Debug.Log("Obsidiana = "+ mate3qnt);
        Debug.Log("Chumbo = " + mate4qnt);
        Debug.Log("Ouro = " + mate5qnt);
        Debug.Log("Urânio = " + mate6qnt);

        //IF TRUE WIN ELSE LOSE
        if(result == true)
        {
            for(int i = 0; i < 6; i++)
            {
                int currentMaterial = 0;
                int randomNumberOfSomething = 0;

                //GERANDO BRONZE
                if (i == 0)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if(currentMaterial <= 17)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 10);
                    }
                    TXT_Cobre.text = "+" + randomNumberOfSomething;
                    mate1qnt += randomNumberOfSomething;
                }
                //GERANDO FERRO
                else if (i == 1)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 13 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 13)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 8);
                    }
                    TXT_Ferro.text = "+" + randomNumberOfSomething;
                    mate2qnt += randomNumberOfSomething;
                }

                //GERANDO OBSIDIANA
                else if (i == 2)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 10 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 10)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 6);
                    }
                    TXT_Niquel.text = "+" + randomNumberOfSomething;
                    mate3qnt += randomNumberOfSomething;
                }

                //GERANDO CHUMBO
                else if (i == 3)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO  7 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 7)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 4);
                    }
                    TXT_Chumbo.text = "+" + randomNumberOfSomething;
                    mate4qnt += randomNumberOfSomething;
                }

                //GERANDO OURO
                else if (i == 4)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 5 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 5)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 3);
                    }
                    TXT_OURO.text = "+" + randomNumberOfSomething;
                    mate5qnt += randomNumberOfSomething;
                }

                //GERANDO URANIO
                else if (i == 5)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 3 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 3)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 2);
                    }
                    TXT_URANIO.text = "+" + randomNumberOfSomething;
                    mate6qnt += randomNumberOfSomething;
                }

            }
        }

        //SE PERDEU
        else
        {
            for (int i = 0; i < 6; i++)
            {
                int currentMaterial = 0;
                int randomNumberOfSomething = 0;

                //GERANDO BRONZE
                if (i == 0)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 8)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 10);
                    }
                    TXT_Cobre.text = "+" + randomNumberOfSomething;
                    mate1qnt += randomNumberOfSomething;
                }
                //GERANDO FERRO
                else if (i == 1)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 6)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 8);
                    }
                    TXT_Ferro.text = "+" + randomNumberOfSomething;
                    mate2qnt += randomNumberOfSomething;
                }

                //GERANDO OBSIDIANA
                else if (i == 2)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 5)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 6);
                    }
                    TXT_Niquel.text = "+" + randomNumberOfSomething;
                    mate3qnt += randomNumberOfSomething;
                }

                //GERANDO CHUMBO
                else if (i == 3)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 3)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 4);
                    }
                    TXT_Chumbo.text = "+" + randomNumberOfSomething;
                    mate4qnt += randomNumberOfSomething;
                }

                //GERANDO OURO
                else if (i == 4)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 2)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 3);
                    }
                    TXT_OURO.text = "+" + randomNumberOfSomething;
                    mate5qnt += randomNumberOfSomething;
                }

                //GERANDO URANIO
                else if (i == 5)
                {
                    //CHANCE DE 1 A 20 PARA GANHAR
                    currentMaterial = Random.Range(0, 20);
                    //SE FOR MENOR QUE A CHANCE(NO CASO 17 EM  20) ELE É PERMITIDO GANHAR
                    if (currentMaterial <= 1)
                    {
                        //AQUI É GERADO O NUMERO DE MATERIAIS QUE A PESSOA GANHOU
                        randomNumberOfSomething = Random.Range(0, 2);
                    }
                    TXT_URANIO.text = "+" + randomNumberOfSomething;
                    mate6qnt += randomNumberOfSomething;
                }

            }
        }

        //FIM DO GERADOR AGORA SALVA
        Materiais materials;
        Debug.Log("Salvando");
        materials = new Materiais(mate1qnt, mate2qnt, mate3qnt, mate4qnt, mate5qnt, mate6qnt);
        playerJson = JsonMapper.ToJson(materials);
        //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
        File.WriteAllText((Application.persistentDataPath + "/Materiais.json"), playerJson.ToString());

    }

    //TIRA ENERGIA DAQ
    public void Button_lab()
    {
        fileName = lastMonster+".json";

        string str = Read();

        JSONNode json = JSON.Parse(str);

        int energy;
        energy = int.Parse(json["energy"]);
        energy -= 10;

        Debug.Log("Mamute.json do not exist");
        int _damage = Mathf.RoundToInt(total_damage);
        int _health = Mathf.RoundToInt(total_health);
        int _resistance = Mathf.RoundToInt(total_resistance);
        int _energy = Mathf.RoundToInt(total_energy);
        int _toxi = 0;

        _energy = energy;
        int _total_damage = Mathf.RoundToInt(total_damage);
        int _total_health = Mathf.RoundToInt(total_health);
        int _total_resistance = Mathf.RoundToInt(total_resistance);
        int _total_energy = Mathf.RoundToInt(total_energy);


        level = 1;

        Monster_Create Monster_Create;
        Monster_Create = new Monster_Create(_total_damage, _total_health, _total_resistance, _total_energy, _damage, _health, _resistance, level, _energy, _toxi);
        playerJson = JsonMapper.ToJson(Monster_Create);
        //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
        File.WriteAllText((Application.persistentDataPath + "/"+lastMonster+".json"), playerJson.ToString());

        Application.LoadLevel("MonstersAdm");

    }

    public void Button_Continue()
    {
        fileName = lastMonster + ".json";

        string str = Read();

        JSONNode json = JSON.Parse(str);

        int energy;
        energy = int.Parse(json["energy"]);
        energy -= 10;

        Debug.Log("Mamute.json do not exist");
        int _damage = Mathf.RoundToInt(total_damage);
        int _health = Mathf.RoundToInt(total_health);
        int _resistance = Mathf.RoundToInt(total_resistance);
        int _energy = Mathf.RoundToInt(total_energy);
        int _toxi = 0;

        _energy = energy;
        int _total_damage = Mathf.RoundToInt(total_damage);
        int _total_health = Mathf.RoundToInt(total_health);
        int _total_resistance = Mathf.RoundToInt(total_resistance);
        int _total_energy = Mathf.RoundToInt(total_energy);


        level = 1;

        Monster_Create Monster_Create;
        Monster_Create = new Monster_Create(_total_damage, _total_health, _total_resistance, _total_energy, _damage, _health, _resistance, level, _energy, _toxi);
        playerJson = JsonMapper.ToJson(Monster_Create);
        //File.WriteAllText(Application.dataPath + "/lastMonster.json", playerJson.ToString());
        File.WriteAllText((Application.persistentDataPath + "/" + lastMonster + ".json"), playerJson.ToString());

        Application.LoadLevel("Battle");

    }


}
