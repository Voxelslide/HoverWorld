using UnityEngine;

public class CameraLookAtScript : MonoBehaviour
{

  // camera will follow this object
  public Transform Target;

  private void FixedUpdate()
  {
    // update rotation
    transform.LookAt(Target);
  }
}