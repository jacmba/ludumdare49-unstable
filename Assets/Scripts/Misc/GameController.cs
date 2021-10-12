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

  // Start is called before the first frame update
  void Start()
  {
    EventBus.OnRocketEnter += onRockenter;
    EventBus.OnRocketExit += onRocketExit;
    EventBus.OnItemCollected += onItemCollected;
    EventBus.OnItemDropped += onItemDropped;
    EventBus.OnVulcanoEntered += onVulcanoEntered;

    rocketController = rocket.GetComponent<RocketController>();
    playerController = player.GetComponent<PlayerController>();

    exitPoint = rocket.transform.Find("ExitPoint");
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
    Debug.Log("Collected " + type.ToString());
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
      Debug.Log("Press action button to throw " + item.ToString());
    }
  }
}
