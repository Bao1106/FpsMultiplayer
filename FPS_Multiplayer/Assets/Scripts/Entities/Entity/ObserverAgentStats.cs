using Interfaces;
using Managers;
using Services.DependencyInjection;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Entity
{
    public class ObserverAgentStats : MonoBehaviour
    {
        [Inject] private IEntity entity;
        
        private ISceneInit sceneInit;
        private int agentHealth;
        private bool isUserInRange;
        private float agentVelocity =  0.1f;

        private readonly float agentAcceleration = 0.1f;
        private readonly float agentDeceleration = 0.4f;
        
        public int AgentHealth => agentHealth;
        public int EntityHealth() => entity != null ? entity.EntityHealth.Value : 0;

        private void Awake()
        {
            sceneInit = SceneInjectorManager.Instance.SceneInitManager;
        }

        private async void Start()
        {
            if (PlayerManager.Instance.PlayerData.IsMasterClient)
            {
                await sceneInit.SceneInitTask;
                Injector.Instance.InjectSingleField(this, typeof(IEntity));
                OnAddZombieListener();
            }
            else
            {
                var playerName = PlayerManager.Instance.PlayerData.PlayerName;
                entity = (IEntity)Injector.Instance.Resolve(typeof(IEntity), playerName);
                //OnAddZombieListener();
            }
        }

        private void OnAddZombieListener()
        {
            var zombie = GetComponent<Zombie>();
            ZombieManager.Instance.OnInjectPlayerSensor(zombie.zombieName, OnUpdateSensor);
            
            zombie.EnemyHealth.AddListener(OnObserverHealth);
            OnObserverHealth(zombie.EnemyHealth.Value);
        }
        
        private void OnUpdateSensor(string enemyName, bool userInRange)
        {
            isUserInRange = userInRange;
        }

        private void OnObserverHealth(int health)
        {
            agentHealth = health;
        }

        public float CalculateVelocity(Animator animator, NavMeshAgent agent, float defaultSpeed)
        {
            if (isUserInRange && agentVelocity < 1.0f)
            {
                agentVelocity += Time.deltaTime * agentAcceleration;
                agent.speed = defaultSpeed * 2;
                animator.speed = 2f;
            }
            else if (!isUserInRange)
            {
                agent.speed = defaultSpeed;
                animator.speed = 1f;
                switch (agentVelocity)
                {
                    case > 0.1f:
                        agentVelocity -= Time.deltaTime * agentDeceleration;
                        break;
                    case < 0.1f:
                        agentVelocity = 0.1f;
                        break;
                }
            }
            
            return agentVelocity;
        }
    }
}
