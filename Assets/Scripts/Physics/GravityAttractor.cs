using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Planet))]
public class GravityAttractor : MonoBehaviour
{
  private const float GRAVITY = -9.8f;

  private const float TERMINAL_VEL = -20f;
  private const float G = (float)(6.67428f * 10e-11);
  private Planet planet;

  // Start is called before the first frame update
  void Start()
  {
    planet = GetComponent<Planet>();
  }

  public void attract(Transform body, bool rotate = true)
  {
    Vector3 gravityUp = (body.position - transform.position).normalized;
    Vector3 bodyUp = body.up;
    Rigidbody rb = body.GetComponent<Rigidbody>();

    if (rb.velocity.y > TERMINAL_VEL)
    {
      float attractionForce = GRAVITY * rb.mass;
      if (planet != null)
      {
        attractionForce *= planet.planetMass;
      }
      attractionForce *= G;
      float delta2 = (transform.position - body.position).sqrMagnitude;
      attractionForce /= delta2;
      rb.AddForce(gravityUp * attractionForce);
    }

    if (rotate)
    {
      Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
      body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 150f * Time.deltaTime);
    }
  }
}
