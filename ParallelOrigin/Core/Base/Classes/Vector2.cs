namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A generic vec2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tt"></typeparam>
    public struct Vector2<T, Tt>
    {
        public T X;
        public Tt Y;

        public Vector2(T x, Tt y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}