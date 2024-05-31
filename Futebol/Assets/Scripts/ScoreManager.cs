using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int moedas;

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
    }

    // Startar o score fazendo uma verificação
    public void GameStartScoreM()
    {
        if (PlayerPrefs.HasKey("moedasSave"))
        {
            moedas = PlayerPrefs.GetInt("moedasSave");
        }
        else
        {
            moedas = 100;
            PlayerPrefs.SetInt("moedasSave", moedas);
        }
    }

    // Garantir com que a informação da pontuação seja sempre exibida
    public void UpdateScore()
    {
        moedas = PlayerPrefs.GetInt("moedasSave");
    }

    // Coletando moedas
    public void ColetaMoedas(int coin)
    {
        moedas += coin;
        SalvaMoedas(moedas);
    }

    // Perdendo moedas
    public void PerdeMoedas(int coin)
    {
        moedas -= coin;
        SalvaMoedas(moedas);
    }

    // Salvar as informações das moedas
    public void SalvaMoedas(int coin)
    {
        PlayerPrefs.SetInt("moedasSave", coin);
    }
}
