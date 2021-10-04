using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
  [SerializeField]
  private Transform spot;
  private Transform target;

  // Start is called before the first frame update
  void Start()
  {
    target = spot.parent;
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = spot.position;
    transform.LookAt(target);
  }
}
