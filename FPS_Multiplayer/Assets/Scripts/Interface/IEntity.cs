using UnityEngine;

namespace Interface
{
    public interface IEntity
    {
        Rigidbody Rig { get; set; }
        Animator Animator { get; set; }

        void Move();
        void Look();
    }
}
