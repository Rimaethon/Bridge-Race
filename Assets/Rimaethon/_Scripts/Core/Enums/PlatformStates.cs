namespace Rimaethon._Scripts.Managers
{

    public enum PlatformStates
    {

        Default=0,
        StartingPlatform,
        Platform1,
        Platform2,
        Platform3,
        EndingPlatform

    }
    
    public static class PlatformStateExtensions {
        public static PlatformStates GetNextState(this PlatformStates state) {
            switch (state) {
                case PlatformStates.StartingPlatform:
                    return PlatformStates.Platform1;
                case PlatformStates.Platform1:
                    return PlatformStates.Platform2;
                case PlatformStates.Platform2:
                    return PlatformStates.Platform3;
                case PlatformStates.Platform3:
                    return PlatformStates.EndingPlatform;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}

