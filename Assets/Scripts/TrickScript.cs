using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrickScript : MonoBehaviour
{

	Vector2 direction;
	
	private ActionMeterScript boardActionMeter;
	private bool canTrick;

	private void Start()
	{
		boardActionMeter = GetComponent<ActionMeterScript>();
		canTrick = true;
	}


	public void DoTrick(InputAction.CallbackContext ctx)
	{
		direction = ctx.ReadValue<Vector2>();
		if (ctx.started)
		{
			Trick trick = new Trick(direction);
			Debug.Log("Direction: " + trick.direction);
			AddMeter(trick.meterGain);
		}
	}

	private void AddMeter(int meterGain)
	{
		boardActionMeter.AddMeter(meterGain);
	}

}
