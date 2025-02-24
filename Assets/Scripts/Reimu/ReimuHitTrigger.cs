using Sirenix.OdinInspector;
using UnityEngine;

namespace Reimu
{
    public class ReimuHitTrigger : MonoBehaviour
    {
        [Required]
        [SerializeField] private ReimuBattle _reimuBattle;
        [SerializeField] private ReimuBoss _reimuBoss;

        public bool OnHit(int damage = 1)
        {
            if (_reimuBattle.enabled)
            {
                if (_reimuBattle.isHit) return false;
                _reimuBattle.OnHit(damage);
                return true;
            }
            else
            {
                if (_reimuBoss.isHit) return false;
                StartCoroutine(_reimuBoss.OnHitCoroutine());
                return true;
            }
        }
        
    }
}