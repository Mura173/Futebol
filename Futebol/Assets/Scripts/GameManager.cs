using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    void Start()
    {
        ScoreManager.instance.GameStartScoreM();
    }

    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
    }
}
