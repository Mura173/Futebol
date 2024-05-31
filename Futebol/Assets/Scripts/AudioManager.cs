using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource musicaBG;

    public AudioClip[] clipsFX;
    public AudioSource sonsFX;

    public static AudioManager instance;
    void Awake()
    {
        // Fazer com que o audio fique em todas as cenas
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

    void Update()
    {
        if (!musicaBG.isPlaying)
        {
            musicaBG.clip = GetRandom();
            musicaBG.Play();
        }
    }

    // Randomizar o áudio
    AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void SonsFXToca(int index)
    {
        sonsFX.clip = clipsFX[index];
        sonsFX.Play();
    }
}
