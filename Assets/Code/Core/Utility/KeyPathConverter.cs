using UnityEngine.InputSystem;

namespace Code.Core.Utility {
    public static class KeyPathConverter {
        public static string ToKeyPath(this Key key) {
            if (Keyboard.current == null)
                return null;

            var control = Keyboard.current[key];
            return control?.path;
        }
    }
}