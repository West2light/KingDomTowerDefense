using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Shape-", menuName = "Scriptable Objects/ShapeData")]
public class ShapeData : ScriptableObject
{
    public Vector2Int[] cellPosition;
    public int centerIndex;
    public Shape prefabShape;
    public Sprite icon;
    public List<Vector2Int> GetFilledPosition(Vector2Int boardIndexOfCenterShape)
    {
        List<Vector2Int> filledPosition = new List<Vector2Int>();
        int xCenter = cellPosition[centerIndex].x;
        int yCenter = cellPosition[centerIndex].y;
        for (int i = 0; i < cellPosition.Length; i++)
        {
            Vector2Int v = cellPosition[i];
            int xOffset = v.x - xCenter;
            v.x = boardIndexOfCenterShape.x + xOffset;
            int yOffset = v.y - yCenter;
            v.y = boardIndexOfCenterShape.y + yOffset;
            filledPosition.Add(v);
        }
        return filledPosition;
    }

}

[CustomEditor(typeof(ShapeData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class ShapeDataDrawer : Editor
{
    private const float gridSize = 20f;
    private const float borderThickness = 1f;
    private const float rowSpacing = 1f;
    private ShapeData shapeData => target as ShapeData;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(20);
        Rect rect = GUILayoutUtility.GetRect(100, 100);
        GUI.Box(rect, GUIContent.none);
        for (int i = 0; i < shapeData.cellPosition.Length; i++)
        {
            Vector2Int position = shapeData.cellPosition[i];
            Vector2 pos = rect.position + new Vector2(position.x * gridSize, position.y * (gridSize + rowSpacing));
            EditorGUI.DrawRect(new Rect(pos.x - borderThickness, pos.y - borderThickness, gridSize + borderThickness * 2, gridSize + borderThickness * 2), Color.black);
            bool isCenter = i == shapeData.centerIndex;
            EditorGUI.DrawRect(new Rect(pos, new Vector2(gridSize, gridSize)), isCenter ? Color.green : Color.cyan);

            Rect labelRect = new Rect(pos.x, pos.y, gridSize, gridSize);
            EditorGUI.LabelField(labelRect, i.ToString(), new GUIStyle { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = Color.black } });
        }
    }

}