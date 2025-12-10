namespace Member.KJW.Code.CombatSystem
{
    public struct DamageInfo
    {
        public float Damage;
        public float Knockback;

        public DamageInfo(float damage, float knockback)
        {
            Damage = damage;
            Knockback = knockback;
        }
    }
}