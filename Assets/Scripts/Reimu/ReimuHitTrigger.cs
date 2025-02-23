using Sirenix.OdinInspector;
using UnityEngine;

namespace Reimu
{
    public class ReimuHitTrigger : MonoBehaviour
    {
        [Required]
        [SerializeField] private ReimuBattle _reimuBattle;

        public bool OnHit(int damage = 1)
        {
            if (_reimuBattle.isHit) return false;
            _reimuBattle.OnHit(damage);
            return true;
        }
        
    }
}