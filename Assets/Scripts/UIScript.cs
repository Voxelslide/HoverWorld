using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
	private TextMeshProUGUI speedTextObject;
	
	public GameObject boardReference;
	private Rigidbody boardRigidbody;


	void Start()
	{
		speedTextObject = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		boardRigidbody = boardReference.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
			speedTextObject.text = "Speed: " + Mathf.Round(boardRigidbody.velocity.magnitude * 100) / 100;
    }
}
