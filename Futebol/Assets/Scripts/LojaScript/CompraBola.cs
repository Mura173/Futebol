using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompraBola : MonoBehaviour
{
    public int bolasIDe;

    public void CompraBolaBtn()
    {
        for (int i = 0; i < BolasShop.instance.bolasList.Count; i++)
        {
            if (BolasShop.instance.bolasList[i].bolasID == bolasIDe && !BolasShop.instance.bolasList[i].bolasComprou)
            {
                BolasShop.instance.bolasList[i].bolasComprou = true;
            }
        }

        BolasShop.instance.UpdateSprite(bolasIDe);
    }
}
