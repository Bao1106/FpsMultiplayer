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
        [SerializeField] protected Observer<int> entityHealth;
        public static int MaxHealth { get; set; }
        public CharacterController CharacterController { get; set; }
        
        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            entityHealth.Invoke();
        }
    }
}
