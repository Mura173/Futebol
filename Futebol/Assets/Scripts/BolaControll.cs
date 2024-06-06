using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControll : MonoBehaviour
{
    // Rotacao
    // Seta
    public GameObject setaGO;

    //Ângulo
    public float zRotate;

    public bool liberaRot = false;
    public bool liberaTiro = false;

    // Forca
    [SerializeField] private Rigidbody2D bola;
    [SerializeField] private float force = 0;

    public GameObject seta2Img;

    // Parede
    private Transform paredeLE, paredeLD;

    void Awake()
    {     
        setaGO = GameObject.Find("Seta");
        seta2Img = setaGO.transform.GetChild (0).gameObject;

        // Retirar visualização
        setaGO.GetComponent<Image>().enabled = false;      
        seta2Img.GetComponent<Image>().enabled = false;

        paredeLD = GameObject.Find("ParedeLD").GetComponent<Transform>();
        paredeLE = GameObject.Find("ParedeLE").GetComponent<Transform>();
    }

    void Start()
    {
        bola = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();
        PosicionaSeta();

        // Força
        AplicaForca();
        ControlaForca();

        // Paredes
        Paredes();
    }

    void PosicionaSeta()
    {
        // Posição da imagem da seta (Rect Transform é porque é da UI) vai ser igual a posição da bola
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void RotacaoSeta()
    {
        // eulerAngles: Trabalhar com ângulos
        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
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

    // Se eu colocar o dedo (ou apertar o botão esquerdo do mouse) onde o objeto está, libera a rotação
    void OnMouseDown()
    {
        if (GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            setaGO.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;
        }       
    }

    // Se eu tirar o dedo (ou parar de apertar o botão esquerdo do mouse), não libera a rotação
    void OnMouseUp()
    {
        liberaRot = false;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;

        if (GameManager.instance.tiro == 0 && force > 0)
        {
            // Libera o chute da bola
            liberaTiro = true;

            seta2Img.GetComponent<Image>().fillAmount = 0;

            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
        }        
    }

    // Força
    void AplicaForca()
    {
        // Cosseno(x) e Seno(y), aplicado força em 2 eixos diferentes
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro == true)
        {
            // AddForce: Adiciona força
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if (liberaRot == true)
        {
            float moveX = Input.GetAxis("Mouse X");

            // Movimentando o mouse ou dedo para esquerda
            if (moveX < 0)
            {
                // Incrementando valor em fillAmount
                seta2Img.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;

                // Valor incrementado em fillAmount multiplicado por mil
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }

            if (moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }
        }
    }

    void BolaDinamica()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void Paredes()
    {
        if (this.gameObject.transform.position.x > paredeLD.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (this.gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("morte"))
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (outro.gameObject.CompareTag("win"))
        {
            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fase + 1;
            temp++;
            PlayerPrefs.SetInt("Level" + temp, 1);
        }
    }
}
