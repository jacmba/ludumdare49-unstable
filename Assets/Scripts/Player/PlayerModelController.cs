using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
  private PlayerController player;
  private AudioSource sound;

  // Start is called before the first frame update
  void Start()
  {
    player = transform.parent.GetComponent<PlayerController>();
    sound = GetComponent<AudioSource>();
  }

  public void recoverMovement()
  {
    player.recoverMovement();
  }

  public void soundStep()
  {
    sound.Play();
  }

  public void throwItem()
  {
    player.launchItem();
  }
}
