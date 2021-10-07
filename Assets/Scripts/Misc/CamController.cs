using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
  [SerializeField] private Transform playerSpot;
  [SerializeField] private Transform rocketStillSpot;
  [SerializeField] private Transform rocketChaseSpot;
  [SerializeField] private Transform landSpot;
  [SerializeField] private float translateSpeed = 2f;
  [SerializeField] private float rotationSpeed = 5f;
  private Transform spot;
  private Transform target;

  // Start is called before the first frame update
  void Start()
  {
    changeSpot(playerSpot);

    EventBus.OnRocketEnter += OnRocketEnter;
    EventBus.OnRocketExit += OnRocketExit;
    EventBus.OnLitfOff += OnLiftOff;
    EventBus.OnRocketLanding += OnRocketLanding;
  }

  void OnDestroy()
  {
    EventBus.OnRocketEnter -= OnRocketEnter;
    EventBus.OnRocketExit -= OnRocketExit;
    EventBus.OnLitfOff -= OnLiftOff;
    EventBus.OnRocketLanding -= OnRocketLanding;
  }

  // Update is called once per frame
  void Update()
  {
    float dist = Mathf.Abs((transform.position - spot.position).magnitude);
    Vector3 targetPos = dist > translateSpeed ?
      Vector3.Lerp(transform.position, spot.position, translateSpeed * Time.deltaTime) :
      spot.position;
    transform.position = targetPos;

    Quaternion targetRot = Quaternion.Slerp(transform.rotation, spot.rotation, rotationSpeed * Time.deltaTime);
    transform.rotation = targetRot;
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

  private void OnLiftOff()
  {
    changeSpot(rocketChaseSpot);
  }

  private void OnRocketLanding()
  {
    changeSpot(landSpot);
  }
}
