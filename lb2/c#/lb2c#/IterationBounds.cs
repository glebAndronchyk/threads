namespace lb2 {
    class IterationBounds {
        public int start;
        public int end;
        public Func<int, int, int?> cb;
    }
}