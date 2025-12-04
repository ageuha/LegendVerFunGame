using System.IO;
using UnityEngine;

namespace Code.SaveSystem {
    public class JsonSaveManager<T> : SaveManagerBase<T> {
        protected override string Filepath { get; }

        public override void SaveToFile(T data) {
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(Filepath, jsonData);
        }

        public override T LoadSaveData() {
            if (!File.Exists(Filepath)) return default;
            return JsonUtility.FromJson<T>(File.ReadAllText(Filepath));
        }

        public override void DeleteSave() {
            if (!File.Exists(Filepath)) return;
            File.Delete(Filepath);
        }

        public JsonSaveManager(string filename) : base(filename) {
            Filepath = Path.Combine(Application.persistentDataPath, filename);
        }
    }
}