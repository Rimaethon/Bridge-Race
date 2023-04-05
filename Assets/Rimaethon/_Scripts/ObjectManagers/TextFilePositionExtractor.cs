using System.Collections.Generic;
using System.IO;
using AYellowpaper.SerializedCollections;
using Rimaethon._Scripts.Core;
using UnityEngine;

namespace Rimaethon._Scripts.ObjectManagers
{
    public class TextFilePositionExtractor : MonoBehaviour
    {
        [SerializedDictionary("The Platform Name", "Text File Of Spawn Points")]
        public SerializedDictionary<IPlatformAble.PlatformStates, TextAsset> spawnPointsTextFiles;
    

        public Dictionary<IPlatformAble.PlatformStates, List<Vector3>> GetBrickSpawnPoints()
        {
            Dictionary<IPlatformAble.PlatformStates, List<Vector3>> brickSpawnPoints = new Dictionary<IPlatformAble.PlatformStates, List<Vector3>>();
    
            foreach (KeyValuePair<IPlatformAble.PlatformStates, TextAsset> pair in spawnPointsTextFiles)
            {
                List<Vector3> positions = new List<Vector3>();
                string textData = pair.Value.text;
        
                using (StringReader reader = new StringReader(textData))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] coordinates = line.Split('/');

                        if (coordinates.Length != 3)
                        {
                            Debug.LogError("Invalid number of coordinates in line: " + line);
                            continue;
                        }

                        // Try parsing the coordinates into floats
                        float x, y, z;
                        if (!float.TryParse(coordinates[0], out x) || !float.TryParse(coordinates[1], out y) ||
                            !float.TryParse(coordinates[2], out z))
                        {
                            Debug.LogError("Error parsing coordinates in line: " + line);
                            continue;
                        }

                        positions.Add(new Vector3(x, y, z));
                    }
                }

                brickSpawnPoints[pair.Key] = positions;
            }

            return brickSpawnPoints;
        }



    }
}

