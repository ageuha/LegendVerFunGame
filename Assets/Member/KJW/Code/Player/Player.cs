using System;
using Code.Core.GlobalSO;
using Code.Core.GlobalStructs;
using Code.Core.Utility;
using Code.EntityScripts;
using Code.GridSystem.Objects;
using Member.BJH._01Script.Interact;
using Member.JJW.Code.Interface;
using Member.JJW.Code.ResourceObject;
using Member.JJW.EventChannel;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.CombatSystem.DamageSystem;
using Member.KJW.Code.Data;
using Member.KJW.Code.EventChannel;
using Member.KJW.Code.Input;
using Member.YDW.Script;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.NewBuildingSystem;
using Member.YTH.Code.Item;
using UnityEngine;
using UnityEngine.EventSystems;
using YTH.Code.Inventory;

namespace Member.KJW.Code.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public RollingData RollingData { get; private set; }
        
        
        public AgentMovement MoveCompo { get; private set; }
        public HealthSystem HealthCompo { get; private set; }
        public Interactor Interactor { get; private set; }
        public PlayerRenderer PlayerRenderer { get; private set; }
        public Thrower Thrower { get; private set; }
        public Arm Arm { get; private set; }
        public Weapon Weapon { get; private set; }
        
        [Header("Event Channel")]
        [SerializeField] private InventoryManagerEventChannel inventoryChannel;
        [SerializeField] private InventorySelectedSlotChangeEventChannel inventorySelectedSlotChangeChannel;
        [SerializeField] private BuildingGhostEventSO buildingGhostChannel;
        [SerializeField] private BuildingGhostFlagEventChannel buildingGhostFlagEventChannel;
        [SerializeField] private CraftingInteractEventChannel craftingInteractEventChannel;
        [SerializeField] private BreakingFlagEventChannel breakingFlagEventChannel;
        [SerializeField] private FloatEventChannel onVeloctyXChangeChannel;
        [SerializeField] private Vector2EventChannel onVeloctyChangeChannel;

        
        public bool IsRolling { get; private set; }
        private bool _isInvincible;
        private bool _isBuilding;
        private bool _isBreaking;
        
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
        
        [Header("Settings")]
        [SerializeField] private float maxHp;
        [SerializeField] private HashSO moveBoolHash;
        [SerializeField] private HashSO attackHash;
        [SerializeField] private HashSO vxHash;
        [SerializeField] private HashSO vyHash;
        
        private void Awake()
        {
            MoveCompo = GetComponentInChildren<AgentMovement>();
            HealthCompo = GetComponentInChildren<HealthSystem>();
            Interactor = GetComponentInChildren<Interactor>();
            PlayerRenderer = GetComponentInChildren<PlayerRenderer>();
            Thrower = GetComponentInChildren<Thrower>();
            Arm = GetComponentInChildren<Arm>(true);
            Weapon = GetComponentInChildren<Weapon>(true);

            RemainRoll = RollingData.MaxRoll;
            HealthCompo.Initialize(maxHp);
            
            inventoryChannel.OnEvent += InitInventory;
            inventorySelectedSlotChangeChannel.OnEvent += CancelPlace;
            buildingGhostFlagEventChannel.OnEvent += SetIsBuilding;
        }

        private void OnEnable()
        {
            InputReader.OnInteracted += Interactor.Interact;
            InputReader.OnRolled += Roll;
            InputReader.OnMoved += UpdateStandDir;
            InputReader.OnThrew += Throw;
            InputReader.OnAttacked += Click;
            InputReader.OnAttackReleased += StopBreak;
            InputReader.OnPlaced += RClick;
            
            onVeloctyXChangeChannel.OnEvent += SetFlip;
            onVeloctyChangeChannel.OnEvent += SetMoveAnim;
        }

        private void SetMoveAnim(Vector2 moveDir)
        {
            PlayerRenderer.SetValue(moveBoolHash, moveDir.sqrMagnitude > 0);
            PlayerRenderer.SetValue(vxHash, StandDir.x);
            PlayerRenderer.SetValue(vyHash, StandDir.y);
        }

        private void SetFlip(float value)
        {
            PlayerRenderer.SetFlip(StandDir.x);
        }

        private void SetIsBuilding(bool value)
        {
            _isBuilding = value;
        }

        private void CancelPlace(Empty e)
        {
            buildingGhostChannel.Raise(new BuildingGhostEvent(null, false));
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
            InputReader.OnAttackReleased -= StopBreak;
            InputReader.OnPlaced -= RClick;
        }

        private void OnDestroy()
        {
            inventoryChannel.OnEvent -= InitInventory;
        }
        
        private void Click()
        {
            if (_inventoryManager.UIOpen) return;
            // Logging.LogError("ERROR");
            if (CurItem == null)
            {
                Break();
                return;
            }
            
            if (CurItem is WeaponDataSO weaponData)
            {
                Attack(weaponData);
                return;
            }
            
            if (CurItem is PlaceableItemData placeableItemData)
            {
                buildingGhostChannel.Raise(new BuildingGhostEvent(placeableItemData.BuildingData, true));
                return;
            }
            
            Break();
        }

        private void Break()
        {
            if (_isBuilding) return;

            GridObject gridObj = GridManager.Instance.GridMap.GetObjectsAt(Vector2Int.RoundToInt(MouseWorldPos - new Vector2(0.5f, 0.5f)));
            Logging.Log(gridObj);
            
            if(!gridObj) return;

            if (gridObj is Resource r)
            {
                r.Harvest(CurItem);
            }
            
            _isBreaking = true;
            breakingFlagEventChannel.Raise(_isBreaking);
        }

        private void StopBreak()
        {
            _isBreaking = false;
            breakingFlagEventChannel.Raise(_isBreaking);
        }

        private void RClick()
        {
            if (_isBuilding || _inventoryManager.UIOpen) return;

            GridObject gridObj = GridManager.Instance.GridMap.GetObjectsAt(Vector2Int.RoundToInt(MouseWorldPos - new Vector2(0.5f, 0.5f)));
            
            if(!gridObj) return;
            
            if (gridObj.TryGetComponent(out IInteractable interactable))
                interactable.Interaction(new InteractionContext(craftingInteractEventChannel));
        }

        private void Attack(WeaponDataSO weaponData)
        {
            if (Arm.gameObject.activeSelf) return;
            Vector2 dir = MouseWorldPos - (Vector2)transform.position;
            Weapon.Init(weaponData);
            Arm.Init(Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x), weaponData.AttackData.AttackSpeed).Swing();
            PlayerRenderer.SetValue(attackHash);
        }

        private void Throw()
        {
            if (CurItem == null) return;
            
            Thrower.Throw(CurItem, (MouseWorldPos - (Vector2)transform.position).normalized);
            _inventoryManager.UseSelectedItem();
            PlayerRenderer.SetValue(attackHash);
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