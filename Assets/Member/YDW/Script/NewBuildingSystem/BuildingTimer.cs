using System.Collections;
using Member.YDW.Script.BuildingSystem;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class BuildingTimer
    {
        private ICooldownBar _cooldownBar;
        private IWaitable _target;
        private float _time;

        public void StartTimer(IWaitable target, ICooldownBar cooldownBar , float time, MonoBehaviour mono)
        {
            _target = target;
            _time = time;
            mono.StartCoroutine(OnBuildingWaiteBuilding());
        }
        
        private IEnumerator OnBuildingWaiteBuilding() //본인 또한 세팅이 되어야 함.
        {
            _target.SetWaiting(true);
            float endTime = Time.time + _time;

            while (Time.unscaledTime < endTime)
            {
                
                float remaining = endTime - Time.unscaledTime;
                float progress = Mathf.Clamp01(remaining / _time); 

                float newScaleX = Mathf.Lerp(0f, 2f, progress);

                _cooldownBar.SetFillAmount(newScaleX);
                yield return null;
                
            }
            _target.SetWaiting(false);




        }
        

    }
}