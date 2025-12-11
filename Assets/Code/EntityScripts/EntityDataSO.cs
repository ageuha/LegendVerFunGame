using System;
using System.Collections.Generic;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Code.EntityScripts {
    [CreateAssetMenu(fileName = "new EntityData", menuName = "SO/EntityData", order = 0)]
    public class EntityDataSO : ScriptableObject {
        [field: SerializeField] public float MaxHp { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public List<EntityDropData> DropTable { get; private set; }
    }

    [Serializable]
    public struct EntityDropData {
        [field: SerializeField] public ItemDataSO Item { get; private set; }
        [field: SerializeField] public byte Count { get; private set; }
        [field: SerializeField] public float DropRate { get; private set; }
    }
}