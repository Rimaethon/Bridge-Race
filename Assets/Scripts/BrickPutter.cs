using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPutter : MonoBehaviour
{
    
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    private BrickStacker _brickStacker;
 
    void Start()
    {
        _brickStacker = gameObject.GetComponent<BrickStacker>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Renderer>();
    }


    
    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        //other.GetComponent<Renderer>();
        if (other.gameObject.CompareTag("Stair") && other.gameObject.GetComponent<MeshRenderer>().enabled==false && _brickStacker._brickCount> 0)
        {
            Debug.Log("Im working");
            if (gameObject.GetComponent<Character>().ID== 0)
            {
                Debug.Log("renderer is not working");
                other.gameObject.GetComponent<Renderer>().material =greenMaterial;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }else if (gameObject.GetComponent<Character>().ID == 1)
            {
                other.gameObject.GetComponent<Renderer>().material = redMaterial;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }else if (gameObject.GetComponent<Character>().ID== 2)
            {
                other.gameObject.GetComponent<Renderer>().material = blueMaterial;
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

            GameObject brickToRemove = _brickStacker.bricksOnPlayer[_brickStacker._brickCount - 1];
            _brickStacker.bricksOnPlayer.RemoveAt(_brickStacker._brickCount - 1);
            _brickStacker._brickCount--;
            Destroy(brickToRemove);
            Debug.Log(_brickStacker._brickCount);
        
            
        }
    }
}
