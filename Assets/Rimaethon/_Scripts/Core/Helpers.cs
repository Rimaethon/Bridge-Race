using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Rimaethon._Scripts.Core
{
    public static class Helpers
    {
        /// <summary>
        /// This script is a helper script for general use cases
        /// </summary>

        private static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }
        
       private static readonly Dictionary<float,WaitForSeconds> WaitDictionary=new Dictionary<float, WaitForSeconds>();

       public static WaitForSeconds GetWait(float time)
       {
           if (WaitDictionary.TryGetValue(time, out var wait)) return wait;
           WaitDictionary[time] = new WaitForSeconds(time);
           return WaitDictionary[time];

       }


       private static PointerEventData _eventDataCurrentPosition;
       private static List<RaycastResult> _results;

       public static bool IsOverUI()
       {
           _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
           _results = new List<RaycastResult>();
           EventSystem.current.RaycastAll(_eventDataCurrentPosition,_results);
           return _results.Count > 0;
       }
    
       public static T PickRandomFromList<T>(List<T> list)
       {
           if (list == null || list.Count == 0)
           {
               throw new ArgumentException("The list cannot be null or empty.");
           }

           
           return list[GiveRandomNumber(list.Count)];
       }

       public static int GiveRandomNumber(int positiveRange, int negativeRange = 0)
       {
           return Random.Range(negativeRange, positiveRange);
       }

    }
}
