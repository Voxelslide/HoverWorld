using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
	private TextMeshProUGUI speedTextObject;
	private TextMeshProUGUI ActionMeterTextObject;

	public GameObject boardReference;
	private Rigidbody boardRigidbody;
	private ActionMeterScript boardActionMeter;

	void Start()
	{
		speedTextObject = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		ActionMeterTextObject = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		boardRigidbody = boardReference.GetComponent<Rigidbody>();
		boardActionMeter= boardReference.GetComponent<ActionMeterScript>();
	}

	void Update()
    {
			speedTextObject.text = "Speed: " + Mathf.Round(boardRigidbody.velocity.magnitude * 100) / 100;
			ActionMeterTextObject.text = "Action Meter: " + boardActionMeter.GetMeterValue();
    }
}
