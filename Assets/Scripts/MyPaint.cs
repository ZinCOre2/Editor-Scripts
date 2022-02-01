using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyPaint : EditorWindow
{
    private Color _paintColor, _eraseColor;
    private Renderer _renderer;
    private const int TEXTURE_SIZE_X = 12, TEXTURE_SIZE_Y = 12;
    private Color[] _colors = new Color[TEXTURE_SIZE_X * TEXTURE_SIZE_Y];
    
    
    [MenuItem("Paint/My Paint")]
    private static void OpenMyPaint()
    {
        GetWindow<MyPaint>();
    }
    
    private void Awake()
    {
        _paintColor = Color.black;
        _eraseColor = Color.white;

        RandomizeColors();
    }

    private void OnGUI()
    {
        var currentEvent = Event.current;
        
        GUILayout.BeginHorizontal();
        
        // Start: Toolbar
        GUILayout.BeginVertical();
        GUILayout.Label("Toolbar");
        _paintColor = EditorGUILayout.ColorField("Paint Color", _paintColor);
        _eraseColor = EditorGUILayout.ColorField("Erase Color", _eraseColor);
        if (GUILayout.Button("Color All Randomly"))
        {
            RandomizeColors();
        }
        if (GUILayout.Button("Fill All"))
        {
            FillAll(_paintColor);
        }
        if (GUILayout.Button("Erase All"))
        {
            FillAll(_eraseColor);
        }
        GUILayout.FlexibleSpace();
#pragma warning disable 618
        _renderer = (Renderer)EditorGUILayout.ObjectField("Output Renderer", _renderer, typeof(Renderer));
#pragma warning restore 618
        if (GUILayout.Button("Save to Object"))
        {
            SaveTextureInObject();
        }
        GUILayout.EndVertical();
        // End: Toolbar
        
        // Texture Grid
        GUILayout.BeginVertical();
        for (var i = 0; i < TEXTURE_SIZE_Y; i++)
        {
            GUILayout.BeginHorizontal();
            for (var j = 0; j < TEXTURE_SIZE_X; j++)
            {
                var rect = GUILayoutUtility.GetRect(    10, 40, 10, 40);
                
                if (currentEvent.type == EventType.MouseDown)
                {
                    if (rect.Contains(currentEvent.mousePosition))
                    {
                        switch (currentEvent.button)
                        {
                            case 0:
                                _colors[i * TEXTURE_SIZE_X + j] = _paintColor;
                                break;
                            case 1:
                                _colors[i * TEXTURE_SIZE_X + j] = _eraseColor;
                                break;
                        }
                        currentEvent.Use();
                    }
                }
                GUI.color = _colors[i * TEXTURE_SIZE_X + j];
                GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);
            }
            GUILayout.EndHorizontal();
        }
        
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }


    private void RandomizeColors()
    {
        for (var i = 0; i < TEXTURE_SIZE_Y; i++)
        {
            for (var j = 0; j < TEXTURE_SIZE_X; j++)
            {
                _colors[i * TEXTURE_SIZE_X + j] = RandomColor();
            }
        }
    }
    private Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, 1);
    }
    private void FillAll(Color fillColor)
    {
        for (var i = 0; i < TEXTURE_SIZE_Y; i++)
        {
            for (var j = 0; j < TEXTURE_SIZE_X; j++)
            {
                _colors[i * TEXTURE_SIZE_X + j] = fillColor;
            }
        }
    }
    private void SaveTextureInObject()
    {
        var texture = new Texture2D(TEXTURE_SIZE_X, TEXTURE_SIZE_Y);
        
        for (var i = 0; i < TEXTURE_SIZE_Y; i++)
        {
            for (var j = 0; j < TEXTURE_SIZE_X; j++)
            {
                texture.SetPixel(i, j, _colors[i * TEXTURE_SIZE_X + j]);
            }
        }
        texture.filterMode = FilterMode.Point;
        
        texture.Apply();
        _renderer.sharedMaterial.mainTexture = texture;
    }
}