using UnityEngine;

[CreateAssetMenu(fileName = "BoatData", menuName = "SO/BoatData")]
public class BoatSOsceipt : ScriptableObject
{
    [field: SerializeField] public bool IsAuto { get; private set; }
    
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float Deacceleration { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float RowPower { get; private set; }

}
