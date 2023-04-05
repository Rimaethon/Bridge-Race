using System.Collections.Generic;
using UnityEngine;

namespace Rimaethon._Scripts.Core
{
    public class MpbController : MonoBehaviour, ITypeDeterminer
    {
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;
        private MaterialPropertyBlock _tryout;
        [SerializeField] private ITypeDeterminer.ColorEnum colorType;
        private MeshRenderer _meshRenderer;
        private int colorID;

        public ITypeDeterminer.ColorEnum ColorType
        {
            get => colorType;
            set
            {
                colorType = value;
                SetObjectsColor(colorType);
            }
        }

        public static readonly Dictionary<ITypeDeterminer.ColorEnum, Color> ColorMap =
            new Dictionary<ITypeDeterminer.ColorEnum, Color>()
            {
                {ITypeDeterminer.ColorEnum.Red, Color.red},
                {ITypeDeterminer.ColorEnum.Green, Color.green},
                {ITypeDeterminer.ColorEnum.Cyan, Color.cyan},
                {ITypeDeterminer.ColorEnum.White, Color.white},
            };

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _materialPropertyBlock = new MaterialPropertyBlock();
            _meshRenderer = GetComponent<MeshRenderer>();
            colorID = Shader.PropertyToID("_Color");
        }

        public void SetObjectsColor(ITypeDeterminer.ColorEnum color)
        {

            colorType = color;
            _materialPropertyBlock.SetColor(colorID, ColorMap[color]);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
            Debug.Log("Object color set to " + color);
        }
        
    }
}
