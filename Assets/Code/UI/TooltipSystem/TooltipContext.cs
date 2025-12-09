using UnityEngine;

namespace Code.UI.TooltipSystem {
    public struct TooltipContext {
        public bool Active;

        // public bool FollowMouse;
        public Vector2 Position;
        public string TitleText;
        public string DescriptionText;

        /// <summary>
        /// Color에 alpha 0 넣어놓고 안 보인다고 따지면 죽는다
        /// </summary>
        public Color TitleColor;

        /// <summary>
        /// Color에 alpha 0 넣어놓고 안 보인다고 따지면 죽는다
        /// </summary>
        public Color DescriptionColor;

        /// <summary>
        /// Color에 alpha 0 넣어놓고 안 보인다고 따지면 죽는다
        /// </summary>
        public Color BackgroundColor;

        /// <summary>
        /// Color에 alpha 0 넣어놓고 안 보인다고 따지면 죽는다
        /// </summary>
        public Color BorderColor;

        /// <summary>
        /// Color에 alpha 0 넣어놓고 안 보인다고 따지면 죽는다
        /// </summary>
        public Color OutlineColor;
    }
}