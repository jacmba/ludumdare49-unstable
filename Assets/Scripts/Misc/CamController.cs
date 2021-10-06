using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
  [SerializeField] private Transform playerSpot;
  [SerializeField] private Transform rocketStillSpot;
  [SerializeField] private Transform rocketChaseSpot;
  private Transform spot;
  private Transform target;

  // Start is called before the first frame update
  void Start()
  {
    changeSpot(playerSpot);

    EventBus.OnRocketEnter += OnRocketEnter;
    EventBus.OnRocketExit += OnRocketExit;
  }

  void OnDestroy()
  {
    EventBus.OnRocketEnter -= OnRocketEnter;
    EventBus.OnRocketExit -= OnRocketExit;
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = spot.position;
    transform.rotation = spot.rotation;
  }

  private void changeSpot(Transform s)
  {
    spot = s;
    target = spot.parent;
  }

  private void OnRocketEnter()
  {
    changeSpot(rocketStillSpot);
  }

  private void OnRocketExit()
  {
    changeSpot(playerSpot);
  }
}
