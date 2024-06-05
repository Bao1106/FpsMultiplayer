using GOAP.Actions;
using Services;
using UnityEngine;

namespace Interfaces
{
    public interface IEntity
    {
        static int MaxHealth { get; set; }
        CharacterController CharacterController { get; set; }
    }
}
