using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpDropScript : MonoBehaviour
{
	
	private Rigidbody boardRB;
	private ActionMeterScript boardActionMeter;

	[SerializeField]
	private float jumpValue = 12000;
	[SerializeField]
	private int requiredMeterToJump = 25;

	[SerializeField]
	private float dropValue = 50000;
	[SerializeField]
	private int requiredMeterToDrop = 25;


	[SerializeField]
	private float pitchTorque = 3500;


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
		}
		else
		{
			jumpBool = false;
		}
	}

	public void Drop(InputAction.CallbackContext ctx)
	{
		if (ctx.performed && boardActionMeter.GetMeterValue() >= requiredMeterToDrop)
		{
			dropBool = true;
		}
		else dropBool = false;
	}

	private void JumpForce()
	{
		boardRB.AddForce(new Vector3(0, jumpValue, 0), ForceMode.Impulse);
		boardActionMeter.SubtractMeter(requiredMeterToJump);
		//boardRB.AddTorque(-pitchTorque * gameObject.transform.right, ForceMode.Force); //pitch
		//Debug.Log("Jump");
		jumpBool = false;
	}

	private void DropForce()
	{
		boardRB.AddForce(new Vector3(0, -dropValue, 0), ForceMode.Impulse);
		boardActionMeter.SubtractMeter(requiredMeterToDrop);
		//boardRB.AddTorque(pitchTorque * gameObject.transform.right, ForceMode.Force); //pitch
		//Debug.Log("Drop");
		dropBool = false;
	}


}