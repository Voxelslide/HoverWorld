using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMeterScript : MonoBehaviour
{

	private Rigidbody boardRB;

	[SerializeField]
	private int meterValue;
	[SerializeField]
	private int meterMax = 100;

	//Values that are given by Inputs, stored, and then applied during FixedUpdate



	private void Awake()
	{
		boardRB = GetComponent<Rigidbody>();

		meterValue = 0;
	}

	private void FixedUpdate()
	{

		//boardBoost
		BoardBoost();
	}




	//Boost


	//boardBoost internal values
	private bool canBoardBoost = true; //For later if I want to disable boosting

	[SerializeField]
	private float boardBoostCooldown = 20;
	private float boardBoostCooldownCounter = 20;

	[SerializeField]
	private float boardBoostDuration = 10;
	private float boardBoostDurationCounter = 0;
	//boardBoostDurationCounter = the counter that keeps track of the time passed since last boost. When starting a boardBoost, it starts counting down.
	//If a boardBoostCheck sees that the countdown is <= 0 (on a button press), it sets the coutdown = cooldown, which is the condition boardBoost checks in fixedUpdate order to begin

	[SerializeField]
	private float boardBoostValue = 2000;






	public void BoardBoostCheck(InputAction.CallbackContext ctx)
	{
		Debug.Log("BoardBoostCheck");
		if (ctx.started && canBoardBoost && boardBoostDurationCounter <= 0 && boardBoostCooldownCounter >= boardBoostCooldown) //if board is not boosting and the cooldown is over
		{
			Debug.Log("BoardBoost START");
			boardBoostDurationCounter = boardBoostDuration;
			boardBoostCooldownCounter = 0;
		}
	}

	private void BoardBoost()
	{
		if (boardBoostDurationCounter > 0) //boardBoosts if the countdown hasn't run out yet
		{
			boardBoostDurationCounter -= 0.1f;
			boardRB.AddForce(boardBoostValue * gameObject.transform.forward, ForceMode.Impulse);

			//increase camera field of view to 90 when boosting
			//if(boardCamera.fieldOfView < 90.0f) boardCamera.fieldOfView += 2.5f;

			//Debug.Log("Boosting with: " + boardBoostDurationCounter + " left.");
		}
		if (boardBoostCooldownCounter < boardBoostCooldown) //if boardBoost duration is over, then start refreshing the cooldown (if it's not already == the total cooldown)
		{
			boardBoostCooldownCounter += 0.1f;
			//if(boardCamera.fieldOfView > 75.0f) boardCamera.fieldOfView -= 2.5f; //camera FOV only goes to this # and then stays there, unless you change the > / < and like everything else
		}
	}






}
