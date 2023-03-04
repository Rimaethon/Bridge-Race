using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    
    
    public static Dictionary<Character,string > Characters = new Dictionary<Character,string>();

    public void RegisterCharacter(string id, Character character)
    {
    Characters[character] = id;
    }
}
