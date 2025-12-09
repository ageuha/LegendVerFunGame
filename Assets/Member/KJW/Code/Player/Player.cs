using Code.EntityScripts;
using Code.GridSystem.Objects;
using Member.BJH._01Script.Interact;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.Data;
using Member.KJW.Code.Input;
using Member.YDW.Script;
using UnityEngine;
using UnityEngine.EventSystems;
using YTH.Code.Inventory;
using YTH.Code.Item;

namespace Member.KJW.Code.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public RollingData RollingData { get; private set; }
        [SerializeField] private float maxHp;
        
        
        public AgentMovement MoveCompo { get; private set; }
        public HealthSystem HealthCompo { get; private set; }
        public Interactor Interactor { get; private set; }
        public Thrower Thrower { get; private set; }
        public Arm Arm { get; private set; }
        public Weapon Weapon { get; private set; }
        public InventoryManager Inventory { get; private set; }
        
        public bool IsRolling { get; private set; }
        private bool _isInvincible;
        
        public Vector2 StandDir { get; private set; } = Vector2.right;
        public Vector2 MouseWorldPos => Camera.main!.ScreenToWorldPoint(InputReader.MousePos);
        
        private float _coolTimer;
        private int _remainRoll;
        public int RemainRoll
        {
            get => _remainRoll;
            private set => _remainRoll = Mathf.Clamp(value, 0, RollingData.MaxRoll);
        }
        
        private void Awake()
        {
            MoveCompo = GetComponentInChildren<AgentMovement>();
            HealthCompo = GetComponentInChildren<HealthSystem>();
            Interactor = GetComponentInChildren<Interactor>();
            Thrower = GetComponentInChildren<Thrower>();
            Arm = GetComponentInChildren<Arm>(true);
            Weapon = GetComponentInChildren<Weapon>(true);
            Inventory = GetComponentInChildren<InventoryManager>(true);

            RemainRoll = RollingData.MaxRoll;
            HealthCompo.Initialize(maxHp);
        }

        private void OnEnable()
        {
            InputReader.OnInteracted += Interactor.Interact;
            InputReader.OnRolled += Roll;
            InputReader.OnMoved += UpdateStandDir;
            InputReader.OnThrew += Throw;
            InputReader.OnAttacked += Click;
        }

        private void Update()
        {
            if (RemainRoll == RollingData.MaxRoll) return;

            if (_coolTimer >= RollingData.StackCoolTime)
            {
                _coolTimer -= RollingData.StackCoolTime;
                ++RemainRoll;
            }

            _coolTimer += Time.deltaTime;
        }

        private void OnDisable()
        {
            InputReader.OnInteracted -= Interactor.Interact;
            InputReader.OnRolled -= Roll;
            InputReader.OnMoved -= UpdateStandDir;
            InputReader.OnThrew -= Throw;
            InputReader.OnAttacked -= Click;
        }

        private void Click()
        {
            ItemDataSO curItem = Inventory.GetSelectedItem();
            
            if (curItem == null || EventSystem.current.IsPointerOverGameObject()) return;
            
            if (curItem is WeaponDataSO weaponData)
            {
                Attack(weaponData);
                return;
            }

            Break(curItem);
        }

        private void Place(ItemDataSO item)
        {
            
        }

        private void Break(ItemDataSO item)
        {
            
        }

        private void Attack(WeaponDataSO weaponData)
        {
            Vector2 dir = MouseWorldPos - (Vector2)transform.position;
            Weapon.Init(weaponData);
            Arm.Init(Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)).Swing();
        }

        private void Throw()
        {
            ItemDataSO curItem = Inventory.GetSelectedItem();
            if (curItem == null) return;
            
            Thrower.Throw(curItem.DamageInfoData.ToStruct(), curItem.Icon, MouseWorldPos - (Vector2)transform.position, curItem.ThrowSpeed);
            Inventory.UseSelectedItem();
        }

        private void UpdateStandDir(Vector2 dir)
        {
            if (dir != Vector2.zero) StandDir = dir;
        }

        private void Roll()
        {
            if (RemainRoll == 0 || IsRolling || MoveCompo.IsStop) return;

            --RemainRoll;
            IsRolling = true;
            _isInvincible = true;
        }

        public void EndRoll()
        {
            IsRolling = false;
            _isInvincible = false;
            MoveCompo.RestartMove();
        }

        public void GetDamage(DamageInfo damageInfo)
        {
            if (_isInvincible) return;
            HealthCompo.ApplyDamage(damageInfo.Damage);
        }
    }
}