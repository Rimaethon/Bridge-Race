using System;
using System.Collections.Generic;
using System.IO;
using AYellowpaper.SerializedCollections;
using Rimaethon._Scripts.Core;  
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.Editor 
{
    public class TextFilePositionExtractor : MonoBehaviour
    {
        [SerializedDictionary("The Platform Name", "Text File Of Spawn Points")]
        public  SerializedDictionary<PlatformStates, TextAsset> spawnPointsTextFiles;

        

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<Dictionary<PlatformStates,List<Vector3>>>(GameStates.OnBeforeGameStart, GetBrickSpawnPoints);

        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<Dictionary<PlatformStates,List<Vector3>>>(GameStates.OnBeforeGameStart, GetBrickSpawnPoints);

        }


        public void GetBrickSpawnPoints(Dictionary<PlatformStates,List<Vector3>> spawnPoints)
        {
    
            Debug.Log("yeah i populated the points");
            foreach (KeyValuePair<PlatformStates, TextAsset> pair in spawnPointsTextFiles)
            {
                string textData = pair.Value.text;
                spawnPoints[pair.Key] = new List<Vector3>();
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
                        spawnPoints[pair.Key].Add(new Vector3(x, y, z));
                    }
                }

                
            }
            
            GameManager.instance.UpdateGameState(GameStates.OnGameStart);
            
        }



    }
}

