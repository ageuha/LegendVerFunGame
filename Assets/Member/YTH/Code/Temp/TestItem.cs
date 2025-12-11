using System.Diagnostics.Contracts;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

public class TestItem : MonoBehaviour
{
    [SerializeField] private ItemDataSO itemDataSO1;
    [SerializeField] private ItemDataSO itemDataSO2;

    [ContextMenu("Test")]
    private void Test()
    {
        Logging.Log($"{itemDataSO1}");
        Logging.Log($"{itemDataSO2}");
        Logging.Log($"{itemDataSO1 == itemDataSO2}");
    }
}
