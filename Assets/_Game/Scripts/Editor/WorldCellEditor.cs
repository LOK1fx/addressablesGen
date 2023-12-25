using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldCell))]
public class WorldCellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Currect the position"))
        {
            var cell = (WorldCell)target;
            cell.SetCurrentPosition_Editor();

            EditorUtility.SetDirty(target);
        }
        if (GUILayout.Button("Generate Color"))
        {
            var cell = (WorldCell)target;
            cell.GenerateColor_Editor();
            
            EditorUtility.SetDirty(target);
        }
    }
}
