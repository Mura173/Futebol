using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspetosManager : MonoBehaviour {

	private SliderJoint2D espeto;
	private JointMotor2D aux;

	void Start () {

		espeto = GetComponent<SliderJoint2D> ();
		aux = espeto.motor;

	}
	

	void Update () {

		if(espeto.limitState == JointLimitState2D.UpperLimit)
		{
			aux.motorSpeed = Random.Range (-1, -5);
			espeto.motor = aux;
		}

		if(espeto.limitState == JointLimitState2D.LowerLimit)
		{
			aux.motorSpeed = Random.Range (1, 5);
			espeto.motor = aux;
		}
	}
}
