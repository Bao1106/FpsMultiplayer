using UnityEngine;

namespace Interfaces
{
    public interface IEntity
    {
        float Health { get; set; }
        CharacterController CharacterController { get; set; }
    }
}
