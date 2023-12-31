#if SERVER

using FluentBehaviourTree;

namespace ParallelOrigin.Core.ECS.Components
{
    /// <summary>
    ///     A component which acts as an AI controller to controll the attached entity automaticly
    /// </summary>
    public struct AiController
    {
        public IBehaviourTreeNode BehaviourTree;
    }
}

#endif