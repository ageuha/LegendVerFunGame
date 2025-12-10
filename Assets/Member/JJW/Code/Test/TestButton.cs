using Member.JJW.Code.ResourceObject;
using Member.JJW.Code.TemperSystem;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    [SerializeField] private Resource  resource;
    [SerializeField] private TemperatureSystem temperatureSystem;

    public void Test1()
    {
        temperatureSystem.CurrentTemperature += Random.Range(-0.1f, 0.1f);
    }

    public void Test2()
    {
        resource.CurrentHp.ApplyDamage(10);
    }
}
