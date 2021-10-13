using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
  private float timer;

  private const float DISPLAY_TIME = 3f;

  void OnEnable()
  {
    timer = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;
    if (timer >= DISPLAY_TIME)
    {
      gameObject.SetActive(false);
    }
  }
}
