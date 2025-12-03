using UnityEngine;

namespace Code.SaveSystem {
    public class PrefsSaveManager<T> : SaveManagerBase<T> {
        protected override string Filepath { get; }

        public override void SaveToJson(T data) {
            string jsonData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Filepath, jsonData);
        }

        public override T LoadSaveData() {
            if (!PlayerPrefs.HasKey(Filepath)) return default;
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(Filepath, string.Empty));
        }

        public override void DeleteSave() {
            if (!PlayerPrefs.HasKey(Filepath)) return;
            PlayerPrefs.DeleteKey(Filepath);
        }

        public PrefsSaveManager(string filename) : base(filename) {
            Filepath = filename;
        }
    }
}