using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject rocket;

  // Start is called before the first frame update
  void Start()
  {
    EventBus.OnRocketEnter += onRockenter;
    EventBus.OnRocketExit += onRocketExit;
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
  }

  void onRocketExit() { }
}
