using DG.Tweening;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro.transform.DOMove(new Vector3(0, 40, 41), 20f);
    }
}
