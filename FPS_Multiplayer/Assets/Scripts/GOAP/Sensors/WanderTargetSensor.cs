using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GOAP.Config;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Sensors
{
    public class WanderTargetSensor : LocalTargetSensorBase, IInjectable
    {
        private WanderConfig wanderConfig;
        
        public override void Created() { }

        public override void Update() { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var position = GetRandomPosition(agent);
            return new PositionTarget(position);
        }

        private Vector3 GetRandomPosition(IMonoAgent agent)
        {
            var count = 0;
            while (count < 5)
            {
                var random = Random.insideUnitCircle * wanderConfig.wanderRadius;
                var position = agent.transform.position + new Vector3(random.x, 0, random.y);
                if (NavMesh.SamplePosition(position, out var hit, 1, NavMesh.AllAreas))
                {
                    return hit.position;
                }

                count++;
            }

            return agent.transform.position;
        }

        public void Inject(GoapDI injector)
        {
            wanderConfig = injector.wanderConfig;
        }
    }
}