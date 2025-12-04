namespace Code.SaveSystem {
    /// <summary>
    /// Manager라고 다 싱글톤인 건 아님. 아시죠?
    /// </summary>
    /// <typeparam name="T">저장할 타입</typeparam>
    public interface ISaveManager<T> {
        void SaveToJson(T data);
        T LoadSaveData();
        void DeleteSave();
    }
}