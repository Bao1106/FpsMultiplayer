using System;
using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class GoapSetBinder : MonoBehaviour
    {
        [SerializeField] private GoapRunnerBehaviour goapRunner;

        private void Awake()
        {
            var agent = GetComponent<AgentBehaviour>();
            agent.GoapSet = goapRunner.GetGoapSet("ZombieSet");
        }
    }
}