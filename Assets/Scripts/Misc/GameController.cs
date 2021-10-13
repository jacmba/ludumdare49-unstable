using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject rocket;
  private RocketController rocketController;
  private PlayerController playerController;
  private Transform exitPoint;
  private GameObject ui;
  private UIController uiController;

  // Start is called before the first frame update
  void Start()
  {
    EventBus.OnRocketEnter += onRockenter;
    EventBus.OnRocketExit += onRocketExit;
    EventBus.OnItemCollected += onItemCollected;
    EventBus.OnItemDropped += onItemDropped;
    EventBus.OnVulcanoEntered += onVulcanoEntered;
    EventBus.OnItemDemand += onItemDemanded;
    EventBus.OnDropProcessed += onDropProcessed;

    rocketController = rocket.GetComponent<RocketController>();
    playerController = player.GetComponent<PlayerController>();

    exitPoint = rocket.transform.Find("ExitPoint");
    ui = GameObject.Find("UI");
    uiController = ui.GetComponent<UIController>();
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventBus.OnRocketEnter -= onRockenter;
    EventBus.OnRocketExit -= onRocketExit;
    EventBus.OnItemCollected -= onItemCollected;
    EventBus.OnItemDropped -= onItemDropped;
    EventBus.OnVulcanoEntered -= onVulcanoEntered;
    EventBus.OnItemDemand -= onItemDemanded;
    EventBus.OnDropProcessed -= onDropProcessed;
  }

  void onRockenter()
  {
    player.SetActive(false);
    rocketController.enabled = true;
  }

  void onRocketExit()
  {
    rocketController.enabled = false;
    player.transform.position = exitPoint.position;
    player.SetActive(true);
  }

  void onItemCollected(ItemController.ItemType type)
  {
    string msg = "Collected " + type.ToString();
    Debug.Log(msg);
    uiController.notify(msg);
  }

  void onItemDropped(ItemController.ItemType type)
  {
    Debug.Log("Dropped " + type.ToString());
  }

  void onVulcanoEntered()
  {
    ItemController.ItemType item = playerController.checkInventory();
    if (item != ItemController.ItemType.NONE)
    {
      string msg = "Press action button to throw " + item.ToString();
      Debug.Log(msg);
      uiController.notify(msg);
    }
  }

  void onItemDemanded(ItemController.ItemType type)
  {
    if (type != ItemController.ItemType.NONE)
    {
      uiController.notify("Required " + type.ToString());
    }
  }

  void onDropProcessed(string result)
  {
    uiController.notify(result);
  }
}
