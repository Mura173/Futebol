using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotacao : MonoBehaviour
{
    // Posição Seta
    [SerializeField] private Transform posStart;

    // Seta
    [SerializeField] private Image setaImg;
    public GameObject setaGO;

    //Ângulo
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
        // Posição da imagem da seta (Rect Transform é porque é da UI) vai ser igual a posição da bola
        setaImg.rectTransform.position = transform.position;
    }

    void PosicionaBola()
    {
        // "this" para representar o objeto bola, já que o script já está anexado ao gameobjetct da bola
        this.gameObject.transform.position = posStart.position;
    }

    void RotacaoSeta()
    {
        // eulerAngles: Trabalhar com ângulos
        setaImg.rectTransform.eulerAngles = new Vector3(0,0,zRotate);
    }

    void InputDeRotacao()
    {
        if (liberaRot == true)
        {            
            float moveY = Input.GetAxis("Mouse Y");

            // Se a rotação estiver menor que 90º
            if (zRotate < 90)
            {
                // Se o eixo do mouse estiver maior que a ângulação 0, adiciona 1 na rotação
                if (moveY > 0)
                {
                    zRotate += 1f;
                }
            }

            // Se a rotação estiver maior que 0º
            if (zRotate > 0)
            {
                // Se o eixo do mouse estiver menor que a ângulação 0, subtrai 1 na rotação
                if (moveY < 0)
                {
                    zRotate -= 1f;
                }
            }
        }
    }

    void LimitaRotacao()
    {
        // Se a ângulação estiver maior ou igual a 90, trava a rotação em 90º
        if (zRotate >= 90)
        {
            zRotate = 90;
        }

        // Se a ângulação estiver menor ou igual a 0, trava a rotação em 0º
        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    // Se eu colocar o dedo (ou apertar o botão direito do mouse) onde o objeto está, libera a rotação
    void OnMouseDown()
    {
        liberaRot = true;
        setaGO.SetActive(true);
    }

    // Se eu tirar o dedo (ou parar de apertar o botão direito do mouse), não libera a rotação
    void OnMouseUp()
    {
        liberaRot = false;
        // Libera o chute da bola
        liberaTiro = true;

        setaGO.SetActive(false);

        AudioManager.instance.SonsFXToca(1);
    }
}
