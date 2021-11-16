using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject rocket;
  [SerializeField] private AudioClip itemPickSound;
  [SerializeField] private AudioClip itemDropSound;
  [SerializeField] private AudioClip itemCraftSound;
  private AudioSource sound;
  private RocketController rocketController;
  private PlayerController playerController;
  private Transform exitPoint;
  private Transform winSpot;
  private GameObject ui;
  private UIController uiController;
  private CamController cam;
  private bool won;
  private const float WIN_ROT_SPEED = 10f;

  [System.Serializable]
  public class ItemSoundMap
  {
    public AudioClip sound;
    public ItemController.ItemType type;
  }

  [SerializeField] private ItemSoundMap[] itemSounds;

  // Start is called before the first frame update
  void Start()
  {
    won = false;

    EventBus.OnRocketEnter += onRockenter;
    EventBus.OnRocketExit += onRocketExit;
    EventBus.OnItemCollected += onItemCollected;
    EventBus.OnItemDropped += onItemDropped;
    EventBus.OnVulcanoEntered += onVulcanoEntered;
    EventBus.OnItemDemand += onItemDemanded;
    EventBus.OnDropProcessed += onDropProcessed;
    EventBus.OnItemCrafted += onItemCrafted;
    EventBus.OnWin += onWin;

    rocketController = rocket.GetComponent<RocketController>();
    playerController = player.GetComponent<PlayerController>();

    exitPoint = rocket.transform.Find("ExitPoint");
    winSpot = transform.Find("WinCamSpot");
    ui = GameObject.Find("UI");
    uiController = ui.GetComponent<UIController>();
    cam = Camera.main.GetComponent<CamController>();
    sound = GetComponent<AudioSource>();
  }

  void Update()
  {
    if (won)
    {
      transform.Rotate(Vector3.up * WIN_ROT_SPEED * Time.deltaTime, Space.Self);
    }
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventBus.OnRocketEnter -= onRockenter;
    EventBus.OnRocketExit -= onRocketExit;
    EventBus.OnItemCollected -= onItemCollected;
    EventBus.OnItemDropped -= onItemDropped;
    EventBus.OnVulcanoEntered -= onVulcanoEntered;
    EventBus.OnItemDemand -= onItemDemanded;
    EventBus.OnDropProcessed -= onDropProcessed;
    EventBus.OnItemCrafted -= onItemCrafted;
    EventBus.OnWin -= onWin;
  }

  void onRockenter()
  {
    player.SetActive(false);
    rocketController.enabled = true;
  }

  void onRocketExit()
  {
    rocketController.enabled = false;
    player.transform.position = exitPoint.position;
    player.SetActive(true);
  }

  void onItemCollected(ItemController.ItemType type)
  {
    string msg = "Collected " + type.ToString();
    Debug.Log(msg);
    uiController.notify(msg);
    sound.clip = itemPickSound;
    sound.Play();
  }

  void onItemDropped(ItemController.ItemType type)
  {
    Debug.Log("Dropped " + type.ToString());
    sound.clip = itemDropSound;
    sound.Play();
  }

  void onItemCrafted(List<ItemController.ItemType> craft)
  {
    sound.clip = itemCraftSound;
    sound.Play();
  }

  void onVulcanoEntered()
  {
    ItemController.ItemType item = playerController.checkInventory();
    if (item != ItemController.ItemType.NONE)
    {
      string msg = "Press action button to throw " + item.ToString();
      Debug.Log(msg);
      uiController.notify(msg);
    }
  }

  void onItemDemanded(ItemController.ItemType type)
  {
    if (type != ItemController.ItemType.NONE)
    {
      uiController.notify("Required " + type.ToString());
      ItemSoundMap itemSound = itemSounds[(int)type];
      sound.clip = itemSound.sound;
      sound.Play();
    }
  }

  void onDropProcessed(string result)
  {
    uiController.notify(result);
  }

  void onWin()
  {
    won = true;
    ui.SetActive(false);
  }
}
