using UnityEngine;

namespace GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/Attack Config", fileName = "Attack Config", order = 1)]
    public class AttackConfig : ScriptableObject
    {
        public float sensorRadius = 5;
        public float normalAttackRadius = 1f;
        public float attackDelay = 1f;
        public int normalAttackCost = 1;
        public LayerMask attackLayer;
    }
}