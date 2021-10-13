using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDemandController : MonoBehaviour
{
  private Image panel;
  private Text text;
  private ItemController item;
  private ItemController.ItemType demand;
  private Color originalColor;

  // Start is called before the first frame update
  void Start()
  {
    panel = GetComponent<Image>();
    text = GetComponentInChildren<Text>();

    originalColor = panel.color;

    GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
    item = itemPrefab.GetComponent<ItemController>();

    demand = ItemController.ItemType.NONE;

    EventBus.OnItemDemand += onItemDemand;
  }

  void OnDestroy()
  {
    EventBus.OnItemDemand -= onItemDemand;
  }

  // Update is called once per frame
  void Update()
  {
    if (demand != ItemController.ItemType.NONE)
    {
      item.type = demand;
      panel.color = item.getColor();
      text.text = item.getSymbol();
    }
    else
    {
      panel.color = originalColor;
      text.text = "";
    }
  }

  void onItemDemand(ItemController.ItemType type)
  {
    demand = type;
  }
}
