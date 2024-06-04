using System;
using CrashKonijn.Goap.Behaviours;
using Events;
using GOAP.Config;
using GOAP.Goals;
using GOAP.Sensors;
using UnityEngine;

namespace GOAP.Behaviours
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class AgentBrain : MonoBehaviour
    {
        [SerializeField] private PlayerSensor playerSensor;
        [SerializeField] private AttackConfig attackConfig;
        
        private AgentBehaviour agentBehaviour;

        private void Awake()
        {
            agentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void Start()
        {
            agentBehaviour.SetGoal<WanderGoal>(false);
            playerSensor.collider.radius = attackConfig.sensorRadius;
        }
        
        private void OnEnable()
        {
            playerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
            playerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
        }

        private void OnDisable()
        {
            playerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
            playerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
        }

        private void PlayerSensorOnPlayerExit(Vector3 lastPos)
        {
            agentBehaviour.SetGoal<WanderGoal>(true);
        }

        private void PlayerSensorOnPlayerEnter(Transform player)
        {
            agentBehaviour.SetGoal<KillPlayer>(true);
        }
    }
}