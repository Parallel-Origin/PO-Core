namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A generic vec2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TT"></typeparam>
    public struct Vector2<T, TT>
    {
        public T X;
        public TT Y;

        public Vector2(T x, TT y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}