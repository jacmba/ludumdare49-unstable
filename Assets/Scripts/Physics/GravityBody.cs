using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
  public GravityAttractor attractor;
  private Rigidbody body;

  // Start is called before the first frame update
  void Start()
  {
    Rigidbody body = GetComponent<Rigidbody>();
    body.useGravity = false;
    body.constraints = RigidbodyConstraints.FreezeRotation;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    attractor.attract(transform);
  }
}
