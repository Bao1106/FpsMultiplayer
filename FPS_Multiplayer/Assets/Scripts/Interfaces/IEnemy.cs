using GOAP.Sensors;
using Services;

namespace Interfaces
{
    public interface IEnemy
    {
        Observer<int> EnemyHealth { get; set; }
        void OnDamaged(int damage);
    }
}