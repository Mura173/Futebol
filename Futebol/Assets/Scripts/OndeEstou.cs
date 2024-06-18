using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public int fase = -1;
    [SerializeField]
    private GameObject uiManagerGO, gameManagerGO;
    public static OndeEstou instance;

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

        SceneManager.sceneLoaded += VerificaFase;
    }

    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene().buildIndex;

        if(fase != 4 && fase != 5)
        {
            Instantiate(uiManagerGO);
            Instantiate(gameManagerGO);
        }
    }
}
