using GOAP.Sensors;

namespace Interfaces
{
    public interface IEnemy
    {
        int EnemyHealth { get; set; }
        void OnDamaged(int damage);
    }
}