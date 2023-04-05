using System.Collections.Generic;
using Rimaethon._Scripts.Core;
using Rimaethon._Scripts.Managers;
using UnityEngine;


    public  interface IBrickSpawnPointProvider
    {
        Dictionary<IPlatformAble.PlatformStates, List<Vector3>> GetBrickSpawnPoints();
    }
