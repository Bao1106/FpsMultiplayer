using Services;
using Services.DependencyInjection;
using UnityEngine;

namespace Interfaces
{
    public interface IEntity
    {
        int MaxHealth { get; set; }
        CharacterController CharacterController { get; set; }
        Observer<int> EntityHealth { get; }
        Observer<bool> IsDamaged { get; }
    }
}
