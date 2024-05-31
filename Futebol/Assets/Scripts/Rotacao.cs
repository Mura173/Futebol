using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotacao : MonoBehaviour
{
    // Posi��o Seta
    [SerializeField] private Transform posStart;

    // Seta
    [SerializeField] private Image setaImg;
    public GameObject setaGO;

    //�ngulo
    public float zRotate;

    public bool liberaRot = false;
    public bool liberaTiro = false;

    void Start()
    {
        PosicionaBola();
    }

    void Update()
    {
        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();
        PosicionaSeta();
    }

    void FixedUpdate()
    {
        
    }

    void PosicionaSeta()
    {
        // Posi��o da imagem da seta (Rect Transform � porque � da UI) vai ser igual a posi��o da bola
        setaImg.rectTransform.position = transform.position;
    }

    void PosicionaBola()
    {
        // "this" para representar o objeto bola, j� que o script j� est� anexado ao gameobjetct da bola
        this.gameObject.transform.position = posStart.position;
    }

    void RotacaoSeta()
    {
        // eulerAngles: Trabalhar com �ngulos
        setaImg.rectTransform.eulerAngles = new Vector3(0,0,zRotate);
    }

    void InputDeRotacao()
    {
        if (liberaRot == true)
        {            
            float moveY = Input.GetAxis("Mouse Y");

            // Se a rota��o estiver menor que 90�
            if (zRotate < 90)
            {
                // Se o eixo do mouse estiver maior que a �ngula��o 0, adiciona 1 na rota��o
                if (moveY > 0)
                {
                    zRotate += 1f;
                }
            }

            // Se a rota��o estiver maior que 0�
            if (zRotate > 0)
            {
                // Se o eixo do mouse estiver menor que a �ngula��o 0, subtrai 1 na rota��o
                if (moveY < 0)
                {
                    zRotate -= 1f;
                }
            }
        }
    }

    void LimitaRotacao()
    {
        // Se a �ngula��o estiver maior ou igual a 90, trava a rota��o em 90�
        if (zRotate >= 90)
        {
            zRotate = 90;
        }

        // Se a �ngula��o estiver menor ou igual a 0, trava a rota��o em 0�
        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    // Se eu colocar o dedo (ou apertar o bot�o direito do mouse) onde o objeto est�, libera a rota��o
    void OnMouseDown()
    {
        liberaRot = true;
        setaGO.SetActive(true);
    }

    // Se eu tirar o dedo (ou parar de apertar o bot�o direito do mouse), n�o libera a rota��o
    void OnMouseUp()
    {
        liberaRot = false;
        // Libera o chute da bola
        liberaTiro = true;

        setaGO.SetActive(false);

        AudioManager.instance.SonsFXToca(1);
    }
}
