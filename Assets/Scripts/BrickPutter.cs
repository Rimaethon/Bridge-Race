using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickPutter : MonoBehaviour
{
    
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    private BrickStacker _brickStacker;
    private ObjectPooler _objectPooler;
    public GameObject brickPooler;
 
    void Start()
    {
        _brickStacker = gameObject.GetComponent<BrickStacker>();
        _objectPooler = brickPooler.GetComponent<ObjectPooler>();
    }





    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Renderer>();
        if (other.gameObject.CompareTag("Stair") && other.gameObject.GetComponent<MeshRenderer>().enabled == false &&
            _brickStacker._brickCount > 0)
        {
            Debug.Log("Im working");
            
            if (gameObject.GetComponent<Character>().ID == 0)
            {
                Debug.Log("renderer is not working");
                other.gameObject.GetComponent<Renderer>().material = blueMaterial;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;

                other.gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            }
            else if (gameObject.GetComponent<Character>().ID == 1)
            {
                other.gameObject.GetComponent<Renderer>().material = redMaterial;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            }
            else if (gameObject.GetComponent<Character>().ID == 2)
            {
                other.gameObject.GetComponent<Renderer>().material = greenMaterial;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
            }

            GameObject brickToRemove = _brickStacker.bricksOnPlayer[_brickStacker._brickCount - 1];
            _brickStacker.bricksOnPlayer.RemoveAt(_brickStacker._brickCount - 1);
            _brickStacker._brickCount--;
            brickToRemove.transform.parent = null;
            _objectPooler.ReturnEnemyPool(brickToRemove);
            Debug.Log(_brickStacker._brickCount);



        }

        if (_brickStacker._brickCount>0 || (other.gameObject.CompareTag("Stair") && other.gameObject.GetComponent<MeshRenderer>().enabled==true) )
        {
            gameObject.GetComponent<CharacterController>().stepOffset = 0.35f;
        }
        else
        {
            gameObject.GetComponent<CharacterController>().stepOffset = 0.1f;
        }

        
    }

    private void Update()
    {
        if (_brickStacker._brickCount>0)
        {
            
        }
    }
}
