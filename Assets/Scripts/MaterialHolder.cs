using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHolder : MonoBehaviour
{
    public static MaterialHolder Instance;
    public  Material[] materials=new Material[3];
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance!=this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
