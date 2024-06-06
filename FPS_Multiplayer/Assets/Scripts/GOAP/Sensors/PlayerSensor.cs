using Entities.Entity;
using Events;
using UnityEngine;

namespace GOAP.Sensors
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerSensor : MonoBehaviour
    {
        public new SphereCollider collider;
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);

        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;

        private void Awake()
        {
            collider = GetComponent<SphereCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPlayerEnter?.Invoke(player.transform);
                StaticEvents.IsUserInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPlayerExit?.Invoke(other.transform.position);
                StaticEvents.IsUserInRange = false;
            }
        }
    }
}
