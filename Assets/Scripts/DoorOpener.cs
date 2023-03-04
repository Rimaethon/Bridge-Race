using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Transform _leftDoorTransform;
    private Transform _rightDoorTransform;
    private void Start()
    {
        _leftDoorTransform = gameObject.transform.Find("Left Door");
        _rightDoorTransform = gameObject.transform.Find("Right Door");
    }
    
    

    public void OpenDoors()
    {
        _rightDoorTransform.DOMoveX(2.25f, 2);
        _leftDoorTransform.DOMoveX(-2.25f, 2);
    }
}
