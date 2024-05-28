using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using GOAP.Actions;
using GOAP.Config;
using GOAP.Goals;
using GOAP.Sensors;
using GOAP.Targets;
using GOAP.WorldKeys;
using UnityEngine;
using IsWandering = Demos.Complex.WorldKeys.IsWandering;

namespace GOAP.Factories
{
    [RequireComponent(typeof(GoapDI))]
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        private GoapDI injector;
        
        public override IGoapSetConfig Create()
        {
            injector = GetComponent<GoapDI>();
            GoapSetBuilder builder = new("ZombieSet");

            BuildGoals(builder);
            BuildActions(builder);
            BuildSensors(builder);
            
            return builder.Build();
        }

        private void BuildSensors(GoapSetBuilder builder)
        {
            builder.AddTargetSensor<WanderTargetSensor>()
                .SetTarget<WanderTarget>();

            builder.AddTargetSensor<PlayerTargetSensor>()
                .SetTarget<PlayerTarget>();
        }

        private void BuildActions(GoapSetBuilder builder)
        {
            builder.AddAction<WanderAction>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);

            builder.AddAction<NormalAttackAction>()
                .SetTarget<PlayerTarget>()
                .AddEffect<PlayerHealth>(EffectType.Decrease)
                .SetBaseCost(injector.attackConfig.normalAttackCost)
                .SetInRange(injector.attackConfig.sensorRadius);
        }

        private void BuildGoals(GoapSetBuilder builder)
        {
            builder.AddGoal<WanderGoal>()
                .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual, 1);

            builder.AddGoal<KillPlayer>()
                .AddCondition<PlayerHealth>(Comparison.SmallerThanOrEqual, 0);
        }
    }
}