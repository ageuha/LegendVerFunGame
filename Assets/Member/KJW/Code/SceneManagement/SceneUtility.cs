using System;
using UnityEngine.SceneManagement;

namespace Member.KJW.Code.SceneManagement
{
    public static class SceneUtility
    {
        public static event Action<int> BeforeChangeScene;
        public static event Action<int> AfterChangeScene;

        public static void Load(this SceneID id)
        {
            int sceneIndex = id switch
            {
                SceneID.NextScene => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1,
                SceneID.PrevScene => UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1,
                _ => (int)id
            };
            
            BeforeChangeScene?.Invoke(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
            AfterChangeScene?.Invoke(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}