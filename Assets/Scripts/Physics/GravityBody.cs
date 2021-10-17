using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
  [SerializeField] private GravityAttractor currentAttractor;
  [SerializeField] private bool rotate = true;
  private GameObject[] attractors;
  private Rigidbody body;

  // Start is called before the first frame update
  void Start()
  {
    Rigidbody body = GetComponent<Rigidbody>();
    body.useGravity = false;
    if (rotate)
    {
      body.constraints = RigidbodyConstraints.FreezeRotation;
    }
    attractors = GameObject.FindGameObjectsWithTag("Planet");
  }

  void Update()
  {
    Transform closest = attractors
      .Select(o => o.transform)
      .OrderBy(t => Vector3.Distance(transform.position, t.position))
      .FirstOrDefault();

    currentAttractor = closest.GetComponent<GravityAttractor>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (currentAttractor != null)
    {
      currentAttractor.attract(transform, rotate);
    }
  }
}
