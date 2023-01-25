using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoostScript : MonoBehaviour
{
	
	private Rigidbody boardRB;
	private ActionMeterScript boardActionMeter;

	[SerializeField]
	private float boostDuration = 1.5f;

	[SerializeField]
	private float boardBoostValue = 10000;
	[SerializeField]
	private int requiredMeterToBoost = 25;

	private void Awake()
	{
		boardRB = GetComponent<Rigidbody>();
		boardActionMeter = GetComponent<ActionMeterScript>();
	}


	//Boost


	public void BoostCheck(InputAction.CallbackContext ctx)
	{
		Debug.Log("BoardBoostCheck");
		if (ctx.started && boardActionMeter.GetMeterValue() >= requiredMeterToBoost) //check for available meter here
		{
			boardActionMeter.SubtractMeter(requiredMeterToBoost);
			Debug.Log("Boost START");
			StartCoroutine(Boost());
		}
	}

	private System.Collections.IEnumerator Boost()
	{
		float t = 0f;
		
		while (t < boostDuration)
		{
			boardRB.AddForce(boardBoostValue * gameObject.transform.forward, ForceMode.Force);
			yield return null;
			t += Time.deltaTime;
		}
		
	}

	//increase camera field of view to 90 when boosting
	//if(boardCamera.fieldOfView < 90.0f) boardCamera.fieldOfView += 2.5f;




}
