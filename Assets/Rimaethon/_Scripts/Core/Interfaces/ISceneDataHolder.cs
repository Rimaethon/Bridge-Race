using System.Collections.Generic;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Managers;

namespace Rimaethon._Scripts.Core.Interfaces
{
    public interface ISceneDataHolder
    {
        Dictionary<PlatformStates, List<ColorEnum>> CharactersTypesInLevels { get; }
        
        
    }
}