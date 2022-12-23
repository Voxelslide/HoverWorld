using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpDropScript : MonoBehaviour
{
	
	private Rigidbody boardRB;
	private ActionMeterScript boardActionMeter;

	[SerializeField]
	private float jumpValue = 10000;
	[SerializeField]
	private int requiredMeterToJump = 25;

	[SerializeField]
	private float dropValue = 10000;
	[SerializeField]
	private int requiredMeterToDrop = 25;



	//Values that are given by Inputs, stored, and then applied during FixedUpdate
	bool jumpBool = false;
	bool dropBool = false;


	private void Awake()
	{
		boardRB = GetComponent<Rigidbody>();
		boardActionMeter = GetComponent<ActionMeterScript>();
	}

	private void FixedUpdate()
	{
		if (jumpBool)
		{
			JumpForce();
		}

		if (dropBool)
		{
			DropForce();
		}
	}


	//Jump and Drop
	public void Jump(InputAction.CallbackContext ctx)
	{
		if (ctx.performed && boardActionMeter.GetMeterValue() >=  requiredMeterToJump)
		{
			jumpBool = true;
			boardActionMeter.DecrementMeterValue(requiredMeterToJump);
			//Debug.Log("jumpBool: " + jumpBool);
		}
		else
		{
			jumpBool = false;
			//Debug.Log("jumpBool: " + jumpBool);
		}
		//Debug.Log(ctx);
	}

	public void Drop(InputAction.CallbackContext ctx)
	{
		if (ctx.performed && boardActionMeter.GetMeterValue() >= requiredMeterToDrop)
		{
			dropBool = true;
			boardActionMeter.DecrementMeterValue(requiredMeterToDrop);
			//Debug.Log("dropBool: " + dropBool);
		}
		else dropBool = false;
		//Debug.Log(ctx);
	}

	private void JumpForce()
	{
		boardRB.AddForce(new Vector3(0, jumpValue, 0), ForceMode.Impulse);
		Debug.Log("Jump");
		jumpBool = false;
	}

	private void DropForce()
	{
		boardRB.AddForce(new Vector3(0, -dropValue, 0), ForceMode.Impulse);
		Debug.Log("Drop");
		dropBool = false;
	}


}