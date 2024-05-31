using CrashKonijn.Goap.Classes.References;
using UnityEngine;

namespace GOAP.Actions
{
    public class AttackData : CommonData
    {
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Walk = Animator.StringToHash("Walk");
        
        [GetComponent]
        public Animator Animator { get; set; }
    }
}