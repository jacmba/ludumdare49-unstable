using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Planet))]
public class GravityAttractor : MonoBehaviour
{
  private const float GRAVITY = -9.8f;
  private Planet planet;

  // Start is called before the first frame update
  void Start()
  {
    planet = GetComponent<Planet>();
  }

  public void attract(Transform body)
  {
    Vector3 gravityUp = (body.position - transform.position).normalized;
    Vector3 bodyUp = body.up;
    Rigidbody rb = body.GetComponent<Rigidbody>();

    float attractionForce = GRAVITY * rb.mass;
    rb.AddForce(gravityUp * attractionForce);

    Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
    body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 150f * Time.deltaTime);
  }
}
