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
  private bool liftOff;

  void Awake()
  {
    gravityBody = GetComponent<GravityBody>();
  }

  void OnEnable()
  {
    body = GetComponent<Rigidbody>();
    canExit = false;
    gravityBody.enabled = false;
    liftOff = false;
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
      EventBus.exitRocket();
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
    body.AddForce(transform.up * thrust * thrustRate * Time.deltaTime);

    if (thrust > 0.5f && body.velocity.magnitude > 3f && !liftOff)
    {
      liftOff = true;
      EventBus.liftOff();
    }

    transform.Rotate(rotDir * rotSpeed * Time.deltaTime, Space.Self);
    body.rotation = transform.rotation;
  }
}
