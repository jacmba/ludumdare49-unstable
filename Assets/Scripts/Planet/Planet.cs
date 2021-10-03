using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
  [SerializeField]
  [Range(2, 256)]
  int resolution = 10;

  public ShapeSettings shapeSettings;
  public ColorSettings colorSettings;

  public enum FaceRenderMask
  {
    All,
    Top,
    Bottom,
    Left,
    Right,
    Front,
    Back
  }
  public FaceRenderMask faceRenderMask;

  ShapeGenerator shapeGenerator;

  [SerializeField, HideInInspector]
  MeshFilter[] meshFilters;
  TerrainFace[] terrainFaces;

  private void OnValidate()
  {
    generatePlanet();
  }

  public void OnColorSettingsUpdated()
  {
    initialize();
    generateColors();
  }

  public void OnShapeSettingsUpdated()
  {
    initialize();
    generateMesh();
  }

  void initialize()
  {
    shapeGenerator = new ShapeGenerator(shapeSettings);

    if (meshFilters == null || meshFilters.Length == 0)
    {
      meshFilters = new MeshFilter[6];
    }
    terrainFaces = new TerrainFace[6];

    Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    for (int i = 0; i < 6; i++)
    {
      if (meshFilters[i] == null)
      {
        GameObject meshObj = new GameObject("mesh");
        meshObj.transform.parent = transform;

        meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        meshFilters[i] = meshObj.AddComponent<MeshFilter>();
        meshFilters[i].sharedMesh = new Mesh();
      }

      terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
      bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
      meshFilters[i].gameObject.SetActive(renderFace);
    }
  }

  public void generatePlanet()
  {
    initialize();
    generateMesh();
    generateColors();
  }

  void generateMesh()
  {
    for (int i = 0; i < 6; i++)
    {
      if (meshFilters[i].gameObject.activeSelf)
      {
        terrainFaces[i].constructMesh();
      }
    }
  }

  void generateColors()
  {
    foreach (MeshFilter m in meshFilters)
    {
      m.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.planetColor;
    }
  }
}
