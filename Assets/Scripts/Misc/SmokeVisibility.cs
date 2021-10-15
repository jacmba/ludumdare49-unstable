using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVisibility : MonoBehaviour
{
  private ParticleSystem particles;
  private bool finished;

  private const float MAX_DIST = 300f;

  // Start is called before the first frame update
  void Start()
  {
    particles = GetComponent<ParticleSystem>();
    finished = false;

    EventBus.OnWin += finish;
  }

  void OnDestroy()
  {
    EventBus.OnWin -= finish;
  }

  // Update is called once per frame
  void Update()
  {
    float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
    var emission = particles.emission;
    emission.enabled = dist < MAX_DIST && !finished;
  }

  void finish()
  {
    finished = true;
  }
}
