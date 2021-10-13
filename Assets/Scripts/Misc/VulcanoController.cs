using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoController : MonoBehaviour
{
  private int stability = 100;
  private List<ItemController.ItemType> craft;
  private ItemController.ItemType demand;
  private float timer;

  private const float DEMAND_TIME = 5f * 60f;
  private const float STAB_TIMER = 30f;

  // Start is called before the first frame update
  void Start()
  {
    craft = new List<ItemController.ItemType>();
    demand = ItemController.ItemType.NONE;

    timer = DEMAND_TIME * .75f;

    EventBus.OnItemDropped += onItemDropped;
  }

  void OnDestroy()
  {
    EventBus.OnItemDropped -= onItemDropped;
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;
    if (demand == ItemController.ItemType.NONE)
    {
      if (timer >= DEMAND_TIME)
      {
        demand = (ItemController.ItemType)Random.Range(0, 3);
        timer = 0f;
        Debug.Log("Planet demanding " + demand.ToString());
      }
    }
    else
    {
      if (timer >= STAB_TIMER)
      {
        stability--;
        timer = 0f;
        Debug.Log("Stability decreased: " + stability);
      }
    }
  }

  void onItemDropped(ItemController.ItemType item)
  {
    if (demand == ItemController.ItemType.NONE)
    {
      craft.Add(item);
    }
    else
    {
      if (item == demand)
      {
        demand = ItemController.ItemType.NONE;
        timer = 0f;
        Debug.Log("Planet core stabilized with " + item.ToString() + ". Stability: " + stability);
      }
      else
      {
        stability -= 10;
        Debug.Log("Wrong item inserted in planet core. Stability: " + stability);
      }
    }
  }
}