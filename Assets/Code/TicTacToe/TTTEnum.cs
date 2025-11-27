namespace Code.TicTacToe {
    public enum TTTEnum {
        None,
        X,
        O
    }
    public static class TTTEnumMethods {
        public static TTTEnum GetOpposite(this TTTEnum tttEnum) {
            return tttEnum switch {
                TTTEnum.X => TTTEnum.O,
                TTTEnum.O => TTTEnum.X,
                _ => TTTEnum.None
            };
        }
    }
}