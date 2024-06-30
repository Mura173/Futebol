using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompraBola : MonoBehaviour
{
    public int bolasIDe;
    public Text btnText;

    public void CompraBolaBtn()
    {
        for (int i = 0; i < BolasShop.instance.bolasList.Count; i++)
        {
            if (BolasShop.instance.bolasList[i].bolasID == bolasIDe && !BolasShop.instance.bolasList[i].bolasComprou)
            {
                BolasShop.instance.bolasList[i].bolasComprou = true;
                UpdateCompraBtn();
            }
            else if (BolasShop.instance.bolasList[i].bolasID == bolasIDe && BolasShop.instance.bolasList[i].bolasComprou)
            {
                UpdateCompraBtn();
            }
        }

        BolasShop.instance.UpdateSprite(bolasIDe);
    }

    void UpdateCompraBtn()
    {
        btnText.text = "Usando";

        for (int i = 0; i < BolasShop.instance.compraBtnList.Count; i++)
        {
            CompraBola compraBolaScript = BolasShop.instance.compraBtnList[i].GetComponent<CompraBola>();

            for (int j = 0; j < BolasShop.instance.bolasList.Count; j++)
            {
                if (BolasShop.instance.bolasList[j].bolasID == compraBolaScript.bolasIDe &&
                    BolasShop.instance.bolasList[j].bolasComprou && BolasShop.instance.bolasList[j].bolasID != bolasIDe)
                {
                    compraBolaScript.btnText.text = "Use";
                }
            }
        }
    }
}
