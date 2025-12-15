using Member.KJW.Code.SceneManagement;
using UnityEngine;

namespace YTH.Code.Main
{    
    public class Main : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.Instance.LoadScene(SceneID.NextScene);
        }
    }
}
    