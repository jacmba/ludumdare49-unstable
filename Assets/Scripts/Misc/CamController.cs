using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
  [SerializeField] private Transform playerSpot;
  [SerializeField] private Transform rocketStillSpot;
  [SerializeField] private Transform rocketChaseSpot;
  [SerializeField] private Transform landSpot;
  [SerializeField] private Transform winSpot;
  [SerializeField] private float translateSpeed = 2f;
  private Transform spot;
  private Transform target;
  private bool transition;

  // Start is called before the first frame update
  void Start()
  {
    transition = false;
    changeSpot(playerSpot);

    EventBus.OnRocketEnter += OnRocketEnter;
    EventBus.OnRocketExit += OnRocketExit;
    EventBus.OnLitfOff += OnLiftOff;
    EventBus.OnRocketLanding += OnRocketLanding;
    EventBus.OnWin += OnWin;
  }

  void OnDestroy()
  {
    EventBus.OnRocketEnter -= OnRocketEnter;
    EventBus.OnRocketExit -= OnRocketExit;
    EventBus.OnLitfOff -= OnLiftOff;
    EventBus.OnRocketLanding -= OnRocketLanding;
    EventBus.OnWin -= OnWin;
  }

  // Update is called once per frame
  void Update()
  {
    float dist = Mathf.Abs((transform.position - spot.position).magnitude);
    if (dist < .5f)
    {
      transition = false;
    }
    Vector3 targetPos = dist > translateSpeed && transition ?
      Vector3.Lerp(transform.position, spot.position, translateSpeed * Time.deltaTime) :
      spot.position;
    transform.position = targetPos;

    transform.rotation = spot.rotation;
  }

  public void changeSpot(Transform s)
  {
    transition = true;
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

  private void OnLiftOff()
  {
    changeSpot(rocketChaseSpot);
  }

  private void OnRocketLanding()
  {
    changeSpot(landSpot);
  }

  private void OnWin()
  {
    changeSpot(winSpot);
  }
}
