using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataFXBolaMorte1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MataFX());
    }

    IEnumerator MataFX()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
