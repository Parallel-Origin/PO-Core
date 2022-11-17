namespace ParallelOrigin.Core.Base.Interfaces.Storage;

/// <summary>
///     A interface for a class that represents a stack.
/// </summary>
/// <typeparam name="I">The type we store into our stack</typeparam>
public interface IStack<I>
{
    /// <summary>
    ///     Puts a key value pair on top of the stack
    /// </summary>
    /// <param name="value">The element we push onto our stack</param>
    void Push(I value);

    /// <summary>
    ///     Peeks at the first element in the stack and returns it.
    /// </summary>
    /// <returns></returns>
    I Peek();

    /// <summary>
    ///     Removes a Element from the stack completly
    /// </summary>
    /// <returns></returns>
    I Pop();

    /// <summary>
    ///     Checks if this stack is empty and returns a bool value.
    /// </summary>
    /// <returns></returns>
    bool IsEmpty();

    /// <summary>
    ///     Clears the whole stack
    /// </summary>
    void Clear();
}