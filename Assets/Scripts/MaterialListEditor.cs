using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MaterialListEditor : EditorWindow
{
    [MenuItem("Window/Material List")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MaterialListEditor));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Load Materials"))
        {
            LoadMaterials();
        }
    }

    private void LoadMaterials()
    {
        MaterialList.LoadMaterials();
        Debug.Log("Materials loaded.");
    }
}

public static class MaterialList
{
    private static Dictionary<string, Material> _materials = new Dictionary<string, Material>();

    public static void LoadMaterials()
    {
        // Get the folder path for the materials
        string materialsFolderPath = "Assets/Materials/Brick Materials"; // Change this to your materials folder path

        // Get all the materials in the folder and add them to the dictionary
        foreach (string materialPath in AssetDatabase.FindAssets("t:Material", new[] { materialsFolderPath }))
        {
            Material material = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(materialPath));
            if (material != null)
            {
                if (_materials.ContainsKey(material.name))
                {
                    Debug.LogWarning($"MaterialList already contains a material with name {material.name}. Overwriting material.");
                    _materials[material.name] = material;
                }
                else
                {
                    _materials.Add(material.name, material);
                    Debug.Log(material.name);
                }
            }
        }
    }

    public static Material GetMaterial(string name)
    {
        if (_materials.ContainsKey(name))
        {
            return _materials[name];
        }
        else
        {
            Debug.LogWarning($"MaterialList does not contain a material with name {name}. Returning null.");
            return null;
        }
    }
}