using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
  private Rigidbody body;
  private bool canExit;
  private float thrust;
  private GravityBody gravityBody;
  [SerializeField] private float thrustRate = 100f;
  [SerializeField] private float rotSpeed = 90f;
  private Vector3 rotDir;
  private float vs;

  public enum RocketStatus
  {
    GROUNDED,
    FLIGHT,
    LANDING
  }
  private RocketStatus status;

  void Awake()
  {
    gravityBody = GetComponent<GravityBody>();
    status = RocketStatus.GROUNDED;
  }

  void OnEnable()
  {
    body = GetComponent<Rigidbody>();
    canExit = false;
    gravityBody.enabled = false;
  }

  void OnDisable()
  {
    gravityBody.enabled = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonUp("Fire1"))
    {
      canExit = true;
    }

    if (Input.GetButtonDown("Fire1") && canExit)
    {
      canExit = false;
      switch (status)
      {
        case RocketStatus.GROUNDED:
          EventBus.exitRocket();
          break;
        case RocketStatus.FLIGHT:
          status = RocketStatus.LANDING;
          gravityBody.enabled = true;
          EventBus.landRocket();
          break;
      }
    }

    thrust = Input.GetAxis("Fire2");
    if (thrust < 0)
    {
      thrust = 0;
    }

    rotDir = (Input.GetAxis("Horizontal") * Vector3.back + Input.GetAxis("Vertical") * Vector3.right).normalized;
  }

  void FixedUpdate()
  {
    if (thrust > 0.5f && body.velocity.magnitude > 2f && status == RocketStatus.GROUNDED)
    {
      status = RocketStatus.FLIGHT;
      EventBus.liftOff();
    }

    if (status != RocketStatus.LANDING)
    {
      if (gravityBody.enabled)
      {
        gravityBody.enabled = false;
      }
      body.AddForce(transform.up * thrust * thrustRate * Time.deltaTime);

      if (status == RocketStatus.FLIGHT)
      {
        transform.Rotate(rotDir * rotSpeed * Time.deltaTime, Space.Self);
        body.rotation = transform.rotation;
      }
    }

    vs = body.velocity.y;
    Debug.Log(vs);
  }

  void OnCollisionEnter(Collision other)
  {
    if (!enabled)
    {
      return;
    }
    if (other.gameObject.tag == "Planet")
    {
      status = RocketStatus.GROUNDED;
    }
  }
}
