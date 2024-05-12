using System;
using Interface;
using UnityEngine;

namespace Entities.Base
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private float speed;
        
        public Rigidbody Rig { get; set; }
        
        public void Move()
        {
            var hMove = Input.GetAxis("Horizontal");
            var vMove = Input.GetAxis("Vertical");
            
            var direction = new Vector3(hMove, 0, vMove).normalized;

            Rig.velocity = transform.TransformDirection(direction) * (speed * Time.deltaTime);
        }

        protected void Start()
        {
            if (Camera.main != null) 
                Camera.main.enabled = false;
            
            Rig = GetComponent<Rigidbody>();
        }

        protected void FixedUpdate()
        {
            Move();
        }
    }
}
