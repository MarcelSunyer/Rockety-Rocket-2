#if UNITY_EDITOR

using RocketyRocket2;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

  [CustomEditor(typeof(BladeFunction))]
    public class BladeFunctionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("AddPosition"))
            {
                BladeFunction bladeFunction = (BladeFunction)target;

                bladeFunction.positionsToMove[bladeFunction.positionsToMove.Count - 1] = bladeFunction.gameObject.transform.position;
                bladeFunction.positionsToMove.Add(new Vector2(0,0));
            }
        }
    }

#endif

