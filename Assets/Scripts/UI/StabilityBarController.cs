using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilityBarController : MonoBehaviour
{
  private GaugeBarController bar;

  // Start is called before the first frame update
  void Start()
  {
    bar = GetComponentInChildren<GaugeBarController>();

    EventBus.OnStabilityChanged += onStabilityChanged;
  }

  void OnDestroy()
  {
    EventBus.OnStabilityChanged -= onStabilityChanged;
  }

  void onStabilityChanged(int stability)
  {
    bar.value = stability;
  }
}
