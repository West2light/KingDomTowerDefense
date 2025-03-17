using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{

    public Cell prefabCell;
    public int columns = 14;
    public int rows = 18;
    public Transform transform;

    private float cellSize = 0.42f;
    private Dictionary<Cell, int> dictionaryMapCell = new Dictionary<Cell, int>(); //key = Cell, value = index;
    public List<GameObject> shapes = new List<GameObject>();
    public GameObject shapeTestPrefab;
    private int[,] grid;
    private void Start()
    {
        Intialize();
        grid = new int[columns, rows];
        PlaceShape(0, 2, 2, shapeTestPrefab);

    }

    private void Intialize()
    {
        int index = 0;
        Vector2 startPosition = new Vector2(-columns / 2f * cellSize, rows / 2f * cellSize);
        Debug.Log("startPosition = " + startPosition);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector2 spawnPosition = new Vector2(startPosition.x + j * cellSize, startPosition.y - i * cellSize);
                Cell cell = Instantiate(prefabCell, spawnPosition, Quaternion.identity, transform);
                cell.index = index;
                dictionaryMapCell.Add(cell, index);
                index++;

            }
        }

    }
    private bool CanPlaceShape(int startX, int startY, int shapeWidth, int shapeHeight)
    {
        if (startX + shapeWidth > columns || startY + shapeHeight > rows)
        {
            return false;
        }
        for (int x = 0; x < shapeWidth; x++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                if (grid[startX + x, startY + y] == 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void PlaceShape(int index, int shapeWidth, int shapeHeight, GameObject shapePrefab)
    {
        int startX = index % columns;
        int startY = index / columns;
        if (CanPlaceShape(startX, startY, shapeWidth, shapeHeight) == false)
        {
            return;
        }
        Vector2 spawnPosition = GetCellPosition(startX, startY);
        GameObject shape = Instantiate(shapeTestPrefab, spawnPosition, Quaternion.identity);
        for (int x = 0; x < shapeWidth; x++)
        {
            for (int y = 0; y < shapeHeight; y++)
            {
                grid[startX + x, startY + y] = 1;
            }
        }
    }
    private Vector2 GetCellPosition(float x, float y)
    {
        return new Vector2(x * cellSize, -y * cellSize);
    }



}
