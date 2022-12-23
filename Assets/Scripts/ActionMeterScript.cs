using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMeterScript : MonoBehaviour
{
	[SerializeField]
	private int meterValue;
	[SerializeField]
	private int meterMax = 100;

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
