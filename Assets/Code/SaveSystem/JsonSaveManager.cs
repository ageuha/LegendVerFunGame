using System.IO;
using UnityEngine;

namespace Code.SaveSystem {
    public class JsonSaveManager<T> : SaveManagerBase<T> {
        protected override string Filepath { get; }

        public override void SaveToJson(T data) {
            string jsonData = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, Filepath);
            File.WriteAllText(path, jsonData);
        }

        public override T LoadSaveData() {
            string path = Path.Combine(Application.persistentDataPath, Filepath);
            if (!File.Exists(path)) return default;
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }

        public override void DeleteSave() {
            string path = Path.Combine(Application.persistentDataPath, Filepath);
            if (!File.Exists(path)) return;
            File.Delete(path);
        }

        public JsonSaveManager(string filename) : base(filename) {
            Filepath = Path.Combine(Application.persistentDataPath, filename);
        }
    }
}