using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBomba : MonoBehaviour {


	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		StartCoroutine (Vida ());
	}

	IEnumerator Vida()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}
}
