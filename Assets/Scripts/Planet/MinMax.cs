using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
  public float min { get; private set; }
  public float max { get; private set; }

  public MinMax()
  {
    min = float.MaxValue;
    max = float.MinValue;
  }

  public void addValue(float v)
  {
    if (v > max)
    {
      max = v;
    }
    else if (v < min)
    {
      min = v;
    }
  }
}
