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
  public List<ItemController.ItemType> inventory;
  private Transform launchSpot;

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
    inventory = new List<ItemController.ItemType>();
    body = GetComponent<Rigidbody>();
    movDir = Vector3.zero;
    rotDir = Vector3.zero;
    areaType = AreaType.NONE;
    canDo = true;
    launchSpot = transform.Find("LaunchSpot");
  }

  // Update is called once per frame
  void Update()
  {
    movDir = (Vector3.forward * Input.GetAxis("Vertical")).normalized;
    rotDir = Vector3.up * Input.GetAxis("Horizontal");

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

  /// <summary>
  /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
  /// </summary>
  void FixedUpdate()
  {
    body.MovePosition(body.position + transform.TransformDirection(movDir) * movSpeed * Time.deltaTime);
    transform.Rotate(rotDir * rotSpeed * Time.deltaTime);
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
        if (inventory.Count == 0)
        {
          ItemController itemController = item.GetComponent<ItemController>();
          if (!itemController.released)
          {
            GameObject.Destroy(item);
            inventory.Add(itemController.type);
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
    GameObject item = ItemFactory.build(launchSpot, inventory[0], true);
    item.transform.parent = null;
    inventory.RemoveAt(0);
  }

  public ItemController.ItemType checkInventory()
  {
    if (inventory.Count > 0)
    {
      return inventory[0];
    }
    else
    {
      return ItemController.ItemType.NONE;
    }
  }
}
