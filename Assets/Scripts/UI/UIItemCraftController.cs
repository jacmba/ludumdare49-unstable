using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemCraftController : MonoBehaviour
{
  private Image[] panels;
  private Text[] texts;
  private Color originalColor;
  private ItemController itemController;

  // Start is called before the first frame update
  void Start()
  {
    panels = GetComponentsInChildren<Image>();
    texts = GetComponentsInChildren<Text>();
    originalColor = panels[0].color;

    GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
    itemController = itemPrefab.GetComponent<ItemController>();

    EventBus.OnItemCrafted += onItemCrafted;
  }

  void OnDestroy()
  {
    EventBus.OnItemCrafted -= onItemCrafted;
  }

  void onItemCrafted(List<ItemController.ItemType> list)
  {
    for (int i = 0; i < 3; i++)
    {
      if (i < list.Count)
      {
        ItemController.ItemType item = list[i];
        itemController.type = item;
        panels[i].color = itemController.getColor();
        texts[i].text = itemController.getSymbol();
      }
      else
      {
        panels[i].color = originalColor;
        texts[i].text = "";
      }
    }
  }
}
