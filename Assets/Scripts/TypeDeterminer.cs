using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TypeDeterminer: MonoBehaviour
{
    public enum ColorEnum { Cyan, Red, Green,White }
    
    [SerializeField]
    private ColorEnum colorType;

    public ColorEnum ColorType
    {
        get { return colorType; }
        set { colorType = value; }
    }
    
    
    
    
} 

