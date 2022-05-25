using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPadScript : MonoBehaviour
{
	[SerializeField]
	private float boostValue;
	[SerializeField]
	private float boostDuration;

	private void OnTriggerEnter(Collider collision)
	{
		Debug.Log("Collision detected");
		//if a board has entered the boost pad collider
		if(collision.gameObject.tag.Equals("Board") == true)
		{
			collision.gameObject.GetComponent<BoardScript>().BoostPadStart(boostValue, boostDuration);
		}
	}
}
