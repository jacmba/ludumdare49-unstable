using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemFactory
{
  public static GameObject build(Transform point, ItemController.ItemType type, bool released = false)
  {
    GameObject itemPefab = Resources.Load<GameObject>("Prefabs/Item");
    GameObject item = GameObject.Instantiate(itemPefab, point.position, Quaternion.identity);
    item.name = "Item";
    item.transform.parent = point;
    ItemController controller = item.GetComponent<ItemController>();
    controller.type = type;
    controller.released = released;
    return item;
  }
}
