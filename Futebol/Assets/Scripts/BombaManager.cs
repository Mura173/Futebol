using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaManager : MonoBehaviour {

	[SerializeField]
	private GameObject bombaFX;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D outro)
	{
		if(outro.gameObject.CompareTag("bola"))
		{
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Instantiate (bombaFX, new Vector2 (this.transform.position.x,this.transform.position.y), Quaternion.identity);
			StartCoroutine (vidaTempoBomb());
		}
	}

	IEnumerator vidaTempoBomb()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}
}
