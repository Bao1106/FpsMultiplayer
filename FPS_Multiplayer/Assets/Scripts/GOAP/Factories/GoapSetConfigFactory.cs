using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using Demos.Complex.WorldKeys;
using GOAP.Actions;
using GOAP.Goals;
using GOAP.Sensors;
using GOAP.Targets;

namespace GOAP.Factories
{
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        public override IGoapSetConfig Create()
        {
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
        }

        private void BuildActions(GoapSetBuilder builder)
        {
            builder.AddAction<WanderAction>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);
        }

        private void BuildGoals(GoapSetBuilder builder)
        {
            builder.AddGoal<WanderGoal>()
                .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual, 1);
        }
    }
}