using System;
using Defines;
using Entities.Soldier;
using Services;
using UnityEngine;

namespace Entities.Base
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private float speed, sprintModifier, jumpForce;

        protected float Speed => speed;
        protected float SprintModifier => sprintModifier;
        protected float JumpForce => jumpForce;
        
        public Rigidbody Rig { get; set; }
        public Animator Animator { get; set; }

        public void Look() {}
        public void Jump() {}
        public void Move() {}
        
        protected virtual void Start()
        {
            if (Camera.main != null) 
                Camera.main.enabled = false;
            
            Rig = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
        }

        protected virtual void FixedUpdate() { }
    }
}
