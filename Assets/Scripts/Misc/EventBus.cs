using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus
{
  public static event Action OnRocketEnter;
  public static event Action OnRocketExit;

  public static void enterRocket()
  {
    OnRocketEnter?.Invoke();
  }

  public static void exitRocket()
  {
    OnRocketExit?.Invoke();
  }
}
