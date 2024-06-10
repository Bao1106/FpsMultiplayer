using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using Entities.Entity;
using Interfaces;
using Managers;
using Services;
using Services.DependencyInjection;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class AgentMoveBehaviour : MonoBehaviour
    {
        [SerializeField] private float minMoveDistance = 0.25f;
        [SerializeField] private ObserverAgentStats observerAgent;
        
        private ITarget currentTarget;
        
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private AgentBehaviour agentBehaviour;
        
        private Vector3 lastPos;
        private Vector2 smoothDeltaPos;
        
        private float defaultSpeed;
        private int agentHealth;
        private bool isUserInRange;
        
        private static readonly int velocity = Animator.StringToHash("Velocity");

        public ObserverAgentStats ObserverAgent => observerAgent;
        
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            agentBehaviour = GetComponent<AgentBehaviour>();
            
            animator.applyRootMotion = true;
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = true;
            defaultSpeed = navMeshAgent.speed;
        }
        
        private void OnEnable()
        {
            agentBehaviour.Events.OnTargetChanged += EventsOnTargetChanged;
            agentBehaviour.Events.OnTargetOutOfRange += EventsOnTargetOutOfRange;
        }

        private void OnDisable()
        {
            agentBehaviour.Events.OnTargetChanged -= EventsOnTargetChanged;
            agentBehaviour.Events.OnTargetOutOfRange -= EventsOnTargetOutOfRange;
        }
        
        private void EventsOnTargetOutOfRange(ITarget target)
        {
            //animator.SetBool(walk, false);
            animator.SetFloat(velocity, 0.05f);
        }

        private void EventsOnTargetChanged(ITarget target, bool inRange)
        {
            currentTarget = target;

            lastPos = currentTarget.Position;
            navMeshAgent.SetDestination(target.Position);

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                animator.SetFloat(velocity, 0.05f);
            }
            
            //animator.SetBool(walk, true);
            //animator.SetTrigger(walk);
        }

        private void Update()
        {
            if (currentTarget == null) return;

            if (observerAgent.AgentHealth <= 0)
            {
                navMeshAgent.speed = 0;
                return;
            }
            
            if (minMoveDistance <= Vector3.Distance(currentTarget.Position, lastPos) && observerAgent.EntityHealth() > 0)
            {
                lastPos = currentTarget.Position;
                navMeshAgent.SetDestination(currentTarget.Position);
            }

            var agentVelocity = observerAgent.CalculateVelocity(animator, navMeshAgent, defaultSpeed);
            animator.SetFloat(velocity, navMeshAgent.velocity.magnitude > 0.1f 
                ? agentVelocity : 0.05f);
            
            var worldDeltaPos = navMeshAgent.nextPosition - transform.position;
            worldDeltaPos.y = 0;

            var dx = Vector3.Dot(transform.right, worldDeltaPos);
            var dy = Vector3.Dot(transform.forward, worldDeltaPos);
            var deltaPos = new Vector2(dx, dy);

            var smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
            smoothDeltaPos = Vector2.Lerp(smoothDeltaPos, deltaPos, smooth);
            
            var deltaMagnitude = worldDeltaPos.magnitude;
            if (deltaMagnitude > navMeshAgent.radius / 2f)
            {
                transform.position = Vector3
                    .Lerp(animator.rootPosition,
                        navMeshAgent.nextPosition,
                        smooth);
            }
        }
    }
}