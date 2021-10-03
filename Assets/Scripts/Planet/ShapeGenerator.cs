using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
  ShapeSettings settings;
  INoiseFilter[] noiseFilters;

  public ShapeGenerator(ShapeSettings settings)
  {
    this.settings = settings;
    noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
    for (int i = 0; i < noiseFilters.Length; i++)
    {
      noiseFilters[i] = NoiseFilterFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
    }
  }

  public Vector3 calculatePointOnPlanet(Vector3 pointOnUnitSphere)
  {
    float firstLayerValue = 0;
    float elevation = 0f;

    if (noiseFilters.Length > 0)
    {
      firstLayerValue = noiseFilters[0].evaluate(pointOnUnitSphere);
      if (settings.noiseLayers[0].enabled)
      {
        elevation = firstLayerValue;
      }
    }

    for (int i = 0; i < noiseFilters.Length; i++)
    {
      if (settings.noiseLayers[i].enabled)
      {
        float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1f;
        elevation += noiseFilters[i].evaluate(pointOnUnitSphere) * mask;
      }
    }
    return pointOnUnitSphere * settings.planetRadius * (1 + elevation);
  }
}
