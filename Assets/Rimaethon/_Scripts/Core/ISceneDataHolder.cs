using System.Collections.Generic;
using Rimaethon._Scripts.Managers;

namespace Rimaethon._Scripts.Core
{
    public interface ISceneDataHolder
    {
        Dictionary<IPlatformAble.PlatformStates, List<ITypeDeterminer.ColorEnum>> CharactersTypesInLevels { get; }
        
        
    }
}