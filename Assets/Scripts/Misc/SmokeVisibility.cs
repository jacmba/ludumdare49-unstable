using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVisibility : MonoBehaviour
{
  private ParticleSystem particles;

  private const float MAX_DIST = 300f;

  // Start is called before the first frame update
  void Start()
  {
    particles = GetComponent<ParticleSystem>();
  }

  // Update is called once per frame
  void Update()
  {
    float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
    particles.enableEmission = dist < MAX_DIST;
  }
}
