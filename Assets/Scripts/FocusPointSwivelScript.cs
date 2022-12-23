using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPointSwivelScript : MonoBehaviour
{

	[SerializeField]
	public GameObject boardReference;
	private Camera camera;


	private void Awake()
	{
		camera = transform.GetComponent<Camera>();
	}


	// Update is called once per frame
	void Update()
  {
		//boardReference;
  }


	 private void StayUpright(Transform boardTransform)
	{
		//transform.RotateAround()
	}

	//public void RotateAround(Vector3 point, Vector3 axis, float angle);
	//												 Board Transform, board.transform.forward & .right, angle = board rotation * -1 (DON'T FORGET about the 6.5 default rotation for the camera)(add it) 


}
