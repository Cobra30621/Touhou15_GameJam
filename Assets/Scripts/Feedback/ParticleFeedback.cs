using Sirenix.OdinInspector;
using UnityEngine;

namespace Feedback
{
    public class ParticleFeedback : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject particlePrefab;
        
        public void Play(Transform spawnPos)
        {
            if (!particlePrefab) return;
            
            var particle = Instantiate(particlePrefab, spawnPos.position, spawnPos.rotation);
       
        }
    }
}