using System;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

public class WorldCell : MonoBehaviour
{
    private const float CELL_SIZE = 512;
    
    public bool IsLoaded { get; private set; }
    public bool IsVisible { get; private set; }
    public Vector2Int Position => _position;
    public string Id => _id;

    [SerializeField] private Vector2Int _position;
    [SerializeField] private string _id;

    public void Load()
    {
        if(IsLoaded)
        {
            gameObject.SetActive(true);
            IsVisible = true;

            return;
        }

        var operation = AssetsManager.LoadWorldCell(_id, transform);

        operation.Completed += (cell) => IsVisible = true;

        IsLoaded = true;
    }
    

    public void Unload()
    {
        gameObject.SetActive(true);

        IsVisible = false;
    }

#if UNITY_EDITOR
    
    [SerializeField] private Color _editorGizmoColor = Color.white;

    private string _debugName;

    public void SetCurrentPosition_Editor()
    {
        var objectName = gameObject.name.ToCharArray();
        var position = new Vector2Int(int.Parse(objectName[0].ToString()), int.Parse(objectName[1].ToString()));

        _position = position;
    }

    public void GenerateId_Editor()
    {
        _id = Guid.NewGuid().ToString();
    }

    public void GenerateColor_Editor()
    {
        _editorGizmoColor = new Color(GetRandomColorValue_Editor(),
            GetRandomColorValue_Editor(), GetRandomColorValue_Editor(), 255f);
    }

    public void Load_Editor()
    {
        var operation = AssetsManager.LoadWorldCell(_id, transform);

        _debugName = operation.DebugName;

        operation.Destroyed += (cell) => _debugName = "";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _editorGizmoColor;
        Gizmos.DrawWireCube(transform.position, Vector3.one * CELL_SIZE);

        if (string.IsNullOrEmpty(_debugName) != false)
            Handles.Label(transform.position, _debugName);
    }

    private float GetRandomColorValue_Editor()
    {
        return Random.Range(0f, 0.8f);
    }
    
#endif
}
