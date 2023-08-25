using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]private int gridLength = 10, gridWidth = 1;
    [SerializeField] private GameObject cellPrefab;

    public Transform gridHolder;

    public Material gridMaterial1;
    public Material gridMaterial2;

    public bool makeGridCheckered;

    [HideInInspector]
    public Color solidColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

    [HideInInspector]
    public Color checkeredColor1 = new Color(0.772549f, 0.6941177f, 0.6666667f, 1f);
    [HideInInspector]
    public Color checkeredColor2 = new Color(0.6274511f, 0.4549019f, 0.3803921f, 1f);

    private float cellLength, cellWidth;

    public void Init()
    {
        cellLength = cellPrefab.GetComponent<Renderer>().bounds.size.x * cellPrefab.transform.localScale.x;
        cellWidth = cellPrefab.GetComponent<Renderer>().bounds.size.z * cellPrefab.transform.localScale.z;
    }

    public void GenerateGrid()
    {
        for(int i = 0; i < gridLength; i++) 
        {
            for (int j = 0; j < gridWidth; j++) 
            {
                var spawnedTile = Instantiate(cellPrefab, GetCellPosition(i, j), Quaternion.identity);
                spawnedTile.transform.parent = gridHolder;

                //Since material copies not made in edit mode using multiple materials then changing color
                if (makeGridCheckered)
                    spawnedTile.GetComponent<Renderer>().material = ((i + j) % 2 == 0) ? gridMaterial1 : gridMaterial2;
                
                else
                    spawnedTile.GetComponent<Renderer>().material = gridMaterial1;

                spawnedTile.name = $"Cell {i} {j}";
            }
        }
    }

    private Vector3 GetCellPosition(int xIterator, int zIterator)
    {
        float positionX, positionZ;

        positionX = (xIterator - (((float)gridLength -1)/2))*cellLength;
        positionZ = (zIterator - (((float)gridWidth -1)/2))*cellWidth;

        Vector3 cellPosition = new Vector3(positionX, 0, positionZ);

        return cellPosition;
    }

}


[CustomEditor(typeof(GridManager))]
public class MyScriptEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridManager script = (GridManager)target;

        if(GUILayout.Button("Generate Grid"))
        {
            //If generate button pressed multiple times the to avoid multiple copies
            if(script.gridHolder.childCount != 0)
            {
                int childCount = script.gridHolder.childCount;
                for(int i = 0; i < childCount; i++)
                {
                    DestroyImmediate(script.gridHolder.GetChild(0).gameObject);
                }
            }

            script.Init();
            script.GenerateGrid();  
        }

        if (script.makeGridCheckered)
        {
            script.checkeredColor1 = EditorGUILayout.ColorField("Color1", script.checkeredColor1);
            script.gridMaterial1.color = script.checkeredColor1;

            script.checkeredColor2 = EditorGUILayout.ColorField("Color2", script.checkeredColor2);
            script.gridMaterial2.color = script.checkeredColor2;
        }
        else
        {
            script.solidColor = EditorGUILayout.ColorField("Color", script.solidColor);
            script.gridMaterial1.color = script.solidColor;
        }

        //DrawDefaultInspector();
    }
}
