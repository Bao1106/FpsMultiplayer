using UnityEngine;

namespace Interfaces
{
    public interface IEntity
    {
        int MaxHealth { get; set; }
        CharacterController CharacterController { get; set; }
    }
}
