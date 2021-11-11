using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBarController : MonoBehaviour
{
  [SerializeField] private int minValue = 0;
  [SerializeField] private int maxValue = 100;
  public int value = 100;
  private RectTransform foreground;
  private int imgLength;
  private int imgHeight;

  // Start is called before the first frame update
  void Start()
  {
    Transform ft = transform.Find("Foreground");
    foreground = ft.GetComponent<RectTransform>();
    imgLength = (int)foreground.sizeDelta.x;
    imgHeight = (int)foreground.sizeDelta.y;
  }

  // Update is called once per frame
  void Update()
  {
    if (value < minValue)
    {
      value = minValue;
    }
    if (value > maxValue)
    {
      value = maxValue;
    }
    int v = (int)(((float)value / (maxValue - minValue)) * imgLength);
    Vector2 size = new Vector2(v, imgHeight);
    foreground.sizeDelta = size;
  }
}
