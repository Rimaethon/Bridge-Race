using Rimaethon._Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rimaethon._Scripts.Core
{
    public  class AI : Character
    {
        public AIStates currentAIState;
        private int _brickCountOfAI;
        private int _maxBrickCount ;
        private IBrickCountProvider _brickCountProvider;


        private void Start()
        {
            _brickCountProvider = GetComponent<IBrickCountProvider>();

            _maxBrickCount = Helpers.GiveRandomNumber(7);
            currentAIState = AIStates.Collect;
      
        }

        
        
        
        
        
       


     
    }
}


