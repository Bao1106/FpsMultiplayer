using UnityEngine;

namespace GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/Wander Config", fileName = "Wander Config", order = 2)]
    public class WanderConfig : ScriptableObject
    {
        public Vector2 waitRangeBetweenWanders = new Vector2(1, 5);
        public float wanderRadius = 5f;
    }
}