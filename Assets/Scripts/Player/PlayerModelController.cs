using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
  private PlayerController player;

  // Start is called before the first frame update
  void Start()
  {
    player = transform.parent.GetComponent<PlayerController>();
  }

  public void recoverMovement()
  {
    player.recoverMovement();
  }
}
