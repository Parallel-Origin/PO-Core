#if SERVER

using FluentBehaviourTree;
namespace ParallelOrigin.Core.ECS.Components.AI {

    /// <summary>
    /// A component which acts as an AI controller to controll the attached entity automaticly 
    /// </summary>
    public struct AIController {

        public IBehaviourTreeNode behaviourTree;

    }
}

#endif