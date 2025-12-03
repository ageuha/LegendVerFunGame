using System.Collections;
using Code.Core.Utility;
using DG.Tweening;
using Member.KJW.Code.Player;
using UnityEngine;
using YTH.Code.Interface;

namespace YTH.Code.Item
{
    public class ItemObject : MonoBehaviour, IPickable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ItemDataSO itemData;

    private bool m_isPickUp;
    private Transform m_target;
    private Coroutine m_followRoutine;
    private const float speed = 5;
    private const float minDistance = 0.01f;

    private void OnValidate()
    {
        if (itemData == null || spriteRenderer == null) return;

        spriteRenderer.sprite = itemData.Icon;
        gameObject.name = $"ItemObject_{itemData.ItemName}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isPickUp) return;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            m_target = player.transform;
            StartFollow();
        }
    }

    private void StartFollow()
    {
        if (m_followRoutine != null)
            StopCoroutine(m_followRoutine);

        m_followRoutine = StartCoroutine(FollowTargetCoroutine());
    }

    private IEnumerator FollowTargetCoroutine()
    {
        while (!m_isPickUp && m_target != null)
        {
            Vector3 targetPos = m_target.position;
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            float distance = Vector3.Distance(transform.position, targetPos);
            float scale = Mathf.Clamp01(distance);
            transform.localScale = new Vector3(scale, scale, 1);

            if (distance < minDistance)
            {
                PickUp();
                yield break;
            }

            yield return null;
        }
    }

    public void SetItemData(ItemDataSO newData)
    {
        itemData = newData;
        spriteRenderer.sprite = itemData.Icon;
        gameObject.name = $"ItemObject_{itemData.ItemName}";
    }

    public void PickUp()
    {
        Logging.Log($"Picked up item: {itemData.ItemName}");
        Destroy(gameObject);

    }
}

}
