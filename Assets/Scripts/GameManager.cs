/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    
    public static List<Transform>[] AITargets = new List<Transform>[3];

    private void OnEnable()
    {
        EventManager.AddHandler<GameDataHolder.ColorEnum>(GameEvent.OnCollectingBrick, BrickCollected);
        EventManager.AddHandler<GameObject,GameDataHolder.ColorEnum>(GameEvent.OnPuttingBrick,TryToPutBrickToStair);
    }

    private void Awake()
    {
        // Find all objects with "character" tag
        
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Character")) 
        {
                // Check if object has a TypeDeterminer component
                TypeDeterminer typeDeterminer = character.GetComponent<TypeDeterminer>();
                if (typeDeterminer != null) 
                {
                    
                    GameDataHolder.CharactersInScene[character] = typeDeterminer.ColorType;
                    
                }
        }
            

    }
    
    


    private void BrickCollected(GameDataHolder.ColorEnum characterColorType)
    {
        GameDataHolder.PlayerScore += 5;
        Debug.Log("brickcollected event is working");


    }

    private void TryToPutBrickToStair(GameObject stair, GameDataHolder.ColorEnum characterColorType)
    {
        if (GameDataHolder.bricksOnCharacters[characterColorType].Count > 0)
        {
            // Remove the last brick from the character's list and return it to the object pool
            int lastIndex = GameDataHolder.bricksOnCharacters[characterColorType].Count - 1;
            GameObject brick = GameDataHolder.bricksOnCharacters[characterColorType][lastIndex];
            GameDataHolder.bricksOnCharacters[characterColorType].RemoveAt(lastIndex);
            brick.transform.parent = null;
           // gameObject.GetComponent<ObjectPooler>().ReturnEnemyPool(brick);

            stair.GetComponent<MpbController>().SetObjectsColor(GameDataHolder.ColorMap[characterColorType]);
            stair.GetComponent<TypeDeterminer>().ColorType = characterColorType;
            GameDataHolder.characterStepOffsets[characterColorType] = 0.35f;
        }
        else
        {
            GameDataHolder.characterStepOffsets[characterColorType] = 0.12f;
        }
    }

}
*/
