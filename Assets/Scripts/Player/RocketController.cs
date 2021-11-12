using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
  private Rigidbody body;
  private bool canExit;
  private float thrust;
  private GravityBody gravityBody;
  [SerializeField] private float thrustRate = 100f;
  [SerializeField] private float rotSpeed = 90f;
  private Vector3 rotDir;
  private float vs;
  private float radioAlt;
  private Transform altSensor;
  private GameObject flame;
  private AudioSource sound;

  [SerializeField] private AudioClip rocketSound;
  [SerializeField] private AudioClip enterSound;
  [SerializeField] private AudioClip exitSound;
  [SerializeField] private AudioClip landingSound;

  public enum RocketStatus
  {
    GROUNDED,
    FLIGHT,
    LANDING
  }
  private RocketStatus status;

  void Awake()
  {
    gravityBody = GetComponent<GravityBody>();
    status = RocketStatus.GROUNDED;
    radioAlt = 0f;
    altSensor = transform.Find("AltSensor");
    flame = transform.Find("Gfx").Find("Flame").gameObject;
    sound = GetComponent<AudioSource>();
  }

  void OnEnable()
  {
    if (sound.isPlaying)
    {
      sound.Stop();
    }
    sound.clip = enterSound;
    sound.loop = false;
    sound.Play();
    body = GetComponent<Rigidbody>();
    canExit = false;
    gravityBody.enabled = false;
  }

  void OnDisable()
  {
    gravityBody.enabled = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonUp("Fire1"))
    {
      canExit = true;
    }

    if (Input.GetButtonDown("Fire1") && canExit)
    {
      canExit = false;
      switch (status)
      {
        case RocketStatus.GROUNDED:
          EventBus.exitRocket();
          if (sound.isPlaying)
          {
            sound.Stop();
          }
          sound.clip = exitSound;
          sound.loop = false;
          sound.Play();
          break;
        case RocketStatus.FLIGHT:
          if (radioAlt < 100f)
          {
            status = RocketStatus.LANDING;
            gravityBody.enabled = true;
            EventBus.landRocket();
          }
          break;
      }
    }

    thrust = Input.GetAxis("Fire2");
    if (thrust < 0)
    {
      thrust = 0;
    }
    else if (thrust > 0 && status == RocketStatus.GROUNDED)
    {
      flame.SetActive(true);
      if (sound.clip != rocketSound || !sound.isPlaying)
      {
        sound.clip = rocketSound;
        sound.loop = true;
        sound.Play();
      }
    }

    rotDir = (Input.GetAxis("Horizontal") * Vector3.back + Input.GetAxis("Vertical") * Vector3.right).normalized;
  }

  void FixedUpdate()
  {
    if (body.velocity.magnitude > .1f && status == RocketStatus.GROUNDED && radioAlt > .2f)
    {
      status = RocketStatus.FLIGHT;
      EventBus.liftOff();
    }

    if (status != RocketStatus.LANDING)
    {
      if (gravityBody.enabled)
      {
        gravityBody.enabled = false;
      }
      body.AddForce(transform.up * thrust * thrustRate * Time.deltaTime);

      if (status == RocketStatus.FLIGHT)
      {
        transform.Rotate(rotDir * rotSpeed * Time.deltaTime, Space.Self);
        body.rotation = transform.rotation;
      }
    }
    else
    {
      Vector3 vel = body.velocity;
      if (radioAlt < 10f && vs < -1f)
      {
        vel.x = 0f;
        vel.z = 0f;
        vel.y = -1f;
      }
      body.velocity = vel;
    }

    vs = body.velocity.y;

    RaycastHit hit;
    bool isHit = Physics.Raycast(altSensor.position, transform.TransformDirection(Vector3.down), out hit, 200f);
    if (isHit)
    {
      radioAlt = hit.distance;
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (!enabled)
    {
      return;
    }
    if (other.gameObject.tag == "Planet")
    {
      status = RocketStatus.GROUNDED;
      body.velocity = Vector3.zero;
      flame.SetActive(false);

      if (sound.isPlaying)
      {
        sound.Stop();
      }
      sound.clip = landingSound;
      sound.loop = false;
      sound.Play();
    }
  }
}
