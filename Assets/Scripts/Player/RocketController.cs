using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
  private bool canExit;
  void OnEnable()
  {
    canExit = false;
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
  }
}
