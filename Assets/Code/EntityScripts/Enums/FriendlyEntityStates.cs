using Unity.Behavior;

namespace Code.EntityScripts.Enums {
    [BlackboardEnum]
    public enum FriendlyEntityStates {
        Idle,
        Patrolling,
        Fleeing,
        Hurt,
        Dead
    }
}