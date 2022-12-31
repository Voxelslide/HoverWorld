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


	public void AddMeter(int addAmount)
	{
		Debug.Log("Meter: " + meterValue);
		meterValue += addAmount;
		if (meterValue > meterMax) meterValue = meterMax;
		Debug.Log("New Meter Amount: " + meterValue);
	}


	public void SubtractMeter(int decrementAmount) {
		Debug.Log("Meter: " +meterValue);
		if (meterValue >= decrementAmount) {
			meterValue -= decrementAmount;
			Debug.Log("Remaining Meter: " + meterValue);
		}
	}
	
}
