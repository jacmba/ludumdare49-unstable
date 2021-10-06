using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private float movSpeed = 15f;
  [SerializeField]
  private float rotSpeed = 20f;

  private Rigidbody body;
  private Vector3 movDir;
  private Vector3 rotDir;
  private bool canDo;

  public enum AreaType
  {
    NONE,
    ROCKET
  }

  private AreaType areaType;

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody>();
    movDir = Vector3.zero;
    rotDir = Vector3.zero;
    areaType = AreaType.NONE;
    canDo = true;
  }

  // Update is called once per frame
  void Update()
  {
    movDir = (Vector3.forward * Input.GetAxis("Vertical")).normalized;
    rotDir = Vector3.up * Input.GetAxis("Horizontal");

    if (Input.GetButtonUp("Fire1"))
    {
      canDo = true;
    }

    if (Input.GetButtonDown("Fire1") && canDo)
    {
      canDo = false;
      doStuff();
    }
  }

  /// <summary>
  /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
  /// </summary>
  void FixedUpdate()
  {
    body.MovePosition(body.position + transform.TransformDirection(movDir) * movSpeed * Time.deltaTime);

    Quaternion rotation = Quaternion.Euler(rotDir * rotSpeed * Time.deltaTime).normalized;
    body.MoveRotation(body.rotation * rotation);
  }

  void OnTriggerEnter(Collider other)
  {
    switch (other.name)
    {
      case "Cobete":
        areaType = AreaType.ROCKET;
        break;
      default:
        areaType = AreaType.NONE;
        break;
    }
  }

  void OnTriggerExit(Collider other)
  {
    areaType = AreaType.NONE;
  }

  private void doStuff()
  {
    switch (areaType)
    {
      case AreaType.ROCKET:
        EventBus.enterRocket();
        break;
      default:
        break;
    }
  }
}
