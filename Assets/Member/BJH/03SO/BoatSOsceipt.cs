using UnityEngine;

[CreateAssetMenu(fileName = "BoatSOsceipt", menuName = "Scriptable Objects/BoatSOsceipt")]
public class BoatSOsceipt : ScriptableObject
{
    [field: SerializeField] public bool IsAuto { get; private set; }
    
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Deacceleration { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }

}
