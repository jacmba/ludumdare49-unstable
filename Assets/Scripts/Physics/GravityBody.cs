using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
  [SerializeField] private GravityAttractor currentAttractor;
  private GameObject[] attractors;
  private Rigidbody body;

  // Start is called before the first frame update
  void Start()
  {
    Rigidbody body = GetComponent<Rigidbody>();
    body.useGravity = false;
    body.constraints = RigidbodyConstraints.FreezeRotation;
    attractors = GameObject.FindGameObjectsWithTag("Planet");
  }

  void Update()
  {
    Transform closest = null;
    float minDist = float.MaxValue;

    foreach (GameObject planet in attractors)
    {
      float dist = Mathf.Abs((planet.transform.position - transform.position).magnitude);
      if (dist < minDist)
      {
        closest = planet.transform;
        minDist = dist;
      }
    }

    currentAttractor = closest.GetComponent<GravityAttractor>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (currentAttractor != null)
    {
      currentAttractor.attract(transform);
    }
  }
}
