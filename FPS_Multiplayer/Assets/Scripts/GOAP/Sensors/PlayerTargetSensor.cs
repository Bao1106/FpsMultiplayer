using System.Collections.Generic;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using GOAP.Config;
using Interfaces;
using UnityEngine;

namespace GOAP.Sensors
{
    public class PlayerTargetSensor : LocalTargetSensorBase, IInjectable
    {
        private AttackConfig attackConfig;
        private readonly Collider[] colliders = new Collider[1];

        public override void Created() { }

        public override void Update() { }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            if (Physics.OverlapSphereNonAlloc(agent.transform.position, attackConfig.sensorRadius,
                    colliders, attackConfig.attackLayer) > 0)
            {
                return new TransformTarget(colliders[0].transform);
            }
            
            return null;
        }

        public void Inject(GoapDI injector)
        {
            attackConfig = injector.attackConfig;
        }
    }
}