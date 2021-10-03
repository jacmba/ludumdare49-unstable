using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory
{
  public static INoiseFilter createNoiseFilter(NoiseSettings settings)
  {
    switch (settings.filterType)
    {
      case NoiseSettings.FilterType.Simple:
        return new NoiseFilter(settings);
      case NoiseSettings.FilterType.Rigid:
        return new RigidNoiseFilter(settings);
      default:
        return null;
    }
  }
}
