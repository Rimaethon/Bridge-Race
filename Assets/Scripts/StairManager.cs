using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    private GameObject brickPooler;
    private BrickStacker _brickStacker;
    private ObjectPooler _objectPooler;
    private int _stairID=99;
    private Material _playerBrickMaterial;
    private Character _character;
    private Material _stairMaterial;
    [SerializeField] private GameObject door;
    [SerializeField] private bool IsLastStair;

    void Start()
    {
        brickPooler = GameObject.FindWithTag("Brick Pooler");
        _objectPooler = brickPooler.GetComponent<ObjectPooler>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _brickStacker = other.gameObject.GetComponent<BrickStacker>();
            _character = other.gameObject.GetComponent<Character>();
            _stairMaterial = gameObject.GetComponent<MeshRenderer>().material;
            if (_stairID != _character.CharacterID && _brickStacker._brickCount > 0)
            {
                // optimization or dependency ? 
                
                ChangeMaterial(MaterialHolder.Instance.materials[_character.CharacterID]);
                if (IsLastStair)
                {
                    door.GetComponent<DoorOpener>().OpenDoors();
                }
                _stairID = _character.CharacterID;
                other.gameObject.GetComponent<CharacterController>().stepOffset = 0.35f;

            }
            else if (_stairID == _character.CharacterID)
            {
                other.gameObject.GetComponent<CharacterController>().stepOffset = 0.35f;
                
            }
            else
            {
                other.gameObject.GetComponent<CharacterController>().stepOffset = 0.1f;
            }
            

            
            
            
        }

        
    }

    private void ChangeMaterial(Material brickMaterial)
    {
        
        
        gameObject.GetComponent<MeshRenderer>().material = brickMaterial;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        GameObject brickToRemove = _brickStacker.bricksOnPlayer[_brickStacker._brickCount - 1];
        brickToRemove.transform.parent = null;
        _objectPooler.ReturnEnemyPool(brickToRemove);
        _brickStacker.bricksOnPlayer.RemoveAt(_brickStacker._brickCount - 1);
        _brickStacker._brickCount--;
    }
    
}
