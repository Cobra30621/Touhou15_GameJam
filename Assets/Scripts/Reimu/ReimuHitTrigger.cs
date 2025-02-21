using Sirenix.OdinInspector;
using UnityEngine;

namespace Reimu
{
    public class ReimuHitTrigger : MonoBehaviour
    {
        [Required]
        [SerializeField] private ReimuBattle _reimuBattle;

        public void OnHit()
        {
            _reimuBattle.OnHit();
        }
        
    }
}