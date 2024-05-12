using System;
using Entities.Soldier;
using Interface;
using UnityEngine;

namespace Entities.Base
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private Look look;
        [SerializeField] private float speed;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public Rigidbody Rig { get; set; }
        
        public Animator Animator { get; set; }

        public void Move()
        {
            var hMove = Input.GetAxisRaw("Horizontal");
            var vMove = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(hMove, 0, vMove).normalized;
            var isMove = hMove != 0 || vMove != 0;
            
            Rig.velocity = transform.TransformDirection(direction) * (speed * Time.deltaTime);
            
            Animator.SetBool(IsMoving, isMove);
        }

        public void Look()
        {
            look.EntityLook();
        }

        protected void Start()
        {
            if (Camera.main != null) 
                Camera.main.enabled = false;
            
            Rig = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            look.SetupEyes();
        }

        protected void FixedUpdate()
        {
            Look();
            Move();
        }
    }
}
