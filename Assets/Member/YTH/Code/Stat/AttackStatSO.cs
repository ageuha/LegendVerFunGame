using Code.Core.Utility;
using DG.Tweening.Plugins;
using KJW.Code.Player;
using UnityEngine;

namespace YTH.Code.Stat
{
    [CreateAssetMenu(fileName = "AttackStatSO", menuName = "SO/Stat/Attack")]
    public class AttackStatSO : StatBaseSO
    {
        protected override string StatName => "공격력";
        public override void Effect(int amount, Player player)
        {
            Logging.Log($"{StatName} {amount}");
        }
    }
}
