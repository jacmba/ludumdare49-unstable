using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  [SerializeField] private ItemController.ItemType type;
  private List<Transform> points;
  private float timer;
  private const float TIME_LIMIT = 10f;

  private GameObject item;

  // Start is called before the first frame update
  void Start()
  {
    points = new List<Transform>();
    Transform pointsRoot = transform.Find("SpawnPoints");
    for (int i = 0; i < pointsRoot.childCount; i++)
    {
      Transform p = pointsRoot.GetChild(i);
      points.Add(p);
    }

    timer = 0f;
    item = null;
  }

  // Update is called once per frame
  void Update()
  {
    if (item == null)
    {
      timer += Time.deltaTime;
    }

    if (timer >= TIME_LIMIT)
    {
      timer = 0f;
      spawn();
    }
  }

  void spawn()
  {
    int index = Random.Range(0, points.Count);
    Transform p = points[index];
    item = ItemFactory.build(p, type);
  }
}
