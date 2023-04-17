using System.Collections.Generic;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon._Scripts.Core
{
    public class MpbController : MonoBehaviour, ITypeDeterminer
    {
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;
        private MaterialPropertyBlock _tryout;
        [SerializeField] private ColorEnum colorType;
        private MeshRenderer _meshRenderer;
        private int colorID;

        public ColorEnum ColorType
        {
            get => colorType;
            set
            {
                colorType = value;
                SetObjectsColor(colorType);
            }
        }

        public static readonly Dictionary<ColorEnum, Color> ColorMap =
            new Dictionary<ColorEnum, Color>()
            {
                {ColorEnum.Red, Color.red},
                {ColorEnum.Green, Color.green},
                {ColorEnum.Cyan, Color.cyan},
                {ColorEnum.White, Color.white},
            };

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _materialPropertyBlock = new MaterialPropertyBlock();
            _meshRenderer = GetComponent<MeshRenderer>();
            colorID = Shader.PropertyToID("_Color");
        }

        public void SetObjectsColor(ColorEnum color)
        {

            colorType = color;
            _materialPropertyBlock.SetColor(colorID, ColorMap[color]);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
            Debug.Log("Object color set to " + color);
        }
        
    }
}
