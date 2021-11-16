using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroclastController : MonoBehaviour
{
  [SerializeField] private AudioClip launchSound;
  [SerializeField] private AudioClip fallSound;
  private AudioSource sound;
  private float timer;
  private bool touched;
  private Vector3 rotSpeed;
  private Rigidbody body;

  private const float TIME_LIMIT = 10f;

  // Start is called before the first frame update
  void Start()
  {
    timer = 0;
    touched = false;
    rotSpeed = new Vector3(rand(), rand(), rand());

    transform.localScale = transform.localScale * Random.Range(.5f, 1f);

    body = GetComponent<Rigidbody>();
    body.angularVelocity = rotSpeed;

    sound = GetComponent<AudioSource>();
    sound.clip = launchSound;
    sound.Play();
  }

  // Update is called once per frame
  void Update()
  {
    if (touched)
    {
      timer += Time.deltaTime;
      if (timer >= TIME_LIMIT)
      {
        GameObject.Destroy(gameObject);
      }
    }
  }

  void OnCollisionEnter(Collision other)
  {
    touched = true;
    sound.clip = fallSound;
    sound.Play();
  }

  private float rand()
  {
    return Random.Range(1f, 5f);
  }
}
