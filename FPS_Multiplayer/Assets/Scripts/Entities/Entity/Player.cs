using System;
using UnityEngine;

namespace Entities.Entity
{
    public class Player : Base.Entity
    {
        public void OnDamage(int damage)
        {
            Health -= damage;
            // also handle death and stuff :)
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.LogError(other.gameObject.name);
            }
        }
    }
}
