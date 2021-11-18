using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
  [SerializeField] private float rotSpeed = 15f;

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.Self);
  }
}
