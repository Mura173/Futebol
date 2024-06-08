using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [SerializeField]
    private Transform objE, objD, bola;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.jogoComecou == true)
        {
            if (bola == null && GameManager.instance.bolasEmCena > 0)
            {
                bola = GameObject.Find("bola(Clone)").GetComponent<Transform>();
            }
            else if (GameManager.instance.bolasEmCena > 0)
            {
                Vector3 posCam = transform.position;
                posCam.x = bola.position.x;
                posCam.x = Mathf.Clamp(posCam.x, objE.position.x, objD.position.x);
                transform.position = posCam;
            }
        }
    }
}
