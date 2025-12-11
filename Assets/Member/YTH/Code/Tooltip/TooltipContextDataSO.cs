using UnityEngine;

[CreateAssetMenu(fileName = "TooltipContextDataSO", menuName = "SO/TooltipContextDataSO")]
public class TooltipContextDataSO : ScriptableObject
{
    [field:SerializeField] public Vector2 Offset { get; private set; }
    [field:SerializeField] public Color TitleColor { get; private set; }
    [field:SerializeField] public Color DescriptionColor { get; private set; }
    [field:SerializeField] public Color BackgroundColor { get; private set; }
    [field:SerializeField] public Color BorderColor { get; private set; }
    [field:SerializeField] public Color OutlineColor { get; private set; }
}
