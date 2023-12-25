using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BigWorldCompositorEditorWindow : EditorWindow
{
    private List<WorldCell> _worldCells = new List<WorldCell>();
    private int _worldSizeX = 2;
    private int _worldSizeY = 2;

    private List<GameObject> _previewLoadedCells = new List<GameObject>();

    private int _selectedTool = 0;



    [MenuItem("LOK1game/BigWorld/Manage world")]
    public static void ShowWindow()
    {
        GetWindow<BigWorldCompositorEditorWindow>();
    }

    private void Awake()
    {
        Validate();
    }

    private void Validate()
    {
        if (_worldCells.Count > 0)
            _worldCells.Clear();

        var cells = FindObjectsOfType<WorldCell>();

        foreach (var cell in cells)
        {
            _worldCells.Add(cell);
        }
    }

    private void OnGUI()
    {
        _selectedTool = GUILayout.Toolbar(_selectedTool, new string[3] { "Manage world", "Tiles", "Map" });

        if (GUILayout.Button("Validate"))
            Validate();

        if (_worldCells.Count < 1)
        {
            EditorGUILayout.LabelField("Please, validate the compositor!");
            return;
        }

        GUILayout.Space(15f);

        if (_selectedTool == 0)
        {
            DrawManageWorld();
        }
        if (_selectedTool == 1)
        {
            DrawTiles();
        }
        if (_selectedTool == 2)
        {
            DrawMap();
        }
    }

    private void DrawManageWorld()
    {
        DrawPreviewOptions();

        EditorGUILayout.LabelField("Manage size of the world");
        _worldSizeX = EditorGUILayout.IntField("X size: ", _worldSizeX);
        _worldSizeY = EditorGUILayout.IntField("Y size: ", _worldSizeY);
    }

    private void DrawTiles()
    {
        DrawPreviewOptions();

        for (int x = 0; x < _worldSizeX; x++)
        {
            if (GUILayout.Button($"x:{x}"))
            {

            }

            for (int y = 0; y < _worldSizeY; y++)
            {
                if (GUILayout.Button($"y:{y}"))
                {

                }
            }
        }

        GUILayout.FlexibleSpace();

        foreach (var cell in _worldCells)
        {
            if (GUILayout.Button($"{cell.name}"))
            {

            }
        }
    }

    private void DrawPreviewOptions()
    {
        if (GUILayout.Button($"Load preview"))
        {
            foreach (var cell in _worldCells)
            {
                cell.Load_Editor();
                cell.SetCurrentPosition_Editor();
            }
        }
        if (GUILayout.Button("Unload preview"))
        {
            foreach (var cell in _worldCells)
            {
                if (cell.transform.GetChild(0) != null)
                    DestroyImmediate(cell.transform.GetChild(0).gameObject);
            }
        }

        GUILayout.Space(15f);
    }

    private void DrawMap()
    {

    }
}
