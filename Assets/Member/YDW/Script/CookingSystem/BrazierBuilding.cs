using System.Collections;
using Code.Core.Utility;
using Member.YTH.Code.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.CookingSystem
{
    public class BrazierBuilding : MonoBehaviour
    {
        [field: SerializeField] public float MaxFuel { get; private set; }
        [field: SerializeField] public float FuelDeathTic { get; private set; }
        [field: SerializeField] public float FuelDeathValue { get; private set; }
        
        
        private ItemDataSO _slotItemData;
        private ICookable _currentCookedItemData;

        private IEnumerator _cookingCoroutine;

        private float _fuel = 0;
        private bool _isBurning = false;
        private bool _isCooking = false;

        private void Awake()
        {
            _cookingCoroutine = Cook();
        }

        public void SetItem(ItemDataSO item) //추후 요리 후 나온 아이템을 픽업해가면, 아이템을 null로 만들어야 함.
        {
            _slotItemData = null;
            if(item is ICookable cookable &&  !cookable.IsCooked)
                _currentCookedItemData = cookable;
            if(item is IFuel fuel)
                AddFuel(fuel.FuelAmount);
        }

        private void AddFuel(float fuel)
        {
            _fuel += fuel;
            _fuel = Mathf.Clamp(_fuel, 0, MaxFuel);
        }

        private void Update()
        {
            #region TestCode

            if(Keyboard.current.fKey.wasPressedThisFrame)
                AddFuel(20);

            #endregion
            if (_currentCookedItemData != null && _isBurning && !_isCooking)
                StartCoroutine(_cookingCoroutine);
            if(!_isBurning &&  _isCooking)
                StopCoroutine(_cookingCoroutine);
            if(!_isBurning && _fuel > 0)
                StartCoroutine(FuelSystem());
        }

        private IEnumerator FuelSystem()
        {
            _isBurning = true;
            float currentTime = Time.time;
            float endTime = Time.time + FuelDeathTic;
            while (currentTime < endTime)
            {
                currentTime = Time.time;
                yield return null;
            }
            _fuel -= FuelDeathValue;
            _fuel = Mathf.Clamp(_fuel, 0, MaxFuel);
            if (_fuel <= 0)
            {
                Logging.Log("화로가 멈춥니다.");
                _isBurning = false;
            }
            else
            { 
                StartCoroutine(FuelSystem());
            }
        }
                
            
           


        private IEnumerator Cook()
        {
            _isCooking = true;
            yield return new WaitForSeconds(_currentCookedItemData.CookTime);
            _isCooking = false;
            ReturnItem(_currentCookedItemData.CookedItemData);
        }

        private void ReturnItem(ItemDataSO cooked)
        {
            _slotItemData =  cooked;
        }
    }
}