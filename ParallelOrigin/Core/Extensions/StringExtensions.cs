namespace ParallelOrigin.Core.Extensions {
    
    /// <summary>
    /// A extension for strings...
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Creates a simple deterministic hashcode and returns it... is the same on every device. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetDeterministicHashCode(this string path) {
            
            unchecked {
                
                var hash = 23;
                for (var index = 0; index < path.Length; index++) {

                    var currentChar = path[index];
                    hash = hash * 31 + currentChar;
                }
                return hash;
            }
        }
    }
}