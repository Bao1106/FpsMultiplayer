using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using Events;
using GOAP.Config;
using Interfaces;
using UnityEngine;

namespace GOAP.Actions
{
    public class NormalAttackAction : ActionBase<AttackData>, IInjectable
    {
        private AttackConfig attackConfig;
        
        public override void Created() {}

        public override void Start(IMonoAgent agent, AttackData data)
        {
            data.Timer = attackConfig.attackDelay;
        }

        public override ActionRunState Perform(IMonoAgent agent, AttackData data, ActionContext context)
        {
            data.Timer -= context.DeltaTime;
            var shouldAttack = data.Target != null &&
                               Vector3.Distance(data.Target.Position, agent.transform.position) <=
                               attackConfig.normalAttackRadius && StaticEvents.PlayerHealth.Value > 0;

            data.Animator.SetBool(AttackData.Attack, shouldAttack);
            if (shouldAttack)
            {
                //data.Animator.SetTrigger(AttackData.Attack);
                agent.transform.LookAt(data.Target.Position);
            }

            return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, AttackData data)
        {
            //data.Animator.SetBool(AttackData.Attack, false);
            //data.Animator.SetTrigger(AttackData.Walk);
        }

        public void Inject(GoapDI injector)
        {
            attackConfig = injector.attackConfig;
        }
    }
}