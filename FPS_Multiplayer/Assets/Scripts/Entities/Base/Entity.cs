using Interfaces;
using Services;
using Services.DependencyInjection;
using UnityEngine;

namespace Entities.Base
{
    public class Entity : MonoBehaviour, IEntity, IDependencyProvider
    {
        public int MaxHealth { get; set; }
        public CharacterController CharacterController { get; set; }
        public Observer<int> EntityHealth { get; protected set; }
        public Observer<bool> IsDamaged { get; protected set; }

        [Provide]
        public IEntity ProviderEntity()
        {
            return this;
        }
        
        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            EntityHealth.Invoke();
            IsDamaged.Invoke();
        }
    }
}
