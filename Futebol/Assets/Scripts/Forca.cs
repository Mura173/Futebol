using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forca : MonoBehaviour
{

    [SerializeField] private Rigidbody2D bola;
    [SerializeField] private float force = 0;

    // Pegando da outra classe(código) "Rotacao"
    private Rotacao rot;

    public Image seta2Img;

    void Start()
    {
        bola = GetComponent<Rigidbody2D>();
        rot = GetComponent<Rotacao>();
    }

    
    void Update()
    {
        AplicaForca();
        ControlaForca();
    }

    void AplicaForca()
    {
        // Cosseno(x) e Seno(y), aplicado força em 2 eixos diferentes
        float x = force * Mathf.Cos (rot.zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin (rot.zRotate * Mathf.Deg2Rad);

        if (rot.liberaTiro == true)
        {
            // AddForce: Adiciona força
            bola.AddForce(new Vector2(x, y));
            rot.liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if (rot.liberaRot == true)
        {
            float moveX = Input.GetAxis("Mouse X");

            // Movimentando o mouse ou dedo para esquerda
            if (moveX < 0)
            {
                // Incrementando valor em fillAmount
                seta2Img.fillAmount += 0.8f * Time.deltaTime;

                // Valor incrementado em fillAmount multiplicado por mil
                force = seta2Img.fillAmount * 1000;
            }

            if (moveX > 0)
            {
                seta2Img.fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1000;
            }
        }
    }
}
