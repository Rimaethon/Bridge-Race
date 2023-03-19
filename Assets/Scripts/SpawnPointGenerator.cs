using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGenerator : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float spacing = 1f;
    [SerializeField] float edgeBuffer = 0.5f;
    [SerializeField] private List<Vector3> positions;

    private void Start()
    {
        // Get the size of the plane
        Vector3 size = GetComponent<Renderer>().bounds.size;

        // Calculate the spacing between positions
        float xSpacing = spacing;
        float zSpacing = spacing;
        float xPos = -size.x / 2f + edgeBuffer;
        float zPos = -size.z / 2f + edgeBuffer;

        // Generate positions on the plane
        positions = new List<Vector3>();
        while (xPos < size.x / 2f - edgeBuffer)
        {
            while (zPos < size.z / 2f - edgeBuffer)
            {
                Vector3 position = transform.TransformPoint(new Vector3(xPos, 0f, zPos));
                positions.Add(position);
                zPos += zSpacing;
            }
            zPos = -size.z / 2f + edgeBuffer;
            xPos += xSpacing;
        }
    }
}
