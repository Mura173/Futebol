using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{
    private Animator info;
    private AudioSource musica;
    public Sprite somLigado, somDesligado;
    private Button btnSom;

    private void Start()
    {
        info = GameObject.FindGameObjectWithTag("MenuInfo").GetComponent<Animator>() as Animator;
        musica = GameObject.Find("AudioManager").GetComponent<AudioSource>() as AudioSource;
        btnSom = GameObject.Find("SOM").GetComponent<Button>() as Button;
    }

    public void AnimaInfo()
    {       
        info.Play("AnimaInfo");
    }

    public void AnimaInfoInverse()
    {
        info.Play("AnimaInfo_Inverse");
    }

    public void LigaDesligaSom()
    {
        musica.mute = !musica.mute;

        if (musica.mute == true)
        {
            btnSom.image.sprite = somDesligado;
        }
        else
        {
            btnSom.image.sprite = somLigado;
        }
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/mura_173/");
    }
}
