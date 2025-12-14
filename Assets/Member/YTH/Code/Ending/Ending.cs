using DG.Tweening;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private float speed;

    private void Update()
    {
        textMeshPro.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}
