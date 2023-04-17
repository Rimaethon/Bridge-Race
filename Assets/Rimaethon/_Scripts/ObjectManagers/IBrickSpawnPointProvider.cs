using System.Collections.Generic;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.ObjectManagers
{
    public  interface IBrickSpawnPointProvider
    {
        Dictionary<PlatformStates, List<Vector3>> GetBrickSpawnPoints();
    }
}
