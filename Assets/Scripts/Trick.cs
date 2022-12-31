using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick : MonoBehaviour
{
	float deadzoneValue = 0.5f;
	public enum TrickDirection
	{
		DOWN_LEFT = 1,
		DOWN = 2,
		DOWN_RIGHT = 3,
		LEFT = 4,
		NEUTRAL = 5,
		RIGHT = 6,
		UP_LEFT = 7,
		UP = 8,
		UP_RIGHT = 9
	}

	public Vector2 directionVector;
	public TrickDirection direction;
	public int meterGain;


	private TrickDirection DetermineDirection(Vector2 inputDirection)
	{
		if (inputDirection == new Vector2(-1.0f, -1.0f))
		{
			return TrickDirection.DOWN_LEFT;
		}
		if (inputDirection == new Vector2(0f, -1.0f))
		{
			return TrickDirection.DOWN;
		}
		if (inputDirection == new Vector2(1.0f, -1.0f))
		{
			return TrickDirection.DOWN_RIGHT;
		}
		if (inputDirection == new Vector2(-1.0f, 0f))
		{
			return TrickDirection.LEFT;
		}
		if (inputDirection == new Vector2(1.0f, 0f))
		{
			return TrickDirection.RIGHT;
		}
		if (inputDirection == new Vector2(-1.0f, 1.0f))
		{
			return TrickDirection.UP_LEFT;
		}
		if (inputDirection == new Vector2(0f, 1.0f))
		{
			return TrickDirection.UP;
		}
		if (inputDirection == new Vector2(1.0f, 1.0f))
		{
			return TrickDirection.UP_RIGHT;
		}
		else return TrickDirection.NEUTRAL;
	}

	public Trick(Vector2 inputDirection)
	{
		//direction processing- (only want direction if stick is moved far enough in a direction)
		float xDirection = inputDirection.x;
		float yDirection = inputDirection.y;

		if (xDirection >= deadzoneValue) xDirection = 1f;
		else if (xDirection <= -deadzoneValue) xDirection = -1f;
		else xDirection = 0f;
		if (yDirection >= deadzoneValue) yDirection = 1f;
		else if (yDirection <= -deadzoneValue) yDirection = -1f;
		else yDirection = 0f;
		this.directionVector	= new Vector2(xDirection, yDirection);
		this.direction = DetermineDirection(directionVector);
		this.meterGain = 10;
	}
}
