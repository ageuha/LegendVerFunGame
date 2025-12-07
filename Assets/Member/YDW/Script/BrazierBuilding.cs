using System.Collections;
using UnityEngine;
using YTH.Code.Item;

namespace Member.YDW.Script
{
    public class BrazierBuilding : MonoBehaviour
    {

        public IEnumerator Cook(float time, FoodItemDataSO foodItemDataSO)
        {
            yield return new WaitForSeconds(time);
            foodItemDataSO.Cooked();
        }
        
    }
}