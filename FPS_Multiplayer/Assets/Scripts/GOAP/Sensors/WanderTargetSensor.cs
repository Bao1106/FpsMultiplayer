using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Sensors
{
    public class WanderTargetSensor : LocalTargetSensorBase
    {
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
                var random = Random.insideUnitCircle * 5;
                var position = agent.transform.position + new Vector3(random.x, 0, random.y);
                if (NavMesh.SamplePosition(position, out var hit, 1, NavMesh.AllAreas))
                {
                    return hit.position;
                }

                count++;
            }

            return agent.transform.position;
        }
    }
}