using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text pontosUI, bolasUI;

    [SerializeField]
    private GameObject losePainel, winPainel, pausePainel;
    [SerializeField]
    private Button pauseBtn, pauseBtn_Return;
    private Button restartBtn;

    // You Lose
    [SerializeField]
    private Button novamenteBTNLOSE, levelBTNLOSE;

    // You Win
    private Button levelBTNWIN, novamenteBTNWIN, avancaBTNWIN;

    public int moedasNumAntes, moedasNumDepois, resultado;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;

        PegaDados();
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        PegaDados();
    }

    void PegaDados()
    {
        if (OndeEstou.instance.fase >= 3)
        {
            // Elementos UI Pontos e Bolas
            pontosUI = GameObject.FindGameObjectWithTag("PontosUI").GetComponent<Text>();
            bolasUI = GameObject.FindGameObjectWithTag("bolasUI").GetComponent<Text>();

            // Paineis
            losePainel = GameObject.FindGameObjectWithTag("Lose_Panel");
            winPainel = GameObject.FindGameObjectWithTag("Win_Panel");
            pausePainel = GameObject.FindGameObjectWithTag("Pause_Panel");

            // Botões Pause
            pauseBtn = GameObject.FindGameObjectWithTag("pause").GetComponent<Button>();
            pauseBtn_Return = GameObject.FindGameObjectWithTag("cont").GetComponent<Button>();

            // Botao Reiniciar
            restartBtn = GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>();

            // Botões Lose
            novamenteBTNLOSE = GameObject.FindGameObjectWithTag("novamenteLOSE").GetComponent<Button>();
            levelBTNLOSE = GameObject.FindGameObjectWithTag("MenuFasesLOSE").GetComponent<Button>();

            // Botões Win
            levelBTNWIN = GameObject.FindGameObjectWithTag("MenuFasesWIN").GetComponent<Button>();
            novamenteBTNWIN = GameObject.FindGameObjectWithTag("novamenteWIN").GetComponent<Button>();
            avancaBTNWIN = GameObject.FindGameObjectWithTag("avancar").GetComponent<Button>();

            // Eventos

            // Eventos Pause
            pauseBtn.onClick.AddListener(Pause);
            pauseBtn_Return.onClick.AddListener(PauseReturn);

            // Eventos You Lose
            novamenteBTNLOSE.onClick.AddListener(JogarNovamente);
            levelBTNLOSE.onClick.AddListener(Levels);

            // Eventos You Win
            novamenteBTNWIN.onClick.AddListener(JogarNovamente);
            levelBTNWIN.onClick.AddListener(Levels);
            avancaBTNWIN.onClick.AddListener(ProximaFase);

            moedasNumAntes = PlayerPrefs.GetInt("moedasSave");
        }
    }

    public void StartUI()
    {
        LigaDesligaPainel();
    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();

        moedasNumDepois = ScoreManager.instance.moedas;
    }

    public void GameOverUI()
    {
        losePainel.SetActive(true);
    }

    public void WinGameUI()
    {
        winPainel.SetActive(true);
    }

    void LigaDesligaPainel()
    {
        StartCoroutine(Tempo());
    }

    void Pause()
    {
        pausePainel.SetActive(true);
        pausePainel.GetComponent<Animator>().Play("MoveUI_Pause");
        Time.timeScale = 0;
    }

    void PauseReturn()
    {
        pausePainel.GetComponent<Animator>().Play("MoveUI_PauseR");
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());
    }

    IEnumerator EsperaPause()
    {
        yield return new WaitForSeconds(0.8f);
        pausePainel.SetActive(false);
    }

    // IEnumerator para pegar as devidas informações e, quando o jogo inicar, o painel desligar automaticamente
    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(0.001f);
        losePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);
    }

    // You Lose

    void JogarNovamente()
    {
        if (GameManager.instance.win == false)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
        }
        else
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = 0;
        }
    }
    void Levels()
    {
        if (GameManager.instance.win == false)
        {
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
            SceneManager.LoadScene(1);
        }
        else
        {
            resultado = 0;
            SceneManager.LoadScene(1);
        }
    }

    void ProximaFase()
    {
        if (GameManager.instance.win == true)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);
        }
    }
}
