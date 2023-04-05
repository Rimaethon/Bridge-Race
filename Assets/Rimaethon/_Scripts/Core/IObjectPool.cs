using UnityEngine;

namespace Rimaethon._Scripts.Core
{
    public interface IObjectPool
    {

        public void ReturnBrickToPool(GameObject brick);
        public GameObject GetBrickFromPool(ITypeDeterminer.ColorEnum colorType);

        public void RemoveFromPooledBricks(ITypeDeterminer.ColorEnum color, GameObject brick,
            BrickStatus.PooledBrickStatus brickStatus);
    }
}
