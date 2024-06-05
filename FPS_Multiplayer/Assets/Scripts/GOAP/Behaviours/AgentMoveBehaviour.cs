using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using Events;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class AgentMoveBehaviour : MonoBehaviour
    {
        [SerializeField] private float minMoveDistance = 0.25f;
        
        private ITarget currentTarget;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private AgentBehaviour agentBehaviour;
        private Vector3 lastPos;
        private float agentVelocity = 0.1f;
        private bool isUserInRange;
        private readonly float agentAcceleration = 0.1f;
        private readonly float agentDecceleration = 0.4f;
        
        private static readonly int walk = Animator.StringToHash("Walk");
        private static readonly int idle = Animator.StringToHash("Idle");
        private static readonly int velocity = Animator.StringToHash("Velocity");

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            agentBehaviour = GetComponent<AgentBehaviour>();
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
            //animator.SetBool(walk, true);
            //animator.SetTrigger(walk);
        }

        private void Update()
        {
            if (currentTarget == null) return;

            if (minMoveDistance <= Vector3.Distance(currentTarget.Position, lastPos) && StaticEvents.PlayerHealth.Value > 0)
            {
                lastPos = currentTarget.Position;
                navMeshAgent.SetDestination(currentTarget.Position);
            }
            
            if (StaticEvents.IsUserInRange && agentVelocity < 1.0f)
                agentVelocity += Time.deltaTime * agentAcceleration;
            else if (!StaticEvents.IsUserInRange && agentVelocity > 0.1f)
            {
                agentVelocity -= Time.deltaTime * agentDecceleration;
                animator.speed = 1f;
            }

            //animator.SetBool(walk, navMeshAgent.velocity.magnitude > 0.1f);
            
            animator.SetFloat(velocity, agentVelocity);
        }
    }
}