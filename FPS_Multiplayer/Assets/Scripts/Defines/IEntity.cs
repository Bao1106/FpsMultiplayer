using UnityEngine;

namespace Defines
{
    public interface IEntity
    {
        Rigidbody Rig { get; set; }
        Animator Animator { get; set; }

        void Move();
        void Look();
        void Jump();
    }
}
