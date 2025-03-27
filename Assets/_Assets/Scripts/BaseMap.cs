using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{

    public Cell prefabCell;
    public int columns = 14;
    public int rows = 18;
    public Transform transform;
    public List<GameObject> shapes = new List<GameObject>();
    public GameObject shapeTestPrefab;

    private float cellSize = 0.42f;
    private Dictionary<Cell, int> dictionaryMapCell = new Dictionary<Cell, int>(); //key = Cell, value = index;
    private List<Cell> cells = new List<Cell>();
    private void Start()
    {
        Intialize();

        Vector2 cellPos = GetPositionCell(5);
        Debug.Log("cellIndex = " + GetIndexCell(cellPos));
    }

    private void Intialize()
    {
        int index = 0;
        Vector2 startPosition = new Vector2(-columns / 2f * cellSize + 0.2f, rows / 2f * cellSize - 0.6f);
        Debug.Log("startPosition = " + startPosition);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector2 spawnPosition = new Vector2(startPosition.x + j * cellSize, startPosition.y - i * cellSize);
                Cell cell = Instantiate(prefabCell, spawnPosition, Quaternion.identity, transform);
                cell.index = index;
                dictionaryMapCell.Add(cell, index);
                cells.Add(cell);
                index++;

            }
        }

    }

    private Vector2 GetPositionCell(int index)
    {
        float x = 0;
        float y = 0;
        for (int i = 0; i < cells.Count; i++)
        {
            Cell cell = cells[i];
            if (cell.index == index)
            {
                x = cell.transform.position.x;
                y = cell.transform.position.y;
                return new Vector2(x, y);
            }
        }
        return Vector2.zero;
    }


    private int GetIndexCell(Vector2 position)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            Cell cell = cells[i];
            /*if ()
            {
                return cell.index;
            }*/
        }
        return -1;
    }
}
