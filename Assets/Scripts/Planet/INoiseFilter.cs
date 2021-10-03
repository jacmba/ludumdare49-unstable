using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseFilter
{
  float evaluate(Vector3 point);
}
