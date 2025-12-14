using System.Collections.Generic;
using System.Timers;
using Code.Core.Pool;
using Code.Core.Utility;
using Member.YDW.Script.BuildingSystem;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class TurretBuilding : UnitBuilding, IBuilding , IWaitable
    {
        [SerializeField] private float shootingTime;
        [SerializeField] private Animator animator;
        public bool IsActive { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        private readonly int _dirXHash = Animator.StringToHash("dirX");
        private readonly int _dirYHash = Animator.StringToHash("dirY"); 
        
        public bool IsWaiting { get; private set; }
        public void InitializeBuilding(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            Initialize(buildingData,buildingData.MaxHealth);
            timer.StartTimer(this,cooldownBar,buildingData.BuildTime,this,true);
        }

        public void SetWaiting(bool waiting)
        {
            if (!waiting)
            {
                IsActive = true;
            }
            IsWaiting = waiting;
        }

        private void Update()
        {
            if(!IsActive) return;
            if (CheckTarget(out var target) && !IsWaiting)
            {
                Arrow arrow = PoolManager.Instance.Factory<Arrow>().Pop();
                arrow.Initialize(transform.position,target - transform.position);
                timer.StartTimer(this,cooldownBar,shootingTime,this,false);
            }

            if (target == Vector3.zero)
                animator.speed = 0;
            else
                animator.speed = 1;
            animator.SetFloat(_dirXHash,target.x - transform.position.x); 
            animator.SetFloat(_dirYHash, target.y - transform.position.y);
        }


        private bool CheckTarget(out Vector3 position)
        {
            int layer = LayerMask.NameToLayer("Enemy");   // 레이어 번호
            LayerMask mask = 1 << layer;
            Collider2D[] target = Physics2D.OverlapBoxAll(transform.position,new Vector2(9,9),0,mask);
            List<Collider2D> selectTarget = new List<Collider2D>();
            for (int i = 0; i < target.Length; i++)
            {
                Vector2 diff = target[i].transform.position - transform.position;
                Vector2 dir = diff.normalized;
                float tolerance = 1f; //오차 범위
                if (dir.x > 0.9f && Mathf.Abs(diff.y) < tolerance) //오차 범위 내에 있는지.
                {
                    Logging.Log("오른쪽에 있음.");
                    selectTarget.Add(target[i]);
                }
                else if (dir.x < -0.9f && Mathf.Abs(diff.y) < tolerance)
                {
                    Logging.Log("왼쪽에 있음.");
                    selectTarget.Add(target[i]);
                }
                else if (dir.y > 0.9f && Mathf.Abs(diff.x) < tolerance)
                {
                    Logging.Log("위쪽에 있음.");
                    selectTarget.Add(target[i]);
                }
                else if (dir.y < -0.9f && Mathf.Abs(diff.x) < tolerance)
                {
                    Logging.Log("아래쪽d에 있음");
                    selectTarget.Add(target[i]);
                }
            }

            if (selectTarget.Count == 0)
            {
                
                position = Vector2.zero;
                return false;
            }
            Collider2D bestTarget = selectTarget[0];
            for (int i = 0; i < selectTarget.Count; i++)
            {
                if(i == 0) continue;
                if (Vector2.Distance(transform.position, selectTarget[i].transform.position) <
                    Vector2.Distance(transform.position, bestTarget.transform.position))
                {
                    bestTarget = selectTarget[i];
                }
            }

            if (bestTarget != null)
            {
                position = bestTarget.transform.position;
                return true;
            }
            position = Vector2.zero;
            return false;
        }
    }
}