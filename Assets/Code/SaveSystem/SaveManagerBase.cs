namespace Code.SaveSystem {
    public abstract class SaveManagerBase<T> : ISaveManager<T> {
        protected SaveManagerBase(string filename) {
        }

        protected abstract string Filepath { get; }

        public abstract void SaveToFile(T data);
        public abstract T LoadSaveData();
        public abstract void DeleteSave();
    }
}