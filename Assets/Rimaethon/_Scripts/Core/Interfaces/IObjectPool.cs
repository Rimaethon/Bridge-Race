using Rimaethon._Scripts.Core.Enums;
using UnityEngine;

namespace Rimaethon._Scripts.Core.Interfaces
{
    public interface IObjectPool
    {

        public void ReturnBrickToPool(GameObject brick);
        public GameObject GetBrickFromPool(ColorEnum colorType);

        public void HandleBrickDictionary(PooledObjectStatus status,GameObject brick);
    }
}
