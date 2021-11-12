using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private float movSpeed = 15f;
  [SerializeField]
  private float rotSpeed = 20f;

  private Rigidbody body;
  private Vector3 movDir;
  private Vector3 rotDir;
  private bool canDo;
  private bool canMove;
  private bool canDamage;
  private float damageTimer;
  private float damageTimeLimit;
  public Queue<ItemController.ItemType> inventory;
  private Transform launchSpot;
  private float lastCollect;
  private Animator animator;
  private int health;

  private const int INVENTORY_SIZE = 3;

  public enum AreaType
  {
    NONE,
    ROCKET,
    VULCANO
  }

  private AreaType areaType;

  // Start is called before the first frame update
  void Start()
  {
    inventory = new Queue<ItemController.ItemType>();
    body = GetComponent<Rigidbody>();
    animator = GetComponentInChildren<Animator>();
    movDir = Vector3.zero;
    rotDir = Vector3.zero;
    areaType = AreaType.NONE;
    canDo = true;
    canMove = true;
    canDamage = true;
    damageTimeLimit = 3f;
    lastCollect = float.MinValue;
    launchSpot = transform.Find("LaunchSpot");
    health = 100;
  }

  // Update is called once per frame
  void Update()
  {
    if (canMove)
    {
      movDir = (Vector3.forward * Input.GetAxis("Vertical")).normalized;
      rotDir = Vector3.up * Input.GetAxis("Horizontal");

      bool running = Math.Abs(Input.GetAxis("Vertical")) > .1f;

      animator.SetBool("Running", running);

      if (Input.GetButtonUp("Fire1"))
      {
        canDo = true;
      }

      if (Input.GetButtonDown("Fire1") && canDo)
      {
        canDo = false;
        doStuff();
      }
    }

    if (!canDamage)
    {
      damageTimer += Time.deltaTime;
      if (damageTimer >= damageTimeLimit)
      {
        canDamage = true;
      }
    }
  }

  /// <summary>
  /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
  /// </summary>
  void FixedUpdate()
  {
    if (canMove)
    {
      body.MovePosition(body.position + transform.TransformDirection(movDir) * movSpeed * Time.deltaTime);
      transform.Rotate(rotDir * rotSpeed * Time.deltaTime);
    }
  }

  void OnCollisionStay(Collision other)
  {
    Damager damager = other.gameObject.GetComponent<Damager>();
    if (damager != null && canDamage)
    {
      damage(damager.damage);
    }
  }

  void OnTriggerEnter(Collider other)
  {
    switch (other.name)
    {
      case "Cobete":
        areaType = AreaType.ROCKET;
        break;
      case "Item":
        GameObject item = other.gameObject;
        if (inventory.Count < INVENTORY_SIZE && Time.time - lastCollect > 1)
        {
          lastCollect = Time.time;
          ItemController itemController = item.GetComponent<ItemController>();
          if (!itemController.released)
          {
            GameObject.Destroy(item);
            inventory.Enqueue(itemController.type);
            EventBus.collectItem(itemController.type);
          }
        }
        break;
      case "VulcanoArea":
        areaType = AreaType.VULCANO;
        EventBus.enterVulcano();
        break;
      default:
        areaType = AreaType.NONE;
        break;
    }
  }

  void OnTriggerExit(Collider other)
  {
    areaType = AreaType.NONE;
  }

  private void doStuff()
  {
    switch (areaType)
    {
      case AreaType.ROCKET:
        EventBus.enterRocket();
        break;
      case AreaType.VULCANO:
        if (inventory.Count > 0)
        {
          launchItem();
        }
        break;
      default:
        break;
    }
  }

  public void launchItem()
  {
    ItemController.ItemType item = inventory.Dequeue();
    GameObject itemObj = ItemFactory.build(launchSpot, item, true);
    itemObj.transform.parent = null;
  }

  public ItemController.ItemType checkInventory()
  {
    if (inventory.Count > 0)
    {
      return inventory.Peek();
    }
    else
    {
      return ItemController.ItemType.NONE;
    }
  }

  void damage(int d)
  {
    canMove = false;
    animator.SetBool("Running", false);
    health -= d;
    if (health <= 0)
    {
      health = 0;
    }
    damageTimer = 0f;
    canDamage = false;

    EventBus.changeHealth(health);

    if (health == 0)
    {
      animator.SetTrigger("Die");
      EventBus.diePlayer();
      enabled = false;
    }
    else
    {
      animator.SetTrigger("Damage");
    }
  }

  public void recoverMovement()
  {
    canMove = true;
  }
}
