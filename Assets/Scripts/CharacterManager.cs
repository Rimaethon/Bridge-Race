using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    
    
    public static Dictionary<Character,int > Characters = new Dictionary<Character,int>();

    public void RegisterCharacter(int id, Character character)
    {
    Characters[character] = id;
    }
}
