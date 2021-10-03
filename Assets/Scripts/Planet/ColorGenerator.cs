using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
  ColorSettings settings;
  Texture2D texture;
  const int textureResolution = 50;

  public void updateSettings(ColorSettings settings)
  {
    this.settings = settings;
    texture = new Texture2D(textureResolution, 1);
  }

  public void updateElevation(MinMax elevationMinMax)
  {
    settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.min, elevationMinMax.max));
  }

  public void updateColors()
  {
    Color[] colors = new Color[textureResolution];
    for (int i = 0; i < textureResolution; i++)
    {
      colors[i] = settings.planetGradient.Evaluate(i / (textureResolution - 1f));
    }
    texture.SetPixels(colors);
    texture.Apply();
    settings.planetMaterial.SetTexture("_texture", texture);
  }
}
