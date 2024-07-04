using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevels : MonoBehaviour
{
    [SerializeField]
    private Text moedasLevel;
    void Start()
    {
        ScoreManager.instance.UpdateScore();
        moedasLevel.text = PlayerPrefs.GetInt("moedasSave").ToString();
    }
}
