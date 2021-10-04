using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody>();
    movDir = Vector3.zero;
    rotDir = Vector3.zero;
  }

  // Update is called once per frame
  void Update()
  {
    movDir = (Vector3.forward * Input.GetAxis("Vertical")).normalized;
    rotDir = Vector3.up * Input.GetAxis("Horizontal");
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
}
