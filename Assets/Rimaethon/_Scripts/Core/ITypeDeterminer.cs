namespace Rimaethon._Scripts.Core
{
    public interface ITypeDeterminer
    {
        public enum ColorEnum
        {
            Cyan,
            Red,
            Green,
            White
        }

        ColorEnum ColorType { get; }
        
    }
}