using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus
{
  public static event Action OnRocketEnter;
  public static event Action OnRocketExit;
  public static event Action OnLitfOff;
  public static event Action OnRocketLanding;
  public static event Action OnVulcanoEntered;
  public static event Action<ItemController.ItemType> OnItemCollected;
  public static event Action<ItemController.ItemType> OnItemDropped;
  public static event Action<ItemController.ItemType> OnItemDemand;
  public static event Action<string> OnDropProcessed;
  public static event Action<List<ItemController.ItemType>> OnItemCrafted;
  public static event Action<int> OnStabilityChanged;

  public static event Action<int> OnHealthChanged;
  public static event Action<string> OnNotify;
  public static event Action OnWin;

  public static void enterRocket()
  {
    OnRocketEnter?.Invoke();
  }

  public static void exitRocket()
  {
    OnRocketExit?.Invoke();
  }

  public static void liftOff()
  {
    OnLitfOff?.Invoke();
  }

  public static void landRocket()
  {
    OnRocketLanding?.Invoke();
  }

  public static void collectItem(ItemController.ItemType type)
  {
    OnItemCollected?.Invoke(type);
  }

  public static void dropItem(ItemController.ItemType type)
  {
    OnItemDropped?.Invoke(type);
  }

  public static void enterVulcano()
  {
    OnVulcanoEntered?.Invoke();
  }

  public static void demandItem(ItemController.ItemType type)
  {
    OnItemDemand?.Invoke(type);
  }

  public static void dropProcessed(string result)
  {
    OnDropProcessed?.Invoke(result);
  }

  public static void craftItem(List<ItemController.ItemType> list)
  {
    OnItemCrafted?.Invoke(list);
  }

  public static void changeStability(int s)
  {
    OnStabilityChanged?.Invoke(s);
  }

  public static void changeHealth(int h)
  {
    OnHealthChanged?.Invoke(h);
  }

  public static void notify(string msg)
  {
    OnNotify?.Invoke(msg);
  }

  public static void win()
  {
    OnWin?.Invoke();
  }
}
