using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIniciar : MonoBehaviour
{
    private Animator barraAnim;
    private bool sobe;

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void AnimaMenu()
    {
        barraAnim = GameObject.FindGameObjectWithTag("BarraAnim").GetComponent<Animator>();
        if (sobe == false)
        {
            barraAnim.Play("MOVEUI");
            sobe = true;
        }
        else
        {
            barraAnim.Play("MOVEUI_Inverse");
            sobe = false;
        }
    }
}
