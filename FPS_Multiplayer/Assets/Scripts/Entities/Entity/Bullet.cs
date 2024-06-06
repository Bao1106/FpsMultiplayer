using SO;
using UnityEngine;

namespace Entities.Entity
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType bulletType;

        public BulletType GetBullet() => bulletType;
    }
}
