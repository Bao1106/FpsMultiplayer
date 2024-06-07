using CrashKonijn.Goap.Classes.References;
using GOAP.Behaviours;
using Interfaces;
using Services.DependencyInjection;
using UnityEngine;

namespace GOAP.Actions
{
    public class AttackData : CommonData
    {
        public static readonly int Attack = Animator.StringToHash("Attack");
        
        [GetComponent] public Animator Animator { get; set; }
        [GetComponent] public AgentMoveBehaviour MoveBehaviour { get; set; }
    }
}