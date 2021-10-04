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

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody>();
    movDir = Vector3.zero;
  }

  // Update is called once per frame
  void Update()
  {
    movDir = Vector3.forward * Input.GetAxis("Vertical");
  }

  /// <summary>
  /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
  /// </summary>
  void FixedUpdate()
  {
    body.MovePosition(body.position + movDir * movSpeed * Time.deltaTime);
  }
}
