using System;
using System.Collections.Generic;
using Code.Core.Utility;

namespace Code.UI.Setting {
    [Serializable]
    public class FloatSettingDict : SerializableDictionary<SettingType, float> {
        public FloatSettingDict(int capacity) : base(capacity) {
        }

        public FloatSettingDict() {
        }

        public FloatSettingDict(Dictionary<SettingType, float> dict) : base(dict) {
        }
    }
}