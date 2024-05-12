using UnityEngine;

namespace Interface
{
    public interface IEntity
    {
        Rigidbody Rig { get; set; }

        void Move();
    }
}
