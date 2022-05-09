using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatePastelColorTexture : ScriptableObject
{
    [MenuItem("Assets/CreatePastelTexture")]
    static void CreatePastelTexture() 
    { 
        Texture2D texture = new Texture2D(8, 8);
        texture.filterMode = FilterMode.Point;
        for (int i = 0; i < 64; i++)
        {
            if (i < 8)
            {
                texture.SetPixel(i % 8, i / 8, Color.HSVToRGB(0, 0, i/7f));
            }
            else
            {
                texture.SetPixel(i % 8, i / 8, Color.HSVToRGB((i - 8f) / 56f, 0.5f, 1f));
            }
        }
        texture.Apply();
        File.WriteAllBytes("./Assets/texture.png", texture.EncodeToPNG());
    }


}
