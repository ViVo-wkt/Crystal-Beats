using UnityEngine;

[ExecuteInEditMode]
public class CheckerFloorGenerator : MonoBehaviour
{
    public GameObject tileA;
    public GameObject tileB;
    public int width = 75;
    public int height = 75;
    public float tileSize = 1f;

    public bool generate = false;

    void Update()
    {
        // Only run in editor when "generate" is triggered
        if (!Application.isPlaying && generate)
        {
            generate = false;
            ClearChildren();
            GenerateCheckerFloor();
        }
    }

    void GenerateCheckerFloor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject tileToPlace = (x + z) % 2 == 0 ? tileA : tileB;
                Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);
                Instantiate(tileToPlace, position, Quaternion.Euler(90, 0, 0), transform);
            }
        }
    }

    void ClearChildren()
    {
        // Remove previous tiles so you don’t get duplicates
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
