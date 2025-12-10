using System;
using Code.Core.GlobalStructs;
using Code.Core.Utility;
using Code.EntityScripts;
using Code.GridSystem.Objects;
using Member.BJH._01Script.Interact;
using Member.JJW.Code.Interface;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.Data;
using Member.KJW.Code.EventChannel;
using Member.KJW.Code.Input;
using Member.YDW.Script;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.NewBuildingSystem;
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
        
        
        public AgentMovement MoveCompo { get; private set; }
        public HealthSystem HealthCompo { get; private set; }
        public Interactor Interactor { get; private set; }
        public Thrower Thrower { get; private set; }
        public Arm Arm { get; private set; }
        public Weapon Weapon { get; private set; }
        
        [field: SerializeField] public InventoryManagerEventChannel InventoryChannel { get; private set; }
        [field: SerializeField] public InventorySelectedSlotChangeEventChannel InventorySelectedSlotChangeChannel { get; private set; }
        [field: SerializeField] public BuildingGhostEventSO BuildingGhostChannel { get; private set; }
        [SerializeField] private BuildingGhostFlagEventChannel buildingGhostFlagEventChannel;
        
        public bool IsRolling { get; private set; }
        private bool _isInvincible;
        private bool _isBuilding;
        
        public Vector2 StandDir { get; private set; } = Vector2.right;
        public Vector2 MouseWorldPos => Camera.main!.ScreenToWorldPoint(InputReader.MousePos);
        
        private float _coolTimer;
        private int _remainRoll;
        public int RemainRoll
        {
            get => _remainRoll;
            private set => _remainRoll = Mathf.Clamp(value, 0, RollingData.MaxRoll);
        }
        private ItemDataSO CurItem => _inventoryManager.GetSelectedItem();
        private InventoryManager _inventoryManager;
        
        [SerializeField] private float maxHp;
        
        private void Awake()
        {
            MoveCompo = GetComponentInChildren<AgentMovement>();
            HealthCompo = GetComponentInChildren<HealthSystem>();
            Interactor = GetComponentInChildren<Interactor>();
            Thrower = GetComponentInChildren<Thrower>();
            Arm = GetComponentInChildren<Arm>(true);
            Weapon = GetComponentInChildren<Weapon>(true);

            RemainRoll = RollingData.MaxRoll;
            HealthCompo.Initialize(maxHp);
            
            InventoryChannel.OnEvent += InitInventory;
            InventorySelectedSlotChangeChannel.OnEvent += CancelPlace;
            buildingGhostFlagEventChannel.OnEvent += SetIsBuilding;
        }

        private void OnEnable()
        {
            InputReader.OnInteracted += Interactor.Interact;
            InputReader.OnRolled += Roll;
            InputReader.OnMoved += UpdateStandDir;
            InputReader.OnThrew += Throw;
            InputReader.OnAttacked += Click;
        }

        private void SetIsBuilding(bool value)
        {
            _isBuilding = value;
        }

        private void CancelPlace(Empty e)
        {
            BuildingGhostChannel.Raise(new BuildingGhostEvent(null, false));
        }

        private void InitInventory(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
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

        private void OnDestroy()
        {
            InventoryChannel.OnEvent -= InitInventory;
        }
        
        private void Click()
        {
            if (CurItem == null || EventSystem.current.IsPointerOverGameObject()) return;
            
            if (CurItem is WeaponDataSO weaponData)
            {
                Attack(weaponData);
                return;
            }
            
            if (CurItem is PlaceableItemData placeableItemData)
            {
                BuildingGhostChannel.Raise(new BuildingGhostEvent(placeableItemData.BuildingData, true));
                return;
            }

            Break();
        }

        private void Break()
        {
            
        }

        private void RClick()
        {
            if (CurItem == null || _isBuilding || EventSystem.current.IsPointerOverGameObject()) return;
            
            
        }

        private void Place(PlaceableItemData placeableItemData)
        {
            
        }

        private void Attack(WeaponDataSO weaponData)
        {
            Vector2 dir = MouseWorldPos - (Vector2)transform.position;
            Weapon.Init(weaponData);
            Arm.Init(Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x), weaponData.AttackData.AttackSpeed).Swing();
        }

        private void Throw()
        {
            if (CurItem == null || CurItem is not WeaponDataSO w) return;
            
            Thrower.Throw(w, MouseWorldPos - (Vector2)transform.position);
            _inventoryManager.UseSelectedItem();
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