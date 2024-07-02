using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{
    public static BolasShop instance;

    public List<Bolas> bolasList = new List<Bolas>();
    public List<GameObject> bolaSuporteList = new List<GameObject>();
    public List<GameObject> compraBtnList = new List<GameObject>();

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

            // Lista CompraBtn

            compraBtnList.Add(item.btnCompra);

            // Lista bolaSuporteList

            bolaSuporteList.Add(itensBola);

            if (PlayerPrefs.GetInt("BTN" + item.bolaID) == 1)
            {
                b.bolasComprou = true;
            }

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
                            SalvaBolasLojaInfo(bolasSuporteScript.bolaID);
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

    void SalvaBolasLojaInfo(int idBola)
    {
        for (int i = 0; i < bolasList.Count; i++)
        {
            BolasSuporte bolasSup = bolaSuporteList [i].GetComponent<BolasSuporte>();

            if (bolasSup.bolaID == idBola)
            {
                PlayerPrefs.SetInt("BTN" + bolasSup.bolaID, bolasSup.btnCompra ? 1 : 0);
            }
        }
    }
}
