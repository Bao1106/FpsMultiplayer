using System;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class AgentMoveBehaviour : MonoBehaviour
    {
        [SerializeField] private float minMoveDistance = 0.25f;
        
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private AgentBehaviour agentBehaviour;
        private Vector3 lastPos;

        private ITarget currentTarget;
        private static readonly int Walk = Animator.StringToHash("Walk");

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
            animator.SetBool(Walk, false);
        }

        private void EventsOnTargetChanged(ITarget target, bool inRange)
        {
            currentTarget = target;
            lastPos = currentTarget.Position;
            navMeshAgent.SetDestination(target.Position);
            animator.SetBool(Walk, true);
        }

        private void Update()
        {
            if (currentTarget == null) return;

            if (minMoveDistance <= Vector3.Distance(currentTarget.Position, lastPos))
            {
                lastPos = currentTarget.Position;
                navMeshAgent.SetDestination(currentTarget.Position);    
            }
            
            animator.SetBool(Walk, navMeshAgent.velocity.magnitude > 0.1f);
        }
    }
}