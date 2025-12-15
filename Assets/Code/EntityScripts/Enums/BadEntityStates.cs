using Unity.Behavior;

namespace Code.EntityScripts.Enums {
    [BlackboardEnum]
    public enum BadEntityStates {
        Idle,
        Patrolling,
        Chasing,
        Attacking,
        Hurt,
        Dead
    }
}