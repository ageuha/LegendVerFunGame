using DG.Tweening;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private Transform credit;
    [SerializeField] private float speed;
    [SerializeField] private float limit;

    private bool m_End = false;

    

    private void Update()
    {
        if (m_End) return;
        
        credit.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

        if (credit.position.y >= limit) m_End = true;
    }
}
