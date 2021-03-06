using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoController : MonoBehaviour
{
  private int stability = 100;
  private List<ItemController.ItemType> craft;
  private ItemController.ItemType demand;
  private float timer;
  private bool finished;
  private Transform emitter;
  private float pyroTimer;
  private float pyroTimeLimit;
  private GameObject pyroPrefab;
  private AudioSource sound;


  private const float DEMAND_TIME = 5f * 60f;
  private const float STAB_TIMER = 30f;

  // Start is called before the first frame update
  void Start()
  {
    craft = new List<ItemController.ItemType>();
    demand = ItemController.ItemType.NONE;

    timer = DEMAND_TIME * .75f;

    EventBus.OnItemDropped += onItemDropped;
    finished = false;

    emitter = transform.Find("SmokeEmitter");
    pyroPrefab = Resources.Load<GameObject>("Prefabs/Pyroclast");
    sound = GetComponent<AudioSource>();
    pyroChrono();
  }

  void OnDestroy()
  {
    EventBus.OnItemDropped -= onItemDropped;
  }

  // Update is called once per frame
  void Update()
  {
    if (finished)
    {
      if (sound.isPlaying)
      {
        sound.Stop();
      }
      return;
    }
    else if (!sound.isPlaying)
    {
      sound.Play();
    }
    timer += Time.deltaTime;
    if (demand == ItemController.ItemType.NONE)
    {
      if (timer >= DEMAND_TIME)
      {
        demand = (ItemController.ItemType)Random.Range(0, 3);
        timer = 0f;
        Debug.Log("Planet demanding " + demand.ToString());
        EventBus.demandItem(demand);
      }
    }
    else
    {
      if (timer >= STAB_TIMER)
      {
        stability--;
        timer = 0f;
        Debug.Log("Stability decreased: " + stability);
        EventBus.changeStability(stability);
      }
    }

    pyroTimer += Time.deltaTime;
    if (pyroTimer >= pyroTimeLimit)
    {
      var pyroclast = GameObject.Instantiate(pyroPrefab, emitter.position, emitter.rotation);
      var pyroBody = pyroclast.GetComponent<Rigidbody>();
      Vector3 force = new Vector3(Random.Range(1500f, 5000f), Random.Range(5000f, 10000f), Random.Range(1500f, 5000f));
      if (Random.Range(1f, 2f) < 1.5f)
      {
        force.x *= -1;
      }
      if (Random.Range(1f, 2f) < 1.5f)
      {
        force.z *= -1;
      }
      pyroBody.AddForce(force);
      pyroChrono();
    }
  }

  void onItemDropped(ItemController.ItemType item)
  {
    if (demand == ItemController.ItemType.NONE)
    {
      craft.Add(item);
      Debug.Log("Crafting " + craft.ToString());

      EventBus.craftItem(craft);
      EventBus.notify("Added " + item.ToString() + " to crafting queue");

      if (craft.Count >= 3)
      {
        StartCoroutine(makeCraft());
      }
    }
    else
    {
      if (item == demand)
      {
        demand = ItemController.ItemType.NONE;
        timer = 0f;
        Debug.Log("Planet core stabilized with " + item.ToString() + ". Stability: " + stability);
        EventBus.dropProcessed("Successfully added " + item.ToString() + " to planet core");
        EventBus.demandItem(ItemController.ItemType.NONE);
      }
      else
      {
        stability -= 10;
        Debug.Log("Wrong item inserted in planet core. Stability: " + stability);
        EventBus.dropProcessed("Wrong item added to core. Required: " + demand.ToString());
        EventBus.changeStability(stability);
      }
    }
  }

  IEnumerator makeCraft()
  {
    yield return new WaitForSeconds(5f);

    if (craft[0] == ItemController.ItemType.HYDROGEN && craft[1] == ItemController.ItemType.HYDROGEN
      && craft[2] == ItemController.ItemType.OXYGEN)
    {
      finished = true;
      string msg = "Water crafted, planet core calmed down!";
      Debug.Log(msg);
      EventBus.notify(msg);

      yield return new WaitForSeconds(3f);
      EventBus.win();
    }
    else
    {
      string msg = "Useless sequence crafted";
      Debug.Log(msg);
      EventBus.notify(msg);
    }

    craft.Clear();
    EventBus.craftItem(craft);
  }

  void pyroChrono()
  {
    pyroTimer = 0;
    pyroTimeLimit = Random.Range(1, 10);
  }
}
