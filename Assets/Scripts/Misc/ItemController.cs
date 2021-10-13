using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemController : MonoBehaviour
{
  public enum ItemType
  {
    NONE = -1,
    CARBON = 0,
    OXYGEN,
    NITROGEN,
    HYDROGEN
  }

  [System.Serializable]
  public struct ColorMap
  {
    public ItemType type;
    public Color color;
  }

  public ItemType type;
  public bool released;
  private Vector3 rotation;
  private Rigidbody rb;
  private GravityBody body;
  private Transform vulcanoEntry;
  private bool launching;

  private const float LAUNCH_SPEED = 1f;

  [SerializeField] private List<ColorMap> colorMap;

  // Start is called before the first frame update
  void Start()
  {
    rotation.Set(rand(), rand(), rand());
    MeshRenderer mesh = GetComponent<MeshRenderer>();
    Light light = GetComponentInChildren<Light>();
    Color color = colorMap[(int)type].color;
    mesh.material.color = color;
    light.color = color;

    Transform labelsRoot = transform.Find("Labels");
    TextMesh[] labels = labelsRoot.GetComponentsInChildren<TextMesh>();
    foreach (TextMesh label in labels)
    {
      label.text = getSymbol();
    }

    rb = GetComponent<Rigidbody>();
    body = GetComponent<GravityBody>();
    BoxCollider collider = GetComponent<BoxCollider>();

    body.enabled = false;
    rb.constraints = RigidbodyConstraints.FreezeRotation;
    rb.useGravity = false;

    if (released)
    {
      GameObject vulcano = GameObject.FindGameObjectWithTag("Vulcano");
      vulcanoEntry = vulcano.transform.Find("VulcanoEntry");
      collider.isTrigger = false;
      collider.size = collider.size / 2;

      launching = true;
    }
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(rotation, Space.Self);

    if (launching)
    {
      float dist = Vector3.Distance(transform.position, vulcanoEntry.position);
      if (dist < LAUNCH_SPEED)
      {
        launching = false;
        body.enabled = true;
      }
    }
  }

  void FixedUpdate()
  {
    if (launching)
    {
      Vector3 movement = Vector3.Lerp(rb.position, vulcanoEntry.position, LAUNCH_SPEED * Time.deltaTime);
      rb.position = movement;
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.name == "VulcanoMouth")
    {
      EventBus.dropItem(type);
      Destroy(gameObject);
    }
  }

  public string getSymbol() =>
    type switch
    {
      ItemType.CARBON => "C",
      ItemType.HYDROGEN => "H",
      ItemType.OXYGEN => "O",
      ItemType.NITROGEN => "N",
      _ => ""
    };

  public Color getColor()
  {
    return colorMap[(int)type].color;
  }

  public float rand()
  {
    return Random.Range(-2f, 2f);
  }
}
