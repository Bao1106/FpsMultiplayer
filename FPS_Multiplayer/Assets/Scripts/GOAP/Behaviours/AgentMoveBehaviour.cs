﻿using System;
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
        private Vector2 smoothDeltaPos, smoothVelocity;
        
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

            animator.applyRootMotion = true;
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = true;
        }

        /*private void OnAnimatorMove()
        {
            var rootPos = animator.rootPosition;
            rootPos.y = navMeshAgent.nextPosition.y;
            transform.position = rootPos;
            navMeshAgent.nextPosition = rootPos;
        }*/

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
            
            if (minMoveDistance <= Vector3.Distance(currentTarget.Position, lastPos) && StaticEvents.PlayerHealth.Value > 0)
            {
                lastPos = currentTarget.Position;
                navMeshAgent.SetDestination(currentTarget.Position);
            }
            
            switch (StaticEvents.IsUserInRange)
            {
                case true when agentVelocity < 1.0f:
                    agentVelocity += Time.deltaTime * agentAcceleration;
                    break;
                case false when agentVelocity > 0.1f:
                    agentVelocity -= Time.deltaTime * agentDecceleration;
                    break;
            }

            //animator.SetBool(walk, navMeshAgent.velocity.magnitude > 0.1f);
            
            var worldDeltaPos = navMeshAgent.nextPosition - transform.position;
            worldDeltaPos.y = 0;

            var dx = Vector3.Dot(transform.right, worldDeltaPos);
            var dy = Vector3.Dot(transform.forward, worldDeltaPos);
            var deltaPos = new Vector2(dx, dy);

            var smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
            smoothDeltaPos = Vector2.Lerp(smoothDeltaPos, deltaPos, smooth);
            smoothVelocity = smoothDeltaPos / Time.deltaTime;

            var deltaMagnitude = worldDeltaPos.magnitude;
            
            animator.SetFloat(velocity, navMeshAgent.velocity.magnitude > 0.1f 
                ? agentVelocity : 0.05f);
            
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