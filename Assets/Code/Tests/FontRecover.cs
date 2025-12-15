using TMPro;
using UnityEngine;

public class FontRecover : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset fontAsset;
    
    
    [ContextMenu("Recover")]
    public void Recover() {
        var texts = FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var text in texts) {
            text.font = fontAsset;
        }
    }
}
