using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    public AnimationCurve ac;
    private void OnGUI()
    {
        ac.Evaluate(0.5f);
        Rect rect = new Rect(0, 0, 200, 200);
        
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        if (GUILayout.Button("Button 1")) {Debug.Log(1);}
        if (GUILayout.Button("Button 2")) {Debug.Log(2);}
        if (GUILayout.Button("Button 3")) {Debug.Log(3);}
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if (GUILayout.Button("Button 4")) {Debug.Log(4);}
        if (GUILayout.Button("Button 5")) {Debug.Log(5);}
        GUILayout.EndVertical();
        if (GUILayout.Button("Button 6")) {Debug.Log(6);}
        GUILayout.EndHorizontal();

        EditorGUILayout.CurveField(ac);
    }
}
