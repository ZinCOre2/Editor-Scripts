using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyMenus : MonoBehaviour
{
    [MenuItem("GameObject/Hello Menu", false, -1000)]
    public static void HelloMenu()
    {
        Debug.Log("Hello world");
    }
}
