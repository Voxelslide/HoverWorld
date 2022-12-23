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
		meterValue = 0;
	}

	public int GetMeterValue()
	{
		return meterValue;
	}

	public void DecrementMeterValue(int decrementAmount) {
		if (meterValue > decrementAmount) {
			meterValue -= decrementAmount;
		}
	}
	






}
