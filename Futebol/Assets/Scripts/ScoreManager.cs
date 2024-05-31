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

    // Startar o score fazendo uma verifica��o
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

    // Garantir com que a informa��o da pontua��o seja sempre exibida
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

    // Salvar as informa��es das moedas
    public void SalvaMoedas(int coin)
    {
        PlayerPrefs.SetInt("moedasSave", coin);
    }
}
