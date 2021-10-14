using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour
{
  private Image[] panels;
  private Text[] texts;
  private Color originalColor;
  private PlayerController player;
  private ItemController itemController;

  // Start is called before the first frame update
  void Start()
  {
    panels = GetComponentsInChildren<Image>();
    texts = GetComponentsInChildren<Text>();

    originalColor = panels[0].color;

    GameObject playerObj = GameObject.Find("Player");
    player = playerObj.GetComponent<PlayerController>();

    GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
    itemController = itemPrefab.GetComponent<ItemController>();
  }

  // Update is called once per frame
  void Update()
  {
    ItemController.ItemType[] inventory = player.inventory.ToArray();
    for (int i = 0; i < 3; i++)
    {
      if (inventory.Length > i)
      {
        ItemController.ItemType item = inventory[i];
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
