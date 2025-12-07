#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RocketyRocket2
{
    public class BladeFunctionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("AddPosition"))
            {
                Debug.Log("sdf");
            }
        }
    }
}

#endif
