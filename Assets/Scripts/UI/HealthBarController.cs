using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
  private GaugeBarController bar;

  // Start is called before the first frame update
  void Start()
  {
    bar = GetComponentInChildren<GaugeBarController>();
    EventBus.OnHealthChanged += onHealthChanged;
  }

  void OnDestroy()
  {
    EventBus.OnHealthChanged -= onHealthChanged;
  }

  void onHealthChanged(int h)
  {
    bar.value = h;
  }
}
