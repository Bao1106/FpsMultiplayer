using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using GOAP.Config;
using Interfaces;
using UnityEngine;

namespace GOAP.Actions
{
    public class WanderAction : ActionBase<CommonData>, IInjectable
    {
        private WanderConfig wanderConfig;
        
        public override void Created() {}

        public override void Start(IMonoAgent agent, CommonData data)
        {
            data.Timer = Random.Range(wanderConfig.waitRangeBetweenWanders.x, wanderConfig.waitRangeBetweenWanders.y);
        }

        public override ActionRunState Perform(IMonoAgent agent, CommonData data, ActionContext context)
        {
            data.Timer -= context.DeltaTime;
            if (data.Timer > 0)
            {
                return ActionRunState.Continue;
            }

            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, CommonData data) {}
        public void Inject(GoapDI injector)
        {
            wanderConfig = injector.wanderConfig;
        }
    }
}