using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardScript : MonoBehaviour
{
	private Camera boardCamera;

	private Rigidbody boardRB;
	private float gravity = -1f;
	[SerializeField]
	private float rotationLimit = 80f;

	[SerializeField]
	private float speed = 250;
	[SerializeField]
	private float turnTorque = 750;
	[SerializeField]
	private float pitchTorque = 500;
	[SerializeField]
	private float rollTorque = 100;
	[SerializeField]
	private float floatMultiplier = 50;
	[SerializeField]
	private float centerForce = 8f;
	[SerializeField]
	private float tooLowCenterForce = 50f;

	private float pitchChange;
	private float rollChange;


	//boostPad internal values
	private float boostPadDuration;
	private float boostPadValue = 1f;


	//Values that are given by Inputs, stored, and then applied during FixedUpdate
	private float forwardInput;
	private float torqueDirection;

	//corner Object references
	[SerializeField]
	private GameObject FLCorner, FRCorner, BLCorner, BRCorner;
	private Transform[] corners = new Transform[4];
	RaycastHit[] hits = new RaycastHit[4];

	private void Awake()
	{
		//sets reference to rigidbody
		boardRB = GetComponent<Rigidbody>();

		//fills corner array with corners
		corners[0] = FLCorner.transform;
		corners[1] = FRCorner.transform;
		corners[2] = BLCorner.transform;
		corners[3] = BRCorner.transform;

		//this just makes the camera easier/shorter to reference
		boardCamera = gameObject.GetComponentInChildren<Camera>();
		Debug.Log(boardCamera);
	}


	private void FixedUpdate()
	{
		ApplyGravity();
		

		//CornerHover
		for (int i = 0; i < 4; i++)
		{
			ApplyCornerForce(corners[i], hits[i]);
		}

		//Forward/Backward Movement
		boardRB.AddForce(forwardInput * Vector3.Scale(gameObject.transform.forward, new Vector3(1, 0, 1)) * speed, ForceMode.Acceleration);

		//Turning/Torque
		boardRB.AddTorque(torqueDirection * turnTorque * gameObject.transform.up, ForceMode.Force);


		//RotatePitchRoll
		boardRB.AddTorque(pitchChange * pitchTorque * gameObject.transform.right, ForceMode.Force); //pitch
		boardRB.AddTorque(rollChange * rollTorque * gameObject.transform.forward, ForceMode.Force); //roll
		
		//boostPad movement
		BoostPadBoost();



		//Restrict x and z rotation to be within 80 degrees.
		DontFlip();


		//Debug.Log("X: " + gameObject.transform.eulerAngles.x);
		//Debug.Log("Z: " + gameObject.transform.eulerAngles.z);
	}

	private void Update()
	{
		if (boostPadDuration > 0)
		{
			Debug.Log("Boost Pad Duration: " + boostPadDuration);
		}
	}



	//Board Movement
	public void ForwardMovement(InputAction.CallbackContext ctx)
	{
		forwardInput = ctx.ReadValue<float>();
		//Debug.Log("Forward Input: " + ctx);
	}

	public void Turning(InputAction.CallbackContext ctx)
	{
		torqueDirection = ctx.ReadValue<float>();
		//Debug.Log("Move: " + ctx);
	}

	public void RotatePitchRoll(InputAction.CallbackContext ctx)
	{
		Vector2 input = ctx.ReadValue<Vector2>();
		rollChange = -input.x;
		pitchChange = input.y;
	}

	public void LevelOut(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			StartCoroutine(LevelOut(this.transform, Quaternion.Euler(0f, transform.eulerAngles.y, 0f), 0.5f));
		}
	}

	private System.Collections.IEnumerator LevelOut(Transform target, Quaternion rot, float dur)
	{
		float t = 0f;
		Quaternion start = target.rotation;
		while (t < dur)
		{
			target.rotation = Quaternion.Slerp(start, rot, t / dur);
			yield return null;
			t += Time.deltaTime;
		}
		target.rotation = rot;
	}


	/*
	 If I wanted to make it to where you can't board boost and boost pad boost,
	include a check in boost pad boost that checks to see if the boardBoostCooldownCounter is > like 10% of cooldown or something (1/10 of cooldown has passed, whatever that comes out to)
	so that it waits an appropriate time between boardBoost and boostPadBoost,
	and if true, don't do boost pad boost
	also, have a check in boardBoost that checks if boostPadDuration is >0, and if true don't boardBoost
	 */




	//boost pad
	public void BoostPadStart(float padValue, float padDuration)
	{
		boostPadValue = padValue;
		boostPadDuration = padDuration;
	}

	private void BoostPadBoost()
	{
		if (boostPadDuration < 0)
		{
			boostPadDuration = 0;
			boostPadValue = 1;
		}
		if (boostPadDuration > 0)
		{
			boostPadDuration -= 0.1f;
			boardRB.AddForce(Vector3.Scale(new Vector3(boostPadValue, 0, boostPadValue), gameObject.transform.forward) * forwardInput, ForceMode.Impulse);
		}
	}

	//Board floating/ gravity/ not flipping
	private void ApplyGravity()
	{
		boardRB.AddForce(new Vector3(0, gravity * boardRB.mass, 0), ForceMode.Acceleration);
	}

	private void ApplyCornerForce(Transform corner, RaycastHit hit)
	{
		if (Physics.Raycast(corner.position, -corner.up, out hit))
		{
			float force = 0;
			if ((corner.position.y - hit.point.y) <= 0.5 && (Mathf.Abs(hit.normal.y) > Mathf.Abs(hit.normal.x) || Mathf.Abs(hit.normal.y) > Mathf.Abs(hit.normal.z)))
				 //The wack stuff after the && is to disable the tooLowCenterForce for any surface that's greater than 45 degrees horizontal, therefore making it easier to ride along walls
			{
				ApplyTooLowCenterForce();
				force = 20;
			}
			ApplyCenterForce();
			force = Mathf.Abs(1 / (hit.point.y - corner.position.y)) * floatMultiplier;
			if (force > 100f) force = 100f;
			boardRB.AddForceAtPosition(transform.up * force, corner.position, ForceMode.Acceleration);

			if (force > 80f)
			{
				Debug.Log("Force: " + force);
			}
		}
	}

	private void ApplyTooLowCenterForce()
	{
		boardRB.AddForceAtPosition(transform.up * tooLowCenterForce * floatMultiplier, gameObject.transform.localPosition, ForceMode.Force);
	}

	private void ApplyCenterForce()
	{
		boardRB.AddForceAtPosition(transform.up * centerForce * floatMultiplier, gameObject.transform.localPosition, ForceMode.Force);
	}

	private void DontFlip()
	{
		//Get current rotation
		float max = rotationLimit;
		float min = -rotationLimit;
		Vector3 playerEulerAngles = transform.rotation.eulerAngles;

		//Gets Pitch and Roll, turns the values into an equivalent, usable number, and clamps them
		playerEulerAngles.x = (playerEulerAngles.x > 180) ? playerEulerAngles.x - 360: playerEulerAngles.x;
		playerEulerAngles.x = Mathf.Clamp(playerEulerAngles.x, min, max);

		playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
		playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, min, max);

		//Apply changes
		transform.rotation = Quaternion.Euler(playerEulerAngles);
	}
}