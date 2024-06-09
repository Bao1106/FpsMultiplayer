using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class GoapSetBinder : MonoBehaviour
    {
        private GoapRunnerBehaviour goapRunner;

        private void Awake()
        {
            goapRunner = FindObjectOfType<GoapRunnerBehaviour>();
            
            var agent = GetComponent<AgentBehaviour>();
            agent.GoapSet = goapRunner.GetGoapSet("ZombieSet");
        }
    }
}