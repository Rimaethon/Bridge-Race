using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Helpers
{
    public class SpawnPointGenerator : MonoBehaviour
    {
        [SerializeField] GameObject spawnPointObject;
        [SerializeField] private float edgeSpacingRatio = 0.1f;
        [SerializeField] private float xSpacingRatio = 0.2f;
        [SerializeField] private float zSpacingRatio = 0.4f;
        private List<Vector3> _positions;
        private float _spawnYPoint;
        public void GenerateSpawnPoints()
        {
            // Get the size of the plane
            Vector3 size = spawnPointObject.GetComponent<Renderer>().bounds.size;
            Vector3 center = spawnPointObject.GetComponent<Renderer>().bounds.center;

            // Calculate the spacing values based on the plane size and the ratios
            float edgeSpacing = Mathf.Min(size.x, size.z) * edgeSpacingRatio;
            float xSpacing = size.x * xSpacingRatio;
            float zSpacing = size.z * zSpacingRatio;

            // Get the corners of the plane in world space
            Vector3 bottomLeftCorner = new Vector3(center.x - size.x / 2f + edgeSpacing, 0f, center.z - size.z / 2f + edgeSpacing);
            Vector3 bottomRightCorner = new Vector3(center.x + size.x / 2f - edgeSpacing, 0f, center.z - size.z / 2f + edgeSpacing);
            Vector3 topLeftCorner = new Vector3(center.x - size.x / 2f + edgeSpacing, 0f, center.z + size.z / 2f - edgeSpacing);
            Vector3 topRightCorner = new Vector3(center.x + size.x / 2f - edgeSpacing, 0f, center.z + size.z / 2f - edgeSpacing);

            // Calculate the number of spawn points needed
            int numSpawnPointsX = Mathf.FloorToInt((bottomRightCorner - bottomLeftCorner).magnitude / xSpacing) + 1;
            int numSpawnPointsZ = Mathf.FloorToInt((topLeftCorner - topRightCorner).magnitude / zSpacing) + 1;

            
            RaycastHit hit;
            if (Physics.Raycast(center + Vector3.up * 1000f, Vector3.down, out hit, Mathf.Infinity))
            {
                _spawnYPoint = hit.point.y + 0.15f;
            }
            else
            {
                // Unable to determine height, use a default value
                Debug.Log("Unable to determine height of plane at center point.");
                _spawnYPoint = 0f;
            }

            // Generate positions on the plane
            _positions = new List<Vector3>();
            Vector3 currentPosition = new Vector3(bottomLeftCorner.x, bottomLeftCorner.y + _spawnYPoint, bottomLeftCorner.z);
            Debug.Log("ı will start at"+currentPosition+"And my edgespacing was " +edgeSpacing+" my size x"+size.x+" my size y"+size.z);
            for (int i = 0; i < numSpawnPointsZ; i++)
            {
                for (int j = 0; j < numSpawnPointsX; j++)
                {
                    _positions.Add(currentPosition);
                    currentPosition += new Vector3(xSpacing, 0f, 0f);
                }
                currentPosition = new Vector3(bottomLeftCorner.x, _spawnYPoint, currentPosition.z + zSpacing);
            }

            // Save positions to a file
            WritePositionsToTextFile();
            
        }


        void WritePositionsToTextFile()
        {
            string fileName = spawnPointObject.name;
            string filePath = "Assets/Rimaethon/GameData" + fileName + "SpawnPoints.txt";

            List<Vector3> positionsToRemove = new List<Vector3>();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Vector3 position in _positions)
                {
                    writer.WriteLine(position.x.ToString("0.0", CultureInfo.InvariantCulture) + "/" + _spawnYPoint.ToString("0.0", CultureInfo.InvariantCulture) + "/" + position.z.ToString("0.0", CultureInfo.InvariantCulture));
                    positionsToRemove.Add(position);
                }

                foreach (Vector3 positionToRemove in positionsToRemove)
                {
                    _positions.Remove(positionToRemove);
                }
            }
        }
    }

    [CustomEditor(typeof(SpawnPointGenerator))]
    public class SpawnPointGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Generate Spawn Points"))
            {
                SpawnPointGenerator spawnPointGenerator = (SpawnPointGenerator)target;
                spawnPointGenerator.GenerateSpawnPoints();
                Debug.Log("Spawn points generated and saved to file.");
            }
        }
    }
}
