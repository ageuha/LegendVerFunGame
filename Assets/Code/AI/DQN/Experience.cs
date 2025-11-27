namespace Code.AI.DQN {
    [System.Serializable]
    public class Experience {
        public float[] state;
        public int action;
        public float reward;
        public float[] nextState;
        public bool isDone;

        public Experience(float[] state, int action, float reward, float[] nextState, bool isDone) {
            this.state = state;
            this.action = action;
            this.reward = reward;
            this.nextState = nextState;
            this.isDone = isDone;
        }
    }
}