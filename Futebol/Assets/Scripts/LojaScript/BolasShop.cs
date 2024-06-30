using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{
    public static BolasShop instance;

    public List<Bolas> bolasList = new List<Bolas>();
    public List<GameObject> bolaSuporteList = new List<GameObject>();

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
            item.btnCompra.GetComponent<CompraBola>().bolasIDe = b.bolasID;

            // Lista bolaSuporteList

            bolaSuporteList.Add(itensBola);

            if(b.bolasComprou == true)
            {
                item.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + b.bolasNomeSprite);
                item.bolaPreco.text = "Comprado!";
            }
            else
            {
                item.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + b.bolasNomeSprite + "_cinza");
            }
        }
    }

    public void UpdateSprite(int bola_id)
    {
        for(int i = 0; i < bolaSuporteList.Count; i++)
        {
            BolasSuporte bolasSuporteScript = bolaSuporteList[i].GetComponent<BolasSuporte>();
            
            if(bolasSuporteScript.bolaID == bola_id)
            {
                for(int j = 0; j < bolasList.Count; j++)
                {
                    if(bolasList[j].bolasID == bola_id)
                    {
                        if(bolasList[j].bolasComprou == true)
                        {
                            bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + bolasList[j].bolasNomeSprite);
                            bolasSuporteScript.bolaPreco.text = "Comprado!";
                        }
                        else
                        {
                            bolasSuporteScript.bolaSprite.sprite = Resources.Load<Sprite>("SpritesBola/" + bolasList[j].bolasNomeSprite + "_cinza");
                        }
                    }
                }
            }
        }
    }
}
