using System.Collections.Generic;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon._Scripts.Core.Interfaces;
using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon._Scripts.Core
{
    public class MpbController : MonoBehaviour, ITypeDeterminer,IPoolAble
    {
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;
        private MaterialPropertyBlock _tryout;
        [SerializeField] private ColorEnum colorType;
        [SerializeField] private PooledObjectStatus objectStatus;
        
        private MeshRenderer _meshRenderer;
        private int colorID;


        public PooledObjectStatus ObjectStatus
        {
            get =>objectStatus;
            set => objectStatus = value;
        } 
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
        }

    }
}
