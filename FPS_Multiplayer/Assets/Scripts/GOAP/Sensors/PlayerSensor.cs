using Entities.Entity;
using Services;
using Services.DependencyInjection;
using UnityEngine;

namespace GOAP.Sensors
{
    public interface IPlayerSensor
    {
        Observer<string, bool> IsUserInRange { get; }
    }
    
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerSensor : MonoBehaviour, IPlayerSensor, IDependencyProvider
    {
        [SerializeField] private string key;
        
        public new SphereCollider collider;
        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnownPosition);

        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;
        
        public Observer<string, bool> IsUserInRange { get; private set; }

        [Provide]
        public IPlayerSensor ProviderSensor()
        {
            return this;
        }

        public void SetKey(string sensorKey) => key = sensorKey;
        
        private void Awake()
        {
            collider = GetComponent<SphereCollider>();
            IsUserInRange = new Observer<string, bool>(key, false);
            IsUserInRange.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPlayerEnter?.Invoke(player.transform);
                IsUserInRange.Value1 = key;
                IsUserInRange.Value2 = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnPlayerExit?.Invoke(other.transform.position);
                IsUserInRange.Value1 = key;
                IsUserInRange.Value2 = false;
            }
        }
    }
}
