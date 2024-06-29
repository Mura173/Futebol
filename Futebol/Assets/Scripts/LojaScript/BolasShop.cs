using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{
    public static BolasShop instance;

    public List<Bolas> bolasList = new List<Bolas>();

    public GameObject baseBolaItem;
    public Transform conteudo;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        FillList();
    }

    
    void Update()
    {
        
    }

    void FillList()
    {
        foreach(Bolas b in bolasList)
        {
            GameObject itensBola = Instantiate(baseBolaItem) as GameObject;
            itensBola.transform.SetParent(conteudo, false);
            BolasSuporte item = itensBola.GetComponent<BolasSuporte>();

            item.bolaID = b.bolasID;
            item.bolaPreco.text = b.bolasPreco.ToString();

            if(b.bolasComprou == true)
            {
                item.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + b.bolasNomeSprite);
            }
            else
            {
                item.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + b.bolasNomeSprite + "_cinza");
            }
        }
    }
}
