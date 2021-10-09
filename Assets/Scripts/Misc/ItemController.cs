using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemController : MonoBehaviour
{
  public enum ItemType
  {
    CARBON,
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
  private Vector3 rotation;

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
  }

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(rotation, Space.Self);
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

  public float rand()
  {
    return Random.Range(-2f, 2f);
  }
}
