using System;
using CrashKonijn.Goap.Behaviours;
using GOAP.Goals;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class AgentBrain : MonoBehaviour
    {
        private AgentBehaviour agentBehaviour;

        private void Awake()
        {
            agentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void Start()
        {
            agentBehaviour.SetGoal<WanderGoal>(false);
        }
    }
}