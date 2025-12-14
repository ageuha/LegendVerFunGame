using Member.JJW.Code.Interface;
using Member.JJW.Code.TemperSystem;
using Member.KJW.Code.Player;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.JJW.Code.SO
{
    [CreateAssetMenu(fileName = "TemperatureItemDataSO", menuName = "SO/Item/TemperatureItemDataSO", order = 0)]
    public class TemperatureItemDataSO : ItemDataSO,IUsable
    {
        [field: SerializeField] public TemperatureSystem TemperatureSystem { get;private set; }
        [field: SerializeField] public float Temperature { get;private set; }
        public void Use(Player player)
        {
            if (player.TryGetComponent<TemperatureSystem>(out var temperatureSystem))
            {
                temperatureSystem.CurrentTemperature += Temperature;;
            }
        }
    }
}