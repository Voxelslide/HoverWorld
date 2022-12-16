using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpDropScript : MonoBehaviour
{
	
	private Rigidbody boardRB;


	[SerializeField]
	private float jumpValue = 10000;
	[SerializeField]
	private float dropValue = 10000;

	//Values that are given by Inputs, stored, and then applied during FixedUpdate
	bool jumpBool = false;
	bool dropBool = false;


	private void Awake()
	{
		boardRB = GetComponent<Rigidbody>();
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
		if (ctx.performed)
		{
			jumpBool = true;
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
		if (ctx.performed)
		{
			dropBool = true;
		}
		else dropBool = false;
		Debug.Log("dropBool: " + dropBool);
		Debug.Log(ctx);
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
