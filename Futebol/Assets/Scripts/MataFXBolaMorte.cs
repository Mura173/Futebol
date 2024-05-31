using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataFXBolaMorte : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine (MataFX());
	}
	
	IEnumerator MataFX()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}
}
