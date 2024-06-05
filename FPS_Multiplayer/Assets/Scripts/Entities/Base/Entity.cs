using System;
using Entities.Player;
using Interfaces;
using Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Base
{
    public class Entity : MonoBehaviour, IEntity
    {
        protected Observer<int> EntityHealth;
        protected Observer<bool> IsDamaged;

        protected static int MaxHealth { get; set; }
        public CharacterController CharacterController { get; set; }
        
        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            EntityHealth.Invoke();
            IsDamaged.Invoke();
        }
    }
}
