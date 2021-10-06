using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject rocket;
  private RocketController rocketController;
  private Transform exitPoint;

  // Start is called before the first frame update
  void Start()
  {
    EventBus.OnRocketEnter += onRockenter;
    EventBus.OnRocketExit += onRocketExit;
    rocketController = rocket.GetComponent<RocketController>();
    exitPoint = rocket.transform.Find("ExitPoint");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventBus.OnRocketEnter -= onRockenter;
    EventBus.OnRocketExit -= onRocketExit;
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
}
