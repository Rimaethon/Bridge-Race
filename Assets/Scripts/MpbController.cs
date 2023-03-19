using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpbController : TypeDeterminer
{
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    public static readonly Dictionary<ColorEnum, Color> ColorMap = new Dictionary<ColorEnum, Color>()
    {
        { ColorEnum.Red, Color.red },
        { ColorEnum.Green, Color.green },
        { ColorEnum.Cyan, Color.cyan},
        { ColorEnum.White, Color.white},
    };

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        
    }

    public void SetObjectsColor(ColorEnum color)
    {
        _materialPropertyBlock.SetColor("_Color", ColorMap[color]);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
        if (!gameObject.GetComponent<MeshRenderer>().enabled)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}


