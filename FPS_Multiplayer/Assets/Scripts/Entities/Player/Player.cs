using Entities.Base;

namespace Entities.Player
{
    public class Player : Entity
    {
        public void OnDamage(int damage)
        {
            Health -= damage;
            // also handle death and stuff :)
        }
    }
}
