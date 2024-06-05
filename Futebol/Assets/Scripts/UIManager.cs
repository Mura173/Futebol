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
    [SerializeField]
    private Button novamenteBTN, levelBTN;

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
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();
        bolasUI = GameObject.Find("bolasUI").GetComponent<Text>();
        losePainel = GameObject.Find("Lose_Panel");
        winPainel = GameObject.Find("Win_Panel");
        pausePainel = GameObject.Find("Pause_Panel");
        pauseBtn = GameObject.Find("pause").GetComponent<Button>();
        pauseBtn_Return = GameObject.Find("cont").GetComponent<Button>();
        novamenteBTN = GameObject.Find("novamentelose").GetComponent<Button>();
        levelBTN = GameObject.Find("MenuFases").GetComponent<Button>();

        pauseBtn.onClick.AddListener(Pause);
        pauseBtn_Return.onClick.AddListener(PauseReturn);

        // You Lose

        novamenteBTN.onClick.AddListener(JogarNovamente);

        moedasNumAntes = PlayerPrefs.GetInt("moedasSave");
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
        SceneManager.LoadScene(GameManager.instance.ondeEstou);

        resultado = moedasNumDepois - moedasNumAntes;
        ScoreManager.instance.PerdeMoedas(resultado);
        resultado = 0;
    }
}
