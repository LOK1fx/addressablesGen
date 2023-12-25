using UnityEngine;

public class WorldCellLoader : MonoBehaviour
{
    [SerializeField] private float _distanceToLoad = 600f;

    private WorldCell[] _cells;

    private void Awake()
    {
        _cells = FindObjectsOfType<WorldCell>();
    }

    private void Update()
    {
        var nearestCellIndex = 0;

        for (int i = 0; i < _cells.Length; i++)
        {
            if(GetDistanceTo(_cells[i]) <= GetDistanceTo(_cells[nearestCellIndex]))
            {
                nearestCellIndex = i;
            }
        }

        if (GetDistanceTo(_cells[nearestCellIndex]) < _distanceToLoad && _cells[nearestCellIndex].IsLoaded == false)
            _cells[nearestCellIndex].Load();
    }

    private float GetDistanceTo(WorldCell cell)
    {
        return Vector3.Distance(LocalPlayer.Position, cell.transform.position);
    }
}